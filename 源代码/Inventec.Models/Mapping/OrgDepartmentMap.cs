using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class OrgDepartmentMap : EntityTypeConfiguration<OrgDepartment>
    {
        public OrgDepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.departmentname)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.departmentcode)
                .HasMaxLength(50);

            this.Property(t => t.departmenttype)
                .HasMaxLength(50);

            this.Property(t => t.organization)
                .HasMaxLength(50);

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
            this.ToTable("ORG_DEPARTMENT");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.departmentname).HasColumnName("DEPARTMENTNAME");
            this.Property(t => t.departmentcode).HasColumnName("DEPARTMENTCODE");
            this.Property(t => t.parentid).HasColumnName("PARENTID");
            this.Property(t => t.departmenttype).HasColumnName("DEPARTMENTTYPE");
            this.Property(t => t.organization).HasColumnName("ORGANIZATION");
            this.Property(t => t.createdate).HasColumnName("CREATEDATE");
            this.Property(t => t.createby).HasColumnName("CREATEBY");
            this.Property(t => t.updatedate).HasColumnName("UPDATEDATE");
            this.Property(t => t.updateby).HasColumnName("UPDATEBY");
            this.Property(t => t.ext01).HasColumnName("EXT01");
            this.Property(t => t.ext02).HasColumnName("EXT02");
            this.Property(t => t.ext03).HasColumnName("EXT03");
            this.Property(t => t.ext04).HasColumnName("EXT04");
            this.Property(t => t.ext05).HasColumnName("EXT05");
            this.Property(t => t.isactive).HasColumnName("ISACTIVE");
        }
    }
}
