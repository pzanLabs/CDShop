using CDShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDShop.DataAccess.Repository.IRepository
{
    public interface IPackageRepository: IRepository<Package>
    {
        void Update(Package obj);
    }
}
