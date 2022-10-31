using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : GeneralStateBase
{
    [SerializeField] GameObject[] buttons;
    public override void EnterState(GameManager gm)
    {
        base.EnterState(gm);
        //Show battle UI, enable battle systems
        gm.battleUI.SetActive(true);

        //Open Enemy Generate Sequence (TimeLine)
        gm.sequence[gm.waveNum - 1].SetActive(true);
    }
    public override void Process(GameManager gm)
    {
        base.Process(gm);
        gm.timer["battle"] += Time.deltaTime;
        gm.battleTimer.GetComponent<TMPro.TMP_Text>().text = gm.timer["battle"].ToString();
    }
    /*public abstract void FixedUpdate(GameManager gm)
    { }*/
    public override void ExitState(GameManager gm)
    {
        base.ExitState(gm);
        //clearUp
        gm.timer["battle"] = 0f;
        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (activeEnemies.Length > 0)
        {
            for (int i = 0; i < activeEnemies.Length; i++)
            {
                activeEnemies[i].SetActive(false);
            }
        }
        //Deactivate battle UI
        gm.battleUI.SetActive(false);
        foreach(GameObject button in buttons)
        {
            button.SetActive(false);
        }
        //Close Enemy Generate Sequence (TimeLine)
        foreach (GameObject seq in gm.sequence)
        {
            seq.SetActive(false);
        }
    }
}
