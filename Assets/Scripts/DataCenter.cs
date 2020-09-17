using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter
{
    private static DataCenter mInstance = new DataCenter();

    public static DataCenter getInstance()
    {
        return mInstance;
    }

    private Dictionary<int, ArrayList> datas = new Dictionary<int, ArrayList>();

    public ArrayList getDataByFrameId(int id)
    {
        if (!datas.ContainsKey(id))
        {
            datas.Add(id, new ArrayList());
        }
        return datas[id];
    }

    public void clear()
    {
        datas.Clear();
    }



}
