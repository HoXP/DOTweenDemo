using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioSource : UIBasePanel
{
    public enum DoAudioSource
    {
        DOFade = 0,
        DOPitch
    }
    private DoAudioSource enmDoAudioSource = DoAudioSource.DOFade;

    private Dropdown _dpdFunc = null;
    private Slider _sldTo = null;
    private InputField _iptToPitch = null;
    private InputField _iptDuration = null;

    private AudioSource _ads = null;
    private Text _txtVal = null;
    private Button _btnDo = null;

    protected override void Init()
    {
        base.Init();
        
        _dpdFunc = transform.Find("Adapter/dpdFunc").GetComponent<Dropdown>();
        DoAudioSource[] values = Enum.GetValues(typeof(DoAudioSource)) as DoAudioSource[];
        for (int i = 0; i < values.Length; i++)
        {
            _dpdFunc.options.Add(new Dropdown.OptionData(values.GetValue(i).ToString()));
        }
        _dpdFunc.onValueChanged.AddListener(OnDpdFuncSelect);
        _sldTo = transform.Find("Adapter/sldTo").GetComponent<Slider>();
        _iptToPitch = transform.Find("Adapter/iptToPitch").GetComponent<InputField>();
        _iptDuration = transform.Find("Adapter/iptDuration").GetComponent<InputField>();

        _ads = transform.Find("Adapter/ads").GetComponent<AudioSource>();
        _txtVal = transform.Find("Adapter/txtVal").GetComponent<Text>();
        _btnDo = transform.Find("Adapter/btnDo").GetComponent<Button>();
        _btnDo.onClick.AddListener(OnClickBtnDo);
    }

    private void Start()
    {
        _ads.Play();
        _dpdFunc.value = 0;
        _dpdFunc.RefreshShownValue();
        _iptDuration.text = "1";
        UpdateFloat(1);
        ShowHideConfig();
    }

    private void ShowHideConfig()
    {
        switch (enmDoAudioSource)
        {
            case DoAudioSource.DOFade:
                _sldTo.gameObject.SetActive(true);
                _iptToPitch.gameObject.SetActive(false);
                break;
            case DoAudioSource.DOPitch:
                _sldTo.gameObject.SetActive(false);
                _iptToPitch.gameObject.SetActive(true);
                break;
        }
    }

    private void OnDpdFuncSelect(int arg0)
    {
        enmDoAudioSource = (DoAudioSource)arg0;
        ShowHideConfig();
    }

    protected override void OnClickBtnHome()
    {
        base.OnClickBtnHome();
        _ads.volume = 1;
        _ads.pitch = 1;
        _txtVal.text = "1";
    }

    private void OnClickBtnDo()
    {
        float endValue = 0;
        float duration = 0;
        float.TryParse(_iptDuration.text, out duration);
        switch (enmDoAudioSource)
        {
            case DoAudioSource.DOFade:
                endValue = _sldTo.value;
                /*
                 * duration秒内将一个AudioSource的音量Tween到endValue
                 * endValue∈[0,1]
                 */
                _ads.DOFade(endValue, duration).OnUpdate(() => { UpdateFloat(_ads.volume); });
                break;
            case DoAudioSource.DOPitch:
                float.TryParse(_iptToPitch.text, out endValue);
                /*
                 * duration秒内将一个AudioSource的pitch（音高）Tween到endValue
                 */
                _ads.DOPitch(endValue, duration).OnUpdate(() => { UpdateFloat(_ads.pitch); });
                break;
        }
    }
    
    private void UpdateFloat(float val)
    {
        switch (enmDoAudioSource)
        {
            case DoAudioSource.DOFade:
                _ads.volume = val;
                break;
            case DoAudioSource.DOPitch:
                _ads.pitch = val;
                break;
        }
        _txtVal.text = val.ToString();
    }
}