using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster : Creature
    {
        private WeaponType[] _weaponTypes;
        public WeaponType[] weaponTypes => _weaponTypes;

        public Monster(MonsterType type) : base(type.displayName, type.bodySprite, type.sizeCategory)
        {
            _hitPointsMaximum = DiceHelper.Roll(type.hitPoint);
            _weaponTypes = type.weaponTypes;
            InitializeHitPoint();
        }

    }
}
