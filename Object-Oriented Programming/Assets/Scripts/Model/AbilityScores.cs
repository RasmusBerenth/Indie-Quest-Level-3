using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    [Serializable]
    public class AbilityScores
    {
        [SerializeField]
        private AbilityScore _strenght;
        public AbilityScore strenght => _strenght;

        [SerializeField]
        private AbilityScore _dexterity;
        public AbilityScore dexterity => _dexterity;

        [SerializeField]
        private AbilityScore _constitusion;
        public AbilityScore constitusion => _constitusion;

        [SerializeField]
        private AbilityScore _wisdom;
        public AbilityScore wisdom => _wisdom;

        [SerializeField]
        private AbilityScore _intelligence;
        public AbilityScore intelligence => _intelligence;

        [SerializeField]
        private AbilityScore _charisma;
        public AbilityScore charisma => _charisma;

        public AbilityScores(AbilityScore strenght, AbilityScore dexterity, AbilityScore constitusion, AbilityScore intelligence, AbilityScore wisdom, AbilityScore charisma)
        {
            _strenght = strenght;
            _dexterity = dexterity;
            _constitusion = constitusion;
            _intelligence = intelligence;
            _wisdom = wisdom;
            _charisma = charisma;
        }
    }
}
