using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class WfProcessstepMap : EntityTypeConfiguration<WfProcessstep>
    {
        public WfProcessstepMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.processname)
                .HasMaxLength(50);

            this.Property(t => t.stepname)
                .HasMaxLength(50);

            this.Property(t => t.displayname)
                .HasMaxLength(50);

            this.Property(t => t.pcform)
                .HasMaxLength(600);

            this.Property(t => t.mobileform)
                .HasMaxLength(600);

            this.Property(t => t.emailform)
                .HasMaxLength(600);

            this.Property(t => t.approvervariable)
                .HasMaxLength(50);

            this.Property(t => t.functionalid)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.doaid)
                .IsFixedLength()
                .HasMaxLength(36);

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

            this.Property(t => t.ext06)
                .HasMaxLength(200);

            this.Property(t => t.ext07)
                .HasMaxLength(200);

            this.Property(t => t.ext08)
                .HasMaxLength(200);

            this.Property(t => t.ext09)
                .HasMaxLength(200);

            this.Property(t => t.ext10)
                .HasMaxLength(200);

            this.Property(t => t.ext11)
                .HasMaxLength(200);

            this.Property(t => t.ext12)
                .HasMaxLength(200);

            this.Property(t => t.ext13)
                .HasMaxLength(200);

            this.Property(t => t.ext14)
                .HasMaxLength(200);

            this.Property(t => t.ext15)
                .HasMaxLength(200);

            this.Property(t => t.ext16)
                .HasMaxLength(200);

            this.Property(t => t.ext17)
                .HasMaxLength(200);

            this.Property(t => t.ext18)
                .HasMaxLength(200);

            this.Property(t => t.ext19)
                .HasMaxLength(200);

            this.Property(t => t.ext20)
                .HasMaxLength(200);

            this.Property(t => t.ext21)
                .HasMaxLength(200);

            this.Property(t => t.ext22)
                .HasMaxLength(200);

            this.Property(t => t.ext23)
                .HasMaxLength(200);

            this.Property(t => t.ext24)
                .HasMaxLength(200);

            this.Property(t => t.ext25)
                .HasMaxLength(200);

            this.Property(t => t.ext26)
                .HasMaxLength(200);

            this.Property(t => t.ext27)
                .HasMaxLength(200);

            this.Property(t => t.ext28)
                .HasMaxLength(200);

            this.Property(t => t.ext29)
                .HasMaxLength(200);

            this.Property(t => t.ext30)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("WF_PROCESSSTEP");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.processname).HasColumnName("PROCESSNAME");
            this.Property(t => t.processversion).HasColumnName("PROCESSVERSION");
            this.Property(t => t.stepname).HasColumnName("STEPNAME");
            this.Property(t => t.displayname).HasColumnName("DISPLAYNAME");
            this.Property(t => t.pcform).HasColumnName("PCFORM");
            this.Property(t => t.mobileform).HasColumnName("MOBILEFORM");
            this.Property(t => t.emailform).HasColumnName("EMAILFORM");
            this.Property(t => t.approvervariable).HasColumnName("APPROVERVARIABLE");
            this.Property(t => t.isfinance).HasColumnName("ISFINANCE");
            this.Property(t => t.functionalid).HasColumnName("FUNCTIONALID");
            this.Property(t => t.doaid).HasColumnName("DOAID");
            this.Property(t => t.orderno).HasColumnName("ORDERNO");
            this.Property(t => t.ext01).HasColumnName("EXT01");
            this.Property(t => t.ext02).HasColumnName("EXT02");
            this.Property(t => t.ext03).HasColumnName("EXT03");
            this.Property(t => t.ext04).HasColumnName("EXT04");
            this.Property(t => t.ext05).HasColumnName("EXT05");
            this.Property(t => t.ext06).HasColumnName("EXT06");
            this.Property(t => t.ext07).HasColumnName("EXT07");
            this.Property(t => t.ext08).HasColumnName("EXT08");
            this.Property(t => t.ext09).HasColumnName("EXT09");
            this.Property(t => t.ext10).HasColumnName("EXT10");
            this.Property(t => t.ext11).HasColumnName("EXT11");
            this.Property(t => t.ext12).HasColumnName("EXT12");
            this.Property(t => t.ext13).HasColumnName("EXT13");
            this.Property(t => t.ext14).HasColumnName("EXT14");
            this.Property(t => t.ext15).HasColumnName("EXT15");
            this.Property(t => t.ext16).HasColumnName("EXT16");
            this.Property(t => t.ext17).HasColumnName("EXT17");
            this.Property(t => t.ext18).HasColumnName("EXT18");
            this.Property(t => t.ext19).HasColumnName("EXT19");
            this.Property(t => t.ext20).HasColumnName("EXT20");
            this.Property(t => t.ext21).HasColumnName("EXT21");
            this.Property(t => t.ext22).HasColumnName("EXT22");
            this.Property(t => t.ext23).HasColumnName("EXT23");
            this.Property(t => t.ext24).HasColumnName("EXT24");
            this.Property(t => t.ext25).HasColumnName("EXT25");
            this.Property(t => t.ext26).HasColumnName("EXT26");
            this.Property(t => t.ext27).HasColumnName("EXT27");
            this.Property(t => t.ext28).HasColumnName("EXT28");
            this.Property(t => t.ext29).HasColumnName("EXT29");
            this.Property(t => t.ext30).HasColumnName("EXT30");
        }
    }
}
