using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Character : Creature
    {
        public Character(int hitPointsMaximum, string displayName, Sprite bodySprite, SizeCategory sizeCategory, WeaponType weaponType, ArmorType armorType) : base(displayName, bodySprite, sizeCategory)
        {
            _hitPointsMaximum = hitPointsMaximum;
            _displayName = displayName;
            InitializeHitPoint();
        }

        public override string ToString()
        {
            return _displayName;
        }
    }
}
