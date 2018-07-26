using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 内容要和json完全一直
/// </summary>
[Serializable]
public class UIPanelInfo :ISerializationCallbackReceiver{//继承这个接口
    [NonSerialized]//不去解析
    public UIPanelType panelType;

    public string panelTypeString;
    //    {//这个就是个中间过程，组要是为了把string和上面UIPanelType进行转换
    //    get{
    //        return panelType.ToString(); 
    //    }
    //    set{
    //        Debug.Log(value);
    //        panelType=(UIPanelType)System.Enum.Parse(typeof (UIPanelType), value);
    //    }
    //}
    public string path;

    public void OnBeforeSerialize() {//这里没用到对象到文本， 所以就空着
    }

    //反序列号，从文本到对象
    public void OnAfterDeserialize() {
        UIPanelType type= (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        panelType = type;
    }
}
