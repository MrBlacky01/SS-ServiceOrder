using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceOrder.DataProvider.Entities;

namespace ServiceOrder.DataProvider.Utils
{
    internal static class ManyToManyCopierer<T> where T:Entity 
    {
        internal static void CopyList(List<T> newList, List<T> oldList,DbSet<T> db )
        {
            if (newList == null || oldList == null) return;
            var mustDeleteList = oldList.Where(element => newList.Count(c => c.Id == element.Id) == 0x0).ToList();

            foreach (var deleteElement in mustDeleteList)
            {
                oldList.Remove(deleteElement);
            }

            foreach (var element in newList)
            {
                if (oldList.Count(c => c.Id == element.Id) == 0x0)
                {
                    oldList.Add(db.First(c => c.Id == element.Id));
                }
            }
        }
    }
}
