using Assets.Data.Views;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SolarSystemView))]
public class SolarSystemViewScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SolarSystemView view = (SolarSystemView)target;

        //view.zoomLevels = EditorGUILayout.IntField("Zoom level", view.zoomLevels);
        view.zoomLevels = EditorGUILayout.IntSlider("Zoom level", view.zoomLevels, 1, 1000);

        EditorGUILayout.Separator();

        view.UseDebugSprites = EditorGUILayout.Toggle("UseDebugSprites", view.UseDebugSprites);

        if (GUILayout.Button("Redraw all Sprites"))
        {
            view.ShowSolarSystemZero();
        }

    }
}
