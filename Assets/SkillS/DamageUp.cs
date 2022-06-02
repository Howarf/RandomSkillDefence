using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : ISkill
{
    public attack tower;

    private void Awake()
    {
        skillLevel = 0;
    }
    void Update()
    {
        if (onFlag) StartApplyingSkill();
        else tower.additionalDamage = 0;
    }

    void StartApplyingSkill()
    {
        switch (skillLevel)
        {
            case 1:
                tower.additionalDamage = 5;
                break;
            case 2:
                tower.additionalDamage = 10;
                break;
            case 3:
                tower.additionalDamage = 15;
                break;
            case 4:
                tower.additionalDamage = 30;
                break;
            case 5:
                tower.additionalDamage = 50;
                break;
            case 6:
                tower.additionalDamage = 300;
                break;
        }
    }

    void CancleApplyingSkill()
    {
        tower.additionalDamage = 0;
    }
}
