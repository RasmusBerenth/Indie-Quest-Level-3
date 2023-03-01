using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class BeUnconsiousAction : IAction
    {
        private Character _unconsiousCharacter;
        public Character unconsiousCharacter => _unconsiousCharacter;

        public BeUnconsiousAction(Character character)
        {
            _unconsiousCharacter = character;
        }

        public IEnumerator Execute()
        {
            yield return unconsiousCharacter.HandleUnconsiousState();

            //int deathSavingThrow = DiceHelper.Roll("d20");
            //if (deathSavingThrow == 20)
            //{
            //    yield return unconsiousCharacter.Heal(1);
            //}
            //else if (deathSavingThrow >= 10)
            //{
            //    //Succed death savingthrow
            //    unconsiousCharacter.HandleUnconsiousState();
            //}
            //else
            //{
            //    //Fail death savingthrow
            //    unconsiousCharacter.HandleUnconsiousState();
            //}
        }
    }
}
