using DG.Tweening;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private void Awake()
    {
        /*
         * 初始化DOTween。
         * 调用DOTween.Init()而不传入任何参数，会使用【Tools】【Demigiant】【DOTween Utility Panel】【Preferences】中的自定义参数进行初始化；传参的话将使用所传的参数进行初始化。
         * 【bool recycleAllByDefault = false】
         *      true表示Kill一个Tweener时会把它回收进池子中，false表示Kill一个Tweener时会销毁之。
         *      true可降低GC次数，但是必须手动将Tweener的引用置null，否则可能会导致即使Tweener被Kill了依然在活动，可以使用【.OnKill(()=> tweener = null)】来将Tweener引用置null。
         *      可通过更改静态属性DOTween.defaultRecyclable来随时更改此设置，或者可以使用SetRecyclable分别为每个补间设置回收行为。
         * 【bool useSafeMode = true】
         *      true表示使用安全模式，这允许DOTween在运行Tween时目标被销毁时自动处理这些东西，更安全但是会稍慢。
         * 【LogBehaviour logBehaviour = LogBehaviour.ErrorsOnly】
         *      LogBehaviour.Default——仅记录warnings和errors;
         *      LogBehaviour.Verbose——记录warnings，errors和其他信息;
         *      LogBehaviour.ErrorsOnly——仅记录errors;
         */
        DOTween.Init()
            /*
             * 设置Tweener和Sequence容量，默认为200，50。
             */
            .SetCapacity(100, 30);
        //
        gameObject.AddComponent<UIManager>();
    }

    private void Start()
    {
        UIManager.Instance.ShowPanel(UIPanelName.UIMain);
    }

    private void OnEnable()
    {
    }
}

//TODO
//static DOTween.To(getter, setter, to, float duration)