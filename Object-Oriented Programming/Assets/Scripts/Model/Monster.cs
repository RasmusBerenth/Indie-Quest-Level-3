using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster : Creature
    {
        private static readonly bool[] _emptyDeathSavingThrows = new bool[0];
        private MonsterType _monsterType;
        public MonsterType monsterType => _monsterType;

        public override IEnumerable<bool> deathSavingThrows => _emptyDeathSavingThrows;

        public override int armorClass => monsterType.armorClass;

        public override AbilityScores abilityScores { get; }

        public Monster(MonsterType type) : base(type.displayName, type.bodySprite, type.sizeCategory)
        {
            _hitPointsMaximum = DiceHelper.Roll(type.hitPoint);
            _monsterType = type;
            abilityScores = type.abilityScores;
            InitializeHitPoint();
        }

        public override IAction TakeTurn(GameState gameState)
        {
            Character targetCharacter = null;

            if (this.abilityScores.intelligence > 7)
            {
                foreach (Character character in gameState.party.aliveCharacters)
                {
                    if (targetCharacter == null || character.hitPoints < targetCharacter.hitPoints)
                    {
                        targetCharacter = character;
                    }
                }
            }
            else
            {
                int targetIndex = Random.Range(0, gameState.party.aliveCount);
                targetCharacter = gameState.party.aliveCharacters.Skip(targetIndex).First();
            }


            //Get weapon and damage used for monster attack
            int weaponInUseIndex = Random.Range(0, _monsterType.weaponTypes.Length);
            WeaponType weaponInUse = _monsterType.weaponTypes[weaponInUseIndex];

            return new AttackAction(this, targetCharacter, weaponInUse);
        }
    }
}
