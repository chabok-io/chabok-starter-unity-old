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
        Log("~~~~~~> 0 INIIIIITTTT");

        using (AndroidJavaClass mainClass = new AndroidJavaClass("com.adpdigital.push.AdpPushClient"))
        {
            Log("~~~~~~> 1 Get UnityPlayer class");
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            Log("~~~~~~> 2 Get getApplicationContexts");

            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

            Log("~~~~~~> 3 Geting init ");
            mainClass.CallStatic<AndroidJavaObject>("init", context, unityPlayer, "adp-nms-push/845225163503", "e2100f0d7e071c7450f04e530bda746da2fc493b", "adp", "test");

            Log("~~~~~~> 4 Geting setDevelopment ");
            Log(mainClass.CallStatic<AndroidJavaObject>("get").Call<AndroidJavaObject>("setDevelopment", true));

            mainClass.CallStatic<AndroidJavaObject>("get").Get<AndroidJavaClass>("foreground").Call("foreground",true);


            Log("~~~~~~> 5 Geting register ");
            Log(mainClass.CallStatic<AndroidJavaObject>("get").Call<AndroidJavaObject>("register", "98933195"));

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
