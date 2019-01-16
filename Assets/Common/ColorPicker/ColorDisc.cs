using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorDisc : MonoBehaviour
{
    private Texture2D tex2d;

    private const int TexPixelLength = 256;
    private Color[,] arrayColor;

    private RectTransform rt;
    private RectTransform _tranCircle;
    private RawImage _imgDisc = null;

    public delegate void RetureTextuePosition(Vector2 pos);
    public event RetureTextuePosition getPos;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        _imgDisc = transform.Find("imgDisc").GetComponent<RawImage>();
        _tranCircle = transform.Find("imgCircle").GetComponent<RectTransform>();
        //Event
        EventTrigger et = _imgDisc.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerClick;
        entry1.callback.AddListener(OnPointerClick);
        et.triggers.Add(entry1);
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.Drag;
        entry2.callback.AddListener(OnDrag);
        et.triggers.Add(entry2);

        arrayColor = new Color[TexPixelLength, TexPixelLength];
        tex2d = new Texture2D(TexPixelLength, TexPixelLength, TextureFormat.RGB24, true);
        _imgDisc.texture = tex2d;

        SetColorPanel(Color.red);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Color end = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
            SetColorPanel(end);
        }
    }

    private Color[] CalcArrayColor(Color endColor)
    {
        Color value = (endColor - Color.white) / (TexPixelLength - 1);
        for (int i = 0; i < TexPixelLength; i++)
        {
            arrayColor[i, TexPixelLength - 1] = Color.white + value * i;
        }
        for (int i = 0; i < TexPixelLength; i++)
        {
            value = (arrayColor[i, TexPixelLength - 1] - Color.black) / (TexPixelLength - 1);
            for (int j = 0; j < TexPixelLength; j++)
            {
                arrayColor[i, j] = Color.black + value * j;
            }
        }
        List<Color> listColor = new List<Color>();
        for (int i = 0; i < TexPixelLength; i++)
        {
            for (int j = 0; j < TexPixelLength; j++)
            {
                listColor.Add(arrayColor[j, i]);
            }
        }
        return listColor.ToArray();
    }

    private void OnPointerClick(BaseEventData eventData)
    {
        UpdateCirclePos(eventData);
    }
    private void OnDrag(BaseEventData eventData)
    {
        UpdateCirclePos(eventData);
    }
    private void UpdateCirclePos(BaseEventData eventData)
    {
        Vector3 wordPos;
        PointerEventData ped = eventData as PointerEventData;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, ped.position, ped.pressEventCamera, out wordPos))
        {
            _tranCircle.position = wordPos;
        }
        SetShowColor();
    }

    public void SetColorPanel(Color endColor)
    {
        Color[] CalcArray = CalcArrayColor(endColor);
        tex2d.SetPixels(CalcArray);
        tex2d.Apply();
    }

    public Color GetColorByPosition(Vector2 pos)
    {//通过位置获取颜色
        Texture2D tempTex2d = (Texture2D)_imgDisc.texture;
        Color getColor = tempTex2d.GetPixel((int)pos.x, (int)pos.y);
        return getColor;
    }

    public void SetShowColor()
    {
        getPos(_tranCircle.anchoredPosition);
    }
}