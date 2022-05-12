using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    public GameHandler gameHandler;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();

        gameHandler.canOpenDoor = false; // Need key for this level
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
