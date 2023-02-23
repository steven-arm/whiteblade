using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private GameObject player;
    private Status status;
    private Controls controls;

    //import animator
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //import player
        player = GameObject.Find("playercharacter");

        //import status
        status = player.GetComponent<Status>();

        //import controller
        controls = player.GetComponent<Controls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void idle(){
        reset_all();
        animator.SetTrigger("Idle");
    }

    public void walking(){
        reset_all();
        animator.SetTrigger("Walking");
    }

    private void reset_all(){
        foreach (var trigger in animator.parameters){
            if (trigger.type == AnimatorControllerParameterType.Trigger){
                animator.ResetTrigger(trigger.name);
            }
        }
    }
}
