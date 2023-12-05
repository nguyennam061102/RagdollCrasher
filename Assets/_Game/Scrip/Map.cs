using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Transform startPoint;

    public Transform StartPoint { get => startPoint; set => startPoint = value; }
}
