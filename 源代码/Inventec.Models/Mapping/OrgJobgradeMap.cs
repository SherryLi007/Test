using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class OrgJobgradeMap : EntityTypeConfiguration<OrgJobgrade>
    {
        public OrgJobgradeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.jobgrade)
                .HasMaxLength(50);

            this.Property(t => t.organization)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ORG_JOBGRADE");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.jobgrade).HasColumnName("JOBGRADE");
            this.Property(t => t.orderno).HasColumnName("ORDERNO");
            this.Property(t => t.organization).HasColumnName("ORGANIZATION");
        }
    }
}
