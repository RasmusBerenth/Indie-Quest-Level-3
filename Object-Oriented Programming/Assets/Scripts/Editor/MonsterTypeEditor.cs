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
            DropdownField monsterDropDownField = new DropdownField();
            Label label = new Label();

            label.text = "Imported monster";
            inspector.Add(label);
            monsterDropDownField.choices.AddRange(MonsterTypeImporter.monsterIndexNames);
            inspector.Add(monsterDropDownField);

            //Deafult inspector
            InspectorElement.FillDefaultInspector(inspector, serializedObject, this);

            return inspector;
        }
    }
}
