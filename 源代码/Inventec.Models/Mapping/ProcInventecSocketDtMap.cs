using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ProcInventecSocketDtMap : EntityTypeConfiguration<ProcInventecSocketDt>
    {
        public ProcInventecSocketDtMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.socketno)
                .HasMaxLength(50);

            this.Property(t => t.exceptionhandling)
                .HasMaxLength(50);

            this.Property(t => t.exceptionhandlingpersonnel)
                .HasMaxLength(50);

            this.Property(t => t.remark)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PROC_Inventec_Socket_DT");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.rowid).HasColumnName("RowID");
            this.Property(t => t.socketno).HasColumnName("SocketNO");
            this.Property(t => t.unusualid).HasColumnName("UnusualID");
            this.Property(t => t.exceptionhandling).HasColumnName("ExceptionHandling");
            this.Property(t => t.exceptionhandlingpersonnel).HasColumnName("ExceptionHandlingPersonnel");
            this.Property(t => t.handdate).HasColumnName("HandDate");
            this.Property(t => t.resultcodeid).HasColumnName("ResultCodeID");
            this.Property(t => t.remark).HasColumnName("Remark");
        }
    }
}
