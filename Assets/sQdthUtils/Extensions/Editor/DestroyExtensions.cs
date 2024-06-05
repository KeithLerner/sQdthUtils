using UnityEditor;
using UnityEngine;

public static class DestroyExtensions
{
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

    [MenuItem("GameObject/Destroy all children")]
    public static void EditorDestroyChildren(MenuCommand command)
    {
        GameObject go = (GameObject)command.context;
        var children = go.GetComponentsInChildren<Transform>();
        for (var index = children.Length - 1; index >= 0; index--)
        {
            var transform = children[index];
            if (transform != go.transform)
            {
                GameObject.DestroyImmediate(transform.gameObject);
            }
        }
    }
    
    [MenuItem("GameObject/Destroy all children", true)]
    static bool ValidateEditorDestroyChildren()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }
}
