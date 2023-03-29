using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MonsterQuest
{
    [CustomPropertyDrawer(typeof(AbilityScores))]
    public class AbilityScoresPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            MonsterType monsterType = property.serializedObject.targetObject as MonsterType;

            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/AbilityScores_UXML.uxml");
            visualTree.CloneTree(container);

            //Strenght
            Action<SerializedProperty> uppdateStrenght = (SerializedProperty _) =>
            {
                container.Q<Label>("StrenghtModifier").text = $"({monsterType.abilityScores.strenght.modifier:+#;-#;+0})";
            };

            uppdateStrenght(null);
            container.Q("Strenght").TrackPropertyValue(property.FindPropertyRelative("_strenght._score"), uppdateStrenght);

            //Dexterity
            Action<SerializedProperty> uppdateDexterity = (SerializedProperty _) =>
            {
                container.Q<Label>("DexterityModifer").text = $"({monsterType.abilityScores.dexterity.modifier:+#;-#;+0})";
            };

            uppdateDexterity(null);
            container.Q("Dexterity").TrackPropertyValue(property.FindPropertyRelative("_dexterity._score"), uppdateDexterity);

            //Constitusion
            Action<SerializedProperty> uppdateConstitusion = (SerializedProperty _) =>
            {
                container.Q<Label>("ConstitusionModifier").text = $"({monsterType.abilityScores.constitusion.modifier:+#;-#;+0})";
            };

            uppdateConstitusion(null);
            container.Q("Constitusion").TrackPropertyValue(property.FindPropertyRelative("_constitusion._score"), uppdateConstitusion);

            //Wisdom
            Action<SerializedProperty> uppdateWisdom = (SerializedProperty _) =>
            {
                container.Q<Label>("WisdomModifier").text = $"({monsterType.abilityScores.wisdom.modifier:+#;-#;+0})";
            };

            uppdateWisdom(null);
            container.Q("Wisdom").TrackPropertyValue(property.FindPropertyRelative("_wisdom._score"), uppdateWisdom);

            //Intelligence
            Action<SerializedProperty> uppdateIntelligence = (SerializedProperty _) =>
            {
                container.Q<Label>("IntelligenceModifier").text = $"({monsterType.abilityScores.intelligence.modifier:+#;-#;+0})";
            };

            uppdateIntelligence(null);
            container.Q("Intelligence").TrackPropertyValue(property.FindPropertyRelative("_intelligence._score"), uppdateIntelligence);

            //Charisma
            Action<SerializedProperty> uppdateCharisma = (SerializedProperty _) =>
            {
                container.Q<Label>("CharismaModifier").text = $"({monsterType.abilityScores.charisma.modifier:+#;-#;+0})";
            };

            uppdateCharisma(null);
            container.Q("Charisma").TrackPropertyValue(property.FindPropertyRelative("_charisma._score"), uppdateCharisma);

            return container;
        }
    }
}