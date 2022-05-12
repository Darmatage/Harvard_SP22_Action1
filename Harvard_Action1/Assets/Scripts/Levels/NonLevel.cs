using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonLevel : MonoBehaviour
{
    private GameObject playerBars;

    // Start is called before the first frame update
    void Start()
    {
        playerBars = GameObject.FindWithTag("PlayerBars");

        playerBars.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
