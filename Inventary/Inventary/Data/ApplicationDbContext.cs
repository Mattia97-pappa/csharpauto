namespace Inventary.Data


{


    using Microsoft.EntityFrameworkCore;
    using Inventary.Models;
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Componente> Componenti { get; set; }
        public DbSet<Admin> Admin { get; set; }
    }



}

