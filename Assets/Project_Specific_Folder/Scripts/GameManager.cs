using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    [Header("Colors")]
    public Color PlayerColor;
    public Color EnemyColor;
    public float EnemyEnergy = 0;
    public float PlayerEnergy = 0;

    public GameObject Gate;

    public bool IsStarted;
    public int MatchNo = 0;
    public float Timer;
    public GameObject Parent;
    public GameObject Ball;
    [Header("Fields")]
    public GameObject AttackerField;
    public GameObject DefenderField;
    public Attacker BallContainer;
    [Header("Game Parameters")]
    public float TimeLimit = 0f;
    public bool BallHolding;


    public Player Attacker;
    public Player Defender;


    public List<Attacker> PAttacker = new List<Attacker>();
    public List<Defender> PDefender = new List<Defender>();
    public Field FieldScript;
    UiManager _UiInstance;
     List<EnergyBar> m_enemyEnergy = new List<EnergyBar>();
     List<EnergyBar> m_playerEnergy = new List<EnergyBar>();

    public bool GameOver;
    public int AttackerPoint;
    public int DefenderPoint ;
    public override void Start()
    {

        base.Start();
        TimeLimit = Parameters.TimeLeft;
        FillEnergy(m_enemyEnergy, 0);
        FillEnergy(m_playerEnergy, 0);
        _UiInstance = UiManager.Instance ;
        if (PlayerPrefs.GetInt("MatchNo") == 0)
        {
            PlayerPrefs.SetInt("MatchNo", 1);
        }
        MatchNo = PlayerPrefs.GetInt("MatchNo");

    }

   public void SetColor(bool IsEnemy ,MeshRenderer m, Color c)
    {
        if (IsEnemy)
        {
            m.material.color = c;
        }
        else
        {
            m.material.color = c;
        }
    }
    private void Update()
    {
        if (!IsStarted)
            return;
        if (GameOver) return;

        TimeLimit -= Time.deltaTime;
       _UiInstance.TimerText.text = TimeLimit.ToString("0");
        if (TimeLimit <= 0)
        {
            GameEnd(false, true);
        }



            if (IsStarted)
        {
            if (EnemyEnergy < 6)
            {
                EnemyEnergy += Parameters.EnergyRegenAttacker * Time.deltaTime;
                FillEnergy(m_enemyEnergy, EnemyEnergy);

            }
            if (PlayerEnergy < 6)
            {
                PlayerEnergy += Parameters.EnergyRegenAttacker * Time.deltaTime;
                FillEnergy(m_playerEnergy, PlayerEnergy);
            }
        }
    }
    public void FillEnergy(List<EnergyBar> energyPoints, float value)
    {

        int integerPart = (int)(value);
        float fractionalPart = value - integerPart;
        for (int i = 0; i < integerPart; i++)
        {
            EnergyBar energyPoint = energyPoints[i];
            energyPoint.IncreaseValue(1);
        }
        if (integerPart >= energyPoints.Count) return;
        for (int i = integerPart; i < energyPoints.Count; i++)
        {
            EnergyBar energyPoint = energyPoints[i];
            energyPoint.IncreaseValue(0);
        }
        energyPoints[integerPart].IncreaseValue(fractionalPart);
    }
    public void AddEnergy(bool isEnemy, EnergyBar energyPoint)
    {
        // used by GameUICtrl to add energy
        if (isEnemy)
        {
            m_enemyEnergy.Add(energyPoint);
        }
        else
        {
           m_playerEnergy.Add(energyPoint);
        }
    }
    public void StartMatch()
    {
        IsStarted = true;
        
       _UiInstance.StartUI.SetActive(false);
        _UiInstance.InGmePanel.SetActive(true);
        BallGeberation();
        Attacker = Parameters.PlayerState(true);
        Defender = Parameters.PlayerState(false);
        _UiInstance.NameUpdate();
    }

   public void IncreaseMatchNo()
    {
        
        MatchNo = PlayerPrefs.GetInt("MatchNo");
        print(MatchNo);
        MatchNo++;
        PlayerPrefs.SetInt("MatchNo", MatchNo);
        print(PlayerPrefs.GetInt("MatchNo"));
    }

     void BallGeberation()
    {
        List<Vector3> VerticeList = new List<Vector3>(AttackerField.GetComponent<MeshFilter>().sharedMesh.vertices);
        Vector3 leftTop = AttackerField.transform.TransformPoint(VerticeList[0]);
        Vector3 rightTop = AttackerField.transform.TransformPoint(VerticeList[10]);
        Vector3 leftBottom = AttackerField.transform.TransformPoint(VerticeList[110]);
        Vector3 rightBottom = AttackerField.transform.TransformPoint(VerticeList[120]);
        Vector3 XAxis = rightTop - leftTop;
        Vector3 ZAxis = leftBottom - leftTop;
        
        Vector3 RndPointonPlane = leftTop + XAxis * Random.value + ZAxis * Random.value;
        GameObject g = Instantiate(Ball);
        Ball = g;
        g.transform.position = RndPointonPlane;

        g.transform.parent = Parent.transform;
    }

    public void GameEnd(bool IsAttackerWon , bool IsDraw = false)
    {
        GameOver = true;
        foreach(Attacker atker in PAttacker)
        {
            atker.IsActive = false;
        }
        foreach (Defender dfnder in PDefender)
        {
           dfnder.IsActive = false;
        }
        if (!IsDraw)
        {
            if (IsAttackerWon)
            {
                if (Attacker.isAtk)
                {
                    AttackerPoint = PlayerPrefs.GetInt("PlayerLifeTimeScore");
                    AttackerPoint += 1;
                    PlayerPrefs.SetInt("PlayerLifeTimeScore", AttackerPoint);

                }
                else
                {
                    DefenderPoint = PlayerPrefs.GetInt("DefenderLifeTimeScore");
                    DefenderPoint += 1;
                    PlayerPrefs.SetInt("DefenderLifeTimeScore", DefenderPoint);

                }
            }
            else
            {
                if (Attacker.isAtk)
                {
                    DefenderPoint = PlayerPrefs.GetInt("DefenderLifeTimeScore");
                    DefenderPoint += 1;
                    PlayerPrefs.SetInt("DefenderLifeTimeScore", DefenderPoint);
                    print(PlayerPrefs.GetInt("DefenderLifeTimeScore"));



                }
                else
                {

                    AttackerPoint = PlayerPrefs.GetInt("PlayerLifeTimeScore");
                    AttackerPoint += 1;
                    PlayerPrefs.SetInt("PlayerLifeTimeScore", AttackerPoint);
                }
            }
        }
        else
        {

        }
        int total = PlayerPrefs.GetInt("MatchNo");
        if (total >= 5)
        {
            GameOver = true;
            int playerPoint = PlayerPrefs.GetInt("PlayerLifeTimeScore");
            int enemyPoint = PlayerPrefs.GetInt("DefenderLifeTimeScore");
            if (playerPoint > enemyPoint)
            {
                print("Player win");
            }
            else
            {
                print("Player Loose");
            }
        }
        _UiInstance.CompleteUI.gameObject.SetActive(true);

    }

 
 
}
