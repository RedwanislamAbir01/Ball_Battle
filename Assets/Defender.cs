using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour, ISoldier
{

    public GameObject Target;
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
    }
    private void Update()
    {
        Activated();
    }
    public void Activated()
    {
        if(Target !=null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 1 * Time.deltaTime);
        }
    }
    public void InActivated()
    {

    }
}
