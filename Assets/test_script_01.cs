using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_script_01 : MonoBehaviour
{
    public float move_speed = 7;
    int direction = 1;
    public float jump_height = 10;
    bool can_jump = true;
    public float dash_speed = 10;
    bool can_dash = true;
    float dash_timer;
    private Rigidbody2D rigid_body;
    private BoxCollider2D player_collider;
    public PhysicsMaterial2D character_material;
    [SerializeField] private LayerMask platform;
    public Animator animator;



    void Awake()
    {
        rigid_body = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        player_collider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;

        rigid_body.bodyType = RigidbodyType2D.Dynamic;
        rigid_body.freezeRotation = true;

        player_collider.sharedMaterial = character_material;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

        //left and right movement
        if (Input.GetKey (KeyCode.A)) {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Walking");
            direction = -1;
            rigid_body.velocity = new Vector2(direction * move_speed, rigid_body.velocity.y);
        }else if (Input.GetKey (KeyCode.D)) {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Walking");  
            direction = 1;
            rigid_body.velocity = new Vector2(direction * move_speed, rigid_body.velocity.y);
        }else if ((Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.A)) || (!Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A))){
            animator.ResetTrigger("Walking");
            animator.SetTrigger("Idle"); 
        }   
        
        transform.rotation = Quaternion.Euler(0, (Mathf.Acos(direction) * (180/Mathf.PI)) ,0);
        
        //jumping
        if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.Space)) && can_jump) {  
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, jump_height);
            can_jump = false;
        }
        

        //archaic double jump
        //if ((Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.Space)) && !grounded()) {
        //    can_jump = true;
        //}
        
        //ground detection
        if (grounded() || on_wall()){
            can_jump = true;
        }

        //fast fall
        if (Input.GetKey (KeyCode.S) && (!grounded() || on_wall())) {  
            rigid_body.AddForce(new Vector2(0, -10f));
        }

        //dashing
        if (Input.GetKey (KeyCode.LeftShift) && can_dash) {  
            rigid_body.AddForce(new Vector2(dash_speed * direction * rigid_body.velocity.x, 0.1f));
            can_dash = false;
            dash_timer = Time.captureDeltaTime;
        }

        if (Time.captureDeltaTime >= dash_timer + 2){
            can_dash = true;
        }
    }

    private bool grounded(){
        RaycastHit2D raycast = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down, 0.01f, platform);
        //Debug.Log(raycast.collider);
        return (raycast.collider != null);
    }

    private bool on_wall(){
        RaycastHit2D raycast = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size + new Vector3(0.1f,-0.01f,0), 0f, Vector2.down, 0.01f, platform);
        //Debug.Log(raycast.collider);
        return (raycast.collider != null) && (Input.GetKey (KeyCode.D) | Input.GetKey (KeyCode.A));
    }
}
