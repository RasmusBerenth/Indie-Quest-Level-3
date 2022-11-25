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
            CombatManager combatManager = combatTransform.GetComponent<CombatManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            var names = new List<string>();
            names.Add("Ken");
            names.Add("Barby");
            names.Add("Roland");
            names.Add("Melissa");

            Console.WriteLine($"A party of warriors {StringHelper.JoinWithAnd(names)} descends into the dungeon.");

            //random HP orc (2d8+6) mage (9d8) and troll (8d10+40)
            CombatManager.Simulate(gameState); //names, "orc", DiceHelper.Roll("2d8+6"), 12

            if (names.Count > 0)
            {
                CombatManager.Simulate(gameState); //names, "mage", DiceHelper.Roll("9d8"), 20

            }

            if (names.Count > 0)
            {
                CombatManager.Simulate(gameState); //names, "troll", DiceHelper.Roll("8d10+40"), 18
            }

            if (names.Count > 0)
            {
                Console.WriteLine($"{StringHelper.JoinWithAnd(names)} survived the doungeon.");
            }
        }

        private void NewGame()
        {
            List<Character> heros = new List<Character>();
            Party heroParty = new Party(heros);

            Character ken = new Character("Ken");
            Character barbie = new Character("Barbie");
            Character roland = new Character("Roland");
            Character melissa = new Character("Melissa");
            heros.Add(ken);
            heros.Add(barbie);
            heros.Add(roland);
            heros.Add(melissa);

            gameState = new GameState(heroParty);
        }

        private void Simulate()
        {

        }
    }
}
