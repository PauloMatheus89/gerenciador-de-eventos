using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class CategoryMethods
    {
        public static bool CategoryIdIsDifferent(int categoryToUpdateId,int categoryId) 
        {
            return categoryId != 0 && categoryToUpdateId != categoryId;
        }

        public static void UpdateCategoryId(DatabaseContext context, Event entityToUpdate, Event entity)
        {
            if (CategoryIdIsDifferent(entityToUpdate.CategoryId, entity.CategoryId))
            {
                var newCategory = context.Categories.Find(entity.CategoryId);
                if (newCategory == null)
                    return;

                entityToUpdate.CategoryId = entity.CategoryId;
                entityToUpdate.Category = newCategory;
            }
        }
    }
}