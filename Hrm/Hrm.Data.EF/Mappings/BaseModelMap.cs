using System.Data.Entity.ModelConfiguration;
using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Mappings
{
    public abstract class BaseModelMap<T> : EntityTypeConfiguration<T> where T: BaseModel<long>
    {
         protected BaseModelMap()
         {
             this.HasKey(t => t.Id);
         }
    }
}