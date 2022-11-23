using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 8;
    private float outBoundY = 8;

    // Update is called once per frame
    void Update()
    {
        // translate up [speed] / second
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        // destroy laser when out
        if (transform.position.y > outBoundY)
        {
            Destroy(this.gameObject);
        }
    }
}
