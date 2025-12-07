using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface ICategoryService
    {
        Task<Category> AddCategory(CategoryDto categoryDto);
        Task<Category> RemoveCategory(int id);
        Task<Category> UpdateCategory(int id,CategoryDto categoryDto);
        Task<Category?> GetById(int categoryId);
        Task<IEnumerable<Category>> GetAllCaetgories();
    }
}