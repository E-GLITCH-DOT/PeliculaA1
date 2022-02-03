using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Infrastructure.Repositories;
using Pelicula.Domain.dto;
using System.Linq;
using System.Text.Json;
using System.IO;
using Infrastructure;
using System.Threading.Tasks;
using Pelicula.Domain.Entities;
using Pelicula.Domain.interfaces;
using Microsoft.AspNetCore.Http;
using System;
using Pelicula.Domain.dto.Requests;
using AutoMapper;
using FluentValidation;
using Pelicula.Domain.dto.Responses;
using System.Collections.Generic;
namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Peliculascontroller : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly Ipeliculas _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<PeliculasCreateRequest> _createValidator;

        public Peliculascontroller(IHttpContextAccessor httpContext, Ipeliculas repository, IMapper mapper, IValidator<PeliculasCreateRequest> createValidator)
        {
            this._httpContext = httpContext;
            _repository = repository;
            this._mapper = mapper;
            this._createValidator = createValidator;
        }
        [HttpGet]
        [Route("ALL")]
        public async Task<IActionResult> GetAll()
        {
            var peliculas = await _repository.GetAll();
            var respuesta = _mapper.Map<IEnumerable<Peliculas>,IEnumerable<PeliculasResponse>>(peliculas);
            return Ok(respuesta);
        } 

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var peliculas = await _repository.GetById(id);

            if(peliculas == null)
                return NotFound($"No se ha encontrado ningun resultado con el ID {id}...");

            var respuesta = _mapper.Map<Peliculas, PeliculasResponse>(peliculas);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("Find")]
        public async Task<IActionResult> GetByFilter(Peliculas peliculas)
        {
            var pelicula = await _repository.GetByFilter(peliculas); 
            var respuesta = _mapper.Map<IEnumerable<Peliculas>,IEnumerable<PeliculasResponse>>(pelicula);
            return Ok(respuesta);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PeliculasCreateRequest peliculas)
        {
            var validationResult = await _createValidator.ValidateAsync(peliculas);

            if(!validationResult.IsValid)
                return UnprocessableEntity(validationResult.Errors.Select(x => $"Error: {x.ErrorMessage}"));

            var entity = _mapper.Map<PeliculasCreateRequest, Peliculas>(peliculas);
            var id = await _repository.Create(entity); //id=1

            if(id<= 0)
                return Conflict("No se ha realizado el registro, compruebe la informacion e intente de nuevo...");

            var urlresult = $"https://{_httpContext.HttpContext.Request.Host.Value}/api/Clubs/{id}";
            return Created(urlresult, id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Peliculas peliculas)
        {
            if(id <= 0)
                return NotFound("No se encontro el ningun registro con esa id...");

            peliculas.Id = id;

            var update = await _repository.Update(id, peliculas);

            if(!update)
                return Conflict("Error: ha ocurrido algo al intentar modificar...");

            return NoContent();            
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> Delete(int id )
        {
            if(  !_repository.Exist(p => p.Id == id))
            {
                 return NotFound("No se encontro el ningun registro con esa id...");
            }
               
            var Eliminar = await _repository.Delete(id);
           

            return NoContent();
        }
     }

}