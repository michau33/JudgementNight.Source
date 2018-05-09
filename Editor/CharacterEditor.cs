using Assets.Scripts.Characters;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(Enemy))]
    public class EnemyEditor : UnityEditor.Editor
    {

        void OnEnable()
        {

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(30f);
            GUILayout.Label("This class derrive from character base class.");
        }
    }

    [CustomEditor(typeof(Player))]
    public class PlayerEditor : UnityEditor.Editor
    {

        void OnEnable()
        {
        
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(30f);
            GUILayout.Label("This class derrive from character base class.");
        }
    }
}