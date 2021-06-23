using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;


public static class GameProperty {

    private const string keyStartTime = "S";
    private const string keyFinishF = "F";


    private const string keyPlayerWin = "P";

    private static readonly Hashtable propsToSet = new Hashtable();

    // ゲームの開始時刻が設定されていれば取得する
    public static bool GetStartTime(this Room room, out int timestamp) {
        if (room.CustomProperties[keyStartTime] is int value) {
            timestamp = value;
            return true;
        } else {
            timestamp = 0;
            return false;
        }
    }

    // ゲームの開始時刻を設定する
    public static void SetStartTime(this Room room, int timestamp) {
        propsToSet[keyStartTime] = timestamp;
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }


    public static bool GetFinishF(this Room room, out bool flag){

        if (room.CustomProperties[keyFinishF] is bool val) {
            flag = val;
            return true;
        } else {
            flag = false;
            return false;
        }
    }

    public static void SetFinishF(this Room room, bool flag){
        propsToSet[keyFinishF] = flag;
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }


    public static bool GetWinCount(this Player player, out int cnt){
        if(player.CustomProperties[keyPlayerWin] is int val){
            cnt = val;
            return true;
        }else{
            cnt = -1;
            return false;
        }
    }

    public static void SetWinCount(this Player player, int cnt){
        propsToSet[keyPlayerWin] = cnt;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
}

