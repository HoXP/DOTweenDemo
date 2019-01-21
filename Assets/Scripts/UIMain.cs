using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMain : UIBasePanel
{
    private Button _btnAudioSource = null;
    private Button _btnCamera = null;

    internal override void Init()
    {
        base.Init();
        _btnAudioSource = transform.Find("Adapter/GLO/btnAudioSource").GetComponent<Button>();
        _btnAudioSource.onClick.AddListener(OnClickBtnAudioSource);
        _btnCamera = transform.Find("Adapter/GLO/btnCamera").GetComponent<Button>();
        _btnCamera.onClick.AddListener(OnClickBtnamera);
    }

    private void Start()
    {

    }

    private void OnClickBtnAudioSource()
    {
        UIManager.Instance.ShowPanel(UIPanelName.UIAudioSource);
    }

    private void OnClickBtnamera()
    {
        UIManager.Instance.ShowPanel(UIPanelName.UICameraPanel);
    }
}