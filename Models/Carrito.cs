using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace casa_codigo_cursos.Models
{
    public class Carrito
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public ICollection<CarritoCurso> CarritoCursos { get; set; }
    }
} 