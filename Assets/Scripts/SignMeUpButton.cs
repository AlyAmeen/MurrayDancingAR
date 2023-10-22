using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignMeUpButton : MonoBehaviour
{
    [SerializeField]
    string URLToGoTo = "https://fun.email2inbox.com/experience-optin";
    public void OnSignMeUpClicked()
    {
        Application.OpenURL(URLToGoTo);

    }
}
