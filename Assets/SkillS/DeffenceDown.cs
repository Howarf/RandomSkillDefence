using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffenceDown : ISkill
{
    private void Awake()
    {
        skillLevel = 0;
    }
    void Update()
    {
         if(onFlag)StartApplyingSkill();
    }

    void StartApplyingSkill()
    {
        for (int i = 0; i < GameManager.manager.mobs.Count; i++)
        {
            var mobsInfo = GameManager.manager.mobs[i].GetComponent<MonsterBase>();
            switch (skillLevel)
            {
                case 0:
                    mobsInfo.subtractionDeffence = 0;
                    break;
                case 1:
                    mobsInfo.subtractionDeffence = 1;
                    break;
                case 2:
                    mobsInfo.subtractionDeffence = 5;
                    break;
                case 3:
                    mobsInfo.subtractionDeffence = 10;
                    break;
                case 4:
                    mobsInfo.subtractionDeffence = 15;
                    break;
                case 5:
                    mobsInfo.subtractionDeffence = 20;
                    break;
                case 6:
                    mobsInfo.subtractionDeffence = 50;
                    break;
            }
        }
    }
}
