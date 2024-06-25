using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}
public class Sword_Skill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [Header("Bounce Info")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;
    [SerializeField] private float bounceSpeed;

    [Header("Peirce Info")]
    [SerializeField] private int peirceAmount;
    [SerializeField] private float peirceGravity;

    [Header("Spin Info")]
    [SerializeField] private float hitCooldown = .3f;
    [SerializeField] private float maxTravelDistance = 7;
    [SerializeField] private float spinDuration = 2;
    [SerializeField] private float spinGravity = 0.5f;

    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    //[SerializeField] private float swordSpeed;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;

    private Vector2 finalDir;

    protected override void Start()
    {
        base.Start();
        SetUpGravity();
    }

    private void SetUpGravity()
    {
        if(swordType == SwordType.Bounce)
            swordGravity = bounceGravity;
        else if(swordType == SwordType.Pierce)      
            swordGravity = peirceGravity;      
        else if (swordType == SwordType.Spin)
            swordGravity = spinGravity;
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.U))
        {
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
        }
    }
    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();

        if (swordType == SwordType.Bounce)
            newSwordScript.SetUpBounce(true, bounceAmount, bounceSpeed);
        else if (swordType == SwordType.Pierce)
            newSwordScript.SetUpPierce(peirceAmount);
        else if (swordType == SwordType.Spin)
            newSwordScript.SetUpSpin(true, maxTravelDistance, spinDuration, hitCooldown);

        newSwordScript.SetUpSword(finalDir, swordGravity,player, freezeTimeDuration, returnSpeed);
        player.AssignNewSword(newSword);
    }

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 facePosition = new Vector2(100f * -player.facingDir, player.transform.position.y);
        Vector2 direction = playerPosition - facePosition;
        return direction;
    }
}
