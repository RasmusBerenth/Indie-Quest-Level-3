using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public static class IEnumerableHelper
    {
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            T[] array = enumerable.ToArray();

            int weaponInUseIndex = UnityEngine.Random.Range(0, array.Length);
            return array[weaponInUseIndex];
        }
    }
}
