using UnityEngine;
using System.Collections;


/// <summary>
/// 有四种状态：
/// 1.OnEnter显示出来了
/// 2.OnPause暂停（比如当任务界面被开启，再点背包，商城就没用了，不再和鼠标交互）
/// 3.OnResume 恢复使用（比如任务界面被关闭，其他界面又可以点击了）
/// 4.OnExit 界面不显示
/// </summary>
public class MainMenuPanel : BasePanel {

    //把其他页面显示出来
    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType =(UIPanelType) System.Enum.Parse(typeof (UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }



}
