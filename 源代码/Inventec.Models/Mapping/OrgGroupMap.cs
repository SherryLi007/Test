using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class OrgGroupMap : EntityTypeConfiguration<OrgGroup>
    {
        public OrgGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.groupname)
                .HasMaxLength(50);

            this.Property(t => t.isweighted)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.datarightsid)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.organization)
                .HasMaxLength(50);

            this.Property(t => t.ext01)
                .HasMaxLength(200);

            this.Property(t => t.ext02)
                .HasMaxLength(200);

            this.Property(t => t.ext03)
                .HasMaxLength(200);

            this.Property(t => t.ext04)
                .HasMaxLength(200);

            this.Property(t => t.ext05)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ORG_GROUP");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.groupname).HasColumnName("GROUPNAME");
            this.Property(t => t.isweighted).HasColumnName("ISWEIGHTED");
            this.Property(t => t.datarightsid).HasColumnName("DATARIGHTSID");
            this.Property(t => t.organization).HasColumnName("ORGANIZATION");
            this.Property(t => t.ext01).HasColumnName("EXT01");
            this.Property(t => t.ext02).HasColumnName("EXT02");
            this.Property(t => t.ext03).HasColumnName("EXT03");
            this.Property(t => t.ext04).HasColumnName("EXT04");
            this.Property(t => t.ext05).HasColumnName("EXT05");
        }
    }
}
