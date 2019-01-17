using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBasePanel : MonoBehaviour
{
    private Button _btnClose = null;
    private Button _btnHome = null;

    protected object[] _param = null;
    internal object[] Param
    {
        set
        {
            _param = value;
        }
    }

    private void Awake()
    {
        FindComponent<Button>("Adapter/btnClose", _btnClose, OnClickBtnClose);
        FindComponent<Button>("Adapter/btnHome", _btnHome, OnClickBtnHome);
    }

    private void Start()
    {

    }
    private void OnDestroy()
    {
        _param = null;
        _btnClose = null;
        _btnHome = null;
    }

    internal virtual void Init()
    {

    }

    private void FindComponent<T>(string path, T t, UnityAction action)
    {
        Transform tran = transform.Find(path);
        if (tran != null)
        {
            t = tran.GetComponent<T>();
            if (t != null)
            {
                if(typeof(T) == typeof(Button))
                {
                    Button b = t as Button;
                    b.onClick.AddListener(action);
                }
            }
        }
    }

    private void OnClickBtnClose()
    {
        UIManager.Instance.ClosePanel(gameObject.name);
    }

    protected virtual void OnClickBtnHome()
    {
    }
}