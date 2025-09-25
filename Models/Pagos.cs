using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
  //[Table("Inmuebles")]
  public enum enMeses
  {

    Enero = 1,
    Febrero = 2,
    Marzo = 3,
    Abril = 4,
    Mayo = 5,
    Junio = 6,
    Julio = 7,
    Agosto = 8,
    Septiembre = 9,
    Octubre = 10,
    Noviembre = 11,
    Diciembre = 12
	}
  public class Pagos
  {
    [Display(Name = "Numero de Pago")]
    public int IdPagos { get; set; }
    //[Required]
    [Display(Name = "Codigo de Contrato")]
    [Required(ErrorMessage = "El contrato es obligatorio")]
    public int IdContratos { get; set; }
    [Required(ErrorMessage = "La fecha de pago es obligatoria")]
    public DateTime FechaPago { get; set; }
    [Required(ErrorMessage = "El importe es obligatorio")]
    [Display(Name = "Importe Sugerido")]
    public decimal Importe { get; set; }
    [Required(ErrorMessage = "El concepto es obligatorio")]
    [Display(Name = "Concepto de Pago")]
    public String? Concepto { get; set; }
    [Required(ErrorMessage = "El numero de cuota es obligatorio")]
    public int NumeroCuota { get; set; }
    [Required(ErrorMessage = "El mes de pago es obligatorio")]
   

    [Display(Name = "Direccion Inmueble")]
    public String? DireccionInmueble { get; set; }
    public Boolean? Multa{ get; set; } 
    public int MesPago { get; set; }
	//	[NotMapped]//Para EF
		public string MesPagoNombre => MesPago > 0 ? ((enMeses)MesPago).ToString() : "";

		public static IDictionary<int, string> ObtenerMeses()
		{
			SortedDictionary<int, string> meses = new SortedDictionary<int, string>();
			Type tipoEnumMeses = typeof(enMeses);
			foreach (var valor in Enum.GetValues(tipoEnumMeses))
			{
				meses.Add((int)valor, Enum.GetName(tipoEnumMeses, valor));
			}
			return meses;
		}
  }
}