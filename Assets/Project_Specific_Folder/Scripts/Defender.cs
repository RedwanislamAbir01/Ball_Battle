using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour, ISoldier
{

    public GameObject Target;
    public Animator Anim;
    int i;
    public bool IsActive;
    public GameObject DetectionCircle;
    public GameObject GreyScale;
    Vector3 m_StartPostion;
    Collider m_Collider;
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

    GameManager _GmInstance;
    void Start()
    {
        Anim.Play("Idle");
        m_Collider = GetComponent<Collider>();
        IsActive = true;
        m_StartPostion = transform.position;
        _GmInstance = GameManager.Instance;
        _GmInstance.SetColor(true, transform.GetChild(0).transform.GetComponent<MeshRenderer>(), _GmInstance.EnemyColor);
    }
    private void Update()
    {
        Activated();
        InActivated();
    }
    public void Activated()
    {
       
        if(Target !=null && IsActive && Target.GetComponent<Attacker>().HasBall)
        {
            Anim.Play("Run");
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.transform.position.x , 0 , Target.transform.position.z), Parameters.CarryingSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attacker"))
        {
            other.GetComponent<Attacker>().IsCaught = true;
            IsActive = false;
            GreyScale.gameObject.SetActive(true);
            StartCoroutine(Reactivate());
        }
    }
    public void InActivated()
    {
        if (!IsActive)
        {
            Anim.Play("Idle");
            transform.position = Vector3.MoveTowards(transform.position, m_StartPostion, Parameters.ReturnSpeed * Time.deltaTime);
        }
    }
    IEnumerator Reactivate()
    {
        DetectionCircle.gameObject.SetActive(false);
        m_Collider.enabled = false;
        yield return new WaitForSeconds(Parameters.ReactivateTimeDef);
        IsActive = true;
        GreyScale.gameObject.SetActive(false);
        m_Collider.enabled = true;
        DetectionCircle.gameObject.SetActive(true);
    }
}
