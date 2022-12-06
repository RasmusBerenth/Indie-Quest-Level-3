using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    [CreateAssetMenu]
    public class ArmorType : ScriptableObject
    {
        public string displayName;
        public ArmorCategory category;
        public int weight;
    }
}
