using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour {

    //显示界面
    public virtual void OnEnter()
    {
    }
    //暂停界面
    public virtual void OnPause()
    {
    }

    //继续界面
    public virtual void OnResume()
    {
    }
    //不显示界面
    public virtual void OnExit()
    {
    }
}
