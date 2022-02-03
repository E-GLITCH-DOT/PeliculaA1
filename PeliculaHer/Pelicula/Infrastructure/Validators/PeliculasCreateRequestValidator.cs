using System;
using FluentValidation;
using Pelicula.Domain.dto.Requests;
using Pelicula.Domain.interfaces;
namespace Infrastructure.Validators
{
     public class PeliculasCreateRequestValidator : AbstractValidator<PeliculasCreateRequest>
    {
        private readonly Ipeliculas _repository;

        public PeliculasCreateRequestValidator(Ipeliculas repository)
        {
            this._repository = repository;

            RuleFor(x => x.Titulo).NotNull().NotEmpty().Length(5,40).Must(NotExistClub).WithMessage("El Titulo de la pelicula no puede ser identico al ya registrado");
            RuleFor(x => x.Director).NotNull().NotEmpty().Length(5, 40);
            RuleFor(x => x.Género).NotNull().NotEmpty().Length(5,20);
            RuleFor(x => x.Puntuacion).NotNull().NotEmpty().ExclusiveBetween(-1,11);
            RuleFor(x => x.Rating).NotNull().NotEmpty().ExclusiveBetween(0,101);
            RuleFor(x => x.Publicacion).NotNull();

        }

     
        public bool NotExistClub(string Titulo) 
        {
            return !_repository.Exist(p => p.Titulo == Titulo);
        }
    
    }
}
