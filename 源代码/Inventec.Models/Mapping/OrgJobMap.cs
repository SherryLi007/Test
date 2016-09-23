using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class OrgJobMap : EntityTypeConfiguration<OrgJob>
    {
        public OrgJobMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.jobfunction)
                .HasMaxLength(100);

            this.Property(t => t.jobgrade)
                .HasMaxLength(50);

            this.Property(t => t.ismanager)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.isprimary)
                .IsFixedLength()
                .HasMaxLength(1);

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
            this.ToTable("ORG_JOB");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.userid).HasColumnName("USERID");
            this.Property(t => t.departmentid).HasColumnName("DEPARTMENTID");
            this.Property(t => t.jobfunction).HasColumnName("JOBFUNCTION");
            this.Property(t => t.jobgrade).HasColumnName("JOBGRADE");
            this.Property(t => t.jobgroupid).HasColumnName("JOBGROUPID");
            this.Property(t => t.supervisorjobid).HasColumnName("SUPERVISORJOBID");
            this.Property(t => t.ismanager).HasColumnName("ISMANAGER");
            this.Property(t => t.isprimary).HasColumnName("ISPRIMARY");
            this.Property(t => t.organization).HasColumnName("Organization");
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
