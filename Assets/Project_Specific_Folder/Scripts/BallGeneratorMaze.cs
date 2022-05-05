using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGeneratorMaze : MonoBehaviour
{
    public GameObject AttackerField;
    public GameObject Ball;
    // Start is called before the first frame update
    void Start()
    {
        BallGeberation();
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

      
    }
}
