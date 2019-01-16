using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorA : MonoBehaviour
{
    private Texture2D tex2d;
    private RawImage _imgA;
    private int TexPixelWdith = 16;
    private int TexPixelHeight = 256;
    private Color[,] arrayColor;

    private void Awake()
    {
        _imgA = transform.Find("imgA").GetComponent<RawImage>();
        arrayColor = new Color[TexPixelWdith, TexPixelHeight];
        tex2d = new Texture2D(TexPixelWdith, TexPixelHeight, TextureFormat.RGB24, true);
        Color[] calcArray = CalcArrayA();
        tex2d.SetPixels(calcArray);
        tex2d.Apply();
        _imgA.texture = tex2d;
    }

    private Color[] CalcArrayA()
    {
        for (int i = 0; i < TexPixelWdith; i++)
        {
            arrayColor[i, 0] = Color.white;
            arrayColor[i, TexPixelHeight - 1] = Color.black;
        }
        Color value = (Color.white - Color.black) / (TexPixelHeight - 1);
        for (int i = 0; i < TexPixelWdith; i++)
        {
            for (int j = 0; j < TexPixelHeight - 1; j++)
            {
                arrayColor[i, j] = Color.black + value * j;
            }
        }
        List<Color> listColor = new List<Color>();
        for (int i = 0; i < TexPixelHeight; i++)
        {
            for (int j = 0; j < TexPixelWdith; j++)
            {
                listColor.Add(arrayColor[j, i]);
            }
        }
        return listColor.ToArray();
    }
}