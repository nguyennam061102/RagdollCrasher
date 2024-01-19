using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;
    [SerializeField] bool isStart;
    private void OnEnable()
    {
        StartCoroutine(SetStart());

    }
    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
        y += Time.deltaTime * 10;
        transform.rotation = Quaternion.Euler(new Vector3(x, y, z));
    }
    IEnumerator SetStart()
    {
        yield return new WaitForSeconds(3); 
        isStart = true;
    }
}
