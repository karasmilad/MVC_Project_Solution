using MVC_Project_DAL_.Models.DepartmentModel;

namespace MVC_Project_DAL_.Data.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Name).HasColumnType("varchar(50)");
            builder.Property(d => d.Code).HasColumnType("varchar(20)");
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            builder.HasMany(E=> E.Employees)
                   .WithOne(D=> D.Department)
                   .HasForeignKey(D=> D.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
