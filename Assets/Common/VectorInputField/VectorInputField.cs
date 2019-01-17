using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VectorInputField : MonoBehaviour
{
    public enum VectorType
    {
        Vector2 = 0,
        Vector3,
        Vector4
    }
    [SerializeField]
    private VectorType _vectorType = VectorType.Vector3;
    internal float x
    {
        get
        {
            return GetFloatByKey(StrX);
        }
    }
    internal float y
    {
        get
        {
            return GetFloatByKey(StrY);
        }
    }
    internal float z
    {
        get
        {
            return GetFloatByKey(StrZ);
        }
    }
    internal float w
    {
        get
        {
            return GetFloatByKey(StrW);
        }
    }
    internal object value
    {
        get
        {
            switch (_vectorType)
            {
                case VectorType.Vector2:
                    return new Vector2(x, y);
                case VectorType.Vector3:
                    return new Vector3(x, y, z);
                case VectorType.Vector4:
                    return new Vector4(x, y, z, w);
            }
            return null;
        }
    }

    private const string StrX = "x";
    private const string StrY = "y";
    private const string StrZ = "z";
    private const string StrW = "w";
    private string[] xyzw = { StrX, StrY, StrZ, StrW };
    private Dictionary<string, InputField> dict = null;

    private void Awake()
    {
        InputField[] arr = transform.GetComponentsInChildren<InputField>(true);
        dict = new Dictionary<string, InputField>();
        for (int i = 0; i < arr.Length; i++)
        {
            dict.Add(xyzw[i], arr[i]);
        }
        switch (_vectorType)
        {
            case VectorType.Vector2:
                dict[StrZ].gameObject.SetActive(false);
                dict[StrW].gameObject.SetActive(false);
                break;
            case VectorType.Vector3:
                dict[StrW].gameObject.SetActive(false);
                break;
        }
    }

    void Start()
    {

    }

    private float GetFloatByKey(string str)
    {
        if(!dict.ContainsKey(str))
        {
            return 0;
        }
        InputField ipt = dict[str];
        if(ipt == null)
        {
            return 0;
        }
        float f = 0;
        float.TryParse(ipt.text, out f);
        return f;
    }

    /// <summary>
    /// 设置是否可交互
    /// </summary>
    /// <param name="inter"></param>
    internal void SetInteractable(bool inter)
    {
        foreach (var item in dict)
        {
            item.Value.interactable = inter;
        }
    }
}