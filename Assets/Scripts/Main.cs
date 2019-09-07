using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Text txt;

    // Start is called before the first frame update
    void Start()
    {
        AndroidJavaObject unityPlayerActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayerActivity.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

        Log("Initialize chabok............... " + gameObject.name);
        var chabok = new ChabokPush();

        chabok.Init(context,
             unityPlayerActivity,
              "chabok-starter/839879285435",
               "70df4ae2e1fd03518ce3e3b21ee7ca7943577749",
                "chabok-starter",
                 "chabok-starter");
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

    //private void Chabok_ConnectionChanged(string result)
    //{
    //    Log("Chabok connection status is " + result);
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Log(object text)
    {
        txt.text += "\n";
        txt.text += text;
    }
}
