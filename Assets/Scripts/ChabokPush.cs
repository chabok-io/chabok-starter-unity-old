using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChabokPush : MonoBehaviour {


    private AndroidJavaClass _instance;

    #region Properties

    private AndroidJavaClass _adpPushClientClass;
    private AndroidJavaClass adpPushClientClass
    {
        get
        {
            if (_adpPushClientClass == null)
            {
                _adpPushClientClass = new AndroidJavaClass("com.adpdigital.push.AdpPushClient");
            }
            return _adpPushClientClass;
        }
    }

    private AndroidJavaClass _mainActivityClass;
    private AndroidJavaClass mainActivityClass
    {
        get
        {
            if(_mainActivityClass == null)
            {
                _mainActivityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            }
            return _mainActivityClass;
        }
    }

    #endregion

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
            Log("~~~~~~> 1 Get UnityPlayer class");
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            Log("~~~~~~> 2 Get getApplicationContexts");

            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

            Log("~~~~~~> 3 Geting init ");
            mainClass.CallStatic<AndroidJavaObject>("init", context, unityPlayer, "APP_ID/SENDER_ID", "API_KEY", "USERNAME", "PASSWORD");

            Log("~~~~~~> 4 Geting setDevelopment ");
            Log(mainClass.CallStatic<AndroidJavaObject>("get").Call<AndroidJavaObject>("setDevelopment", true));

            //mainClass.CallStatic<AndroidJavaObject>("get").CallStatic<AndroidJavaClass>("foreground").CallStatic("foreground",true);


            Log("~~~~~~> 5 Geting register ");
            Log(mainClass.CallStatic<AndroidJavaObject>("get").Call<AndroidJavaObject>("register", "USER_ID"));

            Log(5);
        }
    }

    private void Log(object text)
    {
        txt.text += "\n";
        txt.text += text;
    }
}
