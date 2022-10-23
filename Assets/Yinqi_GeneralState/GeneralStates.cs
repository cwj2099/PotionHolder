using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralStateBase
{
    public abstract void EnterState(GameManager gm);
    public abstract void Update(GameManager gm);
    //public abstract void FixedUpdate(GameManager gm);
    public abstract void ExitState(GameManager gm);
}

public class PrepareState : GeneralStateBase
{
    public override void EnterState(GameManager gm)
    { 
        //battle systems should be disabled

        //Show prepare UI
        gm.prepareUI.SetActive(true);
    }
    public override void Update(GameManager gm)
    {
        gm.timer["prepare"] += Time.deltaTime;
        gm.prepareTimer.GetComponent<TMPro.TMP_Text>().text = gm.timer["prepare"].ToString();
    }
    /*public abstract void FixedUpdate(GameManager gm)
    { }*/
    public override void ExitState(GameManager gm)
    {
        //clearUp
        gm.timer["prepare"] = 0f;
        //Deactivate prepare state
        gm.prepareUI.SetActive(false);
    }
}

public class BattleState : GeneralStateBase
{
    public override void EnterState(GameManager gm)
    {
        //Show battle UI, enable battle systems
        gm.battleUI.SetActive(true);
        
        //Open Enemy Generate Sequence (TimeLine)
        gm.sequence.SetActive(true);
    }
    public override void Update(GameManager gm)
    {
        gm.timer["battle"] += Time.deltaTime;
        gm.battleTimer.GetComponent<TMPro.TMP_Text>().text = gm.timer["battle"].ToString();
    }
    /*public abstract void FixedUpdate(GameManager gm)
    { }*/
    public override void ExitState(GameManager gm)
    {
        //clearUp
        gm.timer["battle"] = 0f;
        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(activeEnemies.Length > 0)
        {
            for(int i = 0; i < activeEnemies.Length; i++)
            {
                activeEnemies[i].SetActive(false);
            }
        }
        //Deactivate battle UI
        gm.battleUI.SetActive(false);

        //Close Enemy Generate Sequence (TimeLine)
        gm.sequence.SetActive(false);
    }
}

public class EndState : GeneralStateBase
{
    public override void EnterState(GameManager gm)
    {
        //Show Ending UI
        gm.endUI.SetActive(true);

        //END GAME (Show Result?)

    }
    public override void Update(GameManager gm)
    {
        gm.timer["end"] += Time.deltaTime;
        gm.endTimer.GetComponent<TMPro.TMP_Text>().text = gm.timer["end"].ToString();
    }
    /*public abstract void FixedUpdate(GameManager gm)
    { }*/
    public override void ExitState(GameManager gm)
    {
        //clearUp
        gm.timer["end"] = 0f;
        //Deactivate battle UI
        gm.endUI.SetActive(false);

        //switch to a ending scene or back to title

    }
}