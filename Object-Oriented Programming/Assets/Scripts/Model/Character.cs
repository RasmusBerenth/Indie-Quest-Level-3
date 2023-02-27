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

        public override int armorClass => armor.armorClass;

        public Character(int hitPointsMaximum, string displayName, Sprite bodySprite, SizeCategory sizeCategory, WeaponType weaponType, ArmorType armorType) : base(displayName, bodySprite, sizeCategory)
        {
            _hitPointsMaximum = hitPointsMaximum;
            _displayName = displayName;
            _weapon = weaponType;
            _armor = armorType;
            InitializeHitPoint();
        }

        public override IEnumerator ReactToDamage(int damageAmount, bool wasCriticalHit)
        {
            _hitPoints -= Mathf.Max(0, _hitPoints - damageAmount);


            bool instantDeath = _hitPoints <= -_hitPointsMaximum;

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
                    int amountOfFails = wasCriticalHit ? 2 : 1;
                    for (int i = 0; i < amountOfFails; i++)
                    {
                        yield return AddDeathSavingThrow(false);
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

        public override IAction TakeTurn(GameState gameState)
        {
            return new AttackAction(this, gameState.combat.monster, _weapon);
        }
    }
}
