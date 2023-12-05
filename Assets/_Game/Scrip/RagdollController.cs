using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : Singleton<RagdollController>
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject charRagdoll;
    [SerializeField] GameObject charAnim;
    [SerializeField] Animation anim;
    [SerializeField] Animation animRagdoll;
    [SerializeField] Rigidbody[] ragdollRigidbodies;
    [SerializeField] Collider col;
    [SerializeField] Rigidbody rbRagdoll;
    [SerializeField] string currenAnim;
    [SerializeField] bool isMoving;
    [SerializeField] float speed;
    [SerializeField] float velocity;
    [SerializeField] float timeNitro;
    Vector3 dir;
    private void Start()
    {
        isMoving = false;
    }
    private void Update()
    {
        //dir = transform.forward;  
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            timeNitro -= Time.deltaTime;
            if (timeNitro > 0)
            {
                rb.velocity = dir * velocity;
                velocity += speed * Time.fixedDeltaTime * 0.2f;
            }
        }
    }
    public void OnInit(float velocity, float timeNitro, Vector3 direction)
    {
        ChaneAnim(Constants.JETPACKSTART);
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(-15, 0, 0);
        isMoving = true;
        rb.useGravity = true;
        rb.isKinematic = false;
        this.velocity = velocity;
        this.timeNitro = timeNitro;
        dir = direction;
        rb.velocity = dir * velocity;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        StartCoroutine(JetpackAnim());
    }
    IEnumerator JetpackAnim()
    {
        yield return new WaitForSeconds(0.15f);
        ChaneAnim(Constants.JETPACKLOOP);
        Time.timeScale = 1f;

    }
    public void ChaneAnim(string name)
    {
        if (currenAnim != name)
        {
            currenAnim = name;
            anim.Play(currenAnim);
        }
    }
    public void SetRagdoll()
    {
        charAnim.SetActive(false);
        charRagdoll.SetActive(true);
    }
    [Button]
    public void SetStateRagdoll(bool state)
    {
        foreach (var rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = state;
        }
        anim.enabled = state;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isMoving)
        {
            SetRagdoll();
            //rb.velocity = Vector3.zero;
            rbRagdoll.velocity = dir * velocity * 2;
            rb.isKinematic = true;
            CameraManager.Ins.ChangeCam(Constants.CAM_ROTATE);
        }
    }
}
