using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 字典扩展
/// </summary>
public static class DictionaryExtension {//第一步必须是静态方法

    /// <summary>
    /// 根据key得到value,如果没返回null
    /// this Dictionary<Tkey, Tvalue>dict 这个字典表示我们要获取值的字典
    /// </summary>
    public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key) {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }

}
