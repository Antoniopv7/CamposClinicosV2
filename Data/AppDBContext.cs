using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Campos_Clinicos.Data
{
    public class AppDBContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("CamposClinicos"));
        }

        public DbSet<Estudiante> estudiantes { get; set; }
    }
}
