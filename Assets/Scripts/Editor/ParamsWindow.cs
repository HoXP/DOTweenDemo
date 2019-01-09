using Define;
using UnityEditor;
using UnityEngine;

public class ParamsWindow : EditorWindow
{
    private DOTweenTest _script = null;
    private WindiwType _enmWindiwType = WindiwType.DOScale;

    #region DOScale
    private EndValueType _enmEndValueType = EndValueType.Float;
    [SerializeField]
    private float fEndValue = 0.5f;
    [SerializeField]
    private Vector3 v3EndValue = Vector3.one;
    private float fDuration = 1;
    #endregion

    public void OnGUI()
    {
        #region Reset
        if (GUILayout.Button("Reset"))
        {
            _script.Reset();
        }
        #endregion

        switch (_enmWindiwType)
        {
            #region DOScale
            case WindiwType.DOScale:
                EditorGUILayout.LabelField("参数：");
                ScaleTest st = _script as ScaleTest;
                _enmEndValueType = (EndValueType)EditorGUILayout.EnumPopup(_enmEndValueType);
                switch (_enmEndValueType)
                {
                    case EndValueType.Float:
                        fEndValue = EditorGUILayout.FloatField("EndValue", fEndValue);
                        break;
                    case EndValueType.Vector3:
                        v3EndValue = EditorGUILayout.Vector3Field("EndValue", v3EndValue);
                        break;
                    default:
                        break;
                }
                fDuration = EditorGUILayout.FloatField("Duration", fDuration);
                if (GUILayout.Button("DOScale"))
                {
                    switch (_enmEndValueType)
                    {
                        case EndValueType.Float:
                            st.DOScale(fEndValue, fDuration);
                            break;
                        case EndValueType.Vector3:
                            st.DOScale(v3EndValue, fDuration);
                            break;
                        default:
                            break;
                    }
                }
                break;
            #endregion
            default:
                break;
        }
    }
    public void OnFocus()
    {// 获得焦点
        Debug.Log("OnFocus 获得焦点");
        //_script = TestManager.Instance.TestScritp;
        _script = GameObject.Find("Canvas").GetComponent<ScaleTest>();
    }
    public void OnLostFocus()
    {// 失去焦点
        Debug.Log("OnLostFocus 失去焦点");
    }

    public void ShowWindow(WindiwType windowType)
    {
        _enmWindiwType = windowType;
        Show();
    }
}

public enum WindiwType
{
    DOScale,
}