using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndPanel : MonoBehaviour
{

    private void OnEnable()
    {
    
        UiManager.Instance.PlayerPoint.text = PlayerPrefs.GetInt("PlayerLifeTimeScore").ToString();
        UiManager.Instance.EnemyPoint.text = PlayerPrefs.GetInt("DefenderLifeTimeScore").ToString();
    }
}
