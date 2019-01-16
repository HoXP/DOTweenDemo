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
    private ColorPicker _cprTo = null;
    private InputField _iptDuration = null;

    private Text _txtVal = null;
    private Button _btnDo = null;

    protected override void Init()
    {
        base.Init();

        _cam = Camera.main;
        _dpdFunc = transform.Find("Adapter/dpdFunc").GetComponent<Dropdown>();
        DoCamera[] values = Enum.GetValues(typeof(DoCamera)) as DoCamera[];
        for (int i = 0; i < values.Length; i++)
        {
            _dpdFunc.options.Add(new Dropdown.OptionData(values.GetValue(i).ToString()));
        }
        _dpdFunc.onValueChanged.AddListener(OnDpdFuncSelect);
        _sldTo = transform.Find("Adapter/sldTo").GetComponent<Slider>();
        _iptTo = transform.Find("Adapter/iptTo").GetComponent<InputField>();
        _cprTo = transform.Find("Adapter/cprTo").GetComponent<ColorPicker>();
        _iptDuration = transform.Find("Adapter/iptDuration").GetComponent<InputField>();

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

        print(string.Format("### allCamerasCount={0}", Camera.allCamerasCount));
    }

    private void ShowHideConfig()
    {
        _sldTo.gameObject.SetActive(false);
        _iptTo.gameObject.SetActive(false);
        _cprTo.gameObject.SetActive(false);
        switch (enmDoCamera)
        {
            case DoCamera.DOAspect:
                _iptTo.gameObject.SetActive(true);
                break;
            case DoCamera.DOColor:
                _cprTo.gameObject.SetActive(true);
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

    private void OnClickBtnDo()
    {
        float endValue = 0;
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
                _cam.DOColor(_cprTo.color, duration).OnUpdate(() => { print(string.Format("### backgroundColor={0}", _cam.backgroundColor)); });
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