using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace casa_codigo_cursos.Models
{
    public class Curso
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CursoId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede superar los 100 caracteres.")]
        public string Titulo { get; set; }

        [Range(0, 9999999999.99, ErrorMessage = "El precio debe ser un valor positivo.")]
        public double Precio { get; set; }

        [Url(ErrorMessage = "Debe ser una URL válida.")]
        [StringLength(200, ErrorMessage = "La URL de la imagen no puede superar los 200 caracteres.")]
        public string ImagenUrl { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(1000, ErrorMessage = "La descripción no puede superar los 1000 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La duracion es obligatoria.")]
        [Range(45, 9999, ErrorMessage = "La duración de la clase no debe ser menor a 45 minutos.")]
        public int Duracion { get; set; }

        public ICollection<Inscripcion> Inscripciones { get; set; }

    }
}
