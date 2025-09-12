using System.ComponentModel.DataAnnotations;


namespace Inmobiliaria.Models
{
	public class TipoInmueble
	{
		[Key]
		[Display(Name = "Código Int.")]
		public int IdTipoInmueble { get; set; }
		
		[Required(ErrorMessage = "El nombre es obligatorio")]
		 [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
		public string Nombre { get; set; }=string.Empty;
		
		

		
	}
}