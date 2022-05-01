using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour, ISoldier
{

    public GameObject Target;
    int i;
    public bool IsActive;

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
       
        if(Target !=null && IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.transform.position.x , 0 , Target.transform.position.z), 1 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attacker"))
        {
           other.GetComponent<Attacker>().IsCaught = true;
            IsActive = false;
            StartCoroutine(Reactivate());
        }
    }
    public void InActivated()
    {
        if(!IsActive)
        transform.position = Vector3.MoveTowards(transform.position, m_StartPostion, 2 * Time.deltaTime);
    }
    IEnumerator Reactivate()
    {
        m_Collider.enabled = false;
        yield return new WaitForSeconds(4);
        IsActive = true;
        m_Collider.enabled = true;

    }
}
