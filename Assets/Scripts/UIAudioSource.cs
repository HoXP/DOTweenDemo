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
    private InputField _iptDuration = null;

    private AudioSource _ads = null;
    private Scrollbar _scb = null;
    private Text _txtScb = null;
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
        _iptDuration = transform.Find("Adapter/iptDuration").GetComponent<InputField>();

        _ads = transform.Find("Adapter/ads").GetComponent<AudioSource>();
        _scb = transform.Find("Adapter/scb").GetComponent<Scrollbar>();
        _txtScb = _scb.transform.Find("SlidingArea/Handle/txtVolume").GetComponent<Text>();
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
    }

    private void OnDpdFuncSelect(int arg0)
    {
        enmDoAudioSource = (DoAudioSource)arg0;
    }

    protected override void OnClickBtnHome()
    {
        base.OnClickBtnHome();
        UpdateFloat(1);
    }

    private void OnClickBtnDo()
    {
        float endValue = _sldTo.value;
        float duration = 0;
        float.TryParse(_iptDuration.text, out duration);
        switch (enmDoAudioSource)
        {
            case DoAudioSource.DOFade:
                _ads.DOFade(endValue, duration).OnUpdate(() => { UpdateFloat(_ads.volume); });
                break;
            case DoAudioSource.DOPitch:
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
        _scb.value = val;
        _txtScb.text = val.ToString();
    }
}