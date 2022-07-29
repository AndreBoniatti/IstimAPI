using System;
using System.Linq.Expressions;
using IstimAPI.Models;

namespace IstimAPI.Data.Queries
{
    public static class GameQueries
    {
        public static Expression<Func<Game, bool>> GetGames(string globalFilter)
        {
            if (!string.IsNullOrEmpty(globalFilter))
            {
                return x => (x.IsActive == true) &&
                    x.Title.ToLower().Contains(globalFilter.ToLower()) ||
                    x.Description.ToLower().Contains(globalFilter.ToLower()) ||
                    x.Category.Title.ToLower().Contains(globalFilter.ToLower());
            }

            return x => x.IsActive == true;
        }
    }
}