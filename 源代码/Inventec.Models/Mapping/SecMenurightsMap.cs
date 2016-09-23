using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class SecMenurightsMap : EntityTypeConfiguration<SecMenurights>
    {
        public SecMenurightsMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.rightsname)
                .HasMaxLength(50);

            this.Property(t => t.remark)
                .HasMaxLength(200);

            this.Property(t => t.createby)
                .HasMaxLength(50);

            this.Property(t => t.updateby)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SEC_MENURIGHTS");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.rightsname).HasColumnName("RIGHTSNAME");
            this.Property(t => t.remark).HasColumnName("REMARK");
            this.Property(t => t.createdate).HasColumnName("CREATEDATE");
            this.Property(t => t.createby).HasColumnName("CREATEBY");
            this.Property(t => t.updatedate).HasColumnName("UPDATEDATE");
            this.Property(t => t.updateby).HasColumnName("UPDATEBY");
        }
    }
}
