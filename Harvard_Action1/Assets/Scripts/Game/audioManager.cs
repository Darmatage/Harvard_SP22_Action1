using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
	public static AudioClip dieEnemySound, vomitSound, chompSound;
	static AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        dieEnemySound=Resources.Load<AudioClip>("dieEnemy");
		vomitSound=Resources.Load<AudioClip>("vomit");
		chompSound=Resources.Load<AudioClip>("chomp");
		audiosource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public static void PlaySound (string clip)
	{
		switch (clip){
			case "dieEnemy":
			audiosource.PlayOneShot(dieEnemySound);
			break;
			case "vomit":
			audiosource.PlayOneShot(vomitSound);
			break;
			case "chomp":
			audiosource.PlayOneShot(chompSound);
			break;
			
		}
	}
	
}
