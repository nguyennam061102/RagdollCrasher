using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject charRagdoll;
    [SerializeField] GameObject charAnim;
    [SerializeField] Animation anim;
    [SerializeField] Animation animRagdoll;
    [SerializeField] Rigidbody[] ragdollRigidbodies;
    [SerializeField] string currenAnim;

    private void Start()
    {
        
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
