using MarvelBO.ApiModel;
using MarvelBO.Creators;
using MarvelBO.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvelBO.Api.Controllers
{
    internal static class Utils
    {
        internal static IEnumerable<ITEM_TYPE> FilterBy<ITEM_TYPE, FIELD_TYPE>(
                this IEnumerable<ITEM_TYPE> collection,
                FilterOption<FIELD_TYPE> filter,
                Func<ITEM_TYPE, FIELD_TYPE> fieldSelector
            )
            where FIELD_TYPE : struct, IComparable
        {
            if (filter == null)
                return collection;

            return collection.Where(
                item =>
                       (filter.Type == FilterType.greater
                            && fieldSelector(item).CompareTo(filter.Value) > 0)
                    || (filter.Type == FilterType.lesser
                            && fieldSelector(item).CompareTo(filter.Value) < 0)
                    || (filter.Type == FilterType.equal
                            && fieldSelector(item).CompareTo(filter.Value) == 0));
        }

        internal static IEnumerable<ITEM_TYPE> OrderBy<ITEM_TYPE>(
                this IEnumerable<ITEM_TYPE> collection,
                IEnumerable<OrderOption> orderBy,
                Dictionary<DataField, Func<ITEM_TYPE, IComparable>> fieldsMap
            )
        {
            if (orderBy == null)
                return collection;

            Func<ITEM_TYPE, IComparable> fieldSelector;

            foreach (var option in orderBy.Reverse())
            {
                if (option.Descending
                    && fieldsMap.TryGetValue(option.Field, out fieldSelector))
                {
                    collection = collection.OrderByDescending(fieldSelector);
                }
                else if (fieldsMap.TryGetValue(option.Field, out fieldSelector))
                {
                    collection = collection.OrderBy(fieldSelector);
                }
            }
            return collection;
        }

        internal static IEnumerable<Creator> AddNotes(
                this IEnumerable<Creator> creators,
                INotesManager notesManager
            )
        {
            return creators.Select(
                creator =>
                {
                    Note note;
                    if (notesManager.TryGetNote(creator.Id, out note))
                    {
                        creator.Note = note.Content;
                    }
                    return creator;
                });
        }

        internal static IEnumerable<Note> AddCreatorNames(
                this IEnumerable<Note> notes,
                ICreatorsManager creatorsManager
            )
        {
            return notes.Select(
               note =>
               {
                   note.CreatorName = creatorsManager.GetCreator(note.Id).FullName;
                   return note;
               });
        }
    }
}
