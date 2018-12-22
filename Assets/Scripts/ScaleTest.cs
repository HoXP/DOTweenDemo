using DG.Tweening;
using UnityEngine;

[ExecuteInEditMode]
public class ScaleTest : DOTweenTest
{
    private RectTransform _tran = null;

    #region DOScale
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _tran = transform.Find("img").GetComponent<RectTransform>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    
    public void DOScale(float endValue, float duration)
    {
        _tran.DOScale(endValue, duration);
    }
    public void DOScale(Vector3 endValue, float duration)
    {
        _tran.DOScale(endValue, duration);
    }

    public override void Reset()
    {
        base.Reset();
        _tran.localScale = Vector3.one;
    }
}