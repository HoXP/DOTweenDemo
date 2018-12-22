using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScaleTest))]
public class ScaleTestEditor : Editor
{
    private ParamsWindow _win = null;
    private ParamsWindow Win
    {
        get
        {
            if(_win == null)
            {
                _win = CreateInstance<ParamsWindow>();
            }
            return _win;
        }
    }
    private ScaleTest _script = null;

    public void OnEnable()
    {
        _script = target as ScaleTest;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("参数调整"))
        {
            Win.ShowWindow(WindiwType.DOScale);
        }
    }
}