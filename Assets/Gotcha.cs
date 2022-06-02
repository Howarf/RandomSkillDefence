using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gotcha : MonoBehaviour
{
    //GameObject GameMoney;
    void OnMouseDown()
    {
        //GameMoney = GameObject.Find("GameManager").GetComponent<>
        //GameManager.manager.money -= 100;
        //if()
        SkillManager.manager.PushSkills();
    }
}
