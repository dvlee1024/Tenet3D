using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{

    public PlayerController player;
    public PlayerController clonePlayer;
    public Transform doorEnter;
    public Transform doorExist;
    private Timeline timeline;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        timeline = Timeline.getInstance();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
        switch (GameDataCenter.curState)
        {
            case GameState.PLAY:
                {
                    int frameId = timeline.NextFrame();
                    ArrayList list = timeline.getCurFrameData();
                    list.Add(player.getFrame());

                    CheckPlayerEnterDoor();
                }
                break;
            case GameState.REPLAY:
                {
                    int frameId = timeline.NextFrame();
                    print(frameId);
                    ArrayList list = timeline.getCurFrameData();
                    if (list == null)
                    {
                        GameDataCenter.curState = GameState.NONE;
                        break;
                    }
                    if (list.Count > 0)
                    {
                        player.setFrame((Frame)list[0]);
                    }
                    if (list.Count > 1)
                    {
                        clonePlayer.setFrame((Frame)list[1]);
                    }
                }
                break;
            case GameState.MAGIC_PLAY:
                {
                    int frameId = timeline.NextFrame();
                    print(frameId);
                    ArrayList list = timeline.getCurFrameData();
                    if(list == null)
                    {
                        GameDataCenter.curState = GameState.NONE;
                        break;
                    }
                    if (list.Count > 0)
                    {
                        //print(((Frame)list[0]).postion);
                        clonePlayer.setFrame((Frame)list[0]);
                    }

                    list.Add(player.getFrame());
                }
                break;
        }
    }

    private void CheckPlayerEnterDoor()
    {
        float distance = Vector3.Distance(player.transform.position, doorEnter.position);
        if (distance <= 0.2f)
        {
            player.ClearTargetPlace();
            timeline.NextFrame();
            ArrayList list = timeline.getCurFrameData();
            list.Add(player.getFrame());

            RunMagicEvent();
        }
    }

    private void RunMagicEvent()
    {
        timeline.SetInitTimeline(false);
        timeline.TimeReverse();

        player.transform.position = doorExist.position;
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
        
        clonePlayer.gameObject.SetActive(true);
        GameDataCenter.curState = GameState.MAGIC_PLAY;
    }

    public void GameStart()
    {
        player.setCanController(true);
        GameDataCenter.curState = GameState.PLAY;
    }

    public void GameReplay()
    {
        player.setCanController(false);
        timeline.Replay();
        GameDataCenter.curState = GameState.REPLAY;
    }

    public void GameReset()
    {
        player.setCanController(false);
        clonePlayer.gameObject.SetActive(false);
        player.Reset();
        timeline.clear();

        GameStart();
    }
}



