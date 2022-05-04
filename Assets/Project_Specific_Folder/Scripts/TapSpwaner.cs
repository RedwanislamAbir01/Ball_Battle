using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapSpwaner : MonoBehaviour
{
    public GameObject Attacker, Defender;


    GameManager _GmInstance;
    void Start()
    {
        _GmInstance = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Touch handler
        Touch[] touches = Input.touches;
        if (touches.Length > 0 && _GmInstance.IsStarted)
        {
            Touch touch = touches[0];
            if (touch.phase == TouchPhase.Ended)
            {
                ClickDetection(touch.position);
            }
        }

        // Mouse handler
        if (Input.GetMouseButtonDown(0) && _GmInstance.IsStarted) // use InputSystem later
        {
            ClickDetection(Input.mousePosition);
        }
    }
    void ClickDetection(Vector2 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {


            if (hit.transform.tag == "AttackerLand")
            {
                if (_GmInstance.PlayerEnergy > 2)
                {
                    _GmInstance.PlayerEnergy -= Parameters.EnergyCostAttacker;
                    if (_GmInstance.Attacker.isAtk)
                    {

                        GameObject m_attacker = Instantiate(Attacker, hit.point, Quaternion.identity, _GmInstance.Parent.transform);
                        m_attacker.GetComponent<Attacker>().Ball = _GmInstance.Ball;
                        _GmInstance.PAttacker.Add(m_attacker.GetComponent<Attacker>());
                    }
                    else
                    {
                        GameObject m_defender = Instantiate(Defender, hit.point, Quaternion.identity, _GmInstance.Parent.transform);
                    }
                }
            }

            else if (hit.transform.tag == "DefenderrLand")
            {
                if (_GmInstance.EnemyEnergy > 3)
                {
                    _GmInstance.EnemyEnergy -= Parameters.EnergyCostDefender;
                    if (_GmInstance.Defender.isAtk)
                    {
                        GameObject m_attacker = Instantiate(Attacker, hit.point, Quaternion.identity, _GmInstance.Parent.transform);
                        m_attacker.GetComponent<Attacker>().Ball = _GmInstance.Ball;
                        _GmInstance.PAttacker.Add(m_attacker.GetComponent<Attacker>());
                       
                    }
                    else
                    {
                        GameObject m_defender = Instantiate(Defender, hit.point, Quaternion.identity, _GmInstance.Parent.transform);
                        _GmInstance.PDefender.Add(m_defender.GetComponent<Defender>());
                    }
                }
            }
        }

    }
}
