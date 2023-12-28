using Dreamteck.Splines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] SplineComputer spline;
    public Transform StartPoint { get => startPoint; set => startPoint = value; }
    public SplineComputer Spline { get => spline; set => spline = value; }
    public Transform EndPoint { get => endPoint; set => endPoint = value; }
}
