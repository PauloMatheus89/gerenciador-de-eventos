using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Enums;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface ICategoryRepository
    {
        Task<Category> Create(Category category);
        Task<Category> Remove(Category category);
        Task<Category> Update(Category categoryToUpdate, Category newCategory);
        Task<Category?> GetById(int id);
        Task<IEnumerable<Category>> GetAllCategories();
        Task<bool> NameExists(CategoryName name);
    }
}