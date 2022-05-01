using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour, ISoldier
{

    public GameObject Ball;
    public Transform Holder;
    public GameObject HighLighter;


    public int SpwanEnergyPoint
    {
        get => i;
        set => i = value;
    }

    public bool IsAttacking
    {
        get => AttackState;
        set => AttackState = value;
    }

    bool AttackState = true;
    int i;
    GameManager _GmInstance;

    public bool IsCaught;
    public bool IsActive;
    void Start()
    {
        _GmInstance = GameManager.Instance;
        _GmInstance.SetColor(false, transform.GetChild(0).transform.GetComponent<MeshRenderer>() , _GmInstance.PlayerColor);
        _GmInstance.BallHolding = false;
        IsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Activated();
    }
    IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(2.5f);
        IsActive = true;
        if(!_GmInstance.GameOver)
        {
            IsCaught = false;

        }
    }
  public void Activated()
    {
        if (IsActive)
        {
            if (_GmInstance.BallContainer == null && !_GmInstance.GameOver)
            {

                transform.position = Vector3.MoveTowards(transform.position, Ball.transform.position, 2 * Time.deltaTime);
              
                if (Vector3.Distance(transform.position, Ball.transform.position) <= 0.1f)
                {
                    
                    _GmInstance.BallContainer = this.gameObject;
                    Ball.transform.position = Holder.transform.position;
                    Ball.transform.parent = this.transform;
                    HighLighter.SetActive(true);
                }
            }
            else if ((_GmInstance.BallContainer != null) && !_GmInstance.GameOver)  // Running with ball 

            {
               

                if (IsCaught)
                {
                    HighLighter.SetActive(false);
                    Attacker closest = GetNearestAttacker();
                    print(closest);
                    if (closest == null)
                    {
                        _GmInstance.GameOver = true;
                        IsActive = false;                        
                        return;
                    }

                    Vector3 targetDir = closest.transform.position - transform.position;
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, 360, 0f);
                    transform.rotation = Quaternion.LookRotation(newDirection);
                    IsActive = false;
                    _GmInstance.BallContainer = null;
                    Ball.transform.parent = null;            
                    Ball.GetComponent<Ball>().GoTo = closest;
                    StartCoroutine(Reactivate());

                }
                else
                {
                    Vector3 targetDirection = _GmInstance.Gate.transform.position - transform.position;
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0f);
                    transform.rotation = Quaternion.LookRotation(Vector3.forward);
                    transform.position = Vector3.forward * 2 * Time.deltaTime;
                }

            }
        }
    }
 public void InActivated()
    {

    }



    Attacker GetNearestAttacker()
    {
        float minDistance = float.PositiveInfinity;
        Attacker m_Attacker = null;
        foreach(Attacker atk in _GmInstance.PAttacker)
        {
            if (!atk.IsActive || atk == this) continue;
            float distance = Vector3.Distance(atk.transform.position, transform.position);
            if(distance < minDistance)
            {
                m_Attacker = atk;
                minDistance = distance;
            }


        }
        return m_Attacker;
    }

}
