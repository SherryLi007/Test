using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class ComSerialnoMap : EntityTypeConfiguration<ComSerialno>
    {
        public ComSerialnoMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.module)
                .HasMaxLength(50);

            this.Property(t => t.serialtype)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("COM_SERIALNO");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.module).HasColumnName("MODULE");
            this.Property(t => t.serialtype).HasColumnName("SERIALTYPE");
            this.Property(t => t.serialyear).HasColumnName("SERIALYEAR");
            this.Property(t => t.serialmonth).HasColumnName("SERIALMONTH");
            this.Property(t => t.serialday).HasColumnName("SERIALDAY");
            this.Property(t => t.serialno).HasColumnName("SERIALNO");
            this.Property(t => t.updatedate).HasColumnName("UPDATEDATE");
        }
    }
}
