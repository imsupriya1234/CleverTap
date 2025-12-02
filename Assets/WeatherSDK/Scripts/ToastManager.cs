using UnityEngine;
using System.Runtime.InteropServices;

public class ToastManager : MonoBehaviour
{
    public static void Show(string message)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                    AndroidJavaObject toastObj = toastClass.CallStatic<AndroidJavaObject>("makeText",
                        activity, message, 0);
                    toastObj.Call("show");
                }));
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Toast Android error: " + ex);
        }
#elif UNITY_IOS && !UNITY_EDITOR
        _ShowiOSToast(message);
#else
        Debug.Log("Toast (Editor): " + message);
#endif
    }

#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void _ShowiOSToast(string msg);
#endif
}
