using System.Data.Entity;

namespace Hrm.Data.EF
{
    public class DbInitializer : IDatabaseInitializer<HrmContext>
    {
        public void InitializeDatabase(HrmContext context)
        {
            Database.SetInitializer<HrmContext>(new DropCreateDatabaseIfModelChanges<HrmContext>());
            context.Database.CreateIfNotExists();
        }
    }
}
