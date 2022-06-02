using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private const float VERY_NEAR_DISTANCE = 0.0001f;
    public Transform bulletSpawnPoint;
    public GameObject thunderPrefab;

    public GameObject prevTarget = null;
    public GameObject target;
    int damage;     //공격력
    public float speed;

    int additionalChainLightningHit = 0;
    bool isChainLightning = false;

    bool isMissAttack = false;
    bool miss;

    public void Init(int dmg, GameObject target, bool isChainLightning = false, int additionalChainLightningHit = 0,bool isMissAttack = false,bool miss = false)
    {
        bulletSpawnPoint = GameObject.Find("Grid/Tower/AttackPoint").transform;
        damage = dmg;
        this.target = target;
        this.additionalChainLightningHit = additionalChainLightningHit;
        this.isChainLightning = isChainLightning;
        this.isMissAttack = isMissAttack;
        this.miss = miss;
    }
    void Update()
    {
        if (gameObject == null || target == null)
        {
            Destroy(gameObject);
            return;
        }
        if (GameManager.manager.mobs.Count != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if ((transform.position - target.transform.position).magnitude <= VERY_NEAR_DISTANCE)
            {
                if(isMissAttack)
                {
                    if(miss)
                    {
                        Messanger.messanger.FloatMessage(Messanger.MessageType.MoneyGatheringMessage, transform.position, "miss...");
                        int targetIdx = GameManager.manager.mobs.IndexOf(target);
                        if (targetIdx + 1 == GameManager.manager.mobs.Count)
                        {
                            Destroy(gameObject);
                            return;
                        }
                        GameObject bulletToNextTarget = Instantiate(gameObject);
                        Vector3 backPostion = GameManager.manager.mobs[targetIdx].transform.position
                            + (GameManager.manager.mobs[targetIdx + 1].transform.position.normalized * 0.05f);
                        bulletToNextTarget.transform.position = backPostion;
                        bulletToNextTarget.GetComponent<bullet>().Init(this.damage, GameManager.manager.mobs[targetIdx + 1]);
                        Destroy(gameObject);
                        return;
                    }
                }
                target.GetComponent<MonsterBase>().Hit(damage);
                if (isChainLightning)
                {
                    for (int i = 0; i < additionalChainLightningHit; i++)
                    {
                        int nextTargetIdx = GameManager.manager.mobs.IndexOf(target) + 1;
                        if (nextTargetIdx >= GameManager.manager.mobs.Count) break;
                        GameObject nextTarget = GameManager.manager.mobs[nextTargetIdx];
                        GameObject thunderEffect = Instantiate(thunderPrefab);
                        thunderEffect.transform.position = target.transform.position;
                        thunderEffect.GetComponent<LineImage>().Init(target.transform, nextTarget.transform);
                        nextTarget.GetComponent<MonsterBase>().Hit(damage, false, true);
                        target = nextTarget;
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
