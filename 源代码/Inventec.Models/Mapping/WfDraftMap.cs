using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class WfDraftMap : EntityTypeConfiguration<WfDraft>
    {
        public WfDraftMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.processname)
                .HasMaxLength(50);

            this.Property(t => t.tablename)
                .HasMaxLength(300);

            this.Property(t => t.summary)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("WF_DRAFT");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.processname).HasColumnName("PROCESSNAME");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.tablename).HasColumnName("TABLENAME");
            this.Property(t => t.summary).HasColumnName("SUMMARY");
            this.Property(t => t.createdate).HasColumnName("CREATEDATE");
            this.Property(t => t.createby).HasColumnName("CREATEBY");
            this.Property(t => t.information).HasColumnName("INFORMATION");
        }
    }
}
