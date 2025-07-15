
namespace casa_codigo_cursos.Models
{
    public enum CodigoLetra 
    {
        A,B,C,D,E
    }
    public class Inscripcion
    {
        public int InscripcionId { get; set; }
        public int CursoId { get; set; }
        public int AlumnoID { get; set; }
        public CodigoLetra? CodigoLetra { get; set; }

        public Curso Curso { get; set; }
        public Usuario Usuario { get; set; }
    }
}
