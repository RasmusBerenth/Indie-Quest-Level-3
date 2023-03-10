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
            _hitPoints -= damageAmount;

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

            Debug.Log($"{this} has {_hitPoints} hit points.");
        }

        public IEnumerator HandleUnconsiousState()
        {
            bool deathSavingThrowResult;
            bool rolled1 = false;
            int deathSavingThrow = DiceHelper.Roll("d20");


            Console.Write($"{this} rolls a death savingthrow ");

            if (deathSavingThrow == 20)
            {
                Console.WriteLine($"and rolls a 20 {this} comes back to 1 HP!");
                lifeStatus = LifeStatus.Conscious;
                ResetDeathSavingTrows();
                yield return Heal(1);
                yield return presenter.RegainConsciousness();
            }
            else
            {
                if (deathSavingThrow == 1)
                {
                    rolled1 = true;
                    Console.WriteLine("and rolls a 1 failing 2 death savingthrows.");
                }

                if (deathSavingThrow >= 10)
                {
                    deathSavingThrowResult = true;
                    Console.WriteLine($"and rolls a {deathSavingThrow} succed 1 death savingthrows.");
                }
                else
                {
                    deathSavingThrowResult = false;
                    Console.WriteLine($"and rolls a {deathSavingThrow} failing 1 death savingthrows.");
                }

                int amountOfFails = rolled1 ? 2 : 1;
                for (int i = 0; i < amountOfFails; i++)
                {
                    yield return AddDeathSavingThrow(deathSavingThrowResult, i == 0 ? deathSavingThrow : null);
                }
            }

            if (this.deathSavingThrowSucces >= 3)
            {
                lifeStatus = LifeStatus.Conscious;
                ResetDeathSavingTrows();
                lifeStatus = LifeStatus.UnconsciousStable;
                Console.WriteLine($"{this} has stabilized.");
            }

            if (this.deathSavingThrowFailures >= 3)
            {
                lifeStatus = LifeStatus.Dead;
                yield return presenter.Die();
                Console.WriteLine($"{this} has died.");
            }
        }

        private IEnumerator AddDeathSavingThrow(bool succes, int? deathSaveResult = null)
        {
            yield return presenter.PerformDeathSavingThrow(succes, deathSaveResult);

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

        private void ResetDeathSavingTrows()
        {
            _deathSavingThrowSucces = 0;
            _deathSavingThrowFailures = 0;
            _deathSavingThrowsList.Clear();
            presenter.ResetDeathSavingThrows();
        }

        public override IAction TakeTurn(GameState gameState)
        {
            if (lifeStatus != LifeStatus.Conscious)
            {
                return new BeUnconsiousAction(this);
            }

            return new AttackAction(this, gameState.combat.monster, _weapon);
        }

    }
}
