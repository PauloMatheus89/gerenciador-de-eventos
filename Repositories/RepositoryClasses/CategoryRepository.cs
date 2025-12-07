using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Enums;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEventos.Repositories.RepositoryClasses
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Category> Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.Include(e => e.Events).ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories.Include(e => e.Events).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Category> Remove(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> Update(Category categoryToUpdate, Category newCategory)
        {
            categoryToUpdate.Name = newCategory.Name;
            categoryToUpdate.Description = newCategory.Description;

            await _context.SaveChangesAsync();

            return categoryToUpdate;
        }

        public async Task<bool> NameExists(CategoryName name)
        {
            return await _context.Categories.AnyAsync(e => name == e.Name);
        }
    }
}