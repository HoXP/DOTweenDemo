using UnityEngine;

public class DOTweenTest : MonoBehaviour
{
    protected virtual void Awake()
    {
        TestManager.Instance.TestScritp = this;
    }

    protected virtual void OnEnable()
    {
        TestManager.Instance.TestScritp = this;
    }

    public virtual void Reset()
    {
    }
}