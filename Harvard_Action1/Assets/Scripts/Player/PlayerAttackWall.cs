using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
		private void onTriggerEnter2D(Collider2D other)
		{
			if(other.CompareTag("breakable"))
			{
				other.GetComponent<wallsmash>().Smash();
			}
		}
}
