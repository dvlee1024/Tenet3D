using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEffect : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Vector3.Distance(target.position, transform.position);
        float y = x >= 6 ? 0 : -100 * x + 800.0f;
        y = y < 80 ? 80 : y;
        transform.Rotate(Vector3.forward, y * Time.deltaTime);
    }
}
