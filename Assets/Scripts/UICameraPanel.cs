using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UICameraPanel : UIBasePanel
{
    public enum DoCamera
    {
        DOAspect = 0,
        DOColor,
        DOFarClipPlane,
        DOFieldOfView,
        DONearClipPlane,
        DOOrthoSize,
        DOPixelRect,
        DORect,
        DOShakePosition,
        DOShakeRotation
    }
    private DoCamera enmDoCamera = DoCamera.DOAspect;

    private Camera _cam = null;
    private Dropdown _dpdFunc = null;
    private Slider _sldTo = null;
    private InputField _iptTo = null;
    private InputField _iptDuration = null;
    private Button _btnColor = null;
    private Graphic _gpcColor = null;
    private InputField _iptStrength = null;
    private VectorInputField _vifStrength = null;
    private InputField _iptVibrato = null;
    private InputField _iptRandomness = null;
    private Toggle _tglFadeOut = null;

    private Text _txtVal = null;
    private Button _btnDo = null;

    private void Awake()
    {
        _cam = Camera.main;
        _dpdFunc = transform.Find("Adapter/dpdFunc").GetComponent<Dropdown>();
        DoCamera[] values = Enum.GetValues(typeof(DoCamera)) as DoCamera[];
        for (int i = 0; i < values.Length; i++)
        {
            _dpdFunc.options.Add(new Dropdown.OptionData(values.GetValue(i).ToString()));
        }
        _dpdFunc.onValueChanged.AddListener(OnDpdFuncSelect);
        _sldTo = transform.Find("Adapter/vlg/sldTo").GetComponent<Slider>();
        _iptTo = transform.Find("Adapter/vlg/iptTo").GetComponent<InputField>();
        _iptDuration = transform.Find("Adapter/vlg/iptDuration").GetComponent<InputField>();
        _btnColor = transform.Find("Adapter/vlg/btnColor").GetComponent<Button>();
        _btnColor.onClick.AddListener(OnClickBtnColor);
        _gpcColor = _btnColor.transform.GetComponent<Graphic>();
        _iptStrength = transform.Find("Adapter/vlg/iptStrength").GetComponent<InputField>();
        _vifStrength = transform.Find("Adapter/vlg/vifStrength").GetComponent<VectorInputField>();
        _iptVibrato = transform.Find("Adapter/vlg/iptVibrato").GetComponent<InputField>();
        _iptRandomness = transform.Find("Adapter/vlg/iptRandomness").GetComponent<InputField>();
        _tglFadeOut = transform.Find("Adapter/vlg/tglFadeOut").GetComponent<Toggle>();

        _txtVal = transform.Find("Adapter/txtVal").GetComponent<Text>();
        _btnDo = transform.Find("Adapter/btnDo").GetComponent<Button>();
        _btnDo.onClick.AddListener(OnClickBtnDo);
    }
    private void Start()
    {
        _dpdFunc.value = 0;
        _dpdFunc.RefreshShownValue();
        _iptDuration.text = "1";
        UpdateFloat(1);
        ShowHideConfig();
    }

    internal override void Init()
    {
        base.Init();
    }

    private void ShowHideConfig()
    {
        _sldTo.gameObject.SetActive(false);
        _iptTo.gameObject.SetActive(false);
        _btnColor.gameObject.SetActive(false);
        _iptStrength.gameObject.SetActive(false);
        _vifStrength.gameObject.SetActive(false);
        _iptVibrato.gameObject.SetActive(false);
        _iptRandomness.gameObject.SetActive(false);
        _tglFadeOut.gameObject.SetActive(false);
        switch (enmDoCamera)
        {
            case DoCamera.DOAspect:
                _iptTo.gameObject.SetActive(true);
                break;
            case DoCamera.DOColor:
                _btnColor.gameObject.SetActive(true);
                break;
            case DoCamera.DOFarClipPlane:
                break;
            case DoCamera.DOFieldOfView:
                break;
            case DoCamera.DONearClipPlane:
                break;
            case DoCamera.DOOrthoSize:
                break;
            case DoCamera.DOPixelRect:
                break;
            case DoCamera.DORect:
                break;
            case DoCamera.DOShakePosition:
                break;
            case DoCamera.DOShakeRotation:
                break;
        }
    }

    private void OnDpdFuncSelect(int arg0)
    {
        enmDoCamera = (DoCamera)arg0;
        ShowHideConfig();
    }

    protected override void OnClickBtnHome()
    {
        base.OnClickBtnHome();
        _txtVal.text = "1";
        _cam.aspect = 1.777778f;
    }

    private void OnClickBtnColor()
    {
        Graphic gpc = _btnColor.GetComponent<Graphic>();
        UIManager.Instance.ShowPanel(UIPanelName.UIColorPicker, gpc);
    }

    private void OnClickBtnDo()
    {
        float endValue = 0;
        Rect rct = Rect.zero;
        float duration = 0;
        float.TryParse(_iptDuration.text, out duration);
        switch (enmDoCamera)
        {
            case DoCamera.DOAspect:
                float.TryParse(_iptTo.text, out endValue);
                /*
                 * duration秒内将一个Camera的aspect（宽高比）Tween到endValue
                 */
                _cam.DOAspect(endValue, duration).OnUpdate(() => { UpdateFloat(_cam.aspect); });
                break;
            case DoCamera.DOColor:
                /*
                 * duration秒内将一个Camera的backgroundColor（背景色）Tween到color
                 */
                _cam.DOColor(_gpcColor.color, duration).OnUpdate(() => { print(string.Format("### backgroundColor={0}", _cam.backgroundColor)); });
                break;
            case DoCamera.DOFarClipPlane:
                float.TryParse(_iptTo.text, out endValue);
                /*
                 * duration秒内将一个Camera的farClipPlane（远裁剪面）Tween到endValue
                 */
                _cam.DOFarClipPlane(endValue, duration).OnUpdate(() => { UpdateFloat(_cam.farClipPlane); });
                break;
            case DoCamera.DOFieldOfView:
                float.TryParse(_iptTo.text, out endValue);
                /*
                 * duration秒内将一个Camera的fieldOfView（视野）Tween到endValue
                 */
                _cam.DOFieldOfView(endValue, duration).OnUpdate(() => { UpdateFloat(_cam.fieldOfView); });
                break;
            case DoCamera.DONearClipPlane:
                float.TryParse(_iptTo.text, out endValue);
                /*
                 * duration秒内将一个Camera的nearClipPlane（近裁剪面）Tween到endValue
                 */
                _cam.DONearClipPlane(endValue, duration).OnUpdate(() => { UpdateFloat(_cam.nearClipPlane); });
                break;
            case DoCamera.DOOrthoSize:
                float.TryParse(_iptTo.text, out endValue);
                /*
                 * duration秒内将一个Camera的orthographicSize（正交模式下size的一半）Tween到endValue
                 */
                _cam.DOOrthoSize(endValue, duration).OnUpdate(() => { UpdateFloat(_cam.orthographicSize); });
                break;
            case DoCamera.DOPixelRect:
                rct = new Rect();
                /*
                 * duration秒内将一个Camera的pixelRect（像素矩形）Tween到endValue
                 */
                _cam.DOPixelRect(rct, duration).OnUpdate(() => { print(string.Format("### pixelRect={0}", _cam.pixelRect)); });
                break;
            case DoCamera.DORect:
                rct = new Rect();
                /*
                 * duration秒内将一个Camera的orthographicSize（正交模式下size的一半）Tween到endValue
                 */
                _cam.DORect(rct, duration).OnUpdate(() => { print(string.Format("### rect={0}", _cam.rect)); });
                break;
            case DoCamera.DOShakePosition:
                /*
                 * 震动Camera持续duration秒内
                 */
                _cam.DOShakePosition(duration);
                break;
            case DoCamera.DOShakeRotation:
                break;
        }
    }
    
    private void UpdateFloat(float val)
    {
        switch (enmDoCamera)
        {
            case DoCamera.DOAspect:
                break;
            case DoCamera.DOColor:
                break;
            case DoCamera.DOFarClipPlane:
                break;
            case DoCamera.DOFieldOfView:
                break;
            case DoCamera.DONearClipPlane:
                break;
            case DoCamera.DOOrthoSize:
                break;
            case DoCamera.DOPixelRect:
                break;
            case DoCamera.DORect:
                break;
            case DoCamera.DOShakePosition:
                break;
            case DoCamera.DOShakeRotation:
                break;
        }
        _txtVal.text = val.ToString();
    }
}