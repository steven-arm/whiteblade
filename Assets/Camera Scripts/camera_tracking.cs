using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_tracking : MonoBehaviour
{
    public GameObject target;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + new Vector3(0,0,-distance);
    }
}
