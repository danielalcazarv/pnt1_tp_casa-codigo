namespace casa_codigo_cursos.Models
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Titulo { get; set; }
        public double Precio { get; set; }
        public string imagen { get; set; }

        public ICollection<Inscripcion> Inscripciones { get; set; }

    }
}
