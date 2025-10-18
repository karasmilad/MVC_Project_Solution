using MVC_Project_DAL_.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_DAL_.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)");
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.Email).HasColumnType("varchar(50)");
            builder.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            builder.Property(e => e.Gender).HasConversion(
                (EmployeeGender) => EmployeeGender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender));
            builder.Property(e => e.EmployeeTypes).HasConversion(
                (EmployeeType) => EmployeeType.ToString(),
                (Type) => (EmployeeTypes)Enum.Parse(typeof(EmployeeTypes), Type));
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
