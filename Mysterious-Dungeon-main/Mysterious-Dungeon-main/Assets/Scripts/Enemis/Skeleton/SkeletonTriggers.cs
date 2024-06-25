using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonTriggers : MonoBehaviour
{
    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>(); 
    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    public void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStat target = hit.GetComponent<PlayerStat>();
                enemy.stats.DoDamage(target);
                //hit.GetComponent<Player>().Damage();
            }
        }
    }

    public void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    public void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
