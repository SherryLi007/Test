using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class BdMasterdataMap : EntityTypeConfiguration<BdMasterdata>
    {
        public BdMasterdataMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.typename)
                .HasMaxLength(200);

            this.Property(t => t.datadec)
                .HasMaxLength(500);

            this.Property(t => t.createby)
                .HasMaxLength(50);

            this.Property(t => t.updateby)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BD_MasterData");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.typename).HasColumnName("TypeName");
            this.Property(t => t.datadec).HasColumnName("DataDec");
            this.Property(t => t.datavalue).HasColumnName("DataValue");
            this.Property(t => t.createdate).HasColumnName("CREATEDATE");
            this.Property(t => t.createby).HasColumnName("CREATEBY");
            this.Property(t => t.updatedate).HasColumnName("UPDATEDATE");
            this.Property(t => t.updateby).HasColumnName("UPDATEBY");
            this.Property(t => t.isdelete).HasColumnName("ISDELETE");
        }
    }
}
