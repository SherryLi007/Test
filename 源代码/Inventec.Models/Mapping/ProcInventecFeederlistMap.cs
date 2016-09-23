using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ProcInventecFeederlistMap : EntityTypeConfiguration<ProcInventecFeederlist>
    {
        public ProcInventecFeederlistMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.status)
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

            this.Property(t => t.subject)
                .HasMaxLength(50);

            this.Property(t => t.rev)
                .HasMaxLength(50);

            this.Property(t => t.model)
                .HasMaxLength(50);

            this.Property(t => t.mctype)
                .HasMaxLength(50);

            this.Property(t => t.pn)
                .HasMaxLength(50);

            this.Property(t => t.pcbno)
                .HasMaxLength(50);

            this.Property(t => t.mode)
                .HasMaxLength(50);

            this.Property(t => t.pcbrev)
                .HasMaxLength(50);

            this.Property(t => t.array)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PROC_Inventec_FeederList");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.incident).HasColumnName("INCIDENT");
            this.Property(t => t.status).HasColumnName("Status");
            this.Property(t => t.formno).HasColumnName("FORMNO");
            this.Property(t => t.department).HasColumnName("Department");
            this.Property(t => t.departmentid).HasColumnName("DepartmentID");
            this.Property(t => t.factoryname).HasColumnName("FactoryName");
            this.Property(t => t.applicationdate).HasColumnName("ApplicationDate");
            this.Property(t => t.applicantpeople).HasColumnName("ApplicantPeople");
            this.Property(t => t.employeenumber).HasColumnName("EmployeeNumber");
            this.Property(t => t.subject).HasColumnName("Subject");
            this.Property(t => t.rev).HasColumnName("Rev");
            this.Property(t => t.model).HasColumnName("Model");
            this.Property(t => t.mctype).HasColumnName("MCTYPE");
            this.Property(t => t.pn).HasColumnName("PN");
            this.Property(t => t.pcbno).HasColumnName("PCBNO");
            this.Property(t => t.mode).HasColumnName("Mode");
            this.Property(t => t.pcbrev).HasColumnName("PCBREV");
            this.Property(t => t.array).HasColumnName("Array");
        }
    }
}
