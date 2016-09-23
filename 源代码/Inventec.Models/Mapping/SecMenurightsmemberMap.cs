using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class SecMenurightsmemberMap : EntityTypeConfiguration<SecMenurightsmember>
    {
        public SecMenurightsmemberMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.membertype)
                .HasMaxLength(50);

            this.Property(t => t.membername)
                .HasMaxLength(200);

            this.Property(t => t.groupname)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SEC_MENURIGHTSMEMBER");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.rightsid).HasColumnName("RIGHTSID");
            this.Property(t => t.membertype).HasColumnName("MEMBERTYPE");
            this.Property(t => t.memberid).HasColumnName("MEMBERID");
            this.Property(t => t.membername).HasColumnName("MEMBERNAME");
            this.Property(t => t.groupid).HasColumnName("GROUPID");
            this.Property(t => t.groupname).HasColumnName("GROUPNAME");
        }
    }
}
