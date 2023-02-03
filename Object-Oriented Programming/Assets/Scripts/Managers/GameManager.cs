using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public class GameManager : MonoBehaviour
    {
        private CombatManager combatManager;
        private GameState gameState;
        private CombatPresenter combatPresenter;
        [SerializeField] private MonsterType[] monsterTypes;
        [SerializeField] private Sprite[] characterSprites;

        private void Awake()
        {
            Transform combatTransform = transform.Find("Combat");
            combatManager = combatTransform.GetComponent<CombatManager>();
            combatPresenter = combatTransform.GetComponent<CombatPresenter>();
        }

        public IEnumerator Start()
        {
            yield return Database.Initialize();
            NewGame();
            yield return Simulate();
        }

        private void NewGame()
        {
            WeaponType[] characterWeapons = Database.itemTypes.Where(itemType => itemType is WeaponType { weight: > 0 }).Cast<WeaponType>().ToArray();
            ArmorType characterArmor = Database.GetItemType<ArmorType>("Studded Leather");

            Character ken = new Character(10, "Ken", characterSprites[0], SizeCategory.Medium, characterWeapons[Random.Range(0, characterWeapons.Length)], characterArmor);
            Character barbie = new Character(10, "Barbie", characterSprites[1], SizeCategory.Medium, characterWeapons[Random.Range(0, characterWeapons.Length)], characterArmor);
            Character roland = new Character(10, "Roland", characterSprites[2], SizeCategory.Medium, characterWeapons[Random.Range(0, characterWeapons.Length)], characterArmor);
            Character melissa = new Character(10, "Melissa", characterSprites[3], SizeCategory.Medium, characterWeapons[Random.Range(0, characterWeapons.Length)], characterArmor);
            List<Character> heros = new List<Character>();
            heros.Add(ken);
            heros.Add(barbie);
            heros.Add(roland);
            heros.Add(melissa);

            Party heroParty = new Party(heros);

            gameState = new GameState(heroParty);
        }

        private IEnumerator Simulate()
        {
            yield return combatPresenter.InitializeParty(gameState);

            Monster kobold = new Monster(monsterTypes[0]);
            Monster magmin = new Monster(monsterTypes[1]);
            Monster azer = new Monster(monsterTypes[2]);
            Monster troll = new Monster(monsterTypes[3]);
            Monster roc = new Monster(monsterTypes[4]);

            Console.WriteLine($"A party of warriors {gameState.party} descends into the dungeon.");

            //random HP orc (2d8+6) mage (9d8) and troll (8d10+40)
            gameState.EnterCombatWithMonster(kobold);
            yield return combatPresenter.InitializeMonster(gameState);
            yield return combatManager.Simulate(gameState);

            if (gameState.party.characters.Count > 0)
            {
                gameState.EnterCombatWithMonster(magmin);
                combatPresenter.InitializeMonster(gameState);
                yield return combatManager.Simulate(gameState);
            }

            if (gameState.party.characters.Count > 0)
            {
                gameState.EnterCombatWithMonster(azer);
                combatPresenter.InitializeMonster(gameState);
                yield return combatManager.Simulate(gameState);
            }

            if (gameState.party.characters.Count > 0)
            {
                gameState.EnterCombatWithMonster(troll);
                combatPresenter.InitializeMonster(gameState);
                yield return combatManager.Simulate(gameState);
            }

            if (gameState.party.characters.Count > 0)
            {
                gameState.EnterCombatWithMonster(roc);
                combatPresenter.InitializeMonster(gameState);
                yield return combatManager.Simulate(gameState);
            }

            if (gameState.party.characters.Count > 0)
            {
                Console.WriteLine($"{gameState.party} survived the doungeon.");
            }
        }
    }
}
