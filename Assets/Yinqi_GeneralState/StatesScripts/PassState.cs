using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassState : GeneralStateBase
{
    public override void EnterState(GameManager gm)
    {
        base.EnterState(gm);
        if(gm.waveNum >= gm.sequence.Length)
        {
            //winscene
            gm.ChangeGeneralState(gm.winState);
        }
        else
        {
            //Show Ending UI
            gm.endUI.SetActive(true);
            gm.waveNum++;
        }

        //Go Back to Prepare
        gm.ChangeGeneralState(gm.prepareState);
    }
    public override void Process(GameManager gm)
    {
        base.Process(gm);
        gm.timer["end"] += Time.deltaTime;
        gm.endTimer.GetComponent<TMPro.TMP_Text>().text = gm.timer["end"].ToString();
    }
    /*public abstract void FixedUpdate(GameManager gm)
    { }*/
    public override void ExitState(GameManager gm)
    {
        base.ExitState(gm);
        //clearUp
        gm.timer["end"] = 0f;
        //Deactivate battle UI
        gm.endUI.SetActive(false);

        //switch to a ending scene or back to title

    }
}
