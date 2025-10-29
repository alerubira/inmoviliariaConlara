using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria.Models;


namespace Inmobiliaria.Models
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}
		public DbSet<Propietario> Propietario { get; set; }
		public DbSet<Inquilino> Inquilino { get; set; }
		public DbSet<Inmuebles> Inmuebles { get; set; }
		public DbSet<TipoInmueble> TipoInmueble { get; set; }
        public DbSet<Contratos> Contratos { get; set; }
        public DbSet<Pagos> Pagos { get; set; }
		
	}
}