using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid2d;
    SpriteRenderer spriteRenderer;
    public Animator anim;
    public Sprite[] idlesprite = new Sprite[3];
    public float MoveSpeed;
    private bool movelock = false;
    Vector2 newpositon = new Vector2();

    Vector2 dir = Vector2.zero;
    public Vector2 look = Vector2.zero;
    Vector2 lastinput = Vector2.zero;

    private void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void setDirection()
    {
        // 1. 버튼을 누르고 있는 방향을 감지
        if (Input.GetButton("left")) dir = Vector2.left;
        if (Input.GetButton("right")) dir = Vector2.right;
        if (Input.GetButton("up")) dir = Vector2.up;
        if (Input.GetButton("down")) dir = Vector2.down;

        // 2. 최근 입력이 우선시 되므로, 최근 입력이 들어왔을 경우, 1을 무시하고 최근 입력으로 다시 방향을 설정한다.
        if (Input.GetButton("left") && lastinput == Vector2.left) dir = Vector2.left;
        if (Input.GetButton("right") && lastinput == Vector2.right) dir = Vector2.right;
        if (Input.GetButton("up") && lastinput == Vector2.up) dir = Vector2.up;
        if (Input.GetButton("down") && lastinput == Vector2.down) dir = Vector2.down;

        getLastInput();
    }

    void getLastInput()
    {
        if (Input.GetButtonDown("left")) lastinput = Vector2.left;
        if (Input.GetButtonDown("right")) lastinput = Vector2.right;
        if (Input.GetButtonDown("up")) lastinput = Vector2.up;
        if (Input.GetButtonDown("down")) lastinput = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        setDirection();

        Debug.DrawRay(gameObject.transform.position, dir, Color.yellow);
        Debug.DrawRay(gameObject.transform.position, look, Color.red);
        if (dir != Vector2.zero && movelock == false)
        {
            movelock = true;

            if (dir == Vector2.left) newpositon = new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y);
            if (dir == Vector2.right) newpositon = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
            if (dir == Vector2.up) newpositon = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1);
            if (dir == Vector2.down) newpositon = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1);

            if (MoveSpeed != 0f)
            {
                look = dir;
                if (dir.x > 0) spriteRenderer.flipX = true;
                if (dir.x < 0) spriteRenderer.flipX = false;
                anim.SetFloat("Horizontal", dir.x);
                anim.SetFloat("Vertical", dir.y);
            }

            RaycastHit2D hit = Physics2D.Raycast(rigid2d.position, dir, 1f, LayerMask.GetMask("Obstacle"));
            
            if (hit.collider == null && MoveSpeed != 0f)
            {
                StartCoroutine("Move", 1f / MoveSpeed);
                Debug.Log(dir);
            }
            else
            {
                movelock = false;
                dir = Vector2.zero;
            }
        }
        else if(dir == Vector2.zero)
        {
            anim.SetFloat("Horizontal", look.x/2f);
            anim.SetFloat("Vertical", look.y/2f);
        }
    }

    IEnumerator Move(float time)
    {
        while(time > 0.0f)
        {
            time -= Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, newpositon, MoveSpeed*2/100); // 5 -> 0.1, 10 -> 0.2

            yield return new WaitForFixedUpdate();
        }

        movelock = false;
        dir = Vector2.zero;
    }

    public void SetIdle(int i)
    {
        if (look == Vector2.down) spriteRenderer.sprite = idlesprite[0];
        if (look == Vector2.left || look == Vector2.right) spriteRenderer.sprite = idlesprite[1];
        if (look == Vector2.up) spriteRenderer.sprite = idlesprite[2];
    }
}
