using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace MonsterQuest
{
    public class BattleManager : MonoBehaviour
    {
        public static void Simulate(List<string> heroes, string monster, int monsterHP, int savingThrowDC)
        {
            var random = new Random();
            int constitution = 5;


            Console.WriteLine($"A {monster} with {monsterHP}HP appears");

            do
            {
                foreach (var name in heroes)
                {
                    int damage = DiceHelper.Roll("2d6");

                    monsterHP -= damage;

                    if (monsterHP <= 0)
                    {
                        Console.WriteLine($"{name} hits the {monster} for {damage} damage. {monster} has 0 HP left.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{name} hits the {monster} for {damage} damage. {monster} has {monsterHP} HP left.");
                    }
                }

                if (monsterHP <= 0)
                {
                    break;
                }

                int tarrgetIndex = random.Next(heroes.Count);
                string targetName = heroes[tarrgetIndex];

                Console.WriteLine($"The {monster} made a killing blow aginst {targetName}!");

                int savingThrow = DiceHelper.Roll("1d20");

                if (constitution + savingThrow < savingThrowDC)
                {
                    Console.WriteLine($"{targetName} rolls a {savingThrow} and was killed by the {monster}!");
                    heroes.Remove(targetName);
                }
                else
                {
                    Console.WriteLine($"{targetName} rolls a {savingThrow} and is saved from the attack.");
                }

            }
            while (heroes.Count > 0);

            if (monsterHP > 0)
            {
                Console.WriteLine($"Your party has died and the {monster} will ravish the lands!");
            }
            else
            {
                Console.WriteLine($"The {monster} collapses and the heroes celebrate their victory!");
            }
        }
    }
}
