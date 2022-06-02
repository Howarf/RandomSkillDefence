using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleScript1 : MonsterBase
{
    void Start()
    {
        Init(MonsterType.mole1);
    }
    void Update()
    {
        Move();
    }
    
}
