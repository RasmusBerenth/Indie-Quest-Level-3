using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster
    {

        private int _savingThrow;

        public int savingThrow => _savingThrow;

        public Monster(string monsterName, int hitPoint, int savingThrow)
        {
            _displayName = monsterName;
            _hitPoints = hitPoint;
            _savingThrow = savingThrow;
        }

    }
}
