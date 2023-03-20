using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class AttackAction : IAction
    {
        private Creature _attacker;
        public Creature attacker => _attacker;

        private Creature _target;
        public Creature target => _target;

        private WeaponType _weapon;
        public WeaponType weapon => _weapon;

        public AttackAction(Creature attacker, Creature target, WeaponType weapon)
        {
            _attacker = attacker;
            _target = target;
            _weapon = weapon;
        }

        public IEnumerator Execute()
        {
            yield return attacker.presenter.FaceCreature(target);
            yield return attacker.presenter.Attack();

            bool criticalHit = false;
            int damageAmount = 0;

            int attackBonus = weapon.isRanged ? attacker.abilityScores.dexterity.modifier : attacker.abilityScores.strenght.modifier;

            if (weapon.isFinesse)
            {
                if (attacker.abilityScores.strenght.modifier > attackBonus)
                {
                    attackBonus = attacker.abilityScores.strenght.modifier;
                }
                else
                {
                    attackBonus -= attacker.abilityScores.dexterity.modifier;
                }
            }

            if (target.lifeStatus == LifeStatus.Conscious)
            {
                int attackRoll = DiceHelper.Roll("d20");

                if (attackRoll == 1)
                {
                    Console.WriteLine($"{attacker} rolled a 1 and missed {target}!");
                    yield break;
                }
                else if (attackRoll == 20)
                {
                    criticalHit = true;
                    Console.Write($"{attacker} rolled a 20 and landed a critical hit on {target}!");
                }
                else if (attackRoll + attackBonus >= target.armorClass)
                {
                    Console.Write($"{attacker} rolled a {attackRoll} + {attackBonus} and hit {target}!");
                }
                else
                {
                    Console.WriteLine($"{attacker} rolled a {attackRoll} + {attackBonus} and missed {target}!");
                    yield break;
                }

            }
            else
            {
                criticalHit = true;
            }

            int amountOfDamageRolls = criticalHit ? 2 : 1;
            for (int i = 0; i < amountOfDamageRolls; i++)
            {
                damageAmount = DiceHelper.Roll(weapon.damageRoll);
            }
            Console.WriteLine($" Dealing {damageAmount} points of damage using their {weapon}.");

            yield return target.ReactToDamage(damageAmount, criticalHit);
        }
    }
}
