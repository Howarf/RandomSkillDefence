using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    enum SkillEnum      //스킬들
    {
        Empty = -1,
        DamageUp,
        DeffenceDown,
        SpeedUp,
        BitCoin,
        MultiShot,
        CriticalRatioUp,
        EarthQuake,
        ChainLightning,
        HeavyWeapon,
        MissAttack,
        Fail
    }
    const int MAX_SKILL_CAPACITY = 6;

    public static SkillManager manager;

    public attack tower;



    private int _selectedSkill = -1;
    public List<SpriteRenderer> adapedSkillImages;
    public Sprite defaultSkillImage;
    public List<Sprite> skillImages;
    private int currentSkillCounts = 0;
    [SerializeField] private List<SkillEnum> currentSkills;

    [SerializeField] List<ISkill> skills;


    SkillEnum GetRandomSkill()      //스킬 각 확률에 맞게 뽑기
    {
        float randomSkillRatio = Random.Range(0f, 100f);
        SkillEnum skillType;
        if (randomSkillRatio <= 7.5f) skillType = SkillEnum.DamageUp;
        else if (randomSkillRatio <= 12.5f) skillType = SkillEnum.DeffenceDown;
        else if (randomSkillRatio <= 20) skillType = SkillEnum.SpeedUp;
        else if (randomSkillRatio <= 30) skillType = SkillEnum.BitCoin;
        else if (randomSkillRatio <= 35) skillType = SkillEnum.MultiShot;
        else if (randomSkillRatio <= 40) skillType = SkillEnum.CriticalRatioUp;
        else if (randomSkillRatio <= 50) skillType = SkillEnum.EarthQuake;
        else if (randomSkillRatio <= 60) skillType = SkillEnum.ChainLightning;
        else if (randomSkillRatio <= 75) skillType = SkillEnum.HeavyWeapon;
        else if (randomSkillRatio <= 90) skillType = SkillEnum.MissAttack;
        else skillType = SkillEnum.Fail;
        return skillType;
    }
    public void PushSkills()            //스킬 저장
    {
        if (GameManager.GOTCH_MONEY > GameManager.manager.money) return;
        if (currentSkillCounts >= MAX_SKILL_CAPACITY)
        {
            currentSkillCounts = MAX_SKILL_CAPACITY;
            return;
        }
        SkillEnum skillType = GetRandomSkill();
        selectedSkill = -1;
        GameManager.manager.money -= GameManager.GOTCH_MONEY;
        if (skillType == SkillEnum.Fail) /// 20퍼센트 환불
        {
            GameManager.manager.money += Mathf .RoundToInt(GameManager.GOTCH_MONEY * 0.2f);
            return;
        }
        adapedSkillImages[currentSkillCounts].sprite = skillImages[(int)skillType];
        currentSkills[currentSkillCounts++] = skillType;
    }
    public int selectedSkill        //스킬 눌렀을 때 클릭한 스킬 희미하게 보이도록 함.(선택 표시)
    {
        get => _selectedSkill;
        set
        {
            _selectedSkill = value;
            for (int i = 0; i < adapedSkillImages.Count; i++)
            {
                var tempColor = adapedSkillImages[i].material.color;
                if (_selectedSkill == i)
                    tempColor.a = 0.5f;
                else
                    tempColor.a = 1.0f;
                adapedSkillImages[i].material.color = tempColor;
            }
        }
    }

    public void RemoveSkill()       //스킬 제거
    {
        if(selectedSkill != -1 && currentSkillCounts != 0)
        {
            if(currentSkillCounts-1 < selectedSkill)
            {
                selectedSkill = -1;
                return;
            }
            for (int i = selectedSkill; i < currentSkills.Count-1; i++)
            {
                adapedSkillImages[i].sprite = adapedSkillImages[i+1].sprite;
                currentSkills[i] = currentSkills[i + 1];
            }
            currentSkills[--currentSkillCounts] = SkillEnum.Empty;
            adapedSkillImages[currentSkillCounts].sprite = defaultSkillImage;
        }
        selectedSkill = -1;
    }
    private void Awake()
    {
        if(manager == null)
        {
            manager = this;
        }
        else
        {
            print("manage already assigned");
            Application.Quit();
        }
        currentSkills = new List<SkillEnum>();
        for (int i = 0; i < adapedSkillImages.Count; i++)
            currentSkills.Add(SkillEnum.Empty);
        skills.Add(GetComponent<DamageUp>());
        skills.Add(GetComponent<DeffenceDown>());
        skills.Add(GetComponent<SpeedUp>());
        skills.Add(GetComponent<BitCoin>());
        skills.Add(GetComponent<MultiShot>());
        skills.Add(GetComponent<CriticalRatioUp>());
        skills.Add(GetComponent<EarthQuake>());
        skills.Add(GetComponent<ChainLightning > ());
        skills.Add(GetComponent<HeavyWeapon>());
        skills.Add(GetComponent<MissAttack>());
    }

    void ApplyActiveSkill()
    {
        foreach (var skill in skills)
            skill.skillLevel = 0;
        foreach (var item in currentSkills)
        {
            if ((int)item == -1) continue;
            skills[(int)item].skillLevel++;
        }
    }   

    void Update()
    {
        ApplyActiveSkill();
    }
}
