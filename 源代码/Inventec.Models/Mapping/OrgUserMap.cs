using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class OrgUserMap : EntityTypeConfiguration<OrgUser>
    {
        public OrgUserMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.loginname)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.username)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.usernamecn)
                .HasMaxLength(50);

            this.Property(t => t.usercode)
                .HasMaxLength(50);

            this.Property(t => t.jobtitle)
                .HasMaxLength(50);

            this.Property(t => t.deparment)
                .HasMaxLength(50);

            this.Property(t => t.jobfunction)
                .HasMaxLength(100);

            this.Property(t => t.organization)
                .HasMaxLength(50);

            this.Property(t => t.email)
                .HasMaxLength(50);

            this.Property(t => t.mobileno)
                .HasMaxLength(50);

            this.Property(t => t.tel)
                .HasMaxLength(50);

            this.Property(t => t.im)
                .HasMaxLength(50);

            this.Property(t => t.password)
                .HasMaxLength(100);

            this.Property(t => t.language)
                .HasMaxLength(50);

            this.Property(t => t.picture)
                .HasMaxLength(200);

            this.Property(t => t.theme)
                .HasMaxLength(50);

            this.Property(t => t.createby)
                .HasMaxLength(50);

            this.Property(t => t.updateby)
                .HasMaxLength(50);

            this.Property(t => t.isactive)
                .IsFixedLength()
                .HasMaxLength(1);

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

            this.Property(t => t.sapaccount)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ORG_USER");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.loginname).HasColumnName("LOGINNAME");
            this.Property(t => t.username).HasColumnName("USERNAME");
            this.Property(t => t.usernamecn).HasColumnName("USERNAMECN");
            this.Property(t => t.usercode).HasColumnName("USERCODE");
            this.Property(t => t.jobtitle).HasColumnName("JobTitle");
            this.Property(t => t.deparmentid).HasColumnName("DeparmentID");
            this.Property(t => t.deparment).HasColumnName("Deparment");
            this.Property(t => t.jobfunction).HasColumnName("JobFunction");
            this.Property(t => t.organization).HasColumnName("Organization");
            this.Property(t => t.email).HasColumnName("EMAIL");
            this.Property(t => t.mobileno).HasColumnName("MOBILENO");
            this.Property(t => t.tel).HasColumnName("TEL");
            this.Property(t => t.im).HasColumnName("IM");
            this.Property(t => t.password).HasColumnName("PASSWORD");
            this.Property(t => t.language).HasColumnName("LANGUAGE");
            this.Property(t => t.picture).HasColumnName("PICTURE");
            this.Property(t => t.orderno).HasColumnName("ORDERNO");
            this.Property(t => t.theme).HasColumnName("THEME");
            this.Property(t => t.createdate).HasColumnName("CREATEDATE");
            this.Property(t => t.createby).HasColumnName("CREATEBY");
            this.Property(t => t.updatedate).HasColumnName("UPDATEDATE");
            this.Property(t => t.updateby).HasColumnName("UPDATEBY");
            this.Property(t => t.isactive).HasColumnName("ISACTIVE");
            this.Property(t => t.isreceive).HasColumnName("ISRECEIVE");
            this.Property(t => t.ext01).HasColumnName("EXT01");
            this.Property(t => t.ext02).HasColumnName("EXT02");
            this.Property(t => t.ext03).HasColumnName("EXT03");
            this.Property(t => t.ext04).HasColumnName("EXT04");
            this.Property(t => t.ext05).HasColumnName("EXT05");
            this.Property(t => t.costcenter).HasColumnName("CostCenter");
            this.Property(t => t.sapaccount).HasColumnName("SAPAccount");
            this.Property(t => t.istemp).HasColumnName("IsTemp");
        }
    }
}
