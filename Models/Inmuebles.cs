using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inmobiliaria.Models
{
  //[Table("Inmuebles")]
  public class Inmuebles
  {
    [Key]
    [Display(Name = "Codigo Interno")]
    public int IdInmuebles { get; set; }
    //[Required]
    [Display(Name = "Dirección")]
    [Required(ErrorMessage = "La dirección es requerida")]
    [StringLength(100, ErrorMessage = "La dirección no puede superar los 100 caracteres")]
    public string? Direccion { get; set; }
    [Required(ErrorMessage = "La Cantidad de Ambientes es obligatorio")]
    [Range(1, 20, ErrorMessage = "La cantidad de ambientes debe estar entre 1 y 20")]
    public int Ambientes { get; set; }
    [Required(ErrorMessage = "La superficie es obligatoria")]

    public int Superficie { get; set; }
    [Required(ErrorMessage = "La latitud es obligatoria")]

    public decimal Latitud { get; set; }
    [Required(ErrorMessage = "La longitud es obligatoria")]

    public decimal Longitud { get; set; }
    [Display(Name = "Dueño")]
    [Required(ErrorMessage = "El propietario es obligatorio")]
    public int IdPropietario { get; set; }
    [Display(Name = "Tipo de Inmueble")]
    [Required(ErrorMessage = "El tipo de inmueble es obligatorio")]
    [ForeignKey(nameof(IdTipoInmueble))]
    public int IdTipoInmueble { get; set; }
    [Required(ErrorMessage = "El precio es obligatorio")]
    public decimal Valor { get; set; }
    public bool Disponible { get; set; }
    [Display(Name = "Dueño")]
     [ForeignKey(nameof(IdPropietario))]
    public Propietario? Duenio { get; set; }
    [Display(Name = "Tipo de Inmueble")]
    [NotMapped]
       
    public String? Tipo { get; set; }
    [Required]
    public bool Existe { get; set; }
    public String? imagen{ get; set; }
		/*[ForeignKey(nameof(idPropietario))]
   [BindNever]
    public Propietario? Duenio { get; set; }
    public string? Portada { get; set; }
    [NotMapped]//Para EF
    public IFormFile? PortadaFile { get; set; }
    [ForeignKey(nameof(Imagen.InmuebleId))]
    public IList<Imagen> Imagenes { get; set; } = new List<Imagen>();
    [NotMapped]
    public bool Habilitado { get; set; } = true;*/
  }
}