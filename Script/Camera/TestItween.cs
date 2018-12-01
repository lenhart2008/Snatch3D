using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestItween : MonoBehaviour {


    public iTween.EaseType easeType = iTween.EaseType.linear;
    public float tweenTime = 2.0f;
    public float waitTime = 0.25f;

    [System.Serializable]
    public class OnStart : UnityEvent { };
    public OnStart onStart;

    [System.Serializable]
    public class OnComplete : UnityEvent { };
    public OnComplete onComplete;



    // Use this for initialization
    void Start () {
       
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveTo()
    {
        Hashtable ht = new Hashtable();
        //iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("CameraMove1"), "time", 6, "easetype", iTween.EaseType.linear));

        ht.Add("path", iTweenPath.GetPath("CameraMove1"));
        ht.Add("time", tweenTime);
        ht.Add("easetype", easeType);
        ht.Add("delay", waitTime);

        ht.Add("onstart", (Action<object>)(newVal =>
        {
            if (onStart != null)
            {
                onStart.Invoke();
            }
        }));

        ht.Add("oncomplete", (Action<object>)(newVal =>
        {
            if (onComplete != null)
            {
                onComplete.Invoke();
            }
        }));

        iTween.MoveTo(gameObject, ht);
    }
}
