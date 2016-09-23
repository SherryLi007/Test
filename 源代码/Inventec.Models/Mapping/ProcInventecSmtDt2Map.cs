using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ProcInventecSmtDt2Map : EntityTypeConfiguration<ProcInventecSmtDt2>
    {
        public ProcInventecSmtDt2Map()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.plannedoutput)
                .HasMaxLength(50);

            this.Property(t => t.realoutput)
                .HasMaxLength(50);

            this.Property(t => t.badnumber)
                .HasMaxLength(50);

            this.Property(t => t.yieldrate)
                .HasMaxLength(50);

            this.Property(t => t.throwingrate)
                .HasMaxLength(50);

            this.Property(t => t.operationdeclaration)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("PROC_Inventec_SMT_DT2");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.rowid).HasColumnName("RowID");
            this.Property(t => t.plannedoutput).HasColumnName("PlannedOutput");
            this.Property(t => t.realoutput).HasColumnName("RealOutput");
            this.Property(t => t.badnumber).HasColumnName("BadNumber");
            this.Property(t => t.yieldrate).HasColumnName("YieldRate");
            this.Property(t => t.throwingrate).HasColumnName("ThrowingRate");
            this.Property(t => t.operationdeclaration).HasColumnName("OperationDeclaration");

            // Relationships
            this.HasRequired(t => t.proc_inventec_smt)
                .WithMany(t => t.proc_inventec_smt_dt2)
                .HasForeignKey(d => d.formid);

        }
    }
}
