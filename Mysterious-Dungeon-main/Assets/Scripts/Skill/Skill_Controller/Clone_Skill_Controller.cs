using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private Player player;
    private SpriteRenderer sr;
    [SerializeField] private float colorLoosingSpeed;
    private Animator anim;
    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    private Transform closestEnemy;
    private bool canDulicateClone;
    private int facingDir = 1;
    private float chanceToDuplicate;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
            if (sr.color.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetupClone(Transform _newTransform, float _cloneduration, bool _canAttack, Vector3 _offset, Transform _closestEnemy, bool _canDuplicate, float _changeToDuplicate, Player _player)
    {
        if (_canAttack)
        {
            anim.SetInteger("AttackNumber", Random.Range(1, 3));
        }
        transform.position = _newTransform.position + _offset;
        cloneTimer = _cloneduration;
        player = _player;
        closestEnemy = _closestEnemy;
        FaceClosestTarget();
        canDulicateClone = _canDuplicate;
        chanceToDuplicate = _changeToDuplicate;
    }
    public void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }

    public void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {

                player.stats.DoDamage(hit.GetComponent<CharacterStats>());

                if (canDulicateClone)
                {
                    if (Random.Range(0, 100) < chanceToDuplicate)
                    {
                        SkillManager.instance.clone.CreateClone(hit.transform, new Vector3(2f * facingDir, 0));
                    }
                }

            }
        }
    }
    public void FaceClosestTarget()
    {
        if (closestEnemy != null)         
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                Debug.Log("la sao v");
                facingDir = -1;
                transform.Rotate(0, 180, 0);

            }
        }
    }

}
