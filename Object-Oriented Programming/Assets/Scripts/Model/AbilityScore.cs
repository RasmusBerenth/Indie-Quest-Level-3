using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class AbilityScore
    {
        public int score { get; set; }

        private int _modifier;
        public int modifier
        {
            get => _modifier;
            private set
            {
                _modifier = value; //Per 2 score +1 modifer, score 1 = -5. Score goes from 1-30 (-5, +10)
            }
        }
    }
}
