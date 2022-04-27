using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class bookPages : MonoBehaviour
{
    public int pageNumber=1;         // This integer drives game progress!
   // public GameHandler gameHandler; 
    public GameObject BG;
       public GameObject nextButton;
	   public GameObject skipButton;
      //public AudioSource audioSource;
    //private bool allowSpace = true;
   
    void Start()
    {              
		BG.SetActive(true);
        nextButton.SetActive(true);
		skipButton.SetActive(true);
		
		//pageNumber++;
		//Debug.Log("pageNumber in Start"+pageNumber);
    }

    void Update()
    {                 
             
		}
     
       
    
	
        public void nextPage2()
		{
			SceneManager.LoadScene("bookpage2");
		}
		
		public void nextPage3()
		{
			SceneManager.LoadScene("bookpage3");
		}
		
		public void nextPage4()
		{
			SceneManager.LoadScene("bookpage4");
		}
		
		public void nextPage5()
		{
			SceneManager.LoadScene("bookpage5");
		}
    
		public void nextPage6()
		{
			SceneManager.LoadScene("bookpage6");
		}
		
		public void nextPage7()
		{
			SceneManager.LoadScene("bookpage7");
		}
		
		public void nextPage8()
		{
			SceneManager.LoadScene("bookpage8");
		}
		
    public void skiptoGame()
    {
        SceneManager.LoadScene("scene1_OutsideTower");
    }

}   