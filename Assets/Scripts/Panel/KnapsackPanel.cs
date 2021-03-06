﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class KnapsackPanel : BasePanel {
    private CanvasGroup canvasGroup;

    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //显示界面
    public override void OnEnter() {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;

        //动画
        Vector3 temp = transform.localPosition;
        temp.x = 600;
        transform.localPosition = temp;
        transform.DOLocalMoveX(0, 0.5f);
    }
    
    //处理页面的关闭
    public override void OnExit() {
        canvasGroup.blocksRaycasts = false;
        //canvasGroup.alpha = 0;

        //动画
        transform.DOLocalMoveX(600, 0.5f).OnComplete(()=>canvasGroup.alpha=0);
    }

    public void OnClosePanel() {
        UIManager.Instance.PopPanel();
    }

    //点击背包内物品，弹出物品信息界面
    public void OnItemButtonClick() {
        UIManager.Instance.PushPanel(UIPanelType.ItemMessage);
    }

    //当物品信息界面弹出后，他本身就变得不可交互了
    public override void OnPause() {
        canvasGroup.blocksRaycasts = false;
    }
    //变得可交互
    public override void OnResume() {
        canvasGroup.blocksRaycasts = true;

    }
}
