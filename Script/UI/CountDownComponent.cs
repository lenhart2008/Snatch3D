using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownComponent : MonoBehaviour {
    public float timeInterval = 1;
    public float curTime = 0;
    public int showTime
    {
        get
        {
            return _showTime;
        }
        set
        {
            _showTime = value;
            if (timeChangeCallback != null)
            {
                string callbackvalue = IntegerToTime(_showTime);
                timeChangeCallback(callbackvalue);
            }
        }
    }
    [SerializeField]
    int _showTime;
 
    public Action<string> timeChangeCallback;

    string IntegerToTime(int value)
    {
        string str_h = "";
        string str_min = "";
        string str_sec = "";
        string ret = "";
        int h = _showTime / (60 * 60);
        int min = _showTime / 60 % 60;
        int sec = _showTime % 60;

        if (h/10 == 0)
        {
            str_h = "0";
        }
        str_h += h.ToString();

        if (min / 10 == 0)
        {
            str_min = "0";
        }
        str_min += min.ToString();

        if (sec / 10 == 0)
        {
            str_sec = "0";
        }
        str_sec += sec.ToString();

        ret = str_h + ":" + str_min + ":" + str_sec;
        return ret;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (curTime < timeInterval)
        {
            curTime += Time.deltaTime;
        }
        else
        {
            curTime = 0;
            showTime--;
            if (showTime < 0)
            {
                showTime = 0;
            }
        }
	}

    public void SetShowTime(int time)
    {
        showTime = time;
        curTime = 0;
    }
}
