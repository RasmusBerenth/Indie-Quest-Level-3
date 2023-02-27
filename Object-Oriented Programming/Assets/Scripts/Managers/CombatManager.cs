using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace MonsterQuest
{
    public class CombatManager : MonoBehaviour
    {
        public List<Creature> initativeList = new List<Creature>();

        public IEnumerator Simulate(GameState gameState)
        {
            IAction action;
            var random = new Random();
            Monster monster = gameState.combat.monster;

            foreach (Character character in gameState.party.characters)
            {
                initativeList.Add(character);
            }
            initativeList.Add(monster);

            Console.WriteLine($"A {monster} with {monster.hitPoints}HP appears");

            ListHelper.Shuffle(initativeList);

            do
            {
                foreach (var character in gameState.party.characters)
                {
                    if (character.lifeStatus != LifeStatus.Conscious)
                    {
                        continue;
                    }

                    action = character.TakeTurn(gameState);
                    yield return action.Execute();

                    if (monster.lifeStatus == LifeStatus.Dead)
                    {
                        break;
                    }
                }

                if (monster.lifeStatus == LifeStatus.Dead)
                {
                    break;
                }

                action = monster.TakeTurn(gameState);
                yield return action.Execute();
            }
            while (gameState.party.aliveCount > 0);

            if (monster.lifeStatus == LifeStatus.Conscious)
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
