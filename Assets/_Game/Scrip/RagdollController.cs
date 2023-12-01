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
        dir = transform.forward;  
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            timeNitro -= Time.deltaTime;
            if (timeNitro > 0)
            {
                velocity += speed * Time.fixedDeltaTime;
                rb.velocity = dir * velocity;
            }
        }
    }
    public void OnInit(float velocity, float timeNitro)
    {
        isMoving = true;
        rb.useGravity = true;
        rb.isKinematic = false;
        this.velocity = velocity;
        this.timeNitro = timeNitro;
        rb.velocity = dir * velocity;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
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
    public void SetStateRagdoll(bool state)
    {
        foreach (var rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = state;
        }
        anim.enabled = state;
    }
}
