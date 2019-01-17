using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    private RectTransform rt = null;
    internal Graphic graphic { get; set; }

    private ColorDisc CP;
    private ColorRGB CRGB;
    private ColorA CA;

    private Slider _sliderCRGB;
    private Slider _sliderCA;

    internal Color color
    {
        get
        {
            return graphic == null ? Color.white : graphic.color;
        }
    }

    private void Awake()
    {
        rt = GetComponent<RectTransform>();

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
        CP.GetPos += GetPos;
    }
    private void OnDisable()
    {
        CP.GetPos -= GetPos;
    }

    private void GetPos(Vector2 pos)
    {
        Color getColor = CP.GetColorByPosition(pos);
        graphic.color = getColor;
    }

    private void OnCRGBValueChanged(float value)
    {
        Color endColor = CRGB.GetColorBySliderValue(value);
        CP.SetColorPanel(endColor);
        CP.SetShowColor();
    }

    private void OnCAValueChanged(float arg0)
    {
        graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 1 - arg0);
    }

    internal void SetColorChangeCallback(UnityAction ua)
    {

    }
}