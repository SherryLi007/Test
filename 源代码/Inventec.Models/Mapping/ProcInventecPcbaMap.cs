using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ProcInventecPcbaMap : EntityTypeConfiguration<ProcInventecPcba>
    {
        public ProcInventecPcbaMap()
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

            this.Property(t => t.testtype)
                .HasMaxLength(50);

            this.Property(t => t.testtypeid)
                .HasMaxLength(50);

            this.Property(t => t.testsite)
                .HasMaxLength(50);

            this.Property(t => t.linetype)
                .HasMaxLength(50);

            this.Property(t => t.worktype)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PROC_Inventec_PCBA");
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
            this.Property(t => t.testtype).HasColumnName("TestType");
            this.Property(t => t.testtypeid).HasColumnName("TestTypeID");
            this.Property(t => t.testsite).HasColumnName("TestSite");
            this.Property(t => t.linetype).HasColumnName("LineType");
            this.Property(t => t.worktype).HasColumnName("WorkType");
            this.Property(t => t.worktypeid).HasColumnName("WorkTypeID");
        }
    }
}
