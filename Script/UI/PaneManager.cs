using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

    public static PanelManager _instance;
    public static PanelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PanelManager)) as PanelManager;
            }
            return _instance;
        }
    }

    public MsgBox msgbox;

    public void ShowMsgBox(string content)
    {
        if (!msgbox.gameObject.activeInHierarchy)
        {
            msgbox.gameObject.SetActive(true);
           // msgbox.SetTextContent(content);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
