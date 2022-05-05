using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Singleton;
public class PenaltyManager : Singleton<PenaltyManager>
{
    public float Timer;
    float m_TimeLimit = 0f;
    [Header("UI")]
    public TextMeshProUGUI TimerText;
    public GameObject WinPanel;
    public GameObject LossePanel;
    public Button NextButton;
    public Canvas JoyStickCanvas;
    public bool isPlayerWin;


    void Start()
    {
        NextButton.onClick.AddListener(Reset);
        m_TimeLimit = Parameters.TimeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        m_TimeLimit -= Time.deltaTime;
        if(!isPlayerWin)
        TimerText.text = m_TimeLimit.ToString("0");

        if (m_TimeLimit <= 0 && !isPlayerWin)
        {
            JoyStickCanvas.enabled = false;
           LossePanel.gameObject.SetActive(true);
        }
        else if(m_TimeLimit > 0 && isPlayerWin)
        {
            JoyStickCanvas.enabled = false;
            WinPanel.gameObject.SetActive(true);
        }
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("MatchNo", 0);
        PlayerPrefs.SetInt("PlayerLifeTimeScore", 0);
        PlayerPrefs.GetInt("DefenderLifeTimeScore", 0);
        SceneManager.LoadScene(0);
    }
}
