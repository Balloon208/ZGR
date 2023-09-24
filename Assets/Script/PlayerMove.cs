using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid2d;
    public float MoveSpeed;
    private bool Lock = false;
    Vector2 newpositon = new Vector2();

    Vector2 dir = Vector2.zero;
    Vector2 lastinput = Vector2.zero;

    private void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    void setDirection()
    {
        if (Input.GetButton("left")) dir = Vector2.left;
        if (Input.GetButton("right")) dir = Vector2.right;
        if (Input.GetButton("up")) dir = Vector2.up;
        if (Input.GetButton("down")) dir = Vector2.down;

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
        if (dir != Vector2.zero && !Lock)
        {
            Lock = true;

            if (dir == Vector2.left) newpositon = new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y);
            if (dir == Vector2.right) newpositon = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
            if (dir == Vector2.up) newpositon = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1);
            if (dir == Vector2.down) newpositon = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1);



            RaycastHit2D hit = Physics2D.Raycast(rigid2d.position, dir, 1f, LayerMask.GetMask("Entity"));
            
            if (hit.collider == null)
            {
                StartCoroutine("Walking", 1f / MoveSpeed);
                Debug.Log(dir);
            }
            else
            {
                Lock = false;
                dir = Vector2.zero;
            }
        }
    }

    IEnumerator Walking(float time)
    {
        while(time > 0.0f)
        {
            time -= Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, newpositon, MoveSpeed*2/100); // 5 -> 0.1, 10 -> 0.2

            yield return new WaitForFixedUpdate();
        }

        Lock = false;
        dir = Vector2.zero;

    }
}
