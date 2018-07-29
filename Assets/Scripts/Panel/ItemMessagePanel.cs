using UnityEngine;
using System.Collections;
using DG.Tweening;


public class ItemMessagePanel : BasePanel {

    private CanvasGroup canvasGroup;

    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //显示界面
    public override void OnEnter() {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;

        //动画
        transform.localScale = Vector3.zero;
        transform.DOScale(1, .5f);
    }


    //处理页面的关闭
    public override void OnExit() {
        canvasGroup.blocksRaycasts = false;
        //  canvasGroup.alpha = 0;
        //动画
        transform.DOScale(0, .5f).OnComplete(()=>canvasGroup.alpha=0);

    }

    public void OnClosePanel() {
        UIManager.Instance.PopPanel();
    }
}
