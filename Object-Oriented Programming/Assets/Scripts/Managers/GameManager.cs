using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class GameManager : MonoBehaviour
    {
        private CombatManager combatManager;
        private GameState gameState;


        private void Awake()
        {
            Transform combatTransform = transform.Find("Combat");
            combatManager = combatTransform.GetComponent<CombatManager>();
        }

        public void Start()
        {
            NewGame();
            Simulate();
        }

        private void NewGame()
        {

            Character ken = new Character("Ken");
            Character barbie = new Character("Barbie");
            Character roland = new Character("Roland");
            Character melissa = new Character("Melissa");
            List<Character> heros = new List<Character>();
            heros.Add(ken);
            heros.Add(barbie);
            heros.Add(roland);
            heros.Add(melissa);

            Party heroParty = new Party(heros);

            gameState = new GameState(heroParty);

        }

        private void Simulate()
        {
            Monster orc = new Monster("orc", DiceHelper.Roll("2d8+6"), 12);
            Monster mage = new Monster("mage", DiceHelper.Roll("9d8"), 20);
            Monster troll = new Monster("troll", DiceHelper.Roll("8d10+40"), 18);

            Console.WriteLine($"A party of warriors {gameState.party} descends into the dungeon.");

            //random HP orc (2d8+6) mage (9d8) and troll (8d10+40)
            gameState.EnterCombatWithMonster(orc);
            CombatManager.Simulate(gameState);

            if (gameState.party.characters.Count > 0)
            {
                gameState.EnterCombatWithMonster(mage);
                CombatManager.Simulate(gameState);
            }

            if (gameState.party.characters.Count > 0)
            {
                gameState.EnterCombatWithMonster(troll);
                CombatManager.Simulate(gameState);
            }

            if (gameState.party.characters.Count > 0)
            {
                Console.WriteLine($"{gameState.party} survived the doungeon.");
            }
        }
    }
}
