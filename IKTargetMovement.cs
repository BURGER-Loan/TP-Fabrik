using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargetMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
            transform.position += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            transform.position += Vector3.right;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
            transform.position += Vector3.up;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            transform.position += Vector3.down;    
    }
}
