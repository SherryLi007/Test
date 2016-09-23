using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Inventec.Models.Mapping
{
    public class BdLogMap : EntityTypeConfiguration<BdLog>
    {
        public BdLogMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.tablename)
                .HasMaxLength(50);

            this.Property(t => t.modifyby)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BD_Log");
            this.Property(t => t.id).HasColumnName("Id");
            this.Property(t => t.tablename).HasColumnName("TableName");
            this.Property(t => t.originaldata).HasColumnName("OriginalData");
            this.Property(t => t.newdata).HasColumnName("NewData");
            this.Property(t => t.modifyby).HasColumnName("ModifyBy");
            this.Property(t => t.modifytime).HasColumnName("ModifyTime");
            this.Property(t => t.modifytype).HasColumnName("ModifyType");
            this.Property(t => t.dataid).HasColumnName("DataId");
            this.Property(t => t.formid).HasColumnName("FormId");
        }
    }
}
