using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Character : Creature
    {
        private static List<bool> _deathSavingThrowsList = new List<bool>();

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
            _deathSavingThrows = _deathSavingThrowsList;
            InitializeHitPoint();
        }

        public override IEnumerator ReactToDamage(int damageAmount)
        {
            _hitPoints = Mathf.Max(0, _hitPoints - damageAmount);
            bool instantDeath;

            if (_hitPoints <= 0 - _hitPointsMaximum)
            {
                instantDeath = true;
                _lifeStatus = LifeStatus.Dead;
            }
            else
            {
                instantDeath = false;
            }

            yield return presenter.TakeDamage(instantDeath);

            if (hitPoints <= 0)
            {
                _lifeStatus = LifeStatus.UnconsciousUnstable;
                //_deathSavingThrowsList.Add(presenter.PerformDeathSavingThrow(/*bool, int?*/));


                /* if(_deathSavingThrowsList contains 3 true)
                 * {
                 * life status = unconsicous stable
                 * }
                 
                if(_deathSavingThrowsList contains 3 false)
                {
                life status = dead
                }
                 */
                //yield return presenter.Die();
            }
        }


    }
}
