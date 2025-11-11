using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Infrastructure.Databases;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class FavoriteMethods
    {
        public static ICollection<Favorite> FindFavoritesToDelete(ICollection<Favorite> presentList, ICollection<Favorite> newList)
        {
            return presentList.Where(e => !newList.Any(evt => e.Id == evt.Id)).ToList();
        }

        public static void UpdateFavorite(DatabaseContext context, ICollection<Favorite> favorites, ICollection<Favorite> favoritesToUpdate)
        {
            var setA = new HashSet<int>(favorites.Select(e => e.Id));
            var setB = new HashSet<int>(favoritesToUpdate.Select(e => e.Id));

            if (!setA.SetEquals(setB))
            {
                foreach (var fav in favorites)
                {
                    if (!favoritesToUpdate.Any(e => e.Id == fav.Id))
                        favoritesToUpdate.Add(fav);
                }

                var toDelete = FindFavoritesToDelete(favoritesToUpdate, favorites);
                foreach (var evt in toDelete)
                {
                    favoritesToUpdate.Remove(evt);
                }
            }

        }
    }
}