using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using declaracionPatrimonial.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace declaracionPatrimonial.Data
{
    public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

     // Tablas
        public DbSet<Banco> Banco { get; set; }
        public DbSet<tipoCuenta> tipoCuenta { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<cuentaBancaria> cuentaBancaria { get; set; }
        public DbSet<creditoBancario> creditoBancario { get; set; }
        public DbSet<otroPasivo> otroPasivo { get; set; }
        public DbSet<tipoInmueble> tipoInmueble { get; set; }
        public DbSet<tipoPropiedad> tipoPropiedad { get; set; }
        public DbSet<Bienes> Bienes { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;database=Declaracion;User=root;Password=$54K38k5";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // relaciones
            modelBuilder.Entity<cuentaBancaria>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.cuentasBancaria)
                .HasForeignKey(c => c.IDUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<cuentaBancaria>()
                .HasOne(c => c.Banco)
                .WithMany(b => b.cuentasBancaria)
                .HasForeignKey(c => c.IDBanco)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<cuentaBancaria>()
                .HasOne(c => c.TipoCuenta)
                .WithMany(t => t.cuentasBancaria)
                .HasForeignKey(c => c.IDtipoCuenta)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<creditoBancario>()
                .HasOne(cb => cb.Usuario)
                .WithMany(u => u.creditoBancario)
                .HasForeignKey(cb => cb.IDUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<creditoBancario>()
                .HasOne(cb => cb.Banco)
                .WithMany(b => b.creditoBancario)
                .HasForeignKey(cb => cb.IDBanco)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<creditoBancario>()
                .HasOne(cb => cb.tipoCuenta)
                .WithMany(tc => tc.creditoBancario)
                .HasForeignKey(cb => cb.IDtipoCuenta)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<creditoBancario>()
                .HasOne(cb => cb.cuentaBancaria)
                .WithMany(cbk => cbk.creditoBancario)
                .HasForeignKey(cb => cb.IDCuentaBancaria)
                .OnDelete(DeleteBehavior.Restrict);

        
    
            modelBuilder.Entity<otroPasivo>()
                .HasOne(op => op.Usuario)
                .WithMany(u => u.otroPasivo)
                .HasForeignKey(op => op.IDUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<otroPasivo>()
                .HasOne(op => op.cuentaBancaria)
                .WithMany(cb => cb.otroPasivo)
                .HasForeignKey(op => op.IDCuentaBancaria)
                .OnDelete(DeleteBehavior.Restrict);

    
            modelBuilder.Entity<Bienes>()
                .HasOne(b => b.tipoInmueble)
                .WithMany(ti => ti.Bienes)
                .HasForeignKey(b => b.IDtipoInmueble)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bienes>()
                .HasOne(b => b.tipoPropiedad)
                .WithMany(tp => tp.Bienes)
                .HasForeignKey(b => b.IDtipoPropiedad)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bienes>()
                .HasOne(b => b.Usuario)
                .WithMany(u => u.Bienes)
                .HasForeignKey(b => b.IDUsuario)
                .OnDelete(DeleteBehavior.Restrict);
        }
        
}



}


