using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChabokPush : MonoBehaviour {

    public Text txt;

	// Use this for initialization
	void Start () {
        Log("################### OH NOOOOOOO");
        Init();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Init()
    {
        using (AndroidJavaClass mainClass = new AndroidJavaClass("com.adpdigital.push.AdpPushClient"))
        {
            Log(1);
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
            Log(2);
            mainClass.CallStatic<AndroidJavaObject>("init", context, unityPlayer, "tojfojwo/418500099884", "71ccffab0597699fc338b8709bf6b32a18024871", "nacjerkogo", "rofuczapoz");
            Log(3);
            Log(mainClass.CallStatic<AndroidJavaObject>("get"));
            Log(4);
            Log(mainClass.CallStatic<AndroidJavaObject>("get").Call<AndroidJavaObject>("register", SystemInfo.deviceUniqueIdentifier));
            Log(5);
        }
    }

    ////method that calls our native plugin.
    //public void CallNativePlugin()
    //{
    //    // Retrieve the UnityPlayer class.
    //    AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

    //    // Retrieve the UnityPlayerActivity object ( a.k.a. the current context )
    //    AndroidJavaObject unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

    //    // Retrieve the "Bridge" from our native plugin.
    //    // ! Notice we define the complete package name.              
    //    AndroidJavaObject bridge = new AndroidJavaObject("plugins.twnkls.com.mylibrary.Bridge");

    //    // Setup the parameters we want to send to our native plugin.              
    //    object[] parameters = new object[2];
    //    parameters[0] = unityActivity;
    //    parameters[1] = "This is an call to native android!";

    //    // Call PrintString in bridge, with our parameters.
    //    bridge.Call("PrintString", parameters);
    //}

    private void Log(object text)
    {
        txt.text += "\n";
        txt.text += text;
    }
}
