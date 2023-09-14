using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    private bool Lock = false;
    Vector2 newpositon = new Vector2();
    enum Direction
    {
        up,
        down,
        left,
        right,
        none
    }

    Direction dir = Direction.none;
    Direction lastinput = Direction.none;
    
    void setDirection()
    {
        if (Input.GetButton("left")) dir = Direction.left;
        if (Input.GetButton("right")) dir = Direction.right;
        if (Input.GetButton("up")) dir = Direction.up;
        if (Input.GetButton("down")) dir = Direction.down;

        if (Input.GetButton("left") && lastinput == Direction.left) dir = Direction.left;
        if (Input.GetButton("right") && lastinput == Direction.right) dir = Direction.right;
        if (Input.GetButton("up") && lastinput == Direction.up) dir = Direction.up;
        if (Input.GetButton("down") && lastinput == Direction.down) dir = Direction.down;

        getLastInput();
    }

    void getLastInput()
    {
        if (Input.GetButtonDown("left")) lastinput = Direction.left;
        if (Input.GetButtonDown("right")) lastinput = Direction.right;
        if (Input.GetButtonDown("up")) lastinput = Direction.up;
        if (Input.GetButtonDown("down")) lastinput = Direction.down;
    }

    // Update is called once per frame
    void Update()
    {
        setDirection();
        if (dir != Direction.none && !Lock)
        {
            Lock = true;
            
            if (dir == Direction.left) newpositon = new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y);
            if (dir == Direction.right) newpositon = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
            if (dir == Direction.up) newpositon = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1);
            if (dir == Direction.down) newpositon = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1);

            Debug.Log(dir);

            StartCoroutine("Walking", 1f/MoveSpeed);
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
        dir = Direction.none;

    }
}
