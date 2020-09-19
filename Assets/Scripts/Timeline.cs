using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline
{
    private static Timeline mInstance = new Timeline();

    public static Timeline getInstance()
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
