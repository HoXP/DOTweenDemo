using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private Transform _tranUIRoot = null;
    private Dictionary<string, UIBasePanel> _dictPanel = null;

    private void Awake()
    {
        _instance = this;
        _tranUIRoot = GameObject.Find("UIRoot").transform;
        _dictPanel = new Dictionary<string, UIBasePanel>();
    }
    private void Start()
    {
    }

    internal void ShowPanel(string panelName, params object[] param)
    {
        if(_dictPanel.ContainsKey(panelName))
        {
            if(IsPanelVisiable(panelName))
            {
                return;
            }
            else
            {
                _dictPanel[panelName].gameObject.SetActive(true);
            }
        }
        else
        {
            UIBasePanel panel = Resources.Load<UIBasePanel>(string.Format("Prefabs/{0}", panelName));
            UIBasePanel initPanel = GameObject.Instantiate<UIBasePanel>(panel, _tranUIRoot);
            initPanel.gameObject.name = panelName;
            initPanel.transform.localScale = Vector3.one;
            initPanel.transform.localPosition = Vector3.zero;
            initPanel.Param = param;
            initPanel.Init();
            _dictPanel.Add(panelName, initPanel);
        }
    }

    internal void ClosePanel(string panelName)
    {
        if (_dictPanel.ContainsKey(panelName))
        {
            _dictPanel[panelName].gameObject.SetActive(false);
        }
    }

    internal bool IsPanelVisiable(string panelName)
    {
        if (_dictPanel.ContainsKey(panelName))
        {
            return _dictPanel[panelName].gameObject.activeInHierarchy;
        }
        return false;
    }
}

public class UIPanelName
{
    public const string UIMain = "UIMain";
    public const string UIAudioSource = "UIAudioSource";
    public const string UICameraPanel = "UICameraPanel";
    public const string UIColorPicker = "UIColorPicker";
}