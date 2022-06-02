using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissAttack : ISkill
{
    private const int LEVEL_1_DAMAGE = 10, LEVEL_2_DAMAGE = 15;
    private const int LEVEL_3_DAMAGE = 20, LEVEL_4_DAMAGE = 25;
    private const int LEVEL_5_DAMAGE = 30, LEVEL_6_DAMAGE = 35;

    private const float LEVEL_1_POSIBILITY = 0.50f, LEVEL_2_POSIBILITY = 0.60f;
    private const float LEVEL_3_POSIBILITY = 0.70f,LEVEL_4_POSIBILITY = 0.80f;
    private const float LEVEL_5_POSIBILITY = 0.90f, LEVEL_6_POSIBILITY = 1f;

    public attack tower;

    void Awake()
    {
        skillLevel = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (onFlag) StartApplySkill();
        else
        {
            tower.additionalDamageByMissAttack = 0;
            tower.missAttackInvocationRatio = 0;
        }
    }
    void StartApplySkill()
    {
        switch (skillLevel)
        {
            case 1:
                tower.additionalDamageByMissAttack = LEVEL_1_DAMAGE;
                tower.missAttackInvocationRatio = LEVEL_1_POSIBILITY;
                break;
            case 2:
                tower.additionalDamageByMissAttack = LEVEL_2_DAMAGE;
                tower.missAttackInvocationRatio = LEVEL_2_POSIBILITY;
                break;
            case 3:
                tower.additionalDamageByMissAttack = LEVEL_3_DAMAGE;
                tower.missAttackInvocationRatio = LEVEL_3_POSIBILITY;
                break;
            case 4:
                tower.additionalDamageByMissAttack = LEVEL_4_DAMAGE;
                tower.missAttackInvocationRatio = LEVEL_4_POSIBILITY;
                break;
            case 5:
                tower.additionalDamageByMissAttack = LEVEL_5_DAMAGE;
                tower.missAttackInvocationRatio = LEVEL_5_POSIBILITY;
                break;
            case 6:
                tower.additionalDamageByMissAttack = LEVEL_6_DAMAGE;
                tower.missAttackInvocationRatio = LEVEL_6_POSIBILITY;
                break;
        }
}
}
