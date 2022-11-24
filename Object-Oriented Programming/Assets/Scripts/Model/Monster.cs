using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster
    {
        private string _displayName;
        private int _hitPoints;
        private int _savingThrow;

        public string displayName => _displayName;

        public int hitPoints
        {
            get => _hitPoints;
            set => _hitPoints = Mathf.Max(0, value);
        }

        public int savingThrow => _savingThrow;

        public Monster(string monsterName, int hitPoint, int savingThrow)
        {
            _displayName = monsterName;
            _hitPoints = hitPoint;
            _savingThrow = savingThrow;
        }

        public void ReactToDamage(int damageAmount)
        {
            _hitPoints = Mathf.Max(0, _hitPoints - damageAmount);
        }

    }
}
