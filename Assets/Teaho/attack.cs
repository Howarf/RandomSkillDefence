using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public float shootPerSec = 2;
    public GameObject bulletPrefab;
    [SerializeField] private int _damage = 20;
    [SerializeField] private int _additionalDamageByDamageUp = 0;
    [SerializeField] private int appliedDamage = 0;

    [SerializeField] private int _multipliedDamage = 0;

    [SerializeField] private int _addtionalDamageByMissAttack = 0;
    [SerializeField] private float _missAttackInvocationRatio = 0f;

    [SerializeField] private float _delayPercent = 0f;
    [SerializeField] private float _delayPercentageByWeapon = 1f;
    [SerializeField] public float _additionalShotPerSecond = 1f;
    [SerializeField] private float _chainLightningInvocationRatio = 0f;

    [SerializeField] private int baseNumberOfAttack = 1;
    [SerializeField] private int _additionalNumberOfAttackByMultishot = 0;
    [SerializeField] private int _additionalNumberOfAttack = 0;
    [SerializeField] private int _additionalNumberOfAttackByChainLightning = 0;

    [SerializeField] private int _targetNum = 0;

    [SerializeField] private float _criticalRatio = 0f;

    public float missAttackInvocationRatio
    {
        get => _missAttackInvocationRatio;
        set => _missAttackInvocationRatio = value;
    }

    public int additionalDamageByMissAttack
    {
        get => _addtionalDamageByMissAttack;
        set => _addtionalDamageByMissAttack = value;
    }

    public int multipliedDamage
    {
        get => _multipliedDamage;
        set => _multipliedDamage = value;
    }

    public float criticalRatio
    {
        get => _criticalRatio;
        set => _criticalRatio = value;
    }

    public int additionalNumberOfAttackByMultishot
    {
        get => _additionalNumberOfAttackByMultishot;
        set => _additionalNumberOfAttackByMultishot = value;
    }

    public int additionalNumberOfAttack
    {
        get => _additionalNumberOfAttack;
        set => _additionalNumberOfAttack = value;
    }
    public int additionalNumberOfAttackByChainLightning
    {
        get => _additionalNumberOfAttackByChainLightning;
        set => _additionalNumberOfAttackByChainLightning = value;
    }
    public float chainLightningInvocationRatio
    {
        get => _chainLightningInvocationRatio;
        set => _chainLightningInvocationRatio = value;
    }
    public int additionalDamage
    {
        get => _additionalDamageByDamageUp;
        set => _additionalDamageByDamageUp = value;
    }
    public float delayPercent
    {
        get => _delayPercent;
        set => _delayPercent = value;
    }
    public float delayPercentageByWeapon
    {
        get => _delayPercentageByWeapon;
        set => _delayPercentageByWeapon = value;
    }
    public int targetNum
    {
        get => _targetNum;
        set => _targetNum = value;
    }
    public int damage
    {
        get => _damage;
        set => _damage = value;
    }


    void Start()
    {
        _additionalShotPerSecond = 0;
        delayPercentageByWeapon = 1;
        Invoke("Shoot", 1 / ((shootPerSec + _additionalShotPerSecond) * delayPercentageByWeapon));
    }
    bool FindBoss(GameObject mole)
    {
        return mole.CompareTag("Boss");
    }
    //몬스터 공격 함수
    void Shoot()
    {
        if (GameManager.manager.mobs.Count != 0)
        {
            _additionalShotPerSecond = (1 + delayPercent);

            bool doMissAttack = missAttackInvocationRatio >= Random.Range(0f, 1f);
            bool miss = false;
            if (doMissAttack)
            {
                miss = Random.Range(0, 2) == 0;
                //print("1" + miss);
            }
            if(doMissAttack && !miss)
                appliedDamage = (damage + additionalDamage + additionalDamageByMissAttack) * multipliedDamage;
            else appliedDamage = (damage + additionalDamage) * multipliedDamage;

            bool doCritical = Random.Range(0f, 1f) >= 1 - criticalRatio;

            bool doChainLightning = Random.Range(0f, 1f) >= 1 - chainLightningInvocationRatio;

            GameObject bullet = Instantiate(bulletPrefab, transform);
            GameObject targetMole;
            GameObject boss = GameManager.manager.mobs.Find(FindBoss);
            if (boss != null) targetMole = boss;
            else targetMole = GameManager.manager.mobs[0];
            if (doCritical)
            {
                bullet.GetComponent<bullet>().Init(appliedDamage * 2, targetMole, doChainLightning, additionalNumberOfAttackByChainLightning, doMissAttack, miss);
                if (doChainLightning)
                {
                    SkillManager.manager.GetComponent<ChainLightning>().lightningSound.Play();
                }
            }
            else
            {
                bullet.GetComponent<bullet>().Init(appliedDamage, targetMole, doChainLightning, additionalNumberOfAttackByChainLightning, doMissAttack, miss);
                if (doChainLightning)
                {
                    SkillManager.manager.GetComponent<ChainLightning>().lightningSound.Play();
                }
            }
            bullet.transform.position = transform.position;

            List<int> toAttackIdx = new List<int>();
            for (int i = 0; i < GameManager.manager.mobs.Count; i++) toAttackIdx.Add(i);
            int additionalBulletCount = additionalNumberOfAttackByMultishot;
            while (additionalBulletCount > 0)
            {
                int randomNumber = Random.Range(1, toAttackIdx.Count);
                if (randomNumber < 1 || toAttackIdx.Count <= randomNumber) break;
                bullet = Instantiate(bulletPrefab, transform);
                bullet.transform.position = transform.position;
                if (doCritical) bullet.GetComponent<bullet>().Init(appliedDamage, GameManager.manager.mobs[randomNumber]);
                else bullet.GetComponent<bullet>().Init(appliedDamage * 2, GameManager.manager.mobs[randomNumber]);
                additionalBulletCount--;
                toAttackIdx.RemoveAt(randomNumber);
            }
        }
        Invoke("Shoot", 1 / ((shootPerSec + _additionalShotPerSecond) * delayPercentageByWeapon));
    }
}
