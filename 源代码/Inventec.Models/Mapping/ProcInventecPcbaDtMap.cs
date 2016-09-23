using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ProcInventecPcbaDtMap : EntityTypeConfiguration<ProcInventecPcbaDt>
    {
        public ProcInventecPcbaDtMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.inspectiontype)
                .HasMaxLength(50);

            this.Property(t => t.productname)
                .HasMaxLength(50);

            this.Property(t => t.serialnumber)
                .HasMaxLength(200);

            this.Property(t => t.proofer)
                .HasMaxLength(50);

            this.Property(t => t.badlocation)
                .HasMaxLength(50);

            this.Property(t => t.abnormaldescription)
                .HasMaxLength(500);

            this.Property(t => t.handler)
                .HasMaxLength(50);

            this.Property(t => t.ngreason)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("PROC_Inventec_PCBA_DT");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.rowid).HasColumnName("RowID");
            this.Property(t => t.inspectiontype).HasColumnName("InspectionType");
            this.Property(t => t.inspectiontypeid).HasColumnName("InspectionTypeID");
            this.Property(t => t.inspectiondate).HasColumnName("InspectionDate");
            this.Property(t => t.productname).HasColumnName("ProductName");
            this.Property(t => t.serialnumber).HasColumnName("SerialNumber");
            this.Property(t => t.proofer).HasColumnName("Proofer");
            this.Property(t => t.inspectionresultid).HasColumnName("InspectionResultID");
            this.Property(t => t.badlocation).HasColumnName("BadLocation");
            this.Property(t => t.abnormaldescription).HasColumnName("AbnormalDescription");
            this.Property(t => t.handler).HasColumnName("Handler");
            this.Property(t => t.resultcodeid).HasColumnName("ResultCodeID");
            this.Property(t => t.ngreason).HasColumnName("NGReason");

            // Relationships
            this.HasRequired(t => t.proc_inventec_pcba)
                .WithMany(t => t.proc_inventec_pcba_dt)
                .HasForeignKey(d => d.formid);

        }
    }
}
