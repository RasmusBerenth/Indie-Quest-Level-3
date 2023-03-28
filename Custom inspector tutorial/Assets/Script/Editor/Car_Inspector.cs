using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(Car))]
public class Car_Inspector : Editor
{
    public VisualTreeAsset m_InspectorXML;

    public override VisualElement CreateInspectorGUI()
    {
        //Create a new VisualElements to the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        //Load a clone from a visual tree from UXML
        m_InspectorXML.CloneTree(myInspector);

        //Get reference to the deafult inspector foldout control
        VisualElement inspectorFoldout = myInspector.Q("Deafult_Inspector");

        //Attach a defult inspector to the fallout
        InspectorElement.FillDefaultInspector(inspectorFoldout, serializedObject, this);

        // return the finished inspector UI
        return myInspector;
    }
}
