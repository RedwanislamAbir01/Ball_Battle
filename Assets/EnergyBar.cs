using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    public GameObject Bar;
    void Start()
    {
      
    }

  public void IncreaseValue(float number)
    {
        RectTransform m_rt = Bar.GetComponent<RectTransform>();
        m_rt.sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().rect.width * number, m_rt.rect.height);
    }
}
