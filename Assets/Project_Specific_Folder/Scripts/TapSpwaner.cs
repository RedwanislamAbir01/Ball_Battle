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
                GameObject m_attacker = Instantiate(Attacker,hit.point, Quaternion.identity,_GmInstance.Parent.transform);
                m_attacker.GetComponent<Attacker>().Ball = _GmInstance.Ball;

            }
        }

    }
}
