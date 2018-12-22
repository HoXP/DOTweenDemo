using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    private Image img = null;
    private Tweener tweener = null;
    [SerializeField]
    private Transform[] point = null;
    private Vector3[] v3Arr = null;

    private Button _btnScale = null;

    private void Awake()
    {
        img = transform.Find("Image").GetComponent<Image>();

        int count = 16;
        v3Arr = new Vector3[count];
        //for (int i = 0; i < point.Length; i++)
        //{
        //    //v3Arr[i] = point[i].position;
        //    v3Arr[i] = point[i].localPosition;
        //}
        for (int i = 0; i < count; i++)
        {
            //v3Arr[i] = GetQuadraticBezierPoint(point[0].localPosition, point[1].localPosition, point[2].localPosition, i * 1.0f / count);
            v3Arr[i] = GetQuadraticBezierPoint(point[0].position, point[1].position, point[2].position, i * 1.0f / (count - 1));
        }

        _btnScale = transform.Find("btnScale").GetComponent<Button>();
        _btnScale.onClick.AddListener(OnBtnScale);
    }

    private void OnBtnScale()
    {
        _btnScale.transform.DOShakeScale(.5f, .5f, 0, 0);
    }

    void Start()
    {
    }

    private void Update()
    {
        Debug.DrawLine(point[0].position, point[1].position,Color.blue);
        Debug.DrawLine(point[2].position, point[1].position,Color.red);
    }

    private Vector3 GetQuadraticBezierPoint(Vector3 pointA, Vector3 pointB, Vector3 pointC, float t)  //获取二次贝塞尔点，t∈[0,1]
    {
        //local P = pointA + 2 * t * (pointB - pointA) + t * t * (pointC - 2 * pointB + pointA)
        Vector3 P = (1 - t) * (1 - t) * pointA + 2 * (1 - t) * t * pointB + t * t * pointC;
        return P;
    }

    private void KillTween()
    {
        if (tweener != null) { tweener.Kill(); }
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Linear"))
        {
            KillTween();

            tweener = img.transform.DOPath(v3Arr, 3, PathType.Linear, PathMode.Full3D, 1, Color.green).SetLoops(-1, LoopType.Restart);
            //tweener = img.transform.DOLocalPath(v3Arr, 3, PathType.Linear, PathMode.Full3D, 10, Color.green).SetLoops(-1, LoopType.Restart);
        }
        if (GUILayout.Button("CatmullRom"))
        {
            KillTween();
            tweener = img.transform.DOPath(v3Arr, 3, PathType.CatmullRom, PathMode.Full3D, 10, Color.green).SetLoops(-1, LoopType.Restart);
            //tweener = img.transform.DOLocalPath(v3Arr, 3, PathType.CatmullRom, PathMode.Full3D, 10, Color.green).SetLoops(-1, LoopType.Restart);
            //tweener = img.transform.DOLocalPath(v3Arr, 3, PathType.CatmullRom, PathMode.Ignore, 5, Color.green).SetLoops(-1, LoopType.Restart);
            //tweener = img.transform.DOLocalPath(v3Arr, 3, PathType.CatmullRom, PathMode.Sidescroller2D, 5, Color.green).SetLoops(-1, LoopType.Restart);
            //tweener = img.transform.DOLocalPath(v3Arr, 3, PathType.CatmullRom, PathMode.TopDown2D, 5, Color.green).SetLoops(-1, LoopType.Restart);
        }
    }
}