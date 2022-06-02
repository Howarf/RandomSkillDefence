using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitCoin : ISkill
{
    private void Awake()
    {
        skillLevel = 0;
    }
    void Update()
    {
        if (onFlag) StartApplyingSkill();
        else GameManager.manager.amountAddedBySkill = 10;
    }

    void StartApplyingSkill()
    {
        switch (skillLevel)
        {
            case 1:
                GameManager.manager.amountAddedBySkill = Random.Range(7, 16);
                break;
            case 2:
                GameManager.manager.amountAddedBySkill = Random.Range(8, 16);
                break;
            case 3:
                GameManager.manager.amountAddedBySkill = Random.Range(9, 16);
                break;
            case 4:
                GameManager.manager.amountAddedBySkill = Random.Range(10, 21);
                break;
            case 5:
                GameManager.manager.amountAddedBySkill = Random.Range(15, 21);
                break;
            case 6:
                GameManager.manager.amountAddedBySkill = Random.Range(20, 25);
                break;

        }
    }
}
