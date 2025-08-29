
using System.ComponentModel.DataAnnotations;


namespace Inmobiliaria.Models
{
	public class Propietario
	{
		[Key]
		[Display(Name = "Código Int.")]
		public int IdPropietario { get; set; }
		[Required]
		public string Apellido{ get; set; } = string.Empty;
		[Required]
		public string Nombre { get; set; } = string.Empty;
		[Required]
		public string Dni { get; set; } = string.Empty;
		[Display(Name = "Teléfono")]
		public string Telefono { get; set; } = string.Empty;
		[Required, EmailAddress]
		public string eMail { get; set; } = string.Empty;
		[Required(ErrorMessage = "La clave es obligatoria"), DataType(DataType.Password)]
		public string Clave { get; set; } = string.Empty;

		public override string ToString()
		{
			//return $"{Apellido}, {Nombre}";
			//return $"{Nombre} {Apellido}";
			var res = $"{Nombre} {Apellido}";
			if(!String.IsNullOrEmpty(Dni)) {
				res += $" ({Dni})";
			}
			return res;
		}
	}
}