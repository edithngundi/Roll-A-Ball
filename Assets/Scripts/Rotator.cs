using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Make the collectibles spin


    // Update is called once per frame
    void Update()
    {
        // this is transform the component not a variable
        // Smooth and frame rate independent
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
