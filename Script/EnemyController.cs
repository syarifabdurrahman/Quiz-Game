using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float playerDetectionRange = 5f;
    public float playerDeathZone = 1f;
    public float patrolDistance = 5f;
    public float speed = 1f;

    private GameManager gameManager;

    public float waitToReload;
    private bool reloading;
    private GameObject ThePlayer;

    private BoxCollider2D collider2D;

    public bool SwitchSideBattle = false;
    // Using a multiplyer to avoid having another var
    // for walking speed and anothor one for current speed.
    public float runningSpeedMultiplyer = 1.5f;

    Animator playerAnim;
    Animator anim;

    // Patroling
    Vector3[] patrolPositions;
    int patrolPositionsIndex;

    Vector3 startPosition;
    Vector3 targetPosition;
    static Transform player;
    bool isTargetingPlayer = false;


    //DeadPanel
    public GameObject Deadpanel;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        #region Patrol
        collider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        startPosition = transform.position;


        patrolPositions = new Vector3[] {
             new Vector3(startPosition.x + patrolDistance, startPosition.y, startPosition.z)

         };


        // To save preformance if your game has only 1 player,
        // then change it in the declearation to be:
        // static Transform player;
        // and add that here:
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        #endregion


    }

    void Update()
    {


        if (IsPlayerInDetectionRange())
        {
            // The player is moving so we need to update the every frame.
            SetTargetPosition(player.position);

            // Will only not target the player while in range
            // once he enters the detection range.
            if (!isTargetingPlayer)
            {
                isTargetingPlayer = true;
                OnPlayerEnter();
            }

            if (IsPlayerInAttackRange())
            {
                // Do whatever you want in here to
                // indicate that the player is dead.

                // player.Kill();
            }

            if (reloading)
            {
                waitToReload -= Time.deltaTime;
                if (waitToReload < 0)
                {
                    Application.LoadLevel(0);
                    ThePlayer.SetActive(true);
                }
            }
        }
        // If not in range.
        else
        {
            // will only taget the player while not in range
            // once he leaves the detection range.
            if (isTargetingPlayer)
            {
                isTargetingPlayer = false;
                OnPlayerLeave();
            }

            // Patrol only while no player in range.
            Patrol();
        }

        MoveToTargetPosition();


    }

    bool IsPlayerInDetectionRange()
    {
        if (Vector2.Distance(transform.position, player.position) < playerDetectionRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsPlayerInAttackRange()
    {
        if (Vector2.Distance(transform.position, player.position) < playerDeathZone)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnPlayerEnter()
    {
        speed += runningSpeedMultiplyer;
    }

    void OnPlayerLeave()
    {
        speed /= runningSpeedMultiplyer; // same as (speed = speed / runningSpeedMultiplyer)
    }

    void Patrol()
    {
        // Putting it in a var just to make it easier to read.
        Vector3 currentPatrolPosition = patrolPositions[patrolPositionsIndex];

        // If we are not patroling (if after chasing a player)
        if (targetPosition != currentPatrolPosition)
        {
            SetTargetPosition(currentPatrolPosition);
        }

        // If we have reached our target patrol, then switch to the next one.
        if (transform.position == currentPatrolPosition)
        {
            patrolPositionsIndex++; // same as (patrolPositionsIndex = patrolPositionsIndex + 1)

            // We currently have 2 patrol position stored in an array,
            // if the index is bigger than or equal to the length of the array (2),
            // then we will start from the beggining (index 0).
            if (patrolPositionsIndex >= patrolPositions.Length)
                patrolPositionsIndex = 0;

            SetTargetPosition(patrolPositions[patrolPositionsIndex]);
        }
    }

    void SetTargetPosition(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;

        //// if is going left
        //if (transform.position.x < targetPosition.x)
        //{
        //    animator.SetInteger("Direction", 3);
        //}
        //// if is going right
        //else
        //{
        //    animator.SetInteger("Direction", 1);
        //}
    }

    void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            collision.GetComponent<Stats>().Damage(1);
            playerAnim.SetTrigger("IsHit");
            if (collision.GetComponent<Stats>().PlayerCurrentHealth <= 0)
            {
                StartCoroutine(Wait());
                Debug.Log("PlayerDead");
            }
        }
        if (collision.gameObject.tag == "SwordFX")
        {
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    {
        collider2D.isTrigger = false;
        anim.SetTrigger("HitByPlayer");
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

    IEnumerator Wait()
    {

        playerAnim.SetTrigger("IsHit");
        yield return new WaitForSeconds(.7f);
        Debug.Log("Keloardong");
        gameManager.Dead();
        Destroy(playerAnim.gameObject);

    }

}
