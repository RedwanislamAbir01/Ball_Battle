using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
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
    public GameObject BallContainer;
    [Header("Game Parameters")]
    public float TimeLimit = 140f;


    UiManager _UiInstance;
     List<EnergyBar> m_enemyEnergy = new List<EnergyBar>();
     List<EnergyBar> m_playerEnergy = new List<EnergyBar>();
    public override void Start()
    {

        base.Start(); 
        FillEnergy(m_enemyEnergy, 0);
        FillEnergy(m_playerEnergy, 0);
        _UiInstance = UiManager.Instance ;
  


    }
    private void Update()
    {
        if (IsStarted)
        {
            if (EnemyEnergy < 6)
            {
                EnemyEnergy += 1 * Time.deltaTime;
                FillEnergy(m_enemyEnergy, EnemyEnergy);
            }
            if (PlayerEnergy < 6)
            {
                PlayerEnergy += 1 * Time.deltaTime;
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
        IncreaseMatchNo();
       _UiInstance.StartUI.SetActive(false);
        _UiInstance.InGmePanel.SetActive(true);
        BallGeberation();
    }

    void IncreaseMatchNo()
    {
        MatchNo++;
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

}