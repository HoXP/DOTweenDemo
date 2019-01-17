using System;
using UnityEngine;
using UnityEngine.UI;

public class UIColorPicker : UIBasePanel
{
    private RectTransform _tranAdapter = null;
    private ColorPicker _cp = null;
    private Button _btnBg = null;

    internal override void Init()
    {
        base.Init();
        _tranAdapter = transform.Find("Adapter").GetComponent<RectTransform>();
        _btnBg = transform.Find("bg").GetComponent<Button>();
        _btnBg.onClick.AddListener(OnClickBtnBg);
        _cp = transform.Find("Adapter/ColorPicker").GetComponent<ColorPicker>();
        Graphic graphic = _param[0] as Graphic;
        _cp.graphic = graphic;
    }

    private void Start()
    {

    }

    private void OnClickBtnBg()
    {
        UIManager.Instance.ClosePanel(UIPanelName.UIColorPicker);
    }
}