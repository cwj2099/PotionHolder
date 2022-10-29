using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class distance : MonoBehaviour
{
    [SerializeField] TMP_Text m_Text;
    [SerializeField] Animator anim;
    public float dis;
    [SerializeField] int power;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dis = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        m_Text.text = (dis * power).ToString();
    }
}
