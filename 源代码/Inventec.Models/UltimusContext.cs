using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Inventec.Models.Mapping;

namespace Inventec.Models
{
    public partial class UltimusContext : DbContext
    {
        static UltimusContext()
        {
            Database.SetInitializer<UltimusContext>(null);
        }

        public UltimusContext()
            : base("Name=UltimusContext")
        {
        }

    }
}

