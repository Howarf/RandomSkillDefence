using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private bool clickCheck = false;
    void OnMouseDown()
    {
        clickCheck = !clickCheck;
        if (clickCheck)
        {
            switch (this.name)
            {
                case "Skill1":
                    SkillManager.manager.selectedSkill = 0;
                    break;
                case "Skill2":
                    SkillManager.manager.selectedSkill = 1;
                    break;
                case "Skill3":
                    SkillManager.manager.selectedSkill = 2;
                    break;
                case "Skill4":
                    SkillManager.manager.selectedSkill = 3;
                    break;
                case "Skill5":
                    SkillManager.manager.selectedSkill = 4;
                    break;
                case "Skill6":
                    SkillManager.manager.selectedSkill = 5;
                    break;
            }
        }
        else SkillManager.manager.selectedSkill = 7;
    }
}
