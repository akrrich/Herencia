using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VictorController : CharacterController
{    
    public struct VictorStats
    {
        public float maxLife;
        public float life;
        public float maxShield;
        public float shield;
        public float maxMovementSpeed;
        public float movementSpeed;
        public float maxAttackSpeed;
        public float attackSpeed;
    }

    public VictorStats GetStats()
    {
        var stats = new VictorStats();

        stats.maxLife = maxLife;
        stats.life = life;
        stats.maxShield = maxShield;
        stats.shield = shield;
        stats.maxMovementSpeed = maxMovementSpeed;
        stats.movementSpeed = movementSpeed;
        stats.maxAttackSpeed = maxAttackSpeed;
        stats.attackSpeed = attackSpeed;

        return stats;
    }

    public static VictorStats GetDefaultStats()
    {
        var stats = new VictorStats();

        stats.maxLife = 20;
        stats.life = 20;
        stats.maxShield = 20;
        stats.shield = 0;
        stats.maxMovementSpeed = 30;
        stats.movementSpeed = 10;
        stats.maxAttackSpeed = 30;
        stats.attackSpeed = 5;

        return stats;
    }

    public delegate void StatsChangedHandler(VictorStats stats);
    public event StatsChangedHandler OnStatsChanged;

    public delegate void JournalNotePickedHandler(int noteId, bool isJournal);
    public event JournalNotePickedHandler OnJournalNotePicked;

    public static System.Action<VictorController> OnPersonajeInstanciado;

    private MusicController musicController;


    private void Awake()
    {
        OnPersonajeInstanciado?.Invoke(this);
    }
    // called first
    /*
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.transform.position = GameManager.Instance.startingMap.transform.position;

        print(scene);

        switch (scene.name)
        {
            case "Laboratorio":
                SetDefaultSettings();
                break;

            case "BosqueMuerto":
            case "Palacio":
                LoadCurrentSettings();
                break;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    */


    public void MaxLifeShieldDebug()
    {
        life = maxLife;
        shield = maxShield;
        OnStatsChanged?.Invoke(GetStats());
    }

    public void MaxStatsDebug()
    {
        attackSpeed = maxAttackSpeed;
        movementSpeed= maxMovementSpeed;
        OnStatsChanged?.Invoke(GetStats());
    }

    private void SetCurrentStats()
    {
        VictorStats currentStats = VictorSettings.Instance.victorStats;
        life = currentStats.life;
        shield = currentStats.shield;
        movementSpeed = currentStats.movementSpeed;
        attackSpeed = currentStats.attackSpeed;
    }

    protected override void Start()
    {
        base.Start();

        SetCurrentStats();

        OnStatsChanged?.Invoke(GetStats());

        musicController = FindObjectOfType<MusicController>();
    }

    private void CheckVictory()
    {
    }
    private void GoToLoose()
    {
        SceneManager.LoadScene("Derrota");
    }
    override protected void Die()
    {
        base.Die();
        Invoke(nameof(GoToLoose), 2f);
    }
    override public void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
        OnStatsChanged?.Invoke(GetStats());
        VictorSettings.Instance.UpdateVictorStats(GetStats());
    }

    private IEnumerator PickUpColor(Stats type)
    {
        Color effectColor = Color.green;
        switch (type)
        {
            case Stats.LifePoints:
                effectColor = Color.red;
                break;

            case Stats.Shield:
                effectColor = Color.green;
                break;

            case Stats.characterSpeed:
                effectColor = Color.yellow;
                break;

            case Stats.attackSpeed:
                effectColor = Color.cyan;
                break;

            case Stats.journalEntry:
                effectColor = Color.yellow;
                break;
        }

        // Duración del efecto
        float duration = 0.25f;
        // Tiempo transcurrido
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Interpolación entre rojo y el color original
            spriteRenderer.color = Color.Lerp(effectColor, Color.white, elapsed / duration);
            // Incrementar el tiempo transcurrido
            elapsed += Time.deltaTime;
            // Esperar hasta el próximo frame
            yield return null;
        }

        // Asegurar que el color vuelva al original
        spriteRenderer.color = Color.white;
    }

    internal void PickUpJournalNote(int noteId, bool isJournal)
    {
        StartCoroutine(PickUpColor(Stats.journalEntry));
        OnJournalNotePicked?.Invoke(noteId, isJournal);
    }

    internal void PickUpItem(Stats stats, float points)
    {
        if (!IsAlive)
            return;

        switch (stats)
        {
            case Stats.LifePoints:
                this.life = Mathf.Clamp(this.life + points, 0, maxLife);
                break;
            case Stats.Shield:
                this.shield = Mathf.Clamp(this.shield + points, 0, maxShield);
                break;

            case Stats.characterSpeed:
                this.movementSpeed = Mathf.Clamp(this.movementSpeed + points, 0, maxMovementSpeed);
                break;

            case Stats.attackSpeed:
                this.attackSpeed = Mathf.Clamp(this.attackSpeed + points, 0, maxAttackSpeed);
                break;
        }
        StartCoroutine(PickUpColor(stats));
        OnStatsChanged?.Invoke(GetStats());
        VictorSettings.Instance.UpdateVictorStats(GetStats());
    }
    protected override bool IsAttacking()
    {
        return Input.GetMouseButton(0) || Input.GetButton("Square");
    }
    override protected void ExecuteAttack()
    {
        if(shootSound != null)
            AudioSource.PlayClipAtPoint(shootSound, transform.position);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 shootDirection = (mousePosition - transform.position).normalized;

        BulletController newBullet = Instantiate(bullet, bulletSpawner.position, Quaternion.identity);

        newBullet.Init(this, shootDirection);
    }

    override protected void ExecuteMove()
    {
        rb.velocity = Vector2.zero;
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity += movement * movementSpeed;
    }

    override protected void ExecuteUpdate()
    {
        CheckVictory();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        musicController.Stay(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        musicController.Exit(collision);
    }
}
