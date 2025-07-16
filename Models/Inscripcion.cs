using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace casa_codigo_cursos.Models
{
    public enum CodigoLetra
    {
        A, B, C, D, E
    }

    public class Inscripcion
    {
        public int InscripcionId { get; set; }

        [Required(ErrorMessage = "El curso es obligatorio.")]
        public int CursoId { get; set; }

        [Required(ErrorMessage = "El alumno es obligatorio.")]
        public int AlumnoID { get; set; }

        [EnumDataType(typeof(CodigoLetra), ErrorMessage = "Código de letra inválido.")]
        public CodigoLetra? CodigoLetra { get; set; }

        public Curso Curso { get; set; }

        public Usuario Usuario { get; set; }
    }
}

