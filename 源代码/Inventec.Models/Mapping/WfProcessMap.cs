using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class WfProcessMap : EntityTypeConfiguration<WfProcess>
    {
        public WfProcessMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.module)
                .HasMaxLength(50);

            this.Property(t => t.processname)
                .HasMaxLength(50);

            this.Property(t => t.displayname)
                .HasMaxLength(50);

            this.Property(t => t.defaultpcform)
                .HasMaxLength(100);

            this.Property(t => t.defaultmobileform)
                .HasMaxLength(100);

            this.Property(t => t.defaultemailform)
                .HasMaxLength(100);

            this.Property(t => t.categoryid)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.hasform)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.icon)
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
            this.ToTable("WF_PROCESS");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.module).HasColumnName("MODULE");
            this.Property(t => t.processname).HasColumnName("PROCESSNAME");
            this.Property(t => t.displayname).HasColumnName("DISPLAYNAME");
            this.Property(t => t.processversion).HasColumnName("PROCESSVERSION");
            this.Property(t => t.defaultpcform).HasColumnName("DEFAULTPCFORM");
            this.Property(t => t.defaultmobileform).HasColumnName("DEFAULTMOBILEFORM");
            this.Property(t => t.defaultemailform).HasColumnName("DEFAULTEMAILFORM");
            this.Property(t => t.categoryid).HasColumnName("CATEGORYID");
            this.Property(t => t.orderno).HasColumnName("ORDERNO");
            this.Property(t => t.hasform).HasColumnName("HASFORM");
            this.Property(t => t.icon).HasColumnName("ICON");
            this.Property(t => t.hasamount).HasColumnName("HasAmount");
            this.Property(t => t.ext01).HasColumnName("EXT01");
            this.Property(t => t.ext02).HasColumnName("EXT02");
            this.Property(t => t.ext03).HasColumnName("EXT03");
            this.Property(t => t.ext04).HasColumnName("EXT04");
            this.Property(t => t.ext05).HasColumnName("EXT05");
        }
    }
}
