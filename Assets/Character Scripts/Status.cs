using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    //public statusses (idk if thats the plural)
    public bool grounded;
    public bool on_wall;
    public bool can_jump;
    public bool lag;
    public bool can_move = true;

    //used for raycast
    private BoxCollider2D player_collider;
    public PhysicsMaterial2D character_material;
    [SerializeField] private LayerMask platform;

    //default constructor
    void Awake(){
        player_collider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;

        //player_collider.offset = new Vector2(-0.07f, 2.92f);
        //player_collider.size = new Vector2(4.3f, 9.8f);
        player_collider.sharedMaterial = character_material;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        grounded = check_grounded();
        on_wall = check_on_wall();
        can_jump = check_can_jump();
    }

    private bool check_grounded(){
        RaycastHit2D raycast = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down, 0.01f, platform);
        //Debug.Log(raycast.collider);
        return (raycast.collider != null);
    }

    private bool check_on_wall(){
        RaycastHit2D raycast = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size + new Vector3(0.1f,-0.01f,0), 0f, Vector2.down, 0.01f, platform);
        //Debug.Log(raycast.collider);
        return (raycast.collider != null) && (Input.GetKey (KeyCode.D) | Input.GetKey (KeyCode.A));
    }

    private bool check_can_jump(){
        if (grounded || on_wall){
            return true;
        }
        return false;
    }
}
