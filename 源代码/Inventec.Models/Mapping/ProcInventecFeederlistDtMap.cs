using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ProcInventecFeederlistDtMap : EntityTypeConfiguration<ProcInventecFeederlistDt>
    {
        public ProcInventecFeederlistDtMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.zno)
                .HasMaxLength(50);

            this.Property(t => t.partno)
                .HasMaxLength(50);

            this.Property(t => t.description)
                .HasMaxLength(50);

            this.Property(t => t.qty)
                .HasMaxLength(50);

            this.Property(t => t.feeder)
                .HasMaxLength(50);

            this.Property(t => t.remark)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PROC_Inventec_FeederList_DT");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.rowid).HasColumnName("RowID");
            this.Property(t => t.zno).HasColumnName("ZNO");
            this.Property(t => t.partno).HasColumnName("PARTNO");
            this.Property(t => t.description).HasColumnName("DESCRIPTION");
            this.Property(t => t.qty).HasColumnName("QTY");
            this.Property(t => t.feeder).HasColumnName("FEEDER");
            this.Property(t => t.remark).HasColumnName("Remark");
        }
    }
}
