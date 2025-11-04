using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface ICategoryRepository
    {
        void Create(Category category);
        void Remove(Category category);
        void Update(int id, Category category);
        Category GetById(int id);
    }
}