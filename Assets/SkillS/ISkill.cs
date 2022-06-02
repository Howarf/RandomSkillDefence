using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISkill : MonoBehaviour
{
    private int _skillLevel;
    private bool _onFlag = false;

    public int skillLevel
    {
        get => _skillLevel;
        set
        {
            _skillLevel = value;
            if (_skillLevel <= 0) _onFlag = false;
            else _onFlag = true;
        }
    }
    public bool onFlag => _onFlag;
}
