using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCircle : MonoBehaviour
{
    Defender m_Defender;
    void Start()
    {
        m_Defender = GetComponentInParent<Defender>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attacker" && m_Defender.IsActive)
        {
            m_Defender.Target = other.gameObject;

        }
    }
}
