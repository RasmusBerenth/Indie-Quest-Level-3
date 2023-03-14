using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    [Serializable]
    public class AbilityScore
    {
        public int _score;
        public int score => _score;

        private int _modifier;
        public int modifier
        {
            get => _modifier;
            private set
            {


                _modifier = score; //Per 2 score +1 modifer, score 1 = -5. Score goes from 1-30 (-5 to +10)
            }
        }
    }
}
