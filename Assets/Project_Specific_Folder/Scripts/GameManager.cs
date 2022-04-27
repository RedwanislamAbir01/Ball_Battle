using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{



    public bool IsStarted;
    public int MatchNo = 0;
    public float Time;

    public GameObject Ball;
    [Header("Fields")]
    public GameObject AttackerField;
    public GameObject DefenderField;

    [Header("Game Parameters")]
    public float TimeLimit = 140f;
    UiManager _UiInstance;

    public override void Start()
    {

        base.Start();
        _UiInstance = UiManager.Instance ;
        
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

        g.transform.position = RndPointonPlane;

        g.transform.parent = AttackerField.transform.root;
    }

}
