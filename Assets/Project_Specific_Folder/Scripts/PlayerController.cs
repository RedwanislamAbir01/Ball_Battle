using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

  
    public class PlayerController : MonoBehaviour
    {
    public float Speed;
    public FloatingJoystick f;
    Rigidbody rb;
    private void Start()
    {
       
       

    }
    private void FixedUpdate()
    {
        float a = f.Horizontal;
        float b = f.Vertical;

        transform.position += new Vector3(-a * Speed, transform.position.y,- b * Speed);
    }



}
