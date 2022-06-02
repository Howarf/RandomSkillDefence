using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : ISkill
{
    public attack tower;
    private void Awake()
    {
        skillLevel = 0;
    }
    void Update()
    {
        if (onFlag) StartApplyingSkill();
        else tower.delayPercent = 0f;
    }

    void StartApplyingSkill()
    {
        switch (skillLevel)
        {
            case 1:
                tower.delayPercent = 0.5f;
                break;
            case 2:
                tower.delayPercent = 1f;
                break;
            case 3:
                tower.delayPercent = 1.5f;
                break;
            case 4:
                tower.delayPercent = 2f;
                break;
            case 5:
                tower.delayPercent = 2.5f;
                break;
            case 6:
                tower.delayPercent = 3f;
                break;
        }
    }
}
