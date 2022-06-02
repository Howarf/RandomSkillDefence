using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyWeapon : ISkill
{


    public attack tower;

    private void Awake()
    {
        skillLevel = 0;
    }
    void Update()
    {
        if (onFlag) StartApplyingSkill();
        else
        {
            tower.delayPercentageByWeapon = 1f;
            tower.multipliedDamage = 1;
        }
    }

    void StartApplyingSkill()
    {
        tower.multipliedDamage = 2;
        switch (skillLevel)
        {
            case 1:
                tower.delayPercentageByWeapon = 0.5f;
                break;
            case 2:
                tower.delayPercentageByWeapon = 0.5f;
                break;
            case 3:
                tower.delayPercentageByWeapon = 0.6f;
                break;
            case 4:
                tower.delayPercentageByWeapon = 0.6f;
                break;
            case 5:
                tower.delayPercentageByWeapon = 0.7f;
                break;
            case 6:
                tower.delayPercentageByWeapon = 0.8f;
                break;
        }
    }

    void CancleApplyingSkill()
    {
        tower.additionalDamage = 0;
    }
}
