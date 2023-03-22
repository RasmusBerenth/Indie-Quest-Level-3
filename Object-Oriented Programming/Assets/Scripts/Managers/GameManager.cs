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
            //Random.Range(0, characterWeapons.Length)

            Character ken = new Character(10, "Ken", characterSprites[5], SizeCategory.Medium, characterWeapons[1], characterArmor);
            Character barbie = new Character(10, "Barbie", characterSprites[1], SizeCategory.Medium, characterWeapons[3], characterArmor);
            Character roland = new Character(10, "Roland", characterSprites[2], SizeCategory.Medium, characterWeapons[4], characterArmor);
            Character melissa = new Character(10, "Melissa", characterSprites[3], SizeCategory.Medium, characterWeapons[5], characterArmor);
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

            Console.WriteLine($"A party of warriors {gameState.party} descends into the dungeon.");

            foreach (MonsterType monsterType in monsterTypes)
            {
                if (gameState.party.aliveCount == 0)
                {
                    break;
                }

                Monster monster = new Monster(monsterType);
                gameState.EnterCombatWithMonster(monster);
                yield return combatPresenter.InitializeMonster(gameState);
                yield return combatManager.Simulate(gameState);
            }

            if (gameState.party.aliveCount > 0)
            {
                Console.WriteLine($"{gameState.party} survived the doungeon.");
            }
        }
    }
}
