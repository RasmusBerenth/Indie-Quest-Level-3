using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MonsterQuest
{
    [CustomEditor(typeof(MonsterType))]
    public class MonsterTypeEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = new VisualElement();

            //Deafult inspector
            InspectorElement.FillDefaultInspector(inspector, serializedObject, this);

            return inspector;
        }
    }
}
