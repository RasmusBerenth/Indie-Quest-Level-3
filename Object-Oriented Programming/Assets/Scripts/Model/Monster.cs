using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster : Creature
    {
        private int _savingThrow;

        public int savingThrow => _savingThrow;

        public Monster(string monsterName, int hitPoint, int savingThrow, Sprite bodySprite, SizeCategory sizeCategory) : base(hitPoint, monsterName, bodySprite, sizeCategory)
        {
            _savingThrow = savingThrow;
        }

    }
}
