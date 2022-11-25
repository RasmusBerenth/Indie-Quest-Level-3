using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace MonsterQuest
{
    public class CombatManager : MonoBehaviour
    {
        public static void Simulate(GameState gameState)//List<string> heroes, string monster, int monsterHP, int savingThrowDC
        {
            var random = new Random();
            int constitution = 5;


            Console.WriteLine($"A {gameState.combat.monster.displayName} with {gameState.combat.monster.hitPoints}HP appears");

            do
            {
                foreach (var name in gameState.party.characters)
                {
                    int damage = DiceHelper.Roll("2d6");

                    gameState.combat.monster.hitPoints -= damage;

                    if (gameState.combat.monster.hitPoints <= 0)
                    {
                        Console.WriteLine($"{name} hits the {gameState.combat.monster.displayName} for {damage} damage. {gameState.combat.monster.displayName} has 0 HP left.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{name} hits the {gameState.combat.monster.displayName} for {damage} damage. {gameState.combat.monster.displayName} has {gameState.combat.monster.hitPoints} HP left.");
                    }
                }

                if (gameState.combat.monster.hitPoints <= 0)
                {
                    break;
                }

                int targetIndex = random.Next(gameState.party.characters.Count);

                Character targetName = gameState.party.characters[targetIndex];

                Console.WriteLine($"The {gameState.combat.monster.displayName} made a killing blow aginst {targetName}!");

                int savingThrow = DiceHelper.Roll("1d20");

                if (constitution + savingThrow < gameState.combat.monster.savingThrow)
                {
                    Console.WriteLine($"{targetName} rolls a {savingThrow} and was killed by the {gameState.combat.monster.displayName}!");
                    gameState.party.characters.Remove(targetName);
                }
                else
                {
                    Console.WriteLine($"{targetName} rolls a {savingThrow} and is saved from the attack.");
                }

            }
            while (gameState.party.characters.Count > 0);

            if (gameState.combat.monster.hitPoints > 0)
            {
                Console.WriteLine($"Your party has died and the {gameState.combat.monster.displayName} will ravish the lands!");
            }
            else
            {
                Console.WriteLine($"The {gameState.combat.monster.displayName} collapses and the heroes celebrate their victory!");
            }
        }
    }
}
