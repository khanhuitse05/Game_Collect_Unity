using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

public class Utils
{
    public static string linkAndroid = "https://play.google.com/store/apps/details?id=com.starseed.jumping";
    public static string linkIOS = "https://itunes.apple.com/us/app/jumping-jumping-obstacle-rush/id1191420685";
    public static string GetLinkStore()
    {
#if UNITY_IOS
        return linkIOS;
#else
        return linkAndroid;
#endif
    }
    public static void removeAllChildren(Transform paramParent, bool paramInstant=true)
    {
        if (paramParent == null)
            return;
        for (int i = paramParent.childCount - 1; i >= 0; i--)
        {
            if (paramInstant)
            {
                GameObject.DestroyImmediate(paramParent.GetChild(i).gameObject);
            } else
            {
                paramParent.GetChild(i).gameObject.SetActive(false);
                GameObject.Destroy(paramParent.GetChild(i).gameObject);
            }
        }
    }
    public static void Log(string paramLog)
    {
        Debug.Log(paramLog);
    }
    public static void LogError(string paramLog)
    {
        Debug.LogError(paramLog);
    }

    public static Sprite loadResourcesSprite(string param)
    {
        return Resources.Load<Sprite>("" + param);
    }
    public static T[] CloneArray<T>(T[] paramArray)
    {
        if (paramArray == null)
            return null;
        return paramArray.Clone() as T[];
    }
    public static List<T> CloneArray<T>(List<T> paramArray)
    {
        if (paramArray == null)
            return null;
        List<T> list = new List<T>(paramArray.ToArray());
        return list;
    }
    public static GameObject Spawn(GameObject paramPrefab, Transform paramParent = null)
    {
        GameObject newObject = GameObject.Instantiate(paramPrefab) as GameObject;
        newObject.transform.SetParent(paramParent);
        newObject.transform.localPosition = Vector3.zero;
        newObject.transform.localScale = paramPrefab.transform.localScale;
        newObject.SetActive(true);
        return newObject;
    }
    public static string removeExtension(string paramPath)
    {
        int index = paramPath.LastIndexOf('.');
        if (index < 0)
            return paramPath;
        return paramPath.Substring(0, index);
    }
    public static void setActive(GameObject paramObject, bool paramValue)
    {
        if (paramObject != null)
            paramObject.SetActive(paramValue);
    }
  
    public static bool isURL(string paramURL)
    {
        if (string.IsNullOrEmpty(paramURL))
            return false;
        return (paramURL.IndexOf("http") >= 0);
    }
    public static int getScore()
    {
        return getScore(GamePreferences.Instance.setting.currentStar, GamePreferences.Instance.setting.currentTime);
    }
    public static int getScore(int _star, int _time)
    {
        return _star * 10 + _time;
    }
}