using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChabokPush : MonoBehaviour
{

    #region Class Properties

    private static AndroidJavaClass _adpPushClientClass;
    /// <summary>
    /// Gets the adp push client class.
    /// </summary>
    /// <value>The adp push client class.</value>
    private static AndroidJavaClass AdpPushClientClass
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

    /// <summary>
    /// Gets or sets the adp push client instance.
    /// </summary>
    /// <value>The adp push client instance.</value>
    private AndroidJavaObject AdpPushClientInstance
    {
        get
        {
            return AdpPushClientClass.CallStatic<AndroidJavaObject>("get");
        }
    }

    private static AndroidJavaClass _unityPlayerClass;
    /// <summary>
    /// Gets the unity player class.
    /// </summary>
    /// <value>The unity player class.</value>
    private static AndroidJavaClass UnityPlayerClass
    {
        get
        {
            if (_unityPlayerClass == null)
            {
                _unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            }
            return _unityPlayerClass;
        }
    }

    private static AndroidJavaObject _applicationContext;
    /// <summary>
    /// Gets the application context.
    /// </summary>
    /// <value>The application context.</value>
    private static AndroidJavaObject ApplicationContext
    {
        get
        {
            if (_applicationContext == null)
            {
                AndroidJavaObject currentActivity = UnityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
                _applicationContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            }
            return _applicationContext;
        }
    }

    #endregion


    /// <summary>
    /// Initializes a new instance of the <see cref="T:ChabokPush"/> class.
    /// Note: Fill parameter based on environment you choose.
    /// 
    /// </summary>
    /// <param name="appId">App identifier with google <c>senderId</c>  based on environment. </param>
    /// <param name="apiKey">API key.</param>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    private ChabokPush(AndroidJavaObject context, AndroidJavaObject activity, string appId, string apiKey, string username, string password)
    {
        Init(context, activity, appId, apiKey, username, password);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ChabokPush"/> class.
    /// </summary>
    public ChabokPush()
    {
    }


    #region Static Methods

    /// <summary>
    /// Init the specified appId, apiKey, username and password.
    /// Note: Fill parameter based on environment you choose.
    /// 
    /// </summary>
    /// <returns>Instance of ChabokPush</returns>
    /// <param name="appId">App identifier with google <c>senderId</c>  based on environment. </param>
    /// <param name="apiKey">API key.</param>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    public void Init(AndroidJavaObject context, AndroidJavaObject activity, string appId, string apiKey, string username, string password)
    {
        AdpPushClientClass.CallStatic<AndroidJavaObject>("init", context, activity, appId, apiKey, username, password);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the Chabok environment to Sandbox or Production.
    /// </summary>
    /// <param name="devMode">If set to <c>true</c> will be connected to Sandbox environment.</param>
    public void SetDevelopment(bool devMode)
    {
        AdpPushClientInstance.Call<AndroidJavaObject>("setDevelopment", devMode);
    }

    /// <summary>
    /// Register the specified userId.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    public void Register(string userId)
    {
        AdpPushClientInstance.Call<AndroidJavaObject>("register", userId);
    }

    /// <summary>
    /// Registers user as guest.
    /// </summary>
    public void RegisterAsGuest()
    {
        AdpPushClientInstance.Call<AndroidJavaObject>("registerAsGuest");
    }

    /// <summary>
    /// Sets the defaul tracker for tracking pre-install campaigns.
    /// </summary>
    /// <param name="defaultTracker">Tracker id</param>
    public void SetDefaulTracker(string defaultTracker)
    {
        AdpPushClientInstance.Call("setDefaultTracker", defaultTracker);
    }

    /// <summary>
    /// Sets the user info.
    /// </summary>
    /// <param name="userInfo">User info.</param>
    public void SetUserInfo(Dictionary<string, object> userInfo)
    {
        AdpPushClientInstance.Call("setUserInfo", userInfo);
    }

    /// <summary>
    /// Track the specified trackName and data of current userId.
    /// </summary>
    /// <param name="trackName">Track name.</param>
    public void Track(string trackName)
    {
        AdpPushClientInstance.Call("track", trackName, null);
    }

    /// <summary>
    /// Adds the tag to current userId.
    /// </summary>
    /// <param name="tagName">Tag name.</param>
    public void AddTag(string tagName, AndroidPluginCallback callback)
    {
        AdpPushClientInstance.Call("addTag", tagName, callback);
    }

    /// <summary>
    /// Removes the tag.
    /// </summary>
    /// <param name="tagName">Tag name.</param>
    /// <param name="callback">Callback.</param>
    public void RemoveTag(string tagName, AndroidPluginCallback callback)
    {
        AdpPushClientInstance.Call("removeTag", tagName, callback);
    }

    /// <summary>
    /// Gets the user identifier.
    /// </summary>
    /// <returns>The user identifier.</returns>
    public string GetUserId()
    {
        return AdpPushClientInstance.Call<string>("getUserId");
    }

    /// <summary>
    /// Gets the installation identifier.
    /// </summary>
    /// <returns>The installation identifier.</returns>
    public string GetInstallationId()
    {
        return AdpPushClientInstance.Call<string>("getInstallationId");
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Converts the dictionary to map for android object.
    /// </summary>
    /// <returns>The dictionary to map.</returns>
    /// <param name="parameters">Parameters.</param>
    private static AndroidJavaObject ConvertDictionaryToMap(IDictionary<string, object> parameters)
    {
        AndroidJavaObject javaMap = new AndroidJavaObject("java.util.HashMap");
        AndroidJavaClass stringClazz = new AndroidJavaClass("java.lang.String");
        IntPtr putMethod = AndroidJNIHelper.GetMethodID(
     javaMap.GetRawClass(), "put",
         "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

        object[] args = new object[2];
        foreach (KeyValuePair<string, object> kvp in parameters)
        {

            using (AndroidJavaObject k = new AndroidJavaObject(
                "java.lang.String", kvp.Key))
            {
                string type = "java.lang.String";
                if (kvp.Value is int)
                    type = "java.lang.Integer";
                else if (kvp.Value is double)
                    type = "java.lang.Double";
                else if (kvp.Value is bool)
                    type = "java.lang.Boolean";
                else if (kvp.Value is long)
                    type = "java.lang.Long";
                else if (kvp.Value is float)
                    type = "java.lang.Float";


                using (AndroidJavaObject v = new AndroidJavaObject(type, kvp.Value))
                {
                    args[0] = k;
                    args[1] = v;
                    AndroidJNI.CallObjectMethod(javaMap.GetRawObject(),
                            putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
                }
            }
        }

        return javaMap;
    }


    #endregion
}
