using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class OrgGroupmemberMap : EntityTypeConfiguration<OrgGroupmember>
    {
        public OrgGroupmemberMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.membername)
                .HasMaxLength(200);

            this.Property(t => t.membertype)
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
            this.ToTable("ORG_GROUPMEMBER");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.groupid).HasColumnName("GROUPID");
            this.Property(t => t.memberid).HasColumnName("MEMBERID");
            this.Property(t => t.membername).HasColumnName("MEMBERNAME");
            this.Property(t => t.membertype).HasColumnName("MEMBERTYPE");
            this.Property(t => t.weight).HasColumnName("WEIGHT");
            this.Property(t => t.seq).HasColumnName("SEQ");
            this.Property(t => t.ext01).HasColumnName("EXT01");
            this.Property(t => t.ext02).HasColumnName("EXT02");
            this.Property(t => t.ext03).HasColumnName("EXT03");
            this.Property(t => t.ext04).HasColumnName("EXT04");
            this.Property(t => t.ext05).HasColumnName("EXT05");
        }
    }
}
