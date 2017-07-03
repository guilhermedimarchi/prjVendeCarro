using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace prjVendeCarro.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<prjVendeCarro.Models.CarroModels> CarroModels { get; set; }

        public System.Data.Entity.DbSet<prjVendeCarro.Models.CombustivelModels> CombustivelModels { get; set; }

        public System.Data.Entity.DbSet<prjVendeCarro.Models.MarcaModels> MarcaModels { get; set; }

        public System.Data.Entity.DbSet<prjVendeCarro.Models.ModeloModels> ModeloModels { get; set; }

        public System.Data.Entity.DbSet<prjVendeCarro.Models.CidadeModels> CidadeModels { get; set; }

        public System.Data.Entity.DbSet<prjVendeCarro.Models.UsuarioModels> UsuarioModels { get; set; }

        public System.Data.Entity.DbSet<prjVendeCarro.Models.UsuarioTipoModels> UsuarioTipoModels { get; set; }

        public System.Data.Entity.DbSet<prjVendeCarro.Models.EstadoModels> EstadoModels { get; set; }
    }
}