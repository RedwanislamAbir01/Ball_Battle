using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour, ISoldier
{
    public enum EType
    {
        Normal,
        PenaltyTaker
    }
    public EType AttackerType;
    public GameObject Ball;
    public Transform Holder;
    public GameObject HighLighter;
    public GameObject Indicator;
    public GameObject GreyScaleObj;

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
    public float Speed;
    public FloatingJoystick FloatStick;
    public bool HasBall;
    void Start()
    {
        if (AttackerType == EType.PenaltyTaker)
        {
            FloatStick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();
            return;
        }
        _GmInstance = GameManager.Instance;
        _GmInstance.SetColor(false, transform.GetChild(0).transform.GetComponent<MeshRenderer>() , _GmInstance.PlayerColor);
        _GmInstance.BallHolding = false;
        IsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackerType == EType.Normal)
            Activated();
   

    }
    private void FixedUpdate()
    {
        if (AttackerType != EType.Normal)
            MovementControl();
    }

    void MovementControl()
    {
        float m_Xdir = FloatStick.Horizontal;
        float m_Zdir = FloatStick.Vertical;

        transform.position += new Vector3(-m_Xdir * Speed, transform.position.y, -m_Zdir * Speed);
    }
    IEnumerator Reactivate()
    {
        Indicator.gameObject.SetActive(false);
        GreyScaleObj.SetActive(true);
        yield return new WaitForSeconds(Parameters.ReactivateTimeAtk);
        IsActive = true;
        GreyScaleObj.SetActive(false);
        if (!_GmInstance.GameOver)
        {
            IsCaught = false;

        }
    }
    public void MouseMovementControl()
    {
     
    }
  public void Activated()
    {
        if (IsActive)
        {
            if (_GmInstance.BallContainer == null)
            {
                LookAtTarget(Ball);
                transform.position = Vector3.MoveTowards(transform.position, Ball.transform.position, Parameters.SpeedNormalAtk * Time.deltaTime);
              
                if (Vector3.Distance(transform.position, Ball.transform.position) <= 0.1f)
                {
                
                    _GmInstance.BallContainer = GetComponent<Attacker>();
                    Ball.transform.position = Holder.transform.position;
                    Ball.transform.parent = this.transform;
                    HighLighter.SetActive(true);
                }
            }
            else if ((_GmInstance.BallContainer == GetComponent<Attacker>()))  // Running with ball 

            {

                HasBall = true;
                if (IsCaught)
                {
                    HighLighter.SetActive(false);
                    Attacker closest = GetNearestAttacker();
                    print(closest);
                    if (closest == null)
                    {
                        
                        _GmInstance.GameEnd(false);
                        IsActive = false;                        
                        return;
                    }

                    Vector3 targetDir = closest.transform.position - transform.position;
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, 360, 0f);
                    transform.rotation = Quaternion.LookRotation(newDirection);

                  
                    _GmInstance.BallContainer = null;
                    IsActive = false;
                    Ball.transform.parent = null;            
                    Ball.GetComponent<Ball>().GoTo = closest;
                    StartCoroutine(Reactivate());

                }
                else
                {
                    LookAtTarget(_GmInstance.Gate);
                    
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(_GmInstance.Gate.transform.position.x, 0, _GmInstance.Gate.transform.position.z), Parameters.CarryingSpeed * Time.deltaTime);
                 


                }

            }
            else
            {
                HasBall = false;
               
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _GmInstance.Gate.transform.position.y, _GmInstance.Gate.transform.position.z), Parameters.SpeedNormalAtk * Time.deltaTime);
                LookAtTarget(_GmInstance.Gate);
                if(transform.position.z  == _GmInstance.Gate.transform.position.z)

                {
                    _GmInstance.PAttacker.Remove(this);
                    Destroy(gameObject, 1f);
                }
            
            }
        }
    }
    void LookAtTarget(GameObject g)
    {
        Vector3 targetDirection = g.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
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
    private void OnTriggerEnter(Collider other)
    {
     
        if (AttackerType == EType.PenaltyTaker)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                other.transform.position = Holder.transform.position;
                other.transform.parent = this.transform;
                HasBall = true;
            }
            if (other.gameObject.CompareTag("EGate"))
            {
                PenaltyManager.Instance.isPlayerWin = true;
            }
        }
        else
        {
            if (other.gameObject.CompareTag("EGate"))
            {
                if (_GmInstance.Attacker.isAtk && HasBall)
                {
                    _GmInstance.GameEnd(true);
                }

            }
        }
    }
}
