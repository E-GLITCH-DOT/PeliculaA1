using System;

namespace Pelicula.Domain.dto.Requests
{
    public class PeliculasCreateRequest
    {
        public string Titulo { get; set; }
        public string Director { get; set; }
        public string Género { get; set; }
        public double Puntuacion { get; set; }
        public int Rating { get; set; }
        public DateTime Publicacion { get; set; }
    }
}