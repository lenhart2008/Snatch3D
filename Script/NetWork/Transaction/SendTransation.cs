using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System;
using System.Numerics;
using System.Text;

public static class SendTransation {

    [DllImport("__Internal")]
    private static extern string BuyKey(int keyCount, string Eth);
    [DllImport("__Internal")]
    private static extern string StealKey(string target, string targetETHCount, string useETHCount);
    [DllImport("__Internal")]
    private static extern string WithDraw(string round);
    [DllImport("__Internal")]
    public static extern string TestTransaction(UInt64 value);

    public static void BuyTransaction(int count, string eth)
    {
 
        BuyKey(count, eth);
    }

    public static void SnatchTransaction(string target, string targetETHCount, string useETHCount)
    {
        StealKey( target, targetETHCount, useETHCount);
    }

    public static void DrawTransaction(string round)
    {
        WithDraw(round);
    }
}
