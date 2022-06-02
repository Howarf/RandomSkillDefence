using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalRatioUp : ISkill
{
    private const float LEVEL_1_RATIO = 0.20f;
    private const float LEVEL_2_RATIO = 0.25f;
    private const float LEVEL_3_RATIO = 0.30f;
    private const float LEVEL_4_RATIO = 0.40f;
    private const float LEVEL_5_RATIO = 0.50f;
    private const float LEVEL_6_RATIO = 0.70f;

    public attack tower;

    private void Awake()
    {
        skillLevel = 0;
    }

    void Update()
    {
        if (onFlag) StartApplySkill();
        else tower.criticalRatio = 0f;
    }

    void StartApplySkill()
    {
        switch (skillLevel)
        {
            case 1:
                tower.criticalRatio = LEVEL_1_RATIO;
                break;
            case 2:
                tower.criticalRatio = LEVEL_2_RATIO;
                break;
            case 3:
                tower.criticalRatio = LEVEL_3_RATIO;
                break;
            case 4:
                tower.criticalRatio = LEVEL_4_RATIO;
                break;
            case 5:
                tower.criticalRatio = LEVEL_5_RATIO;
                break;
            case 6:
                tower.criticalRatio = LEVEL_6_RATIO;
                break;
        }
    }
}
