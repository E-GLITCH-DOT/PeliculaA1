using System;
using System.Collections.Generic;

#nullable disable

namespace Pelicula.Domain.Entities
{
    public partial class Peliculas
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Director { get; set; }
        public string Género { get; set; }
        public double Puntuacion { get; set; }
        public int Rating { get; set; }
        public DateTime Publicacion { get; set; }
    }
}
