using System;
using UnityEngine;

namespace Application
{
    public class AndroidPluginCallback : AndroidJavaProxy
    {

        public AndroidPluginCallback() : base("com.adpdigital.push.Callback") { }

        public void onSuccess(AndroidJavaObject count)
        {
            Debug.Log("ENTER callback onSuccess: " + count);
        }

        public void onError(AndroidJavaObject exception)
        {
            Debug.Log("ENTER callback onError: " + exception);
        }
    }
}
