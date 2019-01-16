using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    private RectTransform rt = null;
    private Button _btnColor = null;
    private RawImage _imgColor = null;
    private RectTransform _tranPanel = null;
    private Button _btnClose = null;

    private ColorDisc CP;
    private ColorRGB CRGB;
    private ColorA CA;

    private Slider _sliderCRGB;
    private Slider _sliderCA;

    internal Color color
    {
        get
        {
            return _imgColor == null ? Color.white : _imgColor.color;
        }
    }

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        _tranPanel = transform.Find("Panel").GetComponent<RectTransform>();
        _tranPanel.gameObject.SetActive(false);
        _btnColor = transform.Find("btnColor").GetComponent<Button>();
        _btnColor.onClick.AddListener(delegate () {
            _tranPanel.gameObject.SetActive(true);
        });
        _imgColor = transform.Find("btnColor").GetComponent<RawImage>();
        _btnClose = transform.Find("Panel/btnClose").GetComponent<Button>();
        _btnClose.onClick.AddListener(delegate () {
            _tranPanel.gameObject.SetActive(false);
        });

        CP = transform.Find("Panel/BG/ColorDisc").GetComponent<ColorDisc>();
        CRGB = transform.Find("Panel/BG/RGB").GetComponent<ColorRGB>();
        CA = transform.Find("Panel/BG/A").GetComponent<ColorA>();
        _sliderCRGB = CRGB.transform.Find("Slider").GetComponent<Slider>();
        _sliderCA = CA.transform.Find("Slider").GetComponent<Slider>();
        _sliderCRGB.onValueChanged.AddListener(OnCRGBValueChanged);
        _sliderCA.onValueChanged.AddListener(OnCAValueChanged);
    }

    private void OnEnable()
    {
        CP.getPos += CC_getPos;
    }
    private void OnDisable()
    {
        CP.getPos -= CC_getPos;
    }

    private void CC_getPos(Vector2 pos)
    {
        Color getColor = CP.GetColorByPosition(pos);
        _imgColor.color = getColor;
    }

    private void OnCRGBValueChanged(float value)
    {
        Color endColor = CRGB.GetColorBySliderValue(value);
        CP.SetColorPanel(endColor);
        CP.SetShowColor();
    }

    private void OnCAValueChanged(float arg0)
    {
        _imgColor.color = new Color(_imgColor.color.r, _imgColor.color.g, _imgColor.color.b, 1 - arg0);
    }
}