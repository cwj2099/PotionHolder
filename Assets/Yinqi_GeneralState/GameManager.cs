using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GeneralStateBase generalState;
    public PrepareState prepareState;
    public BattleState battleState;
    public PassState passState;
    public FailState failState;
    public WinState winState;

    [Header("Timer and text")]
    public Dictionary<string, float> timer = new Dictionary<string, float>();
    public GameObject prepareUI;
    public GameObject battleUI;
    public GameObject endUI;
    public GameObject prepareTimer;
    public GameObject battleTimer;
    [SerializeField] float startDetectPassTime;
    public GameObject endTimer;

    [Header("EnemySequence")]
    public int waveNum = 1;
    public GameObject[] sequence;

    [Header("PlayerHealth")]
    public int PlayerHealth;
    public int PlayerHealthMax;
    public GameObject HP;

    public void ChangeGeneralState(GeneralStateBase newState)
    {
        if(generalState != null)
        {
            generalState.ExitState(this);
        }
        generalState = newState;
        if(generalState != null)
        {
            generalState.EnterState(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer.Add("prepare", 0f);
        timer.Add("battle", 0f);
        timer.Add("end", 0f);
        PlayerHealth = PlayerHealthMax;
        ChangeGeneralState(prepareState);
    }

    // Update is called once per frame
    void Update()
    {
        generalState.Process(this);
        HP.GetComponent<TMPro.TMP_Text>().text = PlayerHealth.ToString();
    }

    public void switchState(string type)
    {
        switch(type)
        {
            case"prepare":
                ChangeGeneralState(prepareState);
                break;
            case "battle":
                ChangeGeneralState(battleState);
                break;
            case "pass":
                ChangeGeneralState(passState);
                break;
            case "fail":
                ChangeGeneralState(failState);
                break;
        }
    }

}
