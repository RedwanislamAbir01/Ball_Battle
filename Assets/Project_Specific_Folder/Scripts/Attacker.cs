using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour, ISoldier
{

    public GameObject Ball;
    public Transform Holder;
    int i;
    public int SpwanEnergyPoint
    {
        get => i;
        set => i = value;
    }

    GameManager _GmInstance;
    void Start()
    {
        _GmInstance = GameManager.Instance;
        _GmInstance.SetColor(false, transform.GetChild(0).transform.GetComponent<MeshRenderer>() , _GmInstance.PlayerColor);
    }

    // Update is called once per frame
    void Update()
    {
        Activated();
    }
  public void Activated()
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
 public void InActivated()
    {

    }
}
