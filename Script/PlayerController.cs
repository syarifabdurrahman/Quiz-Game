using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerController Instance;
    private Stats PlayerStats;
    private Rigidbody2D r2b;
    public float Speed;

    public GameManager gameManager;
    public Animator anim;

    public GameObject SwordEffect;

    public GameObject[] TheEnemy;
    int count;

    private bool IsAttack;

    private void Awake()
    {
        Instance = this;
        
       
    }

    // Start is called before the first frame update
    void Start()
    {
       

        r2b = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    public void Movement()
    {
        r2b.velocity = new Vector2(Speed, r2b.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) || gameManager.Isbattle)
        {
            anim.SetTrigger("IsAttack");
            StartCoroutine(AttacFx());
            gameManager.Isbattle = false;
        }

    }

    IEnumerator AttacFx()
    {
        SwordEffect.SetActive(true);
        yield return new WaitForSeconds(.2f);
        SwordEffect.SetActive(false);
    }

    void Attack()
    {
        if (gameManager.Isbattle)
        {

            StartCoroutine(AttackEnemy());
        }
    }

    IEnumerator AttackEnemy()
    {
        transform.Translate(transform.position.x + 3f, transform.position.y, transform.position.z);
        yield return null;
        transform.Translate(transform.position.x - 3f, transform.position.y, transform.position.z);
    }
}
