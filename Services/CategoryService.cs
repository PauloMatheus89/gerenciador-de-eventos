using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;
using GerenciadorEventos.Models;
using GerenciadorEventos.Repositories.RepositoryClasses;

namespace GerenciadorEventos.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> AddCategory(CategoryDto categoryDto)
        {
            if (categoryDto is null)
                throw new ArgumentNullException("CategoryDto received is null!");

            if (await _categoryRepository.NameExists(categoryDto.Name))
                throw new DuplicateNameException("There is already a Category with that name!");

            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            var categoryCreated = await _categoryRepository.Create(category);
 
            return categoryCreated;
        }

        public async Task<IEnumerable<Category>> GetAllCaetgories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<Category?> GetById(int categoryId)
        {
            return await _categoryRepository.GetById(categoryId);
        }

        public async Task<Category> RemoveCategory(int id)
        {
            var categoryToDelete = await _categoryRepository.GetById(id);

            if (categoryToDelete is null)
                throw new KeyNotFoundException("Could not find a Category related to that id to delete!");

            if (categoryToDelete.Events.Count == 0)
                throw new ArgumentException("Cannot Delete Category beacuse it's associated with event.");

            var category = await _categoryRepository.Remove(categoryToDelete);

            return category;
        }

        public async Task<Category> UpdateCategory(int id, CategoryDto categoryDto)
        {
            var categoryToUpdate = await _categoryRepository.GetById(id);

            if (categoryDto is null)
                throw new ArgumentNullException("Category Dto received is null!");

            if (categoryToUpdate is null)
                throw new KeyNotFoundException("Could not find a Category related to that Id");

            if (categoryToUpdate.Name != categoryDto.Name && await _categoryRepository.NameExists(categoryDto.Name))
                throw new DuplicateNameException("CategoryDto Name already exists in the database!");

            var newCategory = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            var category = await _categoryRepository.Update(categoryToUpdate, newCategory);

            return category;
        }
    }
}