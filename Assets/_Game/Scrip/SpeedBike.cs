using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpeedMotor", order = 1)]
public class SpeedBike : ScriptableObject
{
    [SerializeField] DataSpeedMotor dataSpeedMotor;
}

[Serializable]
public class DataSpeedMotor : SerializedMonoBehaviour
{
    
}