using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace casa_codigo_cursos.Models
{
    public class CarritoCurso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarritoId { get; set; }

        public Carrito Carrito { get; set; }

        [Required]
        public int CursoId { get; set; }

        public Curso Curso { get; set; }
    }
} 