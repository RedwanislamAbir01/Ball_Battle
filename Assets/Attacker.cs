using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    public GameObject Ball;
    public Transform Holder;

    GameManager _GmInstance;
    void Start()
    {
        _GmInstance = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_GmInstance.BallContainer == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Ball.transform.position, 2 * Time.deltaTime);

            if (Vector3.Distance(transform.position, Ball.transform.position) <= 1)
            {
                _GmInstance.BallContainer = this.gameObject;
                Ball.transform.position = Holder.transform.position;
                Ball.transform.parent = this.transform;

            }
        }
        else
        {
            Vector3 targetDirection = _GmInstance.Gate.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            transform.position = Vector3.MoveTowards(transform.position, _GmInstance.Gate.transform.position, 2 * Time.deltaTime);
        }
    }
}
