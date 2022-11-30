using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Character : Creature
    {
        public string DisplayName { get; private set; }

        public Character(int hitPointMaximum, string displayName, Sprite bodySprite, SizeCategory sizeCategory) : base(hitPointMaximum, displayName, bodySprite, sizeCategory)
        {
            this.DisplayName = displayName;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
