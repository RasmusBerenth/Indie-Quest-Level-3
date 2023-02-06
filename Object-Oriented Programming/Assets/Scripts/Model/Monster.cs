using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster : Creature
    {
        private static bool[] _emptyDeathSavingThrows = new bool[0];
        private MonsterType _monsterType;
        public MonsterType monsterType => _monsterType;

        public Monster(MonsterType type) : base(type.displayName, type.bodySprite, type.sizeCategory)
        {
            _hitPointsMaximum = DiceHelper.Roll(type.hitPoint);
            _monsterType = type;
            _deathSavingThrows = _emptyDeathSavingThrows;
            InitializeHitPoint();
        }

    }
}
