using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class SecMenuMap : EntityTypeConfiguration<SecMenu>
    {
        public SecMenuMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.module)
                .HasMaxLength(50);

            this.Property(t => t.menuname)
                .HasMaxLength(50);

            this.Property(t => t.displayname)
                .HasMaxLength(50);

            this.Property(t => t.menutype)
                .HasMaxLength(255);

            this.Property(t => t.formid)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.url)
                .HasMaxLength(400);

            this.Property(t => t.icon)
                .HasMaxLength(50);

            this.Property(t => t.target)
                .HasMaxLength(50);

            this.Property(t => t.accesslevel)
                .HasMaxLength(50);

            this.Property(t => t.isactive)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ishomepage)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.isvisible)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.relatedfolder)
                .HasMaxLength(400);

            this.Property(t => t.relatedform)
                .HasMaxLength(800);

            this.Property(t => t.remark)
                .HasMaxLength(200);

            this.Property(t => t.createby)
                .HasMaxLength(50);

            this.Property(t => t.updateby)
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
            this.ToTable("SEC_MENU");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.module).HasColumnName("MODULE");
            this.Property(t => t.menuname).HasColumnName("MENUNAME");
            this.Property(t => t.displayname).HasColumnName("DISPLAYNAME");
            this.Property(t => t.menutype).HasColumnName("MENUTYPE");
            this.Property(t => t.parentid).HasColumnName("PARENTID");
            this.Property(t => t.formid).HasColumnName("FORMID");
            this.Property(t => t.url).HasColumnName("URL");
            this.Property(t => t.icon).HasColumnName("ICON");
            this.Property(t => t.target).HasColumnName("TARGET");
            this.Property(t => t.accesslevel).HasColumnName("ACCESSLEVEL");
            this.Property(t => t.isactive).HasColumnName("ISACTIVE");
            this.Property(t => t.ishomepage).HasColumnName("ISHOMEPAGE");
            this.Property(t => t.isvisible).HasColumnName("ISVISIBLE");
            this.Property(t => t.relatedfolder).HasColumnName("RELATEDFOLDER");
            this.Property(t => t.relatedform).HasColumnName("RELATEDFORM");
            this.Property(t => t.remark).HasColumnName("REMARK");
            this.Property(t => t.height).HasColumnName("HEIGHT");
            this.Property(t => t.orderno).HasColumnName("ORDERNO");
            this.Property(t => t.createdate).HasColumnName("CREATEDATE");
            this.Property(t => t.createby).HasColumnName("CREATEBY");
            this.Property(t => t.updatedate).HasColumnName("UPDATEDATE");
            this.Property(t => t.updateby).HasColumnName("UPDATEBY");
            this.Property(t => t.ext01).HasColumnName("EXT01");
            this.Property(t => t.ext02).HasColumnName("EXT02");
            this.Property(t => t.ext03).HasColumnName("EXT03");
            this.Property(t => t.ext04).HasColumnName("EXT04");
            this.Property(t => t.ext05).HasColumnName("EXT05");
        }
    }
}
