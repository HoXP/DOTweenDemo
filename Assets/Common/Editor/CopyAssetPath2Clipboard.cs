///用于在Project视图和Hierarchy视图右键拷贝资源路径或结点层级;
using UnityEngine;
using UnityEditor;

public class CopyNodePath2Clipboard
{
    [MenuItem("Assets/Copy Asset Path to Clipboard")]
    static void CopyAssetPath2Clipboard()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
        path = path.Replace('\\', '/');
        TextEditor text2Editor = new TextEditor();
        text2Editor.text = path;
        text2Editor.OnFocus();
        text2Editor.Copy();
        Debug.LogFormat("Asset Path : <b><color=green>{0}</color></b> Copy Success!", path);
    }

    [MenuItem("GameObject/Copy UI Node Path to Clipboard", false, 1)]
    static void CopyUINodePath2Clipborar()
    {
        string path = GenerateUINodePath(Selection.activeGameObject);
        path = path.Replace('\\', '/');
        TextEditor text2Editor = new TextEditor();
        text2Editor.text = path;
        text2Editor.OnFocus();
        text2Editor.Copy();
        Debug.LogFormat("UI Node Path : <b><color=green>{0}</color></b> Copy Success!", path);
    }

    public static string GenerateUINodePath(GameObject obj)
    {
        if (obj == null) return "";
        string path = obj.name;

        while (obj.transform.parent != null && isContinue(obj.transform.parent.gameObject))
        {
            obj = obj.transform.parent.gameObject;
            path = obj.name + "\\" + path;
        }
        return path;
    }

    static bool isContinue(GameObject go)
    {
        if (go.GetComponent<Canvas>() != null)
        {
            Transform parent = go.transform.parent;
            if (parent != null)
            {
                return false;
            }
        }
        return true;
    }
}