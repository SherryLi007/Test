using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class SecMenurightsobjectMap : EntityTypeConfiguration<SecMenurightsobject>
    {
        public SecMenurightsobjectMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SEC_MENURIGHTSOBJECT");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.rightsid).HasColumnName("RIGHTSID");
            this.Property(t => t.menuid).HasColumnName("MENUID");
            this.Property(t => t.canedit).HasColumnName("CANEDIT");
            this.Property(t => t.candelete).HasColumnName("CANDELETE");
        }
    }
}
