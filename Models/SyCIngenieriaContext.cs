using Microsoft.EntityFrameworkCore;
using S_CIngenieria.Models.GestionContratos;
using S_CIngenieria.Models.Seguridad;


namespace S_CIngenieria.Models
{
    public class SyCIngenieriaContext : DbContext
    {
        public SyCIngenieriaContext(DbContextOptions<SyCIngenieriaContext> options) : base(options)
        {
        }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Modulos> Modulos { get; set; }
        public DbSet<RelacionalModulosRoles> RelacionalModulosRoles { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<Empresas> Empresas { get; set; }
        public DbSet<Proyectos> Proyectos { get; set; }
        public DbSet<Troncales> Troncales { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<AmpliacionContrato> AmpliacionContratos { get; set; }
        public DbSet<OrdenCambio> ordenCambios { get; set; }
        public DbSet<ODS> oDs { get; set; }
        public DbSet<Actas> Actas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuarios>()
                .HasOne(u => u.Rol)
                .WithMany()
                .HasForeignKey(u => u.FkRol)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<RelacionalModulosRoles>()
                .HasOne(rmr => rmr.Modulo)
                .WithMany()
                .HasForeignKey(rmr => rmr.FkModulo)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<RelacionalModulosRoles>()
                .HasOne(rmr => rmr.Rol)
                .WithMany()
                .HasForeignKey(rmr => rmr.FkRol)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Permisos>()
                .HasOne(p => p.RelacionalModulosRoles)
                .WithMany(rmr => rmr.Permisos)
                .HasForeignKey(p => p.FkRelacionalModulosRoles)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contrato>()
             .HasOne(c => c.Empresas)
             .WithMany(e => e.Contratos)
             .HasForeignKey(c => c.EmpresasId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ODS>()
                .HasOne(o => o.Empresas)
                .WithMany(e => e.ODS)
                .HasForeignKey(o => o.EmpesasId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AmpliacionContrato>()
                 .HasOne(a => a.Contrato)
                 .WithMany()
                 .HasForeignKey(a => a.ContratoId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrdenCambio>()
                .HasOne(o => o.Contrato)
                .WithMany(c => c.OrdenesCambio)
                .HasForeignKey(o => o.ContratoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrdenCambio>()
               .HasOne(o => o.AmpliacionContrato)
               .WithOne()
               .HasForeignKey<OrdenCambio>(o => o.AmpliacionContratoId);

            modelBuilder.Entity<ODS>()
               .HasOne(o => o.OrdenCambio)
               .WithMany(oc => oc.ODS)
               .HasForeignKey(o => o.OrdenesCambioId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ODS>()
               .HasOne(o => o.Proyectos)
               .WithMany(p => p.ODS)
               .HasForeignKey(o => o.ProyectosId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ODS>()
              .HasOne(o => o.Troncales)
              .WithMany(t => t.ODS)
              .HasForeignKey(o => o.TroncalesId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ODS>()
              .HasOne(o => o.SupervisorUsuarios)
              .WithMany()
              .HasForeignKey(o => o.Supervisor)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ODS>()
              .HasOne(o => o.SolicitanteUsuarios)
              .WithMany()
              .HasForeignKey(o => o.SolicitanteCliente)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Actas>()
              .HasOne(a => a.ODS)
              .WithMany(o => o.Actas)
              .HasForeignKey(a => a.ODSId)
              .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
