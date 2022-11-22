using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace MonsterQuest
{
    public class GameManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var names = new List<string>();
            names.Add("Ken");
            names.Add("Barby");
            names.Add("Roland");
            names.Add("Melissa");

            Console.WriteLine($"A party of warriors {String.Join(", ", names)} descends into the dungeon.");

            //random HP orc (2d8+6) mage (9d8) and troll (8d10+40)
            SimulateBattle(names, "orc", DiceHelper.Roll(2, 8, 6), 12);

            if (names.Count > 0)
            {
                SimulateBattle(names, "mage", DiceHelper.Roll(9, 8), 20);

            }

            if (names.Count > 0)
            {
                SimulateBattle(names, "troll", DiceHelper.Roll(8, 10, 40), 18);
            }

            if (names.Count > 0)
            {
                Console.WriteLine($"{String.Join(", ", names)} survived the doungeon.");
            }
        }


        static void SimulateBattle(List<string> heroes, string monster, int monsterHP, int savingThrowDC)
        {
            var random = new Random();
            int constitution = 5;




            Console.WriteLine($"A {monster} with {monsterHP}HP appears");

            do
            {
                foreach (var name in heroes)
                {
                    int damage = DiceHelper.Roll(2, 6);

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

                int savingThrow = DiceHelper.Roll(1, 20);

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
