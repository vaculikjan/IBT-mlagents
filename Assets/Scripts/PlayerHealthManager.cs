using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{   
    public Arena arena;
    public PlayerMovement player;
    public PlayerMovement enemy;
    private Animator playerAnimator;
    public Animator heart1Animator;
    public Animator heart2Animator;
    public Animator heart3Animator;
    public Collider2D parry;
    private bool isColliding = false;
    int health = 3;

    private void Start() {
        playerAnimator = GetComponent<Animator> ();
    }

    private void Update() {
        isColliding = false;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (isColliding) {
            return;
        }
        isColliding = true;
        Collider2D[] colliders = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        if(col.gameObject.tag == "Attack 1") {
            
            col.OverlapCollider(filter, colliders);
            if (col.IsTouching(parry)) {
                player.ParryAttack();
            }
            else {
                health -= 1;
                damageHeart();
                player.AddReward(-0.3f);
                enemy.AddReward(0.3f);
            }
        }
        else if (col.gameObject.tag == "Attack 2") {
            health -= 1;
            player.AddReward(-0.3f);
            enemy.AddReward(0.3f);
            damageHeart();
        }
        else if (col.gameObject.tag == "Parry Attack") {
            player.AddReward(-0.6f);
            health -= 2;
            damageHeart();
        }
    }
    private void damageHeart() {
        if (health < 3) {
            heart1Animator.SetTrigger("Damaged");
        }
        if (health < 2) {
            heart2Animator.SetTrigger("Damaged");
        }
        if (health < 1) {
            heart3Animator.SetTrigger("Damaged");
            //playerAnimator.SetTrigger("Dead");
            enemy.AddReward(5f);
            player.AddReward(-5f);
            arena.resetFight();
        }
        else {
            playerAnimator.SetTrigger("Hit");
        }
    }
    public void resetHealth() {
        heart1Animator.ResetTrigger("Damaged");
        heart2Animator.ResetTrigger("Damaged");
        heart3Animator.ResetTrigger("Damaged");
        health = 3;
    }
}
