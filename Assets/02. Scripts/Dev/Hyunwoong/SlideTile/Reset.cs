using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{

    public void ResetButton()
    {
        GameObject slideScript = GameObject.Find("Main Camera");
        SlideGameScript ss = slideScript.GetComponent<SlideGameScript>();
        ss.Suffle();
    }
}
