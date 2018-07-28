using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

/// <summary>
/// 他是整个框架的管理器，不需要继承MonoBehaviour
/// 单例模式的核心
/// 1.定义一个静态的对象，在外界访问，在内部构造
/// 2.构造方法私有化 
/// </summary>
public class UIManager {

    private static UIManager _instance;

    public static UIManager Instance {//外接直接访问着方法
        get {
            if (_instance == null) {//第一次访问的时候是空的，所以会通过私有构造方法创建
                _instance = new UIManager();
            }
            return _instance;//当不为空，说明已经有了， 就return一个。这个单例永远就只有一个
        }
    }

    private Transform canvasTransform;
    private Transform CanvasTransform {
        get {
            if (canvasTransform == null) {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    private Dictionary<UIPanelType, string> panelPathDict;//储存所有面板Prefab的路径
    private Dictionary<UIPanelType, BasePanel> panelDict;//储存所有实例化面板上的BasePanel组件 
    private Stack<BasePanel> panelStack;

    //pratvate构造方法，就无法在外部实例化，因为这里我们用单例
    private UIManager() {
        ParseUIPanelTypeJson();//当构造函数被调用，就会使用着方法
    }

    #region 解析方法
    [Serializable]
    class UIPanelTypeJson {
        public List<UIPanelInfo> infoList;//这个名字要和Json里一致
    }

    //把所有页面和对应的perfab地址放到字典中去
    private void ParseUIPanelTypeJson() {
        panelPathDict = new Dictionary<UIPanelType, string>();//创建一个新的字典

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");
        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);//解析json信息，把json内容都放到list里
        foreach (UIPanelInfo info in jsonObject.infoList) {//这里遍历list都放到Dict中，便于查找
            panelPathDict.Add(info.panelType, info.path);
        }
    }
    #endregion

    /// 根据面板类型 得到实例化的面板
    /// 如果panelDict字典内有就返回，如果没就创建，加入字典
    private BasePanel GetPanel(UIPanelType panelType) {
        if (panelDict == null) {//如果panelDict字典为空，还没被创建
            panelDict = new Dictionary<UIPanelType, BasePanel>();//就新建一个新的字典
        }

        //BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);//TODO
        BasePanel panel = panelDict.TryGet(panelType);

        if (panel == null) {//如果没找到，就取得路径实例化他
            //string path;
            //panelPathDict.TryGetValue(panelType, out path);
            string path = panelPathDict.TryGet(panelType);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);//这里要加上false，才会在Canvas正常位置显示
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else {
            return panel;
        }
    }


    ////测试下json是否成功
    //public void Test() {
    //    string path;
    //    panelPathDict.TryGetValue(UIPanelType.Knapsack, out path);
    //    Debug.Log(path);
    //}


    //页面显示在界面上 ，入栈
    public void PushPanel(UIPanelType panelType) {
        if (panelStack == null) {//如果栈未创建，创建一个
            panelStack = new Stack<BasePanel>();
        }
        //如果栈内已有页面，最上面那个暂停掉
        if (panelStack.Count > 0) {
            BasePanel topPanel = panelStack.Peek();//返回在 Stack 的顶部的对象，不移除，还在栈内。
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);//当前的页面
        panel.OnEnter();//调用当前页面OnEnter方法
        panelStack.Push(panel);//向 Stack 的顶部添加一个当前页面
    }

    //页面从界面上移除，出栈
    public void PopPanel() {
        if (panelStack == null) {//如果栈未创建，创建一个
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack.Count <= 0) return;

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();//移除并返回在 Stack 的顶部的页面，栈内就没了。
        topPanel.OnExit();//调用他自己移除的方法

        //上一步把当前页面取出了，关闭了，那么这里自动把他前一个访问的页面调出。
        //把前一个页面显示出来
        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }


}
