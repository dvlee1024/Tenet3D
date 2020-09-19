using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{

    public PlayerController player;
    private DataCenter dataCenter;
    private int frameId = 0;
    private int endFrameId = 0;

    private GameCmd cmd = GameCmd.NONE;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        dataCenter = DataCenter.getInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cmd == GameCmd.PLAY)
        {
            ArrayList list = dataCenter.getDataByFrameId(frameId);
            list.Add(player.getFrame());

            endFrameId = frameId;
            frameId++;
        } else if(cmd == GameCmd.REPLAY)
        {
            ArrayList list = dataCenter.getDataByFrameId(frameId);
            if(list.Count > 0)
            {
                player.setFrame((Frame)list[0]);
            }

            if(frameId < endFrameId)
            {
                frameId++;
            } else
            {
                cmd = GameCmd.NONE;
            }
        }
    }

    public void GameStart()
    {
        player.setCanController(true);
        cmd = GameCmd.PLAY;
    }

    public void GameReplay()
    {
        player.setCanController(false);
        frameId = 0;
        cmd = GameCmd.REPLAY;
    }

    public void GameReset()
    {
        frameId = 0;
        endFrameId = 0;
        player.setCanController(true);
        dataCenter.clear();
    }
}


public enum GameCmd
{
    NONE,
    PLAY,
    STOP,
    REPLAY,
    RESET
}
