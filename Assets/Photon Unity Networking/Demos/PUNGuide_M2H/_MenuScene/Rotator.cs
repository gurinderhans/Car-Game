using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    private Transform myTrans;
    public int rotateSpeed = 20;

    void Awake()
    {
        myTrans = transform;
    }

    void Update()
    {
        myTrans.Rotate(new Vector3(Time.deltaTime * rotateSpeed, 0, 0));
    }
}