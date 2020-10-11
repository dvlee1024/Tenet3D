using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline
{
    private static Timeline mInstance = new Timeline();

    private int curFrameId = -1;
    private int endFrameId = 0;

    private bool bInitTimeline = true; // 最初写入timeline，没有逆向人
    private bool bReverse = false;

    public static Timeline getInstance()
    {
        return mInstance;
    }

    private Dictionary<int, ArrayList> datas = new Dictionary<int, ArrayList>();

    private ArrayList getDataByFrameId(int id)
    {
        if (id < 0)
        {
            return null;
        }
        if (!bInitTimeline && id > endFrameId)
        {
            return null;
        }
        if (!datas.ContainsKey(id))
        {
            datas.Add(id, new ArrayList());
            endFrameId = id;
        }
        return datas[id];
    }

    public ArrayList getCurFrameData()
    {
        return getDataByFrameId(curFrameId);
    }

    public void SetInitTimeline(bool can)
    {
        bInitTimeline = can;
    }

    public int NextFrame()
    {
        if (bReverse)
        {
            if(curFrameId > 0)
            {
                return --curFrameId;
            } else
            {
                return -1;
            }
        }

        if (bInitTimeline)
        {
            return ++curFrameId;
        }
        else
        {
            if(curFrameId < endFrameId)
            {
                return ++curFrameId;
            } else
            {
                return endFrameId;
            }
        }
    }

    public void Replay()
    {
        bReverse = false;
        curFrameId = -1;
    }

    public bool TimeReverse()
    {
        bReverse = !bReverse;
        return bReverse;
    }

    public void clear()
    {
        datas.Clear();
        endFrameId = 0;
        curFrameId = 0;
        bReverse = false;
        bInitTimeline = true;
    }

    public int GetEndFrameId()
    {
        return endFrameId;
    }





}
