using System;
using System.Collections.Generic;
using System.Linq;
using Pelicula.Domain.Entities;
using Pelicula.Infrastructure.Data;
using Pelicula.Domain.interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace Infrastructure.Repositories
{
    public class RepositoryPeliculas : Ipeliculas
    {
        
       private readonly PeliculasContext _context;

        public RepositoryPeliculas( PeliculasContext context)
        {

            _context =  context;
            

        }
        
        public async Task<IQueryable<Peliculas>> GetAll()
        {
            var query = await _context.Peliculas.AsQueryable<Peliculas>().AsNoTracking().ToListAsync();
            return query.AsQueryable();
        }

        public async Task<Peliculas> GetById(int id)
        {            
            var query = await _context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            return query;
        }

        public bool Exist(Expression<Func<Peliculas, bool>> expression)
        {
            return _context.Peliculas.Any(expression);
        }
        public async Task<IQueryable<Peliculas>> GetByFilter(Peliculas Peliculas)
        {
            if(Peliculas == null)
                return new List<Peliculas>().AsQueryable();

            var query = _context.Peliculas.AsQueryable() ;

            if(!string.IsNullOrEmpty(Peliculas.Director))
                query = query.Where(x => x.Director.Contains(Peliculas.Director));

            if(!string.IsNullOrEmpty(Peliculas.Género))
                query = query.Where(x => x.Género == Peliculas.Género);

            if(!string.IsNullOrEmpty(Peliculas.Titulo)) 
                query = query.Where(x => x.Titulo == Peliculas.Titulo);

            var result = await query.ToListAsync();

            return result.AsQueryable().AsNoTracking();
        }  
        
        public async Task<int> Create(Peliculas Peliculas)
            {
                var entity = Peliculas;
                await _context.AddAsync(entity);
                var rows = await _context.SaveChangesAsync();

                if(rows <= 0)
                    throw new Exception("Error: El registro NO ha sido realizado...");

                return entity.Id;
            }

            public async Task<bool> Update(int Id, Peliculas peliculas)
            {
                if(Id <= 0 || peliculas == null)
                    throw new ArgumentException("INFORMACION INCOMPLETA: llena los campos para realizar la modificacion...");

                var entity = await GetById(Id);

                entity.Titulo = peliculas.Titulo;
                entity.Director = peliculas.Director;
                entity.Género = peliculas.Género;
                entity.Puntuacion = peliculas.Puntuacion;
                entity.Rating = peliculas.Rating;
                entity.Publicacion = peliculas.Publicacion;
                _context.Update(entity);

                var rows = await _context.SaveChangesAsync();
                return rows > 0;
            }
            public async Task<bool> Delete(int Id )
            {
                 if(Id <= 0 )
                    throw new ArgumentException("ERROR AL ELIMINAR:Falta información para continuar con el proceso de Eliminacion ...");

                var entity = await GetById(Id);
                _context.Remove(entity);
                var rows = await _context.SaveChangesAsync();
                return rows > 0;
            }
    }
}