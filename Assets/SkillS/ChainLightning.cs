using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChainLightning : ISkill
{
    private const int LEVEL_1_ADDITIONAL_BULLET = 1, LEVEL_2_ADDITIONAL_BULLET = 1;
    private const int LEVEL_3_ADDITIONAL_BULLET = 2, LEVEL_4_ADDITIONAL_BULLET = 2;
    private const int LEVEL_5_ADDITIONAL_BULLET = 3, LEVEL_6_ADDITIONAL_BULLET = 4;

    private const float LEVEL_1_DAMAGE_REDUCE_C = 0.3f, LEVEL_2_DAMAGE_REDUCE_C = 0f;
    private const float LEVEL_3_DAMAGE_REDUCE_C = 0.3f, LEVEL_4_DAMAGE_REDUCE_C = 0f;
    private const float LEVEL_5_DAMAGE_REDUCE_C = 0.3f, LEVEL_6_DAMAGE_REDUCE_C = 0f;

    private const float LEVEL_1_POSIBILITY_C = 0.30f, LEVEL_2_POSIBILITY_C = 0.40f, LEVEL_3_POSIBILITY_C = 0.40f;
    private const float LEVEL_4_POSIBILITY_C = 0.50f, LEVEL_5_POSIBILITY_C = 0.50f, LEVEL_6_POSIBILITY_C = 1f;

    public attack tower;
    public AudioSource lightningSound;

    private void Awake()
    {
        skillLevel = 0;
    }

    void ReduceDamageByChainLitning(float percent)
    {
        for (int i = 0; i < GameManager.manager.mobs.Count; i++)
        {
            var moleBase = GameManager.manager.mobs[i].GetComponent<MonsterBase>();
            moleBase.damageReducedByChainLightning = percent;
        }
    }
    void Update()
    {
        if (onFlag)
            StartApplySkill();
        else
        {
            tower.additionalNumberOfAttackByChainLightning = 0;
            ReduceDamageByChainLitning(0f);
        }
    }
    void StartApplySkill()
    {
        switch (skillLevel)
        {
            case 1:
                tower.additionalNumberOfAttackByChainLightning = LEVEL_1_ADDITIONAL_BULLET;
                tower.chainLightningInvocationRatio = LEVEL_1_POSIBILITY_C;
                ReduceDamageByChainLitning(LEVEL_1_DAMAGE_REDUCE_C);
                break;
            case 2:
                tower.additionalNumberOfAttackByChainLightning = LEVEL_2_ADDITIONAL_BULLET;
                tower.chainLightningInvocationRatio = LEVEL_2_POSIBILITY_C;
                ReduceDamageByChainLitning(LEVEL_2_DAMAGE_REDUCE_C);
                break;
            case 3:
                tower.additionalNumberOfAttackByChainLightning = LEVEL_3_ADDITIONAL_BULLET;
                tower.chainLightningInvocationRatio = LEVEL_3_POSIBILITY_C;
                ReduceDamageByChainLitning(LEVEL_3_DAMAGE_REDUCE_C);
                break;
            case 4:
                tower.additionalNumberOfAttackByChainLightning = LEVEL_4_ADDITIONAL_BULLET;
                tower.chainLightningInvocationRatio = LEVEL_4_POSIBILITY_C;
                ReduceDamageByChainLitning(LEVEL_4_DAMAGE_REDUCE_C);
                break;
            case 5:
                tower.additionalNumberOfAttackByChainLightning = LEVEL_5_ADDITIONAL_BULLET;
                tower.chainLightningInvocationRatio = LEVEL_5_POSIBILITY_C;
                ReduceDamageByChainLitning(LEVEL_5_DAMAGE_REDUCE_C);
                break;
            case 6:
                tower.additionalNumberOfAttackByChainLightning = LEVEL_6_ADDITIONAL_BULLET;
                tower.chainLightningInvocationRatio = LEVEL_6_POSIBILITY_C;
                ReduceDamageByChainLitning(LEVEL_6_DAMAGE_REDUCE_C);
                break;
        }
    }
}