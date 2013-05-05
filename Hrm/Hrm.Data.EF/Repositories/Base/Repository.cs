using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Hrm.Data.EF.Enums;
using Hrm.Data.EF.Models.Base;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Contracts;

namespace Hrm.Data.EF.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseModel<long> 
    {
        private readonly HrmContext ctx;

        protected IQueryable<TEntity> CurrentQuery
        {
            get { return this.ctx.Set<TEntity>().AsQueryable(); }
        }

        private DbSet<TEntity> CurrentDbSet
        {
            get { return this.ctx.Set<TEntity>(); }
        }

        public Repository()
        {
            this.ctx = new HrmContext();
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return this.CurrentQuery.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.CurrentQuery.GetEnumerator();
        }

        #endregion

        #region Implementation of IQueryable

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.Expressions.Expression"/> that is associated with this instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </returns>
        public Expression Expression
        {
            get { return this.CurrentQuery.Expression; }
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"/> is executed.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Type"/> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.
        /// </returns>
        public Type ElementType
        {
            get { return this.CurrentQuery.ElementType; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryProvider"/> that is associated with this data source.
        /// </returns>
        public IQueryProvider Provider
        {
            get { return this.CurrentQuery.Provider; }
        }

        #endregion

        #region Implementation of IRepository<TEntity>

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        public void SaveOrUpdate(TEntity entity)
        {
            if (entity.Id == 0)
            {
                this.CurrentDbSet.Add(entity);
            }
            else
            {
                this.ctx.Entry(entity).State = EntityState.Modified;
            }
            
            this.ctx.SaveChanges();
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        public void Delete(TEntity entity)
        {
            this.CurrentDbSet.Remove(entity);
            this.ctx.SaveChanges();
        }

        public void SaveChanges()
        {
            this.ctx.SaveChanges();
        }

        public IQueryable<TEntity> Find(ISpecification<TEntity> specification)
        {
            return this.CurrentQuery.Where(specification.IsSatisfiedBy());
        }

        public TEntity FindOne(ISpecification<TEntity> specification)
        {
            return this.CurrentQuery.FirstOrDefault(specification.IsSatisfiedBy());
        }
        
        public IOrderedQueryable<TEntity> SortByAsc(string propertyName, IQueryable<TEntity> data = null)
        {
            return this.Sort(propertyName, SortOrder.Asc, data);
        }

        public IOrderedQueryable<TEntity> SortByDesc(string propertyName, IQueryable<TEntity> data = null)
        {
            return this.Sort(propertyName, SortOrder.Desc, data);
        }

        #endregion

        private IOrderedQueryable<TEntity> Sort(string propertyName, SortOrder sortOrder, IQueryable<TEntity> data = null)
        {
            var propertyInfo = typeof(TEntity).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new Exception("No property '" + propertyName + "' in + " + typeof(TEntity).Name + "'");
            }

            string methodName = string.Empty;
            switch (sortOrder)
            {
                case SortOrder.Asc:
                    methodName = "OrderBy";
                    break;
                case SortOrder.Desc:
                    methodName = "OrderByDescending";
                    break;
            }

            var method = typeof(Queryable).GetMethods().Single(m => m.Name == methodName && m.GetParameters().Length == 2);
            var concreteMethod = method.MakeGenericMethod(typeof(TEntity), propertyInfo.PropertyType);
            var param = Expression.Parameter(typeof(TEntity), "x");
            var expression = Expression.Lambda(Expression.Property(param, propertyInfo), param);

            if (data == null)
            {
                data = this;
            }

            return (IOrderedQueryable<TEntity>)concreteMethod.Invoke(null, new object[] { data, expression });
        }
    }
}
