using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ProcInventecSmtDt1Map : EntityTypeConfiguration<ProcInventecSmtDt1>
    {
        public ProcInventecSmtDt1Map()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.machinetype)
                .HasMaxLength(50);

            this.Property(t => t.machinecapacity)
                .HasMaxLength(50);

            this.Property(t => t.yieldratea)
                .HasMaxLength(50);

            this.Property(t => t.yieldrateb)
                .HasMaxLength(50);

            this.Property(t => t.totaldowntime)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PROC_Inventec_SMT_DT1");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.rowid).HasColumnName("RowID");
            this.Property(t => t.machinetype).HasColumnName("MachineType");
            this.Property(t => t.machinetypeid).HasColumnName("MachineTypeID");
            this.Property(t => t.machinecapacity).HasColumnName("MachineCapacity");
            this.Property(t => t.yieldratea).HasColumnName("YieldRateA");
            this.Property(t => t.yieldrateb).HasColumnName("YieldRateB");
            this.Property(t => t.totaldowntime).HasColumnName("TotalDowntime");

            // Relationships
            this.HasRequired(t => t.proc_inventec_smt)
                .WithMany(t => t.proc_inventec_smt_dt1)
                .HasForeignKey(d => d.formid);

        }
    }
}
