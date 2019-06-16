using UnityEngine;

public class AndroidPluginCallback : AndroidJavaProxy
{
    public delegate void OnSuccessEvent(AndroidJavaObject count);
    public delegate void onErrorEvent(AndroidJavaObject exception);

    public event OnSuccessEvent OnSuccess;
    public event onErrorEvent OnError;

    public AndroidPluginCallback() : base("com.adpdigital.push.Callback") { }

    private void onSuccess(AndroidJavaObject count)
    {
        if (OnSuccess != null)
        {
            OnSuccess.Invoke(count);
        }
        Debug.Log("ENTER callback onSuccess: " + count);
    }

    public void onError(AndroidJavaObject exception)
    {
        if (OnError != null)
        {
            OnError.Invoke(exception);
        }
        Debug.Log("ENTER callback onError: " + exception);
    }
}
