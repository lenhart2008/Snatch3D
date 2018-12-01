using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgBox : MonoBehaviour
{
    public Text content1;
    public InputField content2;
    public Button okBtn;
    private Action onOKBtnClickCallback;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowLetter(string str1, string str2,int type,Action callback)
    {
        gameObject.SetActive(true);
        if (type == 1)
        {
            okBtn.gameObject.SetActive(false);
        }
        else if(type == 2)
        {
            okBtn.gameObject.SetActive(true);
            RegisterOKBtnClickCallBack(callback);
        }

        content1.text = str1;
        content2.text = str2;

    }


    public void RegisterOKBtnClickCallBack(Action callback)
    {
        onOKBtnClickCallback = callback;
    }

    public void OnOKBtnClick()
    {
        gameObject.SetActive(false);
        if (onOKBtnClickCallback != null)
        {
            onOKBtnClickCallback();
        }
    }


    public void OnCancelBtnClick()
    {
        gameObject.SetActive(false);
    }

    private void Reset()
    {
        okBtn.gameObject.SetActive(false);
        onOKBtnClickCallback = null;
    }

    private void OnDisable()
    {
        Reset();
    }
}
