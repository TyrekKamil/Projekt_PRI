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

    public float moveSpeed = 20f;

    bool jump = false;
    private int direction = 0;
    public Transform actionPoint;
    public float attackRange = 2.5f;
    public LayerMask playerLayer;
    public LayerMask enemyLayers;
    public LayerMask ropeLayer;
    public int attackDamage = 50;
    public LayerMask boxLayer;
    public float boxMoveRange = 0.5f;
    private bool isOnRope = false;
    void Start()
    {

        if (Statics.sceneWasLeft) {
            gameObject.transform.position = Statics.recentPlayerPosition;
            Statics.sceneWasLeft = false;
            moveWall.OnMinigameCompletion();
        }
        if (Statics.playPuzzle) {
            Statics.playPuzzle = false;
            gameObject.transform.position = Statics.recentPlayerPosition;
        }

        GameEvents.SaveInitiated += inventory.Save;
        GameEvents.LoadInitiated += inventory.Load;
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
        if (Input.GetKeyDown(KeyCode.E)) {
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
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);

        jump = false;

    }
    Collider2D recentCollider;
    void Grab() {
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
    void JumpOutOfRope() {
        Destroy(recentCollider.gameObject.GetComponent<FixedJoint2D>());
        Statics.isOnRope = false;
        Physics2D.IgnoreLayerCollision(8, 12);
    }
    void Attack()
    {
        Debug.Log("Attack");
        animator.SetTrigger("Attack");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(actionPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyHP>().TakeDamage(attackDamage);
        }
    }

    void MoveObject() {
        if (Physics2D.OverlapCircleAll(actionPoint.position, boxMoveRange, boxLayer).Length > 0 && !animator.GetBool("IsJumping")) {
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
}
