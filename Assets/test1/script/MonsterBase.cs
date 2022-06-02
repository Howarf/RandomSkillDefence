using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBase : MonoBehaviour
{
    // define constant number
    const int DEFAULT_MONSTER_HP = 100;
    const int DEFAULT_NORMAL_MOLES_DEFFENCE = 0;
    const int DEFAULT_STRONG_MOLES_DEFFENCE = 3;
    const int DEFAULT_BOSS_DEFFENCE = 10;
    const int DEFAULT_BOSS_HP = 500;
    const int NO_DAMAGE = 0;

    //몬스터 타입
    public enum MonsterType     //mole1: ~10 stage, mole2: 10~20stage, boss: 10,20stage
    {
        mole1, mole2, boss
    }
    private Transform currentTarget;
    private int currentTargetIdx = 0;

    private bool deathFlag = false;
    public SpriteRenderer molSprite;

    private MonsterType type;
    [Header("MonsterBase : Get From Source")]
    public List<Transform> paths;
    //몬스터 움직이는 속도
    [Header("MonsterBase : Get From Inspector")]
    float speed = 10f;
    float _additionalSpeed = 0f;
    float adaptedSpeed;
    Animator animator;
    [SerializeField] private int _deffence =0;       //방어력
    [SerializeField] private int _hp;       //체력
    [SerializeField] private int appliedDeffence = 0;       //적용된 방어력
    [SerializeField] private int _subtractionDeffence = 0;

    [SerializeField] private float totalDamagePercent = 1f;
    [SerializeField] private float _damageReducedByMultiShot = 0f;
    [SerializeField] private float _damageReducedByChainLightning = 0f;

    public float damageReducedByChainLightning
    {
        get => _damageReducedByChainLightning;
        set => _damageReducedByChainLightning = value;
    }
    public float damageReducedByMultiShot
    {
        get => _damageReducedByMultiShot;
        set => _damageReducedByMultiShot = value;
    }

    public int subtractionDeffence
    {
        get => _subtractionDeffence;
        set
        {
            _subtractionDeffence = value;
        }
    }
    public float additionalSpeed
    {
        get => _additionalSpeed;
        set
        {
            _additionalSpeed = value;
        }
    }
    public MonsterType monsterType { get => type; }
    public int hp
    {
        get => _hp;
        set => _hp = value;
    }
    public int deffence
    {
        get => _deffence;
        set => _deffence = value;
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Init(MonsterType type)
    {
        //몬스터 길찾기
        paths.Add(GameObject.Find("BottomLeft").transform);
        paths.Add(GameObject.Find("BottomRight").transform);
        paths.Add(GameObject.Find("TopRight").transform);
        paths.Add(GameObject.Find("TopLeft").transform);
        this.type = type;
        switch (this.type)      //각 몬스터 방어력, 체력 값 설정
        {
            case MonsterType.mole1:
                hp = DEFAULT_MONSTER_HP + (GameManager.manager.gameWave - 1) * 10;
                deffence = DEFAULT_NORMAL_MOLES_DEFFENCE;
                break;
            case MonsterType.mole2:
                hp = DEFAULT_MONSTER_HP + (GameManager.manager.gameWave - 1) * 15;
                deffence = DEFAULT_STRONG_MOLES_DEFFENCE;
                break;
            case MonsterType.boss:
                hp = DEFAULT_BOSS_HP * GameManager.manager.gameWave / 5;
                deffence = GameManager.manager.gameWave + DEFAULT_BOSS_DEFFENCE;
                break;
        }
        currentTarget = paths[currentTargetIdx];
    }
    //공격 당했을 때
    public void Hit(int damage, bool trueDamage = false, bool isChainLightning = false)
    {
        if (deathFlag) return;
        appliedDeffence = deffence - subtractionDeffence;
        if (trueDamage)
        {
            hp -= damage;
            Messanger.messanger.FloatMessage(Messanger.MessageType.DamageMessage, transform.position, damage.ToString());
        }
        else
        {
            float damagePercent = isChainLightning ? totalDamagePercent - damageReducedByChainLightning : totalDamagePercent - damageReducedByMultiShot;

            if (Mathf.FloorToInt((damage - appliedDeffence) * damagePercent) >= NO_DAMAGE)
            {
                hp -= Mathf.FloorToInt((damage - appliedDeffence) * damagePercent);    //체력 감소
                if (isChainLightning) Messanger.messanger.FloatMessage(Messanger.MessageType.ChainLightningMessage, transform.position,
     ((damage - appliedDeffence) * damagePercent).ToString());
                else Messanger.messanger.FloatMessage(Messanger.MessageType.DamageMessage, transform.position,
                    ((damage - appliedDeffence) * damagePercent).ToString());
            }
            else Messanger.messanger.FloatMessage(Messanger.MessageType.DamageMessage, transform.position, "miss..");
        }
        if (hp <= 0)
        {
            StartCoroutine("Die");
            GameManager.manager.money += GameManager.manager.amountAddedBySkill;                //재화 추가
            Messanger.messanger.FloatMessage(Messanger.MessageType.MoneyGatheringMessage,
                FindObjectOfType<attack>().transform.position + Vector3.back * 2, "+" + GameManager.manager.amountAddedBySkill);
            GameManager.manager.mobs.Remove(gameObject);
            GameManager.manager.remainMoleCount--;
        }
    }
    IEnumerator Die()
    {
        deathFlag = true;
        speed = 0;
        animator.SetTrigger("DeadTrigger");
        StartCoroutine("FadeOutCoroutine");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    IEnumerator FadeOutCoroutine()
    {
        float fadecount = 1;
        while(fadecount > 0f)
        {
            fadecount -= 0.1f;
            yield return new WaitForSeconds(0.1f);
            molSprite.color = new Color(255, 255, 255, fadecount);
        }
    }
    public void Move()
    {
        adaptedSpeed = speed + additionalSpeed;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, adaptedSpeed * 0.2f * Time.deltaTime);
        //두더지가 타겟에 0.0001f만큼 가까워졌을 때 다음 타겟 설정하여 타겟따라 움직인다.
        if ((currentTarget.position - transform.position).magnitude <= 0.0001f)
        {
            if (currentTargetIdx + 1 >= paths.Count)    //한 바퀴 돌 때마다 방어력 1 증가
            {
                deffence++;
            }
            currentTargetIdx = (currentTargetIdx + 1) % paths.Count; //currentTargetIdx => 0,1,2,3중 하나
            animator.SetInteger("PathsNum", currentTargetIdx);
            currentTarget = paths[currentTargetIdx];    //이동 타겟 변경
        }
    }
}