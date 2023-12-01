using UnityEngine;
using Dreamteck.Splines;
using Sirenix.OdinInspector;

public class BikeController : Singleton<BikeController>
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Animation anim;
    [Header("--------Spline-------")]
    [SerializeField] SplineFollower splineFollower;
    [SerializeField] SplineComputer spline;
    [Header("---------Wheel-------")]
    [SerializeField] private Transform _transformF;
    [SerializeField] private Transform _transformB;
    [SerializeField] private WheelCollider _colliderF;
    [SerializeField] private WheelCollider _colliderB;
    [Header("--------Parameter----")]
    [SerializeField] float speed;
    [SerializeField] float timeNitro;
    [SerializeField] bool isAirborne = false;
    void Start()
    {
        spline.AddTrigger(0, 1, SplineTrigger.Type.Forward).AddListener(() =>
        {
            AddForce();
        });
        spline.AddTrigger(0, 1, SplineTrigger.Type.Forward).AddListener(() =>
        {
            SetAirBorne();
        });
    }
    void FixedUpdate()
    {
        MoveAlongSpline();
    }

    void MoveAlongSpline()
    {
        Vector3 dir = transform.forward;
        if (Input.GetMouseButton(0))
        {
            if (isAirborne)
            {
                timeNitro -= Time.deltaTime;
                if (timeNitro > 0)
                {
                    splineFollower.followSpeed += Time.fixedDeltaTime;
                }
            }
            else
            {
                splineFollower.followSpeed += speed * Time.fixedDeltaTime;
            }
        }
        
        rb.velocity = dir * splineFollower.followSpeed;
        RotateWheel(_colliderF, _transformF);
        RotateWheel(_colliderB, _transformB);
    }
    void StopAlongSpline()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (isAirborne)
            {
                timeNitro -= Time.deltaTime;
                if (timeNitro > 0)
                {
                    //Vector3 dir = transform.forward;
                    //rb.velocity = dir * splineFollower.followSpeed;
                    splineFollower.followSpeed += Time.fixedDeltaTime;
                    RotateWheel(_colliderF, _transformF);
                    RotateWheel(_colliderB, _transformB);
                }
            }
            else
            {
                //Vector3 dir = transform.forward;
                //rb.velocity = dir * splineFollower.followSpeed;
                splineFollower.followSpeed += speed * Time.fixedDeltaTime;
                RotateWheel(_colliderF, _transformF);
                RotateWheel(_colliderB, _transformB);
            }
        }
    }

    private void RotateWheel(WheelCollider coll, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        coll.GetWorldPose(out position, out rotation);

        transform.rotation = rotation;
        transform.position = position;
    }
    void LiftOff()
    {
        isAirborne = true;
    }

    void HandleAirborne()
    {

    }
    public bool isRunRb;

    public void AddForce()
    {
        isRunRb = true;
        splineFollower.follow = false;
        Vector3 dir = transform.forward;
        rb.velocity = dir * splineFollower.followSpeed;
        isAirborne = true;
    }
    public void SetAirBorne()
    {
        isAirborne = true;
    }
}
