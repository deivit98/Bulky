﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Update(Category category)
        {
            this.db.Update(category);
        }
    }
}
