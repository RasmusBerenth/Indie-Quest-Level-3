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
            Monster monster = gameState.combat.monster;

            Console.WriteLine($"A {monster} with {monster.hitPoints}HP appears");

            do
            {
                foreach (var character in gameState.party.characters)
                {
                    int characterDamage = DiceHelper.Roll(character.weapon.damageRoll);

                    yield return character.presenter.Attack();
                    yield return monster.ReactToDamage(characterDamage);

                    if (monster.hitPoints <= 0) //Add: && character.lifeStatus == LifeStatus.Conscious
                    {
                        Console.WriteLine($"{character} hits the {monster} with thier {character.weapon.displayName} for {characterDamage} damage. {monster} has 0 HP left.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{character} hits the {monster} with thier {character.weapon.displayName} for {characterDamage} damage. {monster} has {monster.hitPoints} HP left.");
                    }
                }

                if (monster.hitPoints <= 0)
                {
                    break;
                }

                int targetIndex = random.Next(gameState.party.characters.Count);

                Character targetCharacter = gameState.party.characters[targetIndex];

                //Get weapon and damage used for monster attack
                int weaponInUseIndex = random.Next(monster.monsterType.weaponTypes.Length);
                WeaponType weaponInUse = monster.monsterType.weaponTypes[weaponInUseIndex];

                int monstersDamage = DiceHelper.Roll(weaponInUse.damageRoll);

                yield return monster.presenter.Attack();
                yield return targetCharacter.ReactToDamage(monstersDamage);

                if (targetCharacter.hitPoints <= 0) //(or targetCharacter.lifeStatus == 0 or LifeStatus.Conscious)
                {
                    Console.WriteLine($"{targetCharacter} was attacked by {monster} using its {weaponInUse}, dealing {monstersDamage} damage, killing {targetCharacter}!");
                    gameState.party.characters.Remove(targetCharacter);
                }
                else
                {
                    Console.WriteLine($"{targetCharacter} was attacked by {monster} using its {weaponInUse} dealing {monstersDamage} damage.");
                }
            }
            while (gameState.party.characters.Count > 0);

            if (monster.hitPoints > 0)
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
