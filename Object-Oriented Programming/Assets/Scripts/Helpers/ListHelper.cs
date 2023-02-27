using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class ListHelper
    {
        public static void Shuffle<Creature>(List<Creature> items)
        {
            //-- To shuffle an array a of n elements (indices 0..n-1):
            //for i from n?1 downto 1 do
            //        j ? random integer such that 0 ? j ? i
            //        exchange a[j] and a[i]


            //var random = new Random();

            for (int i = items.Count - 1; i >= 1; i--)
            {
                int j = Random.Range(0, i + 1);
                //T itemJ = items[j];
                //items[j] = items[i];
                //items[i] = itemJ;

                (items[j], items[i]) = (items[i], items[j]);
            }

        }
    }
}
