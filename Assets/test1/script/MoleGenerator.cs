using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleGenerator : MonoBehaviour
{
    public GameObject mole1Prefab;
    public GameObject mole2Prefab;
    public GameObject bossPrefab;

    public Transform spawnZone;     //두더지 생성장소
    private const int maximumMoleCount = 20;    //두더지  최대 생성갯수
    private const float spawnDelay = 0.5f;  //생성주기
    private float spawnTimer = 0f;  //

    void SpawnMole()        //몬스터 생성 함수
    {
        if (GameManager.manager.gameWave < GameManager.MIDDLE_STAGE)
        {
            AddMole(Instantiate(mole1Prefab, spawnZone),0);
        }
        else if (GameManager.manager.gameWave == GameManager.MIDDLE_STAGE)
        {
            if (GameManager.MAX_FIRST_BOSS_CAPACITY > GameManager.manager.firstBoss.Count)
            {
                GameObject boss = Instantiate(bossPrefab, spawnZone);
                AddMole(boss, 1);
                GameManager.manager.remite_wave = 13;
                GameManager.manager.bossStat = false;
            }
        }
        else if (GameManager.manager.gameWave < GameManager.FINAL_STAGE)
        {
            AddMole(Instantiate(mole2Prefab, spawnZone),0);
        }
        else if ( GameManager.manager.gameWave == GameManager.FINAL_STAGE)
        {
            if (GameManager.MAX_FINAL_BOSS_CAPACITY > GameManager.manager.finalBoss.Count)
            {
                GameObject boss = Instantiate(bossPrefab, spawnZone);
                GameManager.manager.remite_wave = 20;
                GameManager.manager.bossStat = false;
                AddMole(boss,2);
            }
        }
        spawnTimer = 0;
    }
    private void AddMole(GameObject mole,int bossStage)     //몬스터 수 count
    {
        if (bossStage != 0)
        {
            if (bossStage == 1)
            {
                GameManager.manager.firstBoss.Add(mole);
                GameManager.manager.mobs.Add(null);
                for (int i = GameManager.manager.mobs.Count - 1; i > 0; i--)
                    GameManager.manager.mobs[i] = GameManager.manager.mobs[i - 1];
                GameManager.manager.mobs[0] = mole;
                return;
            }
            else if (bossStage == 2)
            {
                GameManager.manager.finalBoss.Add(mole);
                GameManager.manager.mobs.Add(null);
                for (int i = GameManager.manager.mobs.Count - 1; i > 0; i--)
                    GameManager.manager.mobs[i] = GameManager.manager.mobs[i - 1];
                GameManager.manager.mobs[0] = mole;
                return;
            }
        }
        else GameManager.manager.mobs.Add(mole);
        GameManager.manager.remainMoleCount++;
        GameManager.manager.currentMoleCount++;

    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (GameManager.manager.currentMoleCount < maximumMoleCount)
        {
            if(spawnTimer >= spawnDelay)
            {
                SpawnMole();
            }
        }
    }
}
