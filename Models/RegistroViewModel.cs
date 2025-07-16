
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace casa_codigo_cursos.Models
{
    public class RegistroViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "DNI")]
        [Required(ErrorMessage = "El campo DNI es obligatorio.")]
        [Range(1000000, 99999999, ErrorMessage = "El DNI ingresado no es correcto.")]
        public int Dni { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Ingreso de nombre Obligatorio")]
        [MaxLength(20, ErrorMessage = "Máx. 20 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingreso de apellido Obligatorio")]
        [MaxLength(20, ErrorMessage = "Máx. 20 caracteres")]
        public string Apellido { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }
    }
}
