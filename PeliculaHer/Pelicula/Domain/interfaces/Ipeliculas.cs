using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Pelicula.Domain.Entities;
namespace Pelicula.Domain.interfaces
{
    public interface Ipeliculas
    {
        Task<Peliculas> GetById(int id);
        Task<IQueryable<Peliculas>> GetAll();
        Task<IQueryable<Peliculas>> GetByFilter(Peliculas Peliculas);
        bool Exist(Expression<Func<Peliculas, bool>> expression);
        //Insert,Update,Delete
        Task<int> Create(Peliculas peliculas);
        Task<bool> Update(int id, Peliculas peliculas);
        Task<bool> Delete(int id);
    }
}