using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using TMPro;
using UnityEngine.UI;
public class UiManager : Singleton<UiManager>
{
    [Header("UI Panels")]
    public GameObject StartUI;
    public GameObject EndUi;
    public GameObject CompleteUI;
    public GameObject InGmePanel;

    public TextMeshProUGUI TimerText;
    [Header("Btns")]
    public Button StartGameBtn;

    GameManager _GmInstance;

    public override void Start()
    {
        base.Start(); 
        StartGameBtn.onClick.AddListener(StartOnClick);
        _GmInstance = GameManager.Instance;
    
    }
    private void StartOnClick()
    {
        _GmInstance.StartMatch();
    }

    private void Update()
    {
        if(!_GmInstance.IsStarted) return;
        _GmInstance.TimeLimit -= Time.deltaTime;
        TimerText.text = _GmInstance.TimeLimit.ToString("0");
    }


}
