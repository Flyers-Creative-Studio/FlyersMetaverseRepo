using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

using System;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
       // UpdateSession();
    }
    public void UpdateSession()
    {
        if (PlayerPrefs.HasKey("LOGINDATA"))
        {
           // JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("LOGINDATA"), APIResponseAllocator.instance.loginResponse);
          //  LoginSuccess();
        }
        else if (PlayerPrefs.HasKey("REGISTERDATA"))
        {
          //  JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("REGISTERDATA"), APIResponseAllocator.instance.registerResponse);
          //  LoginSuccess();
        }
        else
        {
            HelperUtil.HideMessage();
        }

       
    }
}