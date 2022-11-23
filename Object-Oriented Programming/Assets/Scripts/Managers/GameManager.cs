using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace MonsterQuest
{
    public class GameManager : MonoBehaviour
    {
        private BattleManager battleManager;

        private void Awake()
        {
            Transform combatTransform = transform.Find("Combat");
            BattleManager battleManager = combatTransform.GetComponent<BattleManager>();
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
            BattleManager.Simulate(names, "orc", DiceHelper.Roll("2d8+6"), 12);

            if (names.Count > 0)
            {
                BattleManager.Simulate(names, "mage", DiceHelper.Roll("9d8"), 20);

            }

            if (names.Count > 0)
            {
                BattleManager.Simulate(names, "troll", DiceHelper.Roll("8d10+40"), 18);
            }

            if (names.Count > 0)
            {
                Console.WriteLine($"{StringHelper.JoinWithAnd(names)} survived the doungeon.");
            }
        }




    }
}
