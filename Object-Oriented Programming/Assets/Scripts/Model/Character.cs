using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Character : Creature
    {
        public string DisplayName { get; private set; }

        public Character(int hitPointsMaximum, string displayName, Sprite bodySprite, SizeCategory sizeCategory) : base(hitPointsMaximum, displayName, bodySprite, sizeCategory)
        {
            this.DisplayName = displayName;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
