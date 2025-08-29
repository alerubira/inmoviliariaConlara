using System.ComponentModel.DataAnnotations;


namespace Inmobiliaria.Models
{
	public class TipoInmueble
	{
		[Key]
		[Display(Name = "Código Int.")]
		public int IdTipoInmueble { get; set; }
		
		[Required]
		public string Nombre { get; set; }=string.Empty;
		
		

		
	}
}