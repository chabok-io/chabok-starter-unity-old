using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AndroidJavaObject unityPlayerActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayerActivity.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

        var chabok = new ChabokPush();
        chabok.Init(context,
             unityPlayerActivity,
              "APP_ID/SENDERID",
               "API_KEY",
                "USERNAME",
                 "PASSWORD");
        chabok.SetDevelopment(true);

        //chabok.SetDefaulTracker("TRACKER_ID");

        var userId = chabok.GetUserId();
        if (userId != null)
        {
            chabok.Register(userId);
        }
        else
        {
            chabok.RegisterAsGuest();
        }

        //var callback = new AndroidPluginCallback();
        //callback.OnSuccess += (count) => { 
        
        //};
        //callback.OnError += (exception) => { 
        
        //};

        //chabok.AddTag("Test", callback);

        //JSONObject data = new JSONObject(JSONObject.Type.OBJECT);
        //data.AddField("field1", "5");


        //chabok.Track("LIKE", null);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
