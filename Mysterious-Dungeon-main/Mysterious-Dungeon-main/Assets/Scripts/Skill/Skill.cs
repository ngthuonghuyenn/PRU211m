using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]protected float cooldown;
    protected float cooldownTimer;
    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }else
            return false;
    }

    public virtual void UseSkill()
    {

    }  
    
    protected virtual Transform FindClosestEnemy(Transform _checkTranform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTranform.position, 30);
        Debug.Log(_checkTranform.position.x);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null; 
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTranform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }
        return closestEnemy;
    }

    protected virtual Transform FindClosestEnemyClone()
    {
        Transform _checkTranform = player.transform;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTranform.position, 15);
        Debug.Log(_checkTranform.position.x);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTranform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }
        return closestEnemy;
    }
}
