using System;
using System.CodeDom.Compiler;
using System.Linq;
using CodeBase.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIDEditor: UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueID = (UniqueId)target;

            if (string.IsNullOrEmpty(uniqueID.Id))
            {
                Generate(uniqueID);
            }
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();

                if (uniqueIds.Any(other => other != uniqueID && other.Id == uniqueID.Id))
                {
                    Generate(uniqueID);
                }
            }
        }

        private void Generate(UniqueId uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}