using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    protected PlayerMove playerMove;
    protected GameObject ChatUI;
    protected bool scriptlock = false;
    protected int k = 1;
    protected TextScript ts;
    protected RaycastHit2D lookhit;

    protected void CheckHit()
    {
        // Check condition to start event

        lookhit = Physics2D.Raycast(gameObject.transform.position, Vector2.zero, 0f);

        // Basic : four direction and player is seeing them
        if (playerMove.look == Vector2.down) lookhit = Physics2D.Raycast(gameObject.transform.position, Vector2.up, 1f, LayerMask.GetMask("PlayerEntity"));
        if ((lookhit.collider == null || lookhit.collider.tag != "Player") && playerMove.look == Vector2.up) lookhit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1f, LayerMask.GetMask("PlayerEntity"));
        if ((lookhit.collider == null || lookhit.collider.tag != "Player") && playerMove.look == Vector2.right) lookhit = Physics2D.Raycast(gameObject.transform.position, Vector2.left, 1f, LayerMask.GetMask("PlayerEntity"));
        if ((lookhit.collider == null || lookhit.collider.tag != "Player") && playerMove.look == Vector2.left) lookhit = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 1f, LayerMask.GetMask("PlayerEntity"));
        if (lookhit.collider != null && lookhit.collider.tag == "Player")
        {
            Debug.Log("asdfghjuytrdv");
            StartCoroutine(Fullshow(false));
        }
    }

    protected void Awake()
    {
        // Set variables
        ts = GameObject.Find("GameManager").GetComponent<TextScript>();
        ChatUI = GameObject.Find("Canvas").transform.Find("ChatUI").gameObject;
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    protected virtual IEnumerator Fullshow(bool recursive)
    {
        yield return null;
    }

    protected void Update()
    {
        // Debugging
        Debug.DrawRay(gameObject.transform.position, Vector2.up, Color.red);
        Debug.DrawRay(gameObject.transform.position, Vector2.down, Color.red);
        Debug.DrawRay(gameObject.transform.position, Vector2.left, Color.red);
        Debug.DrawRay(gameObject.transform.position, Vector2.right, Color.red);

        // Get Interaction key
        if (Input.GetKeyDown(KeyCode.C) && scriptlock == false)
        {
            CheckHit();
        }
    }
}
