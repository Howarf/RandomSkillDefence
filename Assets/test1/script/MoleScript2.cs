using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleScript2 : MonsterBase
{
    void Start()
    {
        Init(MonsterType.mole2);
    }
    void Update()
    {
        Move();
    }
    
}