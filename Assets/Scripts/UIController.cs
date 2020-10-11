using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    GameMain gameMain;

    public GameObject StartPage;
    // Start is called before the first frame update
    void Start()
    {
        gameMain = GetComponent<GameMain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        StartPage.SetActive(false);
        gameMain.GameStart();
    }

    public void OnClickReplay()
    {
        gameMain.GameReplay();
    }

    public void OnClickReset()
    {
        gameMain.GameReset();
    }
}
