using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MaterialSlotAttribute))]
public class MaterialSlotDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var component = property.serializedObject.targetObject as Component;
        var renerer = component != null ? component.GetComponent<Renderer>() : null;

        if (property.propertyType != SerializedPropertyType.Integer || renerer == null || renerer.sharedMaterials.Length == 0)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        var materials = renerer.sharedMaterials;
        var names = new string[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            var currentMaterial = materials[i];
            names[i] = $"{i}: {(currentMaterial ? currentMaterial.name : "None")}";
        }

        property.intValue = EditorGUI.Popup(position, label.text, Mathf.Clamp(property.intValue, 0, materials.Length - 1), names);
    }
}
