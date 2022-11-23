using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Charcter : MonoBehaviour
    {
        private string _displayName;

        public string displayName
        {
            get
            {
                return _displayName;
            }
        }

        public string Character(string displayName)
        {
            this._displayName = displayName;

            return _displayName;
        }
    }
}
