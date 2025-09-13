
using System.ComponentModel.DataAnnotations;


namespace Inmobiliaria.Models
{
	public class Propietario
	{
		[Key]
		[Display(Name = "Código Int.")]
		public int IdPropietario { get; set; }
		[Required(ErrorMessage = "El apellido es obligatorio")]
		 [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres")]
		public string Apellido{ get; set; } = string.Empty;
		[Required(ErrorMessage = "El nombre es obligatorio")]
		 [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
		public string Nombre { get; set; } = string.Empty;
		[Required(ErrorMessage = "El DNI es obligatorio")]
		 [StringLength(100, ErrorMessage = "El DNI no puede superar los 100 caracteres")]
		public string Dni { get; set; } = string.Empty;
		[Display(Name = "Teléfono")]
		 [StringLength(100, ErrorMessage = "El teléfono no puede superar los 100 caracteres")]
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