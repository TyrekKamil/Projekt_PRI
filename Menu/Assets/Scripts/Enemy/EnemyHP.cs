using UnityEngine;
public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;

    public bool isBoss;
    public bool endAction = false;
    public float powerAttack = -5f;
    public float powerAttackY = 2f;
    public bool isAreaLevel = false;
    private bool isAlive = true;
    void Start()
    {
        currentHealth = maxHealth;
        GameEvents.SaveInitiated += SaveEnemyData;
        GameEvents.LoadInitiated += LoadEnemyData;

        if (Statics.isLoadedGame)
        {
            LoadEnemyData();
        }
    }

    public void TakeDamage(int damage)
    {
        endAction = false;
        currentHealth -= damage;
        if (isBoss)
        {
            GetComponent<BossUITakeDamageService>().TakeDamageUI(damage / (maxHealth / 100));
            float timePassed = 0;
            while (timePassed < 1)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(powerAttack * GetComponent<BossController>().direction, powerAttackY), ForceMode2D.Force);
                timePassed += Time.deltaTime;
            }
        }
        else
        {
            float timePassed = 0;
            while (timePassed < 1)
            {
                if (!isAreaLevel)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(5f * GetComponent<EnemyAnimationController>().direction, 2f), ForceMode2D.Force);
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(5f * GetComponent<AreaEnemyAnimationController>().direction, 2f), ForceMode2D.Force);
                }
                timePassed += Time.deltaTime;
            }
            endAction = true;
        }
        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            Die();
        }
    }

    void Die()
    {
        GameEvents.SaveInitiated -= SaveEnemyData;
        GameEvents.LoadInitiated -= LoadEnemyData;
        animator.Play("Rogue_death_01");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<RespawnPlayer>().enabled = false;
        
        //TODO: sprawdzic ifologie
        if (!isBoss)
        {
            GameObject.Find("Player").GetComponent<PlayerUIUpdates>().updateExperience(30);
        }

        if (isBoss)
        {
            GetComponent<BossController>().enabled = false;
        }
        else if (!isAreaLevel)
        {
            GetComponent<EnemyAnimationController>().enabled = false;

        }
        else
        {
            GetComponent<AreaEnemyAnimationController>().enabled = false;
        }

        if (isBoss &&   GetComponent<BossController>().areaLevel)
        {
            GameObject.Find("EndLvl1Trigger").GetComponent<EndLevel1Trigger>().isBossAlive = false;
        }
        if (isAreaLevel)
        {
            GameObject enemyGenerator = GameObject.Find("EnemyGenerator");
            enemyGenerator.GetComponent<AreaEnemyGenerator>().decrementRemainingEnemies();
        }
        this.enabled = false;
        Destroy(gameObject, 0.5f);
    }

    private void SaveEnemyData()
    {
        SerializableEnemy serializedEnemy = new SerializableEnemy()
        {
            enemyName = this.name,
            health = this.currentHealth,
            positionX = transform.position.x,
            positionY = transform.position.y,
            positionZ = transform.position.z,
        };

        SaveLoad.Save<SerializableEnemy>(serializedEnemy, "Enemy" + this.name);
    }

    private void LoadEnemyData()
    {
        if (SaveLoad.SaveExists("Enemy" + this.name))
        {
            SerializableEnemy enemyData = SaveLoad.Load<SerializableEnemy>("Enemy" + this.name);

            currentHealth = enemyData.health;

            Vector3 position;
            position.x = enemyData.positionX;
            position.y = enemyData.positionY;
            position.z = enemyData.positionZ;

            transform.position = position;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        GameEvents.SaveInitiated -= SaveEnemyData;
        GameEvents.LoadInitiated -= LoadEnemyData;
    }


}

[System.Serializable]
public class SerializableEnemy
{
    public string enemyName { get; set; }
    public int health { get; set; }
    public float positionX { get; set; }
    public float positionY { get; set; }
    public float positionZ { get; set; }

}