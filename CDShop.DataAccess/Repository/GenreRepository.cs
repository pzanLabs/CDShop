using CDShop.DataAccess.Data;
using CDShop.DataAccess.Repository.IRepository;
using CDShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDShop.DataAccess.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        public void Update(Genre obj)
        {
           _db.Genres.Update(obj);
        }
    }
}
