using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public float move_speed = 7;
    int direction = 1;
    public float jump_height = 10;
    public float dash_speed = 10;
    bool can_dash = true;
    float dash_timer;

    //import player
    private GameObject player;
    private Status status;
    private Animation animator;

    //physics body
    private Rigidbody2D rigid_body;


    void Awake()
    {
        rigid_body = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rigid_body.bodyType = RigidbodyType2D.Dynamic;
        rigid_body.freezeRotation = true;

        //import player
        player = GameObject.Find("playercharacter");

        //import status
        status = player.GetComponent<Status>();

        //import animation handler
        animator = player.GetComponent<Animation>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {   
        if(status.can_move){
            //left and right movement
            if (Input.GetKey (KeyCode.A)) {

                animator.walking();
                direction = -1;
                rigid_body.velocity = new Vector2(direction * move_speed, rigid_body.velocity.y);

            }else if (Input.GetKey (KeyCode.D)) {

                animator.walking();
                direction = 1;
                rigid_body.velocity = new Vector2(direction * move_speed, rigid_body.velocity.y);

            }else if ((Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.A)) || (!Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A))){

                animator.idle();

            }   
        }
        
        //rigid_body.velocity = new Vector2(-move_speed, rigid_body.velocity.y); //nullify collider glitch
        
        //flip right/left
        transform.rotation = Quaternion.Euler(0, (Mathf.Acos(direction) * (180/Mathf.PI)) ,0);
        
        //jumping
        if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.Space)) && status.can_jump) {  
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, jump_height);
            Debug.Log("Jump");
            status.can_jump = false;
        }
        

        //archaic double jump
        //if ((Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.Space)) && !grounded()) {
        //    status.can_jump = true;
        //}

        //fast fall
        if (Input.GetKey (KeyCode.S) && (!status.grounded || status.on_wall)) {  
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
}
