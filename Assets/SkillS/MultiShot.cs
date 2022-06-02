using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShot : ISkill
{
    private const int LEVEL_1_ADDITIONAL_ARROWS = 1, LEVEL_2_ADDITIONAL_ARROWS = 1;
    private const int LEVEL_3_ADDITIONAL_ARROWS = 2, LEVEL_4_ADDITIONAL_ARROWS = 2;
    private const int LEVEL_5_ADDITIONAL_ARROWS = 4, LEVEL_6_ADDITIONAL_ARROWS = 4;

    private const float LEVEL_1_DAMAGE_REDUCE = 0.3f, LEVEL_2_DAMAGE_REDUCE = 0.0f;
    private const float LEVEL_3_DAMAGE_REDUCE = 0.3f, LEVEL_4_DAMAGE_REDUCE = 0.0f;
    private const float LEVEL_5_DAMAGE_REDUCE = 0.3f, LEVEL_6_DAMAGE_REDUCE = 0.0f;

    public attack tower;

    private void Awake()
    {
        skillLevel = 0;
    }

    void Update()
    {
        if(onFlag)
            StartApplySkill();
        else
        {
            tower.additionalNumberOfAttackByMultishot = 0;
            ReduceDamageByMultiShot(0f);
        }
    }
    void ReduceDamageByMultiShot(float percent)
    {
        for (int i = 0; i < GameManager.manager.mobs.Count; i++)
        {
            var moleBase = GameManager.manager.mobs[i].GetComponent<MonsterBase>();
            moleBase.damageReducedByMultiShot = percent;
        }

    }
    void StartApplySkill()
    {
        switch (skillLevel)
        {
            case 1:
                tower.additionalNumberOfAttackByMultishot = LEVEL_1_ADDITIONAL_ARROWS;
                ReduceDamageByMultiShot(LEVEL_1_DAMAGE_REDUCE);
                break;
            case 2:
                tower.additionalNumberOfAttackByMultishot = LEVEL_2_ADDITIONAL_ARROWS;
                ReduceDamageByMultiShot(LEVEL_2_DAMAGE_REDUCE);
                break;
            case 3:
                tower.additionalNumberOfAttackByMultishot = LEVEL_3_ADDITIONAL_ARROWS;
                ReduceDamageByMultiShot(LEVEL_3_DAMAGE_REDUCE);
                break;
            case 4:
                tower.additionalNumberOfAttackByMultishot = LEVEL_4_ADDITIONAL_ARROWS;
                ReduceDamageByMultiShot(LEVEL_4_DAMAGE_REDUCE);
                break;
            case 5:
                tower.additionalNumberOfAttackByMultishot = LEVEL_5_ADDITIONAL_ARROWS;
                ReduceDamageByMultiShot(LEVEL_5_DAMAGE_REDUCE);
                break;
            case 6:
                tower.additionalNumberOfAttackByMultishot = LEVEL_6_ADDITIONAL_ARROWS;
                ReduceDamageByMultiShot(LEVEL_6_DAMAGE_REDUCE);
                break;
        }
    }

}
