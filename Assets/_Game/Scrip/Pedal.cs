using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    [SerializeField] Animation pedal;
    public void PedalRotate()
    {
         pedal.Play("start_vehicle");
    }
}
