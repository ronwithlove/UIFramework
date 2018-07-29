using UnityEngine;
using System.Collections;

public class SystemPanel : BasePanel {

    private CanvasGroup canvasGroup;

    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //显示界面
    public override void OnEnter() {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }


    //处理页面的关闭
    public override void OnExit() {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public void OnClosePanel() {
        UIManager.Instance.PopPanel();
    }
}
