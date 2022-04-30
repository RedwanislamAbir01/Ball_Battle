using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 1;
    public Attacker GoTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null && GoTo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, GoTo.transform.position, speed * Time.deltaTime);
            if (transform.position == GoTo.transform.position)
            {
                GoTo = null;
                transform.parent = GoTo.transform;
            }
        }
  
    }
}
