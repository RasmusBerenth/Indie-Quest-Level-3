using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster : Creature
    {
        private int _savingThrow;

        public int savingThrow => _savingThrow;

        public Monster(MonsterType type, int savingThrow) : base(type.displayName, type.bodySprite, type.sizeCategory)
        {
            _hitPointsMaximum = DiceHelper.Roll(type.hitPoint);
            _savingThrow = savingThrow;
            InitializeHitPoint();
        }

    }
}
