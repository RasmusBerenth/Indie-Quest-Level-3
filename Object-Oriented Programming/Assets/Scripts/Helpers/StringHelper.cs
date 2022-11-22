using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public class StringHelper
    {
        public static string JoinWithAnd(IEnumerable<string> items, bool useSerialComma = true)
        {
            int count = items.Count();
            if (count == 0)
            {
                return "";
            }

            if (count == 1)
            {
                return items[0];
            }

            if (count == 2)
            {
                return String.Join(" and ", items);
            }
            else
            {
                var itemsCopy = new List<string>(items);

                if (useSerialComma)
                {
                    //Prepend "and" to the last item in the copied list
                    string lastItem = itemsCopy[items.Count() - 1];
                    lastItem = $"and {lastItem}";
                    itemsCopy[items.Count() - 1] = lastItem;

                }
                else
                {
                    //Join the two last items with "and" and set this text as the second last item in the copied list
                    string lastTwoItems = $"{itemsCopy[items.Count() - 2]} and {itemsCopy[items.Count() - 1]}";
                    itemsCopy[items.Count() - 2] = lastTwoItems;


                    //Remove the last item in the copied list
                    itemsCopy.RemoveAt(items.Count() - 1);

                }
                return String.Join(", ", itemsCopy);
            }
        }
    }
}
