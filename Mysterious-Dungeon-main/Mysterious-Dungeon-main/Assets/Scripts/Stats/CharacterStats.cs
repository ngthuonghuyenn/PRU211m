using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private EtityFX fx;

    [Header("Major stats")]
    public Stat strengh; // 1 point increate damage
    public Stat agility; //1 point increate critrate
    public Stat intelligence; //1 point increate magic
    public Stat vitality; //1 point increate health;s

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critRate;
    public Stat critDamage; // defautlt 150%

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;

    public bool isIgnited;
    public bool isChilled;
    public bool isShocked;

    private float igniteTimer;
    private float chilledTimer;
    private float shockedTimer;


    private float igniteDamageCoolDown = .3f;
    private float igniteDamageTimer;
    private int igniteDamage;
    [SerializeField] private GameObject shockStrikePrefab;
    private int shockDamage;

    public int currentHealth;

    [SerializeField] private float alimentDuration = 2;

    public bool isDead { get; private set; }

    public System.Action onHealthChanged;
    protected virtual void Start()
    {
        critDamage.SetupDefaultValue(150);
        currentHealth = GetMaxHealthValue();

        fx = GetComponent<EtityFX>();
    }

    // Update is called once per frame
    void Update()
    {
        igniteTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;
        if (igniteTimer < 0)
            isIgnited = false;

        if (chilledTimer < 0)
            isChilled = false;

        if (shockedTimer < 0)
            isShocked = false;

        if (isIgnited)
        {
            ApplyIgniteDamage();
        }
    }

    private void ApplyIgniteDamage()
    {
        if (igniteDamageTimer < 0)
        {

            DescreaseHealthBy(igniteDamage);
            if (currentHealth < 0 && !isDead)
                Die();
            igniteDamageTimer = igniteDamageCoolDown;
        }
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;
        int totalDamage = damage.GetValue() + strengh.GetValue();
        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        //DoMagicalDamage(_targetStats);
        _targetStats.TakeDamage(totalDamage);
    }



    public virtual void TakeDamage(int _damage)
    {
        DescreaseHealthBy(_damage);

        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFX");
        if (currentHealth < 0 && !isDead)
        {
            Die();
        }
    }

    protected virtual void DescreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;

        if (onHealthChanged != null)
            onHealthChanged();
    }
    protected virtual void Die()
    {
        isDead = true;
    }
    #region Magic damage and aliment
    public virtual void DoMagicalDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightningDamage = lightningDamage.GetValue();
        Debug.Log(_fireDamage);
        int totalMagicalDamage = _fireDamage + _iceDamage + _lightningDamage + intelligence.GetValue();

        totalMagicalDamage = CheckTargetResitance(_targetStats, totalMagicalDamage);
        _targetStats.TakeDamage(totalMagicalDamage);

        if (Mathf.Max(_fireDamage, _iceDamage, _lightningDamage) <= 0)
            return;

        AttemptyToApplyAliment(_targetStats, _fireDamage, _iceDamage, _lightningDamage);
    }

    private void AttemptyToApplyAliment(CharacterStats _targetStats, int _fireDamage, int _iceDamage, int _lightningDamage)
    {
        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightningDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightningDamage;
        bool canApplyShock = _lightningDamage > _fireDamage && _lightningDamage > _iceDamage;

        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < .5f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAliment(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("Applied Ignite");
                return;
            }
            if (Random.value < .5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyAliment(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("Applied Chill");
                return;
            }
            if (Random.value < .5f && _lightningDamage > 0)
            {
                canApplyShock = true;
                _targetStats.ApplyAliment(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("Applied Shock");
                return;
            }
        }
        if (canApplyIgnite)
        {
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * .2f));
        }

        if (canApplyShock)
            _targetStats.SetupShockStrikeDamage(Mathf.RoundToInt(_lightningDamage * .1f));

        _targetStats.ApplyAliment(canApplyIgnite, canApplyChill, canApplyShock);
    }

    private int CheckTargetResitance(CharacterStats _targetStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

    public void ApplyAliment(bool _ignite, bool _chill, bool _shock)
    {
        bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
        bool canApplyChill = !isIgnited && !isChilled && !isShocked;
        bool canApplyShock = !isIgnited && !isChilled;
        if (_ignite && canApplyIgnite)
        {
            isIgnited = _ignite;
            igniteTimer = alimentDuration;
            fx.IgniteFxFor(alimentDuration);
        }
        if (_chill && canApplyChill)
        {
            isChilled = _chill;
            chilledTimer = alimentDuration;

            float slowPercentage = .2f;
            GetComponent<Entity>().SlowEntityFx(slowPercentage, alimentDuration);
            fx.ChillFxFor(alimentDuration);
        }
        if (_shock && canApplyShock)
        {
            if (!isShocked)
            {
                ApplyShock(_shock);

            }
            else
            {
                if (GetComponent<Player>() != null)
                    return;
                HitNearestTargerWithShockStrike();
            }
        }
    }

    public void ApplyShock(bool _shock)
    {
        if (isShocked)
            return;
        isShocked = _shock;
        shockedTimer = alimentDuration;
        fx.ShockFxFor(alimentDuration);
    }

    public void HitNearestTargerWithShockStrike()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }

            if (closestEnemy == null)// delete if don't want shocked target to be by shock strike
                closestEnemy = transform;
        }


        if (closestEnemy != null)
        {
            GameObject newShockStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);
            newShockStrike.GetComponent<ThunderStrike_Controller>().Setup(shockDamage, closestEnemy.GetComponent<CharacterStats>());
        }
    }

    public void SetupIgniteDamage(int _damage) => igniteDamage = _damage;
    public void SetupShockStrikeDamage(int _damage) => shockDamage = _damage;
    #endregion
    #region Stat Calculations
    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvation = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();
        if (isShocked)
            totalEvation += 20;
        if (Random.Range(0, 100) < totalEvation)
        {

            return true;
        }
        return false;
    }
    private static int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        if (_targetStats.isChilled)
        {
            totalDamage -= Mathf.RoundToInt(_targetStats.armor.GetValue() * .8f);
        }
        else
        {
            totalDamage -= _targetStats.armor.GetValue();

        }
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool CanCrit()
    {
        int totalCriticalRate = critRate.GetValue() + agility.GetValue();

        if (Random.Range(0, 100) <= totalCriticalRate)
        {
            return true;
        }
        return false;
    }
    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritDamage = (critDamage.GetValue() + strengh.GetValue()) * .01f;
        float totalcritDamage = _damage * totalCritDamage;
        return Mathf.RoundToInt(totalcritDamage);
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }
    #endregion
}
