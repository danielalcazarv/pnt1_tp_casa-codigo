using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace casa_codigo_cursos.Models
{
    public class Inscripcion
    {
        public int InscripcionId { get; set; }

        [Required(ErrorMessage = "El curso es obligatorio.")]
        public int CursoId { get; set; }

        [Required(ErrorMessage = "El alumno es obligatorio.")]
        public int UsuarioID { get; set; }

        public DateTime FechaInscripcion { get; set; }

        public Curso Curso { get; set; }

        public Usuario Usuario { get; set; }
    }
}

