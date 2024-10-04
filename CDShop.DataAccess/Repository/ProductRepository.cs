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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u=>u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Title=obj.Title;
                objFromDb.Author=obj.Author;
                objFromDb.Description=obj.Description;
                objFromDb.Year=obj.Year;
                objFromDb.Price=obj.Price;
                objFromDb.Price50=obj.Price50;
                objFromDb.Price100=obj.Price100;
                objFromDb.ListPrice=obj.ListPrice;
                objFromDb.GenreId=obj.GenreId;
                objFromDb.PackageId=obj.PackageId;
            }
        }
    }
}
