using UnityEngine;
using System.Collections;
using DG.Tweening;


public class TaskPanel : BasePanel {

    private CanvasGroup canvasGroup;

    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //显示界面
    public override void OnEnter() {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, .5f);//动画
    }

    //处理页面的关闭
    public override void OnExit() {
        canvasGroup.blocksRaycasts = false;
        //canvasGroup.alpha = 0;
        canvasGroup.DOFade(0, .5f);//动画

    }

    public void OnClosePanel() {
        UIManager.Instance.PopPanel();
    }
}
