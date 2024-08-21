using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;

    SpriteRenderer sprite;
    Rigidbody2D rigid;
    public RuntimeAnimatorController[] animCon;
    Animator anim;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    private void OnEnable()
    {
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (inputVec.x < 0)  sprite.flipX = true;
        if (inputVec.x > 0) sprite.flipX = false;
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;
        GameManager.instance.health -= (Time.deltaTime * 10);

        if(GameManager.instance.health <= 0) { 
            for(int index = 1; index < transform.childCount; index++) {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            sprite.color = new Color(1, 1, 1, 0.4f);
           // anim.SetTrigger("Dead");
        }
    }
}
