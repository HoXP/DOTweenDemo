using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioSource : UIBasePanel
{
    private AudioSource _ads = null;
    private Scrollbar _scbVolume = null;
    private Text _txtVolume = null;
    private Button _btnDo = null;

    protected override void Init()
    {
        base.Init();
        _ads = transform.Find("Adapter/ads").GetComponent<AudioSource>();
        _scbVolume = transform.Find("Adapter/scbVolume").GetComponent<Scrollbar>();
        _txtVolume = _scbVolume.transform.Find("Sliding Area/Handle/txtVolume").GetComponent<Text>();
        _btnDo = transform.Find("Adapter/btnDo").GetComponent<Button>();
        _btnDo.onClick.AddListener(OnClickBtnDo);
    }

    private void Start()
    {
        _ads.Play();
    }
    
    void Update()
    {

    }

    protected override void OnClickBtnHome()
    {
        base.OnClickBtnHome();
        UpdateVolume(1);
    }

    private void OnClickBtnDo()
    {
        _ads.DOFade(.5f, 1).OnUpdate(()=> { UpdateVolume(_ads.volume); });
    }

    private void UpdateVolume(float vol)
    {
        _ads.volume = vol;
        _scbVolume.value = vol;
    }
}