using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
 
    public GameObject Bar;
    public bool isEnemy = false;
    public void SetColor(bool _isEnemy)
    {
        isEnemy = _isEnemy;
        if (_isEnemy)
        {
            Bar.GetComponent<Image>().color = GameManager.Instance.EnemyColor;
        }
        else
        {
            Bar.GetComponent<Image>().color = GameManager.Instance.PlayerColor;
        }
    }

    public void IncreaseValue(float number)
    {
        RectTransform m_rt = Bar.GetComponent<RectTransform>();
        m_rt.sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().rect.width * number, m_rt.rect.height);
    }
}
