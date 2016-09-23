using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ProcInventecMfgDtMap : EntityTypeConfiguration<ProcInventecMfgDt>
    {
        public ProcInventecMfgDtMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.confirmproject)
                .HasMaxLength(50);

            this.Property(t => t.checkunit)
                .HasMaxLength(50);

            this.Property(t => t.performsituation)
                .HasMaxLength(50);

            this.Property(t => t.confirmpeople)
                .HasMaxLength(50);

            this.Property(t => t.abnormalities)
                .HasMaxLength(50);

            this.Property(t => t.exceptionhandling)
                .HasMaxLength(50);

            this.Property(t => t.handler)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PROC_Inventec_MFG_DT");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.rowid).HasColumnName("RowID");
            this.Property(t => t.confirmproject).HasColumnName("ConfirmProject");
            this.Property(t => t.checkunit).HasColumnName("CheckUnit");
            this.Property(t => t.performsituation).HasColumnName("PerformSituation");
            this.Property(t => t.confirmpeople).HasColumnName("ConfirmPeople");
            this.Property(t => t.abnormalities).HasColumnName("Abnormalities");
            this.Property(t => t.exceptionhandling).HasColumnName("ExceptionHandling");
            this.Property(t => t.handler).HasColumnName("Handler");

            // Relationships
            this.HasRequired(t => t.proc_inventec_mfg)
                .WithMany(t => t.proc_inventec_mfg_dt)
                .HasForeignKey(d => d.formid);

        }
    }
}
