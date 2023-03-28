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
        public VisualTreeAsset m_InspectorXML;

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = new VisualElement();

            m_InspectorXML.CloneTree(inspector);

            //Deafult inspector
            VisualElement inspectorFoldout = inspector.Q("Deafult_Inspector");
            InspectorElement.FillDefaultInspector(inspectorFoldout, serializedObject, this);

            return inspector;
        }
    }
}
