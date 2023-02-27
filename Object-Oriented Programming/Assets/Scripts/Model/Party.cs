using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public class Party
    {
        public IEnumerable<Character> aliveCharacters => characters.Where(character => character.lifeStatus != LifeStatus.Dead);
        public int aliveCount => aliveCharacters.Count();
        public List<Character> characters { get; private set; }

        public Party(IEnumerable<Character> initialCharacters)
        {
            characters = new List<Character>(initialCharacters);
        }

        public override string ToString()
        {
            return StringHelper.JoinWithAnd(characters.Select(character => character.displayName));
        }


    }
}
