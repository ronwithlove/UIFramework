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

    private UIManager()//pratvate构造方法，就无法在外部实例化，因为这里我们用单例
    {
        ParseUIPanelTypeJson();//当构造函数被调用，就会使用着方法
    }

    /// <summary>
    /// 根据面板类型 得到实例化的面板
    /// 如果panelDict字典内有就返回，如果没就创建，加入字典
    /// </summary>
    /// <returns></returns>
    public BasePanel GetPanel(UIPanelType panelType) {
        if (panelDict == null) {//如果这个字典为空，还没被创建
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
            instPanel.transform.SetParent(CanvasTransform);//TODO
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else {
            return panel;
        }

    }


    [Serializable]
    class UIPanelTypeJson {
        public List<UIPanelInfo> infoList;//这个名字要和Json里一致
    }

    private void ParseUIPanelTypeJson() {//解析方法
        panelPathDict = new Dictionary<UIPanelType, string>();//创建一个新的字典

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");
        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);//解析json信息，把json内容都放到list里
        foreach (UIPanelInfo info in jsonObject.infoList) {//这里遍历list都放到Dict中，便于查找
            panelPathDict.Add(info.panelType, info.path);
        }


    }

    //测试下json是否成功
    public void Test() {
        string path;
        panelPathDict.TryGetValue(UIPanelType.Knapsack, out path);
        Debug.Log(path);
    }

}
