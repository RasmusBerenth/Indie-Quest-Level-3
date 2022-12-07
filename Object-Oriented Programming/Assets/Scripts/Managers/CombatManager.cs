using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace MonsterQuest
{
    public class CombatManager : MonoBehaviour
    {
        public IEnumerator Simulate(GameState gameState) //List<string> heroes, string monster, int monsterHP, int savingThrowDC
        {
            var random = new Random();
            int constitution = 5;


            Console.WriteLine($"A {gameState.combat} with {gameState.combat.monster.hitPoints}HP appears");

            do
            {
                foreach (var character in gameState.party.characters)
                {
                    int damage = DiceHelper.Roll("2d6");

                    yield return character.presenter.Attack();
                    yield return gameState.combat.monster.ReactToDamage(damage);

                    if (gameState.combat.monster.hitPoints <= 0)
                    {
                        Console.WriteLine($"{character} hits the {gameState.combat} for {damage} damage. {gameState.combat} has 0 HP left.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{character} hits the {gameState.combat} for {damage} damage. {gameState.combat} has {gameState.combat.monster.hitPoints} HP left.");
                    }
                }

                if (gameState.combat.monster.hitPoints <= 0)
                {
                    break;
                }

                int targetIndex = random.Next(gameState.party.characters.Count);

                Character targetCharacter = gameState.party.characters[targetIndex];

                Console.WriteLine($"The {gameState.combat} made a killing blow aginst {targetCharacter}!");
                yield return gameState.combat.monster.presenter.Attack();

                //Change between here
                int savingThrow = DiceHelper.Roll("1d20");

                if (constitution + savingThrow < gameState.combat.monster.savingThrow)
                {
                    Console.WriteLine($"{targetCharacter} rolls a {savingThrow} and was killed by the {gameState.combat}!");
                    gameState.party.characters.Remove(targetCharacter);
                    yield return targetCharacter.ReactToDamage(10);
                }
                else
                {
                    Console.WriteLine($"{targetCharacter} rolls a {savingThrow} and is saved from the attack.");
                }

            }
            while (gameState.party.characters.Count > 0);
            //and here


            if (gameState.combat.monster.hitPoints > 0)
            {
                Console.WriteLine($"Your party has died and the {gameState.combat} will ravish the lands!");
            }
            else
            {
                Console.WriteLine($"The {gameState.combat} collapses and the heroes celebrate their victory!");
            }
        }
    }
}
