using UnityEngine;
using System.Collections;

public class ChildLock
{

    public static void Enable()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("EnableChildLock");
#endif
    }

    public static void Disable()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("DisableChildLock");
#endif
    }

    public static bool isEnabled()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("UpdateChildLockStatus");
        return jo.GetStatic<bool>("isEnabled");
#else
        return false;
#endif
    }

    public static bool InstructionsHasReturned()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        return jo.GetStatic<bool>("hasReturned");
#else
        return false;
#endif
    }

    public static bool InstructionsResult()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        return jo.GetStatic<bool>("returnResult");
#else
        return false;
#endif
    }

    public static void SetInstructionStrings(string preInstructions, string postInstructions, string ok, string cancel)
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("SetStrings", new[] { preInstructions, postInstructions, ok, cancel });
#endif
    }

    public static void SetInstructionImage(byte[] image)
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("SetImage", new[] { image });
#endif
    }
}
