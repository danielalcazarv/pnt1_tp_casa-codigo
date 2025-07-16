using System.ComponentModel.DataAnnotations.Schema;

namespace casa_codigo_cursos.Models
{
    public class Curso
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CursoId { get; set; }
        public string Titulo { get; set; }
        public double Precio { get; set; }
        public string ImagenUrl { get; set; }

        public ICollection<Inscripcion> Inscripciones { get; set; }

    }
}
