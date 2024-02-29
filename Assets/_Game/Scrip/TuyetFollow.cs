using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyetFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    private void Start()
    {
        target = LevelManager.Ins.CurrentMotor.AimPlayer;
    }
    // Update is called once per frame
    void Update()
    {
        target = LevelManager.Ins.CurrentMotor.AimPlayer;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * moveSpeed);
        
    }
}
