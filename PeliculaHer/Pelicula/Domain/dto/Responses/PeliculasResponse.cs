using System;
namespace Pelicula.Domain.dto.Responses
{
    public class PeliculasResponse
    {
         public int Id { get; set; }
        public string Titulo { get; set; }
        public string Director { get; set; }
        public string Género { get; set; }
        public double Puntuacion { get; set; }
        public DateTime Publicacion { get; set; }

    }
}