using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public MoveWall moveWall;
    public InventoryObject inventory;
    public DisplayInventory displayInventory;
    float horizontalMove = 0f;
    private PlayerSkills playerSkills;
    public float moveSpeed = 20f;

    bool jump = false;
    public int direction = 0;
    [SerializeField] private Transform actionPoint;
    [SerializeField] private LayerMask Walls;
    public float attackRange = 2.5f;
    public LayerMask playerLayer;
    public LayerMask enemyLayers;
    public LayerMask ropeLayer;
    public int attackDamage = 50;
    public LayerMask boxLayer;

    public float boxMoveRange = 0.5f;
    public float dashForce = 4f;
    private bool isOnRope = false;
    private bool dash = false;
    private bool touchingWall = false;

    public float forcePushX = 10f;
    public float forcePushY = 2f;

    public GameObject bullet;
    public GameObject explodeBullet;
    private bool increaseDMGSkillActivated = false;
    private float scaleSpeed = 1.5f;
    private void Awake()
    {
        playerSkills = new PlayerSkills();
    }
    void Start()
    {

        if (Statics.sceneWasLeft)
        {
            gameObject.transform.position = Statics.recentPlayerPosition;
            Statics.sceneWasLeft = false;
            moveWall.OnMinigameCompletion();
        }
        if (Statics.playPuzzle)
        {
            Statics.playPuzzle = false;
            gameObject.transform.position = Statics.recentPlayerPosition;
        }

        GameEvents.SaveInitiated += inventory.Save;
        GameEvents.LoadInitiated += inventory.Load;
    }
    float dashValue = 0f;
    void Dash()
    {
        dashValue += 0.2f;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + (direction * 0.2f), gameObject.transform.position.y, gameObject.transform.position.z);
    }

    void ShootBullet()
    {
        // TODO cooldown
        Instantiate(bullet, actionPoint.position, actionPoint.rotation).SetActive(true);
    }

    void Explode()
    {
        GameObject explode = Instantiate(explodeBullet, transform.position, transform.rotation);
        explode.SetActive(true);
    }

    void IncreaseDMG()
    {
        randomDMG();
        WaitForSeconds(5);
        attackDamage = 50;
        increaseDMGSkillActivated = false;
    }

    void randomDMG()
    {
        int random = new System.Random().Next(65, 125);
        attackDamage = random;
    }
    void Update()
    {
        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftButton"))))
        {
            direction = -1;
            MoveObject();
        }
        else if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightButton"))))
        {
            direction = 1;
            MoveObject();
        }
        //TODO: Cooldown for skills
        if (dashValue < dashForce && dash && !touchingWall)
        {
            Dash();
        }
        else
        {
            dashValue = 0f;
            dash = false;
            animator.SetBool("Dash", false);
        }
        if (CanUseDash() && Input.GetKeyDown(KeyCode.R) && !dash)
        {
            animator.SetBool("Dash", true);
            dash = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            increaseDMGSkillActivated = true;
            IncreaseDMG();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Explode();
        }

        horizontalMove = direction * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        direction = 0;

        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton"))))
        {
            animator.SetBool("IsJumping", true);
            jump = true;
            if (isOnRope)
            {
                JumpOutOfRope();
                isOnRope = false;
            }
        }
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AttackButton"))))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("IsJumping", false);
            jump = false;
            Grab();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            jump = false;
            if (isOnRope)
            {
                JumpOutOfRope();
                isOnRope = false;
            }
        }
    }
    public PlayerSkills GetPlayerSkills()
    {
        return playerSkills;
    }
    public bool CanUseDash()
    {
        return playerSkills.IsSkillTypeUnlocked(PlayerSkills.SkillType.Dash);
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    void FixedUpdate()
    {
        touchingWall = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(actionPoint.position, .2f, Walls);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                touchingWall = true;
            }

        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;


    }
    Collider2D recentCollider;
    void Grab()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(actionPoint.position, attackRange, ropeLayer);
        if (colliders.Length > 0 && !Statics.isOnRope)
        {
            recentCollider = colliders[0];
            FixedJoint2D joint = colliders[0].gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = gameObject.GetComponent<Rigidbody2D>();
            isOnRope = true;
            Statics.isOnRope = true;
            controller.extraJumps = 1;
            //this.transform.parent = colliders[0].transform;
        }
    }
    void JumpOutOfRope()
    {
        Destroy(recentCollider.gameObject.GetComponent<FixedJoint2D>());
        Statics.isOnRope = false;
        Physics2D.IgnoreLayerCollision(8, 12);
    }
    void Attack()
    {
        Debug.Log(attackDamage);
        animator.SetTrigger("Attack");
        if (increaseDMGSkillActivated)
        {
            randomDMG();
        }
        Collider2D[] enemies = Physics2D.OverlapCircleAll(actionPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyHP>().TakeDamage(attackDamage);
        }
    }

    void MoveObject()
    {
        if (Physics2D.OverlapCircleAll(actionPoint.position, boxMoveRange, boxLayer).Length > 0 && !animator.GetBool("IsJumping"))
        {
            Physics2D.OverlapCircleAll(actionPoint.position, boxMoveRange, boxLayer)[0]
                .GetComponent<BoxMoving>().MoveBox(direction);
            animator.SetTrigger("PushObject");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<GroundItem>();

        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            displayInventory.UpdateSlots();
            Destroy(collision.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("MGround"))
        {
            this.transform.parent = other.transform;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("MGround"))
        {
            this.transform.parent = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (actionPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(actionPoint.position, attackRange);
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[32];
    }

    public void forcePushPlayer()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(forcePushX * direction, forcePushY), ForceMode2D.Force);
    }

    IEnumerator WaitForSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
