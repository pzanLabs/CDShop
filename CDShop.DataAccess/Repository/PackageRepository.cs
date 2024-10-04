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
    public class PackageRepository : Repository<Package>, IPackageRepository
    {
        private readonly ApplicationDbContext _db;

        public PackageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Package obj)
        {
            _db.Packages.Update(obj);
        }
    }
}
