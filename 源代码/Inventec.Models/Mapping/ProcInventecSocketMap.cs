using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ProcInventecSocketMap : EntityTypeConfiguration<ProcInventecSocket>
    {
        public ProcInventecSocketMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.statuss)
                .HasMaxLength(50);

            this.Property(t => t.formno)
                .HasMaxLength(50);

            this.Property(t => t.department)
                .HasMaxLength(50);

            this.Property(t => t.factoryname)
                .HasMaxLength(50);

            this.Property(t => t.applicantpeople)
                .HasMaxLength(50);

            this.Property(t => t.employeenumber)
                .HasMaxLength(50);

            this.Property(t => t.tally)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PROC_Inventec_Socket");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.incident).HasColumnName("INCIDENT");
            this.Property(t => t.statuss).HasColumnName("statuss");
            this.Property(t => t.formno).HasColumnName("FORMNO");
            this.Property(t => t.department).HasColumnName("Department");
            this.Property(t => t.departmentid).HasColumnName("DepartmentID");
            this.Property(t => t.factoryname).HasColumnName("FactoryName");
            this.Property(t => t.applicationdate).HasColumnName("ApplicationDate");
            this.Property(t => t.applicantpeople).HasColumnName("ApplicantPeople");
            this.Property(t => t.employeenumber).HasColumnName("EmployeeNumber");
            this.Property(t => t.pcasmtid).HasColumnName("PCASMTID");
            this.Property(t => t.linetypeid).HasColumnName("LineTypeID");
            this.Property(t => t.tally).HasColumnName("Tally");
        }
    }
}
