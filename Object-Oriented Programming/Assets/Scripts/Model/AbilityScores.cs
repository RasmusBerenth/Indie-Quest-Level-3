using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class AbilityScores
    {
        [field: SerializeField]
        private AbilityScore _strenght;
        public AbilityScore strenght => _strenght;

        [field: SerializeField]
        private AbilityScore _dexterity;
        public AbilityScore dexterity => _dexterity;

        [field: SerializeField]
        private AbilityScore _constitusion;
        public AbilityScore constitusion => _constitusion;

        [field: SerializeField]
        private AbilityScore _wisdom;
        public AbilityScore wisdom => _wisdom;

        [field: SerializeField]
        private AbilityScore _intelligence;
        public AbilityScore intelligence => _intelligence;

        [field: SerializeField]
        private AbilityScore _charisma;
        public AbilityScore charisma => _charisma;

        public AbilityScores(AbilityScore strenght, AbilityScore dexterity, AbilityScore constitusion, AbilityScore wisdom, AbilityScore intelligence, AbilityScore charisma)
        {
            _strenght = strenght;
            _dexterity = dexterity;
            _constitusion = constitusion;
            _wisdom = wisdom;
            _intelligence = intelligence;
            _charisma = charisma;
        }
    }
}
