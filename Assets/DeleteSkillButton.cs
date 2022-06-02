using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSkillButton : MonoBehaviour
{
    void OnMouseDown()
    {
        SkillManager.manager.RemoveSkill();
    }
}
