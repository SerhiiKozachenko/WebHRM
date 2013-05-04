using System.Collections.Generic;
using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class TestCategory : BaseModel<long>
    {
        public virtual string Name { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}