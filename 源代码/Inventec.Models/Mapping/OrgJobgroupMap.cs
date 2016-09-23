using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class OrgJobgroupMap : EntityTypeConfiguration<OrgJobgroup>
    {
        public OrgJobgroupMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.jobgroup)
                .HasMaxLength(50);

            this.Property(t => t.organization)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ORG_JOBGROUP");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.jobgroupid).HasColumnName("JOBGROUPID");
            this.Property(t => t.jobgroup).HasColumnName("JOBGROUP");
            this.Property(t => t.organization).HasColumnName("ORGANIZATION");
        }
    }
}
