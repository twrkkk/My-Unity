using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ScriptableObjectExample
{
    [CustomEditor(typeof(TimeLineManager))]
    public class TimeLineManagerInspector : Editor
    {
        private SerializedProperty playAniClip;

        private SerializedObject assetSo;
        private void OnEnable()
        {
            playAniClip = serializedObject.FindProperty("playAniClip");
            SetAssetSo();
        }

        private void SetAssetSo()
        {
            if (playAniClip.objectReferenceValue != null)
            {
                assetSo = new SerializedObject(playAniClip.objectReferenceValue, serializedObject.targetObject);
                Debug.Log("Create new SerializedObject");
            }
            else
            {
                assetSo = null;
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(playAniClip, new GUIContent("Target Asset"));

            if (EditorGUI.EndChangeCheck())
            {
                SetAssetSo();
            }

            if (assetSo != null)
            {
                SerializedProperty sp = assetSo.GetIterator();
                bool enterChild = true;
                while (sp.NextVisible(enterChild))
                {
                    enterChild = false;
                    EditorGUILayout.PropertyField(sp, true);
                }
                assetSo.ApplyModifiedProperties();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
