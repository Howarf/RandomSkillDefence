using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonsterBase
{
    void Start()
    {
        Init(MonsterType.boss);
    }
    void Update()
    {
        Move();
    }
    
}
