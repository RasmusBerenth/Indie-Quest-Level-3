using System.Collections;
using System.Collections.Generic;
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

        public Monster(MonsterType type) : base(type.displayName, type.bodySprite, type.sizeCategory)
        {
            _hitPointsMaximum = DiceHelper.Roll(type.hitPoint);
            _monsterType = type;
            InitializeHitPoint();
        }

        public override IAction TakeTurn(GameState gameState)
        {
            return base.TakeTurn(gameState);
        }
    }
}
