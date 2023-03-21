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
        public int score
        {
            get => _score;

            set => _score = value;
        }

        public int modifier => Mathf.FloorToInt((_score - 10) / 2.0f);

        public AbilityScore(int value)
        {
            _score = value;
        }

        public static implicit operator int(AbilityScore abilityScore)
        {
            return abilityScore.score;
        }
    }


}
