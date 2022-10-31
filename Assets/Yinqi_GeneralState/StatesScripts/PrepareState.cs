using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareState : GeneralStateBase
{
    [SerializeField] float autoSwitch = 10f;
    public override void EnterState(GameManager gm)
    { 
        //battle systems should be disabled
        base.EnterState(gm);
        //Show prepare UI
        gm.prepareUI.SetActive(true);
    }
    public override void Process(GameManager gm)
    {
        base.Process(gm);
        gm.timer["prepare"] += Time.deltaTime;
        if(gm.timer["prepare"] >= autoSwitch)
            gm.ChangeGeneralState(gm.battleState);
        gm.prepareTimer.GetComponent<TMPro.TMP_Text>().text = gm.timer["prepare"].ToString();
    }
    /*public abstract void FixedUpdate(GameManager gm)
    { }*/
    public override void ExitState(GameManager gm)
    {
        base.ExitState(gm); 
        //clearUp
        gm.timer["prepare"] = 0f;
        //Deactivate prepare state
        gm.prepareUI.SetActive(false);
    }
}
