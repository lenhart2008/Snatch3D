using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkManager : MonoBehaviour {
    public static NetWorkManager _instance;
    public static NetWorkManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(NetWorkManager)) as NetWorkManager;
            }
            return _instance;
        }
    }

    public ContractService contractService
    {
        get
        {
            if (_contractService == null)
            {
                _contractService = new ContractService();
            }
            return _contractService;
        }
    }

    ContractService _contractService;
    // Use this for initialization
    void Start () {
		
	}

	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClientSendMessage2Contract(PacketBase packet)
    {
        StartCoroutine(_ClientSendMessage2Contract(packet));
    }

    private IEnumerator _ClientSendMessage2Contract(PacketBase packet)
    {
        PauseUI(true);
        yield return packet.Send();
        PauseUI(false);
    }

    private void PauseUI(bool b)
    {
        if (b)
        {
            UIManager.Instance.ShowUIPanel(UIPanelType.eUIPT_Pause);
        }
        else
        {
            UIManager.Instance.HideUIPanel(UIPanelType.eUIPT_Pause);
        }
        
    }

    public IEnumerator checkTx(string txHash, Action<bool> callback)
    {       
        var mined = false;
        // we are going to set the tries to 999 for testing purposes
        var tries = 16;
        while (!mined)
        {
            if (tries > 0)
            {
                tries = tries - 1;
            }
            else
            {
                mined = true;
                Debug.Log("Performing last try..");
            }
            Debug.Log("Checking receipt for: " + txHash);
            var receiptRequest = new EthGetTransactionReceiptUnityRequest(ContractParams._url);
            yield return receiptRequest.SendRequest(txHash);
            if (receiptRequest.Exception == null)
            {
                if (receiptRequest.Result != null && receiptRequest.Result.Logs.HasValues)
                {
                    var txType = receiptRequest.Result.Logs[0]["type"].ToString();
                    if (txType == "mined")
                    {
                        // if we have a transaction type == mined we return the callback
                        // and exit the loop
                        mined = true;
                        callback(mined);
                    }
                }
            }
            else
            {
                // If we had an error doing the request
                Debug.Log("Error checking receipt: " + receiptRequest.Exception.Message);
            }
            yield return new WaitForSeconds(5);
        }
    }


}
