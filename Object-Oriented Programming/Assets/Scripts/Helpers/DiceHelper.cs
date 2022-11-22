using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace MonsterQuest
{
    public class DiceHelper
    {
        public static int Roll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {

            var random = new Random();
            int result = 0;
            for (int i = 0; i < numberOfRolls; i++)
            {
                int roll = random.Next(1, diceSides + 1);
                result += roll;
            }
            result += fixedBonus;
            return result;
        }
    }
}
