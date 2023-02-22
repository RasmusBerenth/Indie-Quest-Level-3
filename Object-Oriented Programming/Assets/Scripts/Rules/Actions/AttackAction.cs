using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class AttackAction //: IAction
    {
        private Creature _attackingCreature;
        public Creature attackingCreature => _attackingCreature;

        private Creature _targetCreature;
        public Creature targetCreature => _targetCreature;

        private WeaponType _usedWeapon;
        public WeaponType usedWeapon => _usedWeapon;

        public AttackAction(Creature attacker, Creature target, WeaponType weapon)
        {
            _attackingCreature = attacker;
            _targetCreature = target;
            _usedWeapon = weapon;
        }

        //public IEnumerator Execute()
        //{
        //    //yield return FaceDiraction();
        //    //yield return Attack();

        //    if (_targetCreature.lifeStatus == LifeStatus.Conscious)
        //    {
        //        //Preform attack roll
        //        /*if(attackRoll >= target armor class)
        //        {
        //            if(attackRoll == 20)
        //            {
        //            bool critical hit = true
        //            }
        //            else if(attackRoll == 1)
        //            {
        //            garantied miss
        //            Console: attacker missed the target
        //            }

        //        hit target with weapon, twice if critical hit = true
        //        Console: hit message, damage amount
        //        ReactToDamage(int damage amount,bool critical hit)
        //        }
        //        else
        //        {
        //        Console: attacker missed the target
        //        }
        //        */
        //    }
        //    else
        //    {
        //        //Auto Critical hit
        //    }
        //}
    }
}
