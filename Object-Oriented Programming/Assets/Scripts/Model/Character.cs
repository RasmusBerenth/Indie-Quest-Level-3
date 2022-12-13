using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Character : Creature
    {
        private WeaponType _weapon;
        private ArmorType _armor;

        public WeaponType weapon => _weapon;
        public ArmorType armor => _armor;

        public Character(int hitPointsMaximum, string displayName, Sprite bodySprite, SizeCategory sizeCategory, WeaponType weaponType, ArmorType armorType) : base(displayName, bodySprite, sizeCategory)
        {
            _hitPointsMaximum = hitPointsMaximum;
            _displayName = displayName;
            _weapon = weaponType;
            _armor = armorType;
            InitializeHitPoint();
        }

        public override string ToString()
        {
            return _displayName;
        }
    }
}
