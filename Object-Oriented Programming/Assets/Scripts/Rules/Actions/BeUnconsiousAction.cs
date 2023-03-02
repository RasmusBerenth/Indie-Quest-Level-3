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
        }
    }
}
