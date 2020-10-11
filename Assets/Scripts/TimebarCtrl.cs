using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimebarCtrl : MonoBehaviour
{

    Slider slider;

    Text txtCurTime;
    Text txtTotalTime;

    float curTime = 0;
    float maxTime = 0;

    void Start()
    {
        txtCurTime = GameObject.Find("txtCurTime").GetComponent<Text>();
        txtTotalTime = GameObject.Find("txtTotalTime").GetComponent<Text>();
        slider = GameObject.Find("Slider").GetComponent<Slider>();

        slider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameDataCenter.curState)
        {
            case GameState.PLAY:
                {
                    curTime += Time.deltaTime;
                    txtCurTime.text = FloatToTimeString(curTime);
                    txtTotalTime.text = FloatToTimeString(curTime);
                    slider.value = 1;
                    maxTime = curTime;
                }
                break;

            case GameState.MAGIC_PLAY:
                {
                    curTime -= Time.deltaTime;
                    curTime = curTime <= 0 ? 0 : curTime; 
                    txtCurTime.text = FloatToTimeString(curTime);
                    txtTotalTime.text = FloatToTimeString(maxTime);
                    slider.value = curTime / maxTime;
                }
                break;
            case GameState.REPLAY:
                {
                    curTime += Time.deltaTime;
                    txtCurTime.text = FloatToTimeString(curTime);
                    txtTotalTime.text = FloatToTimeString(maxTime);
                    slider.value = curTime / maxTime;
                }
                break;
        }   
    }

    public void Replay()
    {
        curTime = 0;
    }

    private string FloatToTimeString(float data)
    {

        int second = (int)data % 60;
        int min = (int)data / 60 % 60;
        int hour = (int)data / 60 / 60;

        if(hour <= 0)
        {
            return string.Format("{0:D2}:{1:D2}", min, second);
        }

        return string.Format("{0:D2}:{1:D2}:{2:D2}", hour ,  min, second);
    }


}
