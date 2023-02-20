using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Character : Creature
    {
        private List<bool> _deathSavingThrowsList = new List<bool>();

        private WeaponType _weapon;
        private ArmorType _armor;

        public WeaponType weapon => _weapon;
        public ArmorType armor => _armor;

        public override IEnumerable<bool> deathSavingThrows => _deathSavingThrowsList;

        public Character(int hitPointsMaximum, string displayName, Sprite bodySprite, SizeCategory sizeCategory, WeaponType weaponType, ArmorType armorType) : base(displayName, bodySprite, sizeCategory)
        {
            _hitPointsMaximum = hitPointsMaximum;
            _displayName = displayName;
            _weapon = weaponType;
            _armor = armorType;
            InitializeHitPoint();
        }

        public override IEnumerator ReactToDamage(int damageAmount)
        {
            _hitPoints -= damageAmount;

            bool instantDeath = _hitPoints <= -_hitPointsMaximum;
            bool wasCriticalHit = false;

            int i = 1;

            if (wasCriticalHit == true)
            {
                i = 0;
            }

            if (_hitPoints < 0)
            {
                _hitPoints = 0;
            }

            yield return presenter.TakeDamage(instantDeath);
            if (instantDeath)
            {
                lifeStatus = LifeStatus.Dead;
                yield return presenter.Die();
            }
            else if (hitPoints == 0)
            {
                if (lifeStatus == LifeStatus.Conscious)
                {
                    lifeStatus = LifeStatus.UnconsciousUnstable;
                }
                else
                {
                    lifeStatus = LifeStatus.UnconsciousUnstable;

                    while (i < 2)
                    {
                        yield return AddDeathSavingThrow(false);
                        i++;
                    }

                    if (deathSavingThrowFailures >= 3)
                    {
                        lifeStatus = LifeStatus.Dead;
                        yield return presenter.Die();
                    }
                }
            }
        }

        private IEnumerator AddDeathSavingThrow(bool succes)
        {
            yield return presenter.PerformDeathSavingThrow(succes);

            if (succes)
            {
                _deathSavingThrowSucces++;
            }
            else
            {
                _deathSavingThrowFailures++;
            }

            _deathSavingThrowsList.Add(succes);
        }
    }
}
