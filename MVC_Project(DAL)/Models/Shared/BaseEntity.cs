using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_DAL_.Models.Shared
{
    public class BaseEntity
    {
        public int Id { get; set; } //PK
        public int CreatedBy { get; set; } //USerId
        public DateTime? CreatedOn { get; set; } //Datetime
        public int LastModifiedBy { get; set; } //UserId
        public DateTime? LastModifiedOn { get; set; } //Datetime
        public bool IsDeleted { get; set; } //Soft Delete

    }
}
