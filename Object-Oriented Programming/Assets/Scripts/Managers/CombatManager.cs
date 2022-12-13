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


            Console.WriteLine($"A {gameState.combat} with {gameState.combat.monster.hitPoints}HP appears");

            do
            {
                foreach (var character in gameState.party.characters)
                {
                    int characterDamage = DiceHelper.Roll(character.weapon.damageRoll);

                    yield return character.presenter.Attack();
                    yield return gameState.combat.monster.ReactToDamage(characterDamage);

                    if (gameState.combat.monster.hitPoints <= 0)
                    {
                        Console.WriteLine($"{character} hits the {gameState.combat} with thier {character.weapon} for {characterDamage} damage. {gameState.combat} has 0 HP left.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{character} hits the {gameState.combat} for {characterDamage} damage. {gameState.combat} has {gameState.combat.monster.hitPoints} HP left.");
                    }
                }

                if (gameState.combat.monster.hitPoints <= 0)
                {
                    break;
                }

                int targetIndex = random.Next(gameState.party.characters.Count);

                Character targetCharacter = gameState.party.characters[targetIndex];

                //Get weapon and damage used for monster attack
                int weaponInUse = random.Next(0, gameState.combat.monster.weaponTypes.Length - 1);

                int monstersDamage = DiceHelper.Roll(gameState.combat.monster.weaponTypes[weaponInUse].damageRoll);

                yield return gameState.combat.monster.presenter.Attack();
                yield return targetCharacter.ReactToDamage(monstersDamage);

                if (targetCharacter.hitPoints <= 0)
                {
                    Console.WriteLine($"{targetCharacter} was attacked by {gameState.combat} using its {gameState.combat.monster.weaponTypes[weaponInUse].displayName}, dealing {monstersDamage} damage, killing {targetCharacter}!");
                    gameState.party.characters.Remove(targetCharacter);
                }
                else
                {
                    Console.WriteLine($"{targetCharacter} was attacked by {gameState.combat} using its {gameState.combat.monster.weaponTypes[weaponInUse].displayName} dealing {monstersDamage} damage.");
                }
            }
            while (gameState.party.characters.Count > 0);

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
