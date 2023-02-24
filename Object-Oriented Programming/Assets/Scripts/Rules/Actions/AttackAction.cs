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

            if (target.lifeStatus == LifeStatus.Conscious)
            {
                int attackRoll = DiceHelper.Roll("d20");

                if (attackRoll == 1)
                {
                    attackRoll = 0;
                    Console.WriteLine($"{attacker} rolled a {attackRoll} and missed {target}!");
                }
                else if (attackRoll == 20)
                {
                    criticalHit = true;
                    attackRoll = target.armorClass;
                }
                else if (attackRoll >= target.armorClass)
                {
                    damageAmount = DiceHelper.Roll(weapon.damageRoll);

                    Console.WriteLine($"{attacker} rolled a {attackRoll} and hit {target}!");
                }
                else
                {
                    Console.WriteLine($"{attacker} rolled a {attackRoll} and missed {target}!");
                }
            }
            else
            {
                criticalHit = true;
            }

            if (criticalHit == true)
            {
                damageAmount += DiceHelper.Roll(weapon.damageRoll);
            }

            target.ReactToDamage(damageAmount, criticalHit);
        }
    }
}
