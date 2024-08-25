using UnityEditor;
using UnityEngine;

namespace SqdthUtils.EditorUtilities
{
    public static class GameObjectExtensions
    {
        #region Sort Children
        [MenuItem("GameObject/Sort Children")]
        private static void SortChildren()
        {
            // Get references
            GameObject selectedObject = Selection.activeGameObject;
            int childCount = selectedObject.transform.childCount;
            Transform[] children = new Transform[childCount];
            
            // Register hierarchy for undo/redo
            Undo.RegisterFullObjectHierarchyUndo(selectedObject, "Sort Children");

            // Store the children in an array
            for (int i = 0; i < childCount; i++)
            {
                children[i] = selectedObject.transform.GetChild(i);
            }

            // Sort the children by their names
            System.Array.Sort(children, 
                (a, b) => string.Compare(a.name, b.name));

            // Reassign the sorted children back to their parent
            for (int i = 0; i < childCount; i++)
            {
                children[i].SetSiblingIndex(i);
            }
        } 
    
        [MenuItem("GameObject/Sort Children", true)]
        private static bool SortChildrenValidator()
        {
            return Selection.activeGameObject != null &&
                Selection.activeTransform.childCount > 0;
        }
        #endregion

        public static void DestroyAllChildren(this GameObject target)
        {
            foreach (Transform transform in target.GetComponentInChildren<Transform>())
            {
                GameObject.Destroy(transform.gameObject);
            }
        }
    
        public static void DestroyImmediateAllChildren(this GameObject target)
        {
            foreach (GameObject transform in target.GetComponentInChildren<Transform>())
            {
                GameObject.DestroyImmediate(transform.gameObject);
            }
        }

        #if UNITY_EDITOR
    
        /// <summary>
        /// Destroys all children of the selected GameObject.
        /// </summary>
        [MenuItem("GameObject/Destroy all children")]
        private static void EditorDestroyChildren(MenuCommand command)
        {
            GameObject go = (GameObject)command.context;
            Transform[] childTranforms = go.GetComponentsInChildren<Transform>();

            Undo.RegisterFullObjectHierarchyUndo(go, "Destroyed Children");
        
            for (int i = childTranforms.Length - 1; i >= 0; i--)
            {
                Transform transform = childTranforms[i];
                if (transform != go.transform)
                {
                    GameObject.DestroyImmediate(transform.gameObject);
                }
            }
        }
    
        /// <summary>
        /// Validates that EditorDestroyChildren can be called.
        /// </summary>
        /// <returns> <b>True</b> when a GameObject is selected,
        /// <b>false</b> otherwise. </returns>
        [MenuItem("GameObject/Destroy all children", true)]
        private static bool ValidateEditorDestroyChildren()
        {
            // Return false if no transform is selected.
            return Selection.activeTransform != null;
        }
    
        #endif
    }
}
