using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailState : GeneralStateBase
{
    public override void EnterState(GameManager gm)
    {
        base.EnterState(gm);
        //END GAME (Show Result?)
        gm.waveNum = 1;
        SceneManager.LoadScene("FailScene");
    }
    public override void Process(GameManager gm)
    {
        base.Process(gm);
        /*gm.timer["end"] += Time.deltaTime;
        gm.endTimer.GetComponent<TMPro.TMP_Text>().text = gm.timer["end"].ToString();*/
    }
    /*public abstract void FixedUpdate(GameManager gm)
    { }*/
    public override void ExitState(GameManager gm)
    {
        base.ExitState(gm);
        //clearUp
        //ChangeScene
        //SceneManager.LoadScene("MainScene");

    }
}

