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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            
            Genre = new GenreRepository(_db);
            Package = new PackageRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ApplicationUser= new ApplicationUserRepository(_db);
            ShoppingCart= new ShoppingCartRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
        }
        public IGenreRepository Genre { get; private set; }
        public IPackageRepository Package { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; set; }
        public IShoppingCartRepository ShoppingCart { get; set; }
        public IOrderDetailRepository OrderDetail { get; set; }
        public IOrderHeaderRepository OrderHeader { get; set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
