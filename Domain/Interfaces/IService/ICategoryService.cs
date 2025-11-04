using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void RemoveCategory(Category category);
        void UpdateCategory(int id,Category category);
        Category? GetById(int categoryId);
    }
}