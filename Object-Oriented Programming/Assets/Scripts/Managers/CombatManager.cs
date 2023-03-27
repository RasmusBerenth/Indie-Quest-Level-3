using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace MonsterQuest
{
    public class CombatManager : MonoBehaviour
    {

        public IEnumerator Simulate(GameState gameState)
        {
            List<Creature> initativeList = new List<Creature>(gameState.party.characters);

            Monster monster = gameState.combat.monster;

            initativeList.Add(monster);

            Console.WriteLine($"A {monster} with {monster.hitPoints}HP appears");

            initativeList.Shuffle();
            bool combatEnded = false;

            do
            {
                foreach (Creature creature in initativeList)
                {
                    if (creature.lifeStatus == LifeStatus.Dead)
                    {
                        continue;
                    }

                    IAction action = creature.TakeTurn(gameState);
                    yield return action.Execute();

                    combatEnded = monster.lifeStatus == LifeStatus.Dead || gameState.party.aliveCount == 0;

                    if (combatEnded)
                    {
                        break;
                    }
                }

            }
            while (!combatEnded);

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
