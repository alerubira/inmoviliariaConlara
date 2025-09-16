
using System.ComponentModel.DataAnnotations;


namespace InmobiliariaConlara.Models
{
	public class Login
	{
		[DataType(DataType.EmailAddress)]
		public string? Usuario { get; set; }
		[DataType(DataType.Password)]
		public string? Clave { get; set; }
	}
}