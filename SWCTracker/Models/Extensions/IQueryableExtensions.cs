
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace SWCTracker.Models
{
    public static class IQueryableExtensions
    {
        #region Fields
        private static readonly SelectListItem DefaultItem = new SelectListItem()
        {
            Value = "0",
            Text = "-- please select --"
        };

        private static readonly DropDownListItem DefaultDropDownItem = new DropDownListItem()
        {
            Value = 0,
            Text = "-- please select --"
        };
        #endregion


        #region Methods
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ?
                source.Where(predicate) :
                source;
        }

        public static IOrderedEnumerable<TSource> OrderByWithDirection<TSource, TKey>
     (this IEnumerable<TSource> source,
      Func<TSource, TKey> keySelector,
      bool descending)
        {
            return descending ? source.OrderByDescending(keySelector)
                              : source.OrderBy(keySelector);
        }

        public static IOrderedQueryable<TSource> OrderByWithDirection<TSource, TKey>
            (this IQueryable<TSource> source,
             Expression<Func<TSource, TKey>> keySelector,
             bool descending)
        {
            return descending ? source.OrderByDescending(keySelector)
                              : source.OrderBy(keySelector);
        }

        public static IEnumerable<SelectListItem> ToSelectListItem<TSource>(
            this IEnumerable<TSource> enumerable,
            Func<TSource, string> text,
            Func<TSource, string> value,
            bool excludeDefaultItem = false,
            IEnumerable<int> selected = null,
            bool includeDefaultItem = false,
            bool excludeSort = false)
        {
            var result = new List<SelectListItem>();
            string record = string.Empty;
            if (!excludeDefaultItem && enumerable.Count() != 1)
            {
                result.Add(DefaultItem);
            }
            else if (includeDefaultItem)
            {
                result.Add(DefaultItem);
            }

            foreach (TSource model in enumerable)
            {
                var selectItem = new SelectListItem
                {
                    Text = text(model),
                    Value = value(model)
                };

                if (selected != null)
                {
                    int selectedId = 0;
                    if (int.TryParse(selectItem.Value, out selectedId))
                    {

                        selectItem.Selected = selected.Contains(int.Parse(selectItem.Value)) ? true : false;
                    }
                }

                result.Add(selectItem);
            }

            if (excludeSort)
                return result;
            else
                return result.OrderBy(a => a.Text);
        }


        public static IEnumerable<DropDownListItem> ToDropDownListItem<TSource>(
            this IEnumerable<TSource> enumerable,
            Func<TSource, string> text,
            Func<TSource, int> value,
            Func<TSource, string> value2 = null,
            Func<TSource, string> value3 = null,
            bool excludeDefaultItem = false,
            IEnumerable<int> selected = null,
            bool includeDefaultItem = false,
            bool excludeSort = false)
        {
            var result = new List<DropDownListItem>();
            string record = string.Empty;
            if (!excludeDefaultItem && enumerable.Count() != 1)
            {
                result.Add(DefaultDropDownItem);
            }
            else if (includeDefaultItem)
            {
                result.Add(DefaultDropDownItem);
            }

            foreach (TSource model in enumerable)
            {
                var selectItem = new DropDownListItem
                {
                    Text = text(model),
                    Value = value(model),
                    Value2 = value2(model),
                    Value3 = value3(model),
                };

                if (selected != null)
                {

                    selectItem.Selected = selected.Contains(selectItem.Value) ? true : false;

                }

                result.Add(selectItem);
            }

            if (excludeSort)
                return result;
            else
                return result.OrderBy(a => a.Text);
        }

        public static IEnumerable<SelectListItem> DefaultSelectListItem()
        {
            return new[] { DefaultItem };
        }

        public static IEnumerable<DropDownListItem> Default_DropDownItem()
        {
            return new[] { DefaultDropDownItem };
        }

       // public static ResultSetPage<T> ToResultSetPage<T>(
       //this GridLoadParam param,
       // System.Reflection.PropertyInfo propertyInfo,
       // IEnumerable<T> entityList)
       // {
       //     return new ResultSetPage<T>(
       //    param.Skip,
       //    entityList.Count(),
       //    entityList.ToList()
       //   .OrderByWithDirection(x => propertyInfo.GetValue(x, null), param.IsDescending)
       //   .Skip(param.Skip)
       //   .Take(param.Take)
       //   .ToList());
       // }
        #endregion

    }
}