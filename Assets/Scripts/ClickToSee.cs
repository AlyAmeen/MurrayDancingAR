using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSee : MonoBehaviour
{

    [SerializeField] MurrayDustShow dustShow;
    [SerializeField] GameObject findLogoCanvas;
    public void ButtonPressed()
    {
        if (!dustShow.enabled)//if experience already started dont show the find logo canvas
        {
            findLogoCanvas.gameObject.SetActive(true);
        }
    }
}
