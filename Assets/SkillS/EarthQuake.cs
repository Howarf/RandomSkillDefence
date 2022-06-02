using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : ISkill
{
    private const int LEVEL_1_DAMAGE = 50, LEVEL_2_DAMAGE = 70, LEVEL_3_DAMAGE = 90, LEVEL_4_DAMAGE = 120, LEVEL_5_DAMAGE = 150, LEVEL_6_DAMAGE = 300;
    private const float LEVEL_1_POSIBILITY = 0.10f, LEVEL_2_POSIBILITY = 0.20f, LEVEL_3_POSIBILITY = 0.25f;
    private const float LEVEL_4_POSIBILITY = 0.30f, LEVEL_5_POSIBILITY = 0.40f, LEVEL_6_POSIBILITY = 0.7f;

    Vector3 mainCamPos;
    public AudioSource earthQuake_sound;

    private float invocationPossibility = 0f;

    private void Awake()
    {
        skillLevel = 0;
    }

    private void Start()
    {
        mainCamPos = Camera.main.transform.position;
        
        InvokeRepeating("HitEarthQuake", 0f, 4f);
    }
    private void Update()
    {
        if (onFlag) StartApplySkill();
        else invocationPossibility = 0f;
        if (isShaking)
        {
            Vector2 tempCamPos = mainCamPos;
            Vector3 tempPos = tempCamPos + (Random.insideUnitCircle.normalized * Random.Range(-0.015f, 0.015f));
            tempPos.z = -30;
            Camera.main.transform.position = tempPos;
        }
    }

    void HitEarthQuake()
    {
        if (Random.Range(0f, 1f) > invocationPossibility) return;
        ShakeCamera();
        for (int i = 0; i < GameManager.manager.mobs.Count; i++)
        {
            switch (skillLevel)
            {
                case 1:
                    GameManager.manager.mobs[i].GetComponent<MonsterBase>().Hit(LEVEL_1_DAMAGE, true);
                    break;
                case 2:
                    GameManager.manager.mobs[i].GetComponent<MonsterBase>().Hit(LEVEL_2_DAMAGE, true);
                    break;
                case 3:
                    GameManager.manager.mobs[i].GetComponent<MonsterBase>().Hit(LEVEL_3_DAMAGE, true);
                    break;
                case 4:
                    GameManager.manager.mobs[i].GetComponent<MonsterBase>().Hit(LEVEL_4_DAMAGE, true);
                    break;
                case 5:
                    GameManager.manager.mobs[i].GetComponent<MonsterBase>().Hit(LEVEL_5_DAMAGE, true);
                    break;
                case 6:
                    GameManager.manager.mobs[i].GetComponent<MonsterBase>().Hit(LEVEL_6_DAMAGE, true);
                    break;
            }
        }
    }
    bool isShaking = false;
    void ShakeCamera()
    {
        isShaking = true;
        earthQuake_sound.Play();
        Invoke("StopShakeCamera",1f);
    }
    void StopShakeCamera()
    {
        isShaking = false;
        Camera.main.transform.position = mainCamPos;
    }
    void StartApplySkill()
    {
        foreach (var mole in GameManager.manager.mobs)
        {
            switch (skillLevel)
            {
                case 1:
                    invocationPossibility = LEVEL_1_POSIBILITY;
                    break;
                case 2:
                    invocationPossibility = LEVEL_2_POSIBILITY;
                    break;
                case 3:
                    invocationPossibility = LEVEL_3_POSIBILITY;
                    break;
                case 4:
                    invocationPossibility = LEVEL_4_POSIBILITY;
                    break;
                case 5:
                    invocationPossibility = LEVEL_5_POSIBILITY;
                    break;
                case 6:
                    invocationPossibility = LEVEL_6_POSIBILITY;
                    break;
            }
        }
    }
}
