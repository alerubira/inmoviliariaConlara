using System.ComponentModel.DataAnnotations;


namespace Inmobiliaria.Models
{
	public class Inquilino
	{
		[Key]
		[Display(Name = "Código Int.")]
		public int IdInquilino { get; set; }
		[Required]
		public string Apellido { get; set; }
		[Required]
		public string Nombre { get; set; }
		[Required]
		public string Dni { get; set; }
		[Display(Name = "Teléfono")]
		public string Telefono { get; set; }
		[Required, EmailAddress]
		public string eMail { get; set; }
		

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