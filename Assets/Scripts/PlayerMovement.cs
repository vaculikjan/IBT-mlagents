using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PlayerMovement : Agent
{
    private Rigidbody2D rBody;
    private BoxCollider2D boxCollider2D;
    public PhysicsMaterial2D friction, noFriction;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private string axis;

    float lastDistance = 999f;
    

    public Transform Enemy;
    public BoxCollider2D EnemyAttack1;
    public BoxCollider2D EnemyAttack2;
    public BoxCollider2D EnemyParry;
    public float forceMultiplier = 10;

    void Start () {
        rBody = GetComponent<Rigidbody2D>();
        rBody.sharedMaterial = noFriction;
        boxCollider2D =  GetComponent<BoxCollider2D> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator> ();
        if (this.name == "Player 1") {
            axis = "Horizontal";
        }
        else {
            axis = "Horizontal2";
        }

        this.MaxStep = 5000;

    }
    
    public override void OnEpisodeBegin() {
        if (this.name == "Player 1") {
            this.transform.localPosition = new Vector3(-2f,2f,0f);
        }
        else {
            this.transform.localPosition = new Vector3(2f,2f,0f);
        }
        
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(Enemy.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(EnemyAttack1.enabled);
        sensor.AddObservation(EnemyAttack2.enabled);
        sensor.AddObservation(EnemyParry.enabled);
    }

    public override void OnActionReceived(float[] vectorAction) {
        
        //Actions
        int movement = Mathf.FloorToInt(vectorAction[0]);
        int action = Mathf.FloorToInt(vectorAction[0]);
        int direction = 0;

        if (movement == 1) { direction = -1; }
        if (movement == 2) { direction = 1; }

        if(!isInAction()) {
            if (action == 3) { Attack1(); }
            if (action == 4) { Attack2(); }
            if (action == 5) { Parry(); }
        }

        Move(direction);

        /*
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Enemy.localPosition);

        if (distanceToTarget < lastDistance) {
            AddReward(0.005f);
        }

        else if (distanceToTarget > lastDistance) {
            AddReward(-0.005f);
        }

        lastDistance = distanceToTarget;
        */
    }

    public override void Heuristic(float[] actionsOut) {
        actionsOut[0] = 0;
        int input = Mathf.FloorToInt(Input.GetAxisRaw(axis));
        if (input == -1) { actionsOut[0] = 1; }
        if (input == 1) { actionsOut[0] = 2; }
        if ((Input.GetKey(KeyCode.J) && this.name == "Player 1") || (Input.GetKey(KeyCode.Delete) && this.name == "Player 2")) {
            actionsOut[0] = 3;
        }
        if ((Input.GetKey(KeyCode.K) && this.name == "Player 1") || (Input.GetKey(KeyCode.End) && this.name == "Player 2")) {
            actionsOut[0] = 4;
        }
        if ((Input.GetKey(KeyCode.L) && this.name == "Player 1") || (Input.GetKey(KeyCode.PageDown) && this.name == "Player 2")) {
            actionsOut[0] = 5;
        }
    }

    private void Move(int direction) {

        if(isInAction() || direction == 0) {
            rBody.sharedMaterial = friction;
            animator.SetFloat("Speed", Mathf.Abs(rBody.velocity.x));
            return;
        }

        rBody.sharedMaterial = noFriction;
        float movement_x = direction;

        rBody.velocity = new Vector2(movement_x * forceMultiplier, rBody.velocity.y);
        
        if ((rBody.velocity.x < 0 && this.name == "Player 1") || (rBody.velocity.x > 0 && this.name == "Player 2")) {
            forceMultiplier = 2.5f;
            animator.SetBool("Forward", false);

        }
        else if ((rBody.velocity.x > 0 && this.name == "Player 1") || (rBody.velocity.x < 0 && this.name == "Player 2")) {
            forceMultiplier = 4f;

            animator.SetBool("Forward", true);
        }

        animator.SetFloat("Speed", Mathf.Abs(rBody.velocity.x));
    }
 
    private bool isInAction() {
        return animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).IsTag("Action");
    }
    
    private void Attack1() {
        if(isInAction()) {
            return;
        }
        //AddReward(-0.05f);
        animator.SetTrigger("Attack");
    }
    private void Attack2() {
        if(isInAction()) {
            return;
        }
        //AddReward(-0.05f);
        animator.SetTrigger("Attack2");
    }
    private void Parry() {
        if(isInAction()) {
            return;
        }
        //AddReward(-0.1f);
        animator.SetTrigger("Parry");
    }
    public void ParryAttack() {
        AddReward(0.6f);
        animator.SetTrigger("Parry_attack");
    }
    
}
