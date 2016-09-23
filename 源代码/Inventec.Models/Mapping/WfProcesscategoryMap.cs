using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class WfProcesscategoryMap : EntityTypeConfiguration<WfProcesscategory>
    {
        public WfProcesscategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.categoryid)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.categoryname)
                .HasMaxLength(50);

            this.Property(t => t.displayname)
                .HasMaxLength(50);

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
            this.ToTable("WF_PROCESSCATEGORY");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.categoryid).HasColumnName("CATEGORYID");
            this.Property(t => t.categoryname).HasColumnName("CATEGORYNAME");
            this.Property(t => t.displayname).HasColumnName("DISPLAYNAME");
            this.Property(t => t.orderno).HasColumnName("ORDERNO");
            this.Property(t => t.icon).HasColumnName("ICON");
            this.Property(t => t.ext01).HasColumnName("EXT01");
            this.Property(t => t.ext02).HasColumnName("EXT02");
            this.Property(t => t.ext03).HasColumnName("EXT03");
            this.Property(t => t.ext04).HasColumnName("EXT04");
            this.Property(t => t.ext05).HasColumnName("EXT05");
        }
    }
}
