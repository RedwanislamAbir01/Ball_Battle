using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UiManager : Singleton<UiManager>
{
    [Header("UI Panels")]
    public GameObject StartUI;
    public GameObject EndUi;
    public GameObject CompleteUI;
    public GameObject InGmePanel;
    public GameObject EnemyEnergBar;
    public GameObject PlayerEnergyBar;
    public GameObject EnergyBarPrefab;

    public TextMeshProUGUI LevelNo;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI PlayerText;
    public TextMeshProUGUI EnemyText;
    public TextMeshProUGUI EnemyPoint;
    public TextMeshProUGUI PlayerPoint;
    [Header("Btns")]
    public Button StartGameBtn;
    public Button NextButton;
    GameManager _GmInstance;

    public override void Start()
    {
        base.Start();

        LevelNo.text = PlayerPrefs.GetInt("MatchNo").ToString();

        StartGameBtn.onClick.AddListener(StartOnClick);
        NextButton.onClick.AddListener(NextOnClick);
        _GmInstance = GameManager.Instance;

    }
    private void NextOnClick()
    {


        _GmInstance.IncreaseMatchNo();
        SceneManager.LoadScene(0);
    }
    private void StartOnClick()
    {
        _GmInstance.StartMatch();
       
        GenerateEnergyBar();
    }

    private void GenerateEnergyBar()
    {
        for (int i = 0; i < 6; i++)
        {

            GameObject PBar = Instantiate(EnergyBarPrefab, PlayerEnergyBar.transform);
            EnergyBar E = PBar.GetComponent<EnergyBar>();
            E.SetColor(false);
            _GmInstance.AddEnergy(false, E);


            GameObject EBar = Instantiate(EnergyBarPrefab, EnemyEnergBar.transform);
            EnergyBar P = EBar.GetComponent<EnergyBar>();
            P.SetColor(true);
            _GmInstance.AddEnergy(true, P);

        }
    }
    public void NameUpdate()
    {
        if (_GmInstance.Attacker.isAtk)
        {
            PlayerText.text = "Attacker"; 
            EnemyText.text = "Defender";
        }
        else
        {
            PlayerText.text = "Defender";
            EnemyText.text = "Attacker";
        }
    }

    private void Update()
    {
        if(!_GmInstance.IsStarted) return;
        _GmInstance.TimeLimit -= Time.deltaTime;
        TimerText.text = _GmInstance.TimeLimit.ToString("0");
    }


}
