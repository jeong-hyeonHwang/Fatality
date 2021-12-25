using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRangeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int Number = Random.Range(0, 11);
        Debug.Log("Number Is " + Number);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
