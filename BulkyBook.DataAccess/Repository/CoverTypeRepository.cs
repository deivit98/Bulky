using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository: Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext db;
        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }



        public void Update(CoverType coverType)
        {
            this.db.CoverTypes.Update(coverType);
        }
    }
}
