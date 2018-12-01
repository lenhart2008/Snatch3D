using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSnatchTarPanel : MonoBehaviour {
    public GameObject snatchTarItemPrefab;
    public ToggleGroup itemToggleGroup;
    [SerializeField]
    SnatchTarData curSelectPlayerData ;
    public InputField searchTarInputField;

    List<GameObject> itmeList = new List<GameObject>();
    // Use this for initialization
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init()
    {
        DataManager.Instance.curSelectPlayerData = null;
        DataManager.Instance.snatchTarListDataCtrl.RegisterDataChangeCallBack(OnTarListDataChange);
        CreateItem();
    }

    public void OnOKBtnClick()
    {
        if (curSelectPlayerData != null)
        {
            bool isInvalid = DataManager.Instance.snatchTarListDataCtrl.AddIsInvalid(curSelectPlayerData.address);
            if (!isInvalid && (curSelectPlayerData.shieldTime <= 0))
            {
                DataManager.Instance.curSelectPlayerData = curSelectPlayerData;
                UIManager.Instance.ChangeDetail(UIPanelType.eUIPT_Snatch);
            }
        }

    }

    private void OnDisable()
    {
        Clear();
        DataManager.Instance.snatchTarListDataCtrl.UnRegisterDataChangeCallBack(OnTarListDataChange);
    }

    public void OnCancelBtnClick()
    {
        curSelectPlayerData = null;
        UIManager.Instance.ChangeDetail(UIPanelType.eUIPT_JackPot);
    }

    public void OnItemSelected(SnatchTarData data)
    {
        curSelectPlayerData = data;
    }

    public void CreateItem()
    {
        Clear();
        List<SnatchTarData> datalist = DataManager.Instance.snatchTarListDataCtrl.snatchTarDatasList;
        for (int i = 0; i < datalist.Count; i++)
        {
            GameObject itemObj = GameObject.Instantiate(snatchTarItemPrefab);
            itemObj.transform.SetParent(itemToggleGroup.transform, false);
            SnatchTarItem item = itemObj.GetComponent<SnatchTarItem>();
            if (item != null)
            {
                item.Init(datalist[i]);
                item.RegisterToggleValueChangeCallBack(OnItemSelected);
            }

            UIToggle toggle = itemObj.GetComponent<UIToggle>();
            if (toggle != null)
            {
                toggle.SetToggleGroup(itemToggleGroup);
            }
            itmeList.Add(itemObj);
        }
    }


    public void Clear()
    {
        curSelectPlayerData = null;
        for (int i = 0; i < itmeList.Count; i++)
        {
            Destroy(itmeList[i]);
        }
        itmeList.Clear();
    }

    public void OnRefreshBtnClick()
    {
        Clear();
        DataManager.Instance.snatchTarListDataCtrl.GetSnatchTarDataList();
    }

    public void OnTarListDataChange(List<SnatchTarData> datalist)
    {
        CreateItem();
    }

    public void OnSearchTarDataBtnClick()
    {        
        string add = searchTarInputField.text;
        if (DataManager.Instance.snatchTarListDataCtrl.SearchTarDataByAdd(add))
        {
            Clear();
        }
    }


}
