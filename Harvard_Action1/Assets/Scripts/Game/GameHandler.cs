using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour {
      public Level currentLevel;
	public static bool GameisPaused = false;
      Level[] levels = new Level[2]; // When adding more levels, increase size
      public GameObject pauseMenuUI;
      public AudioMixer mixer;
      public static float volumeLevel = 1.0f;
      private Slider sliderVolumeCtrl;

      private GameObject player;
      public static int playerHealth = 100;
      public int StartPlayerHealth = 100;
      public GameObject healthText;

      public static int gotTokens = 0;
      public GameObject tokensText;

      public bool isDefending = false;
	  public int playercolor;
	 // public GameObject NextButton;
	 // public GameObject ButtonPlayStory;
	  public GameObject StartButton;
	  public int pageNumber=0;
	  public GameObject QuitButton;
	 

      void Awake () {
            SetAudioLevel (volumeLevel);
            GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");

            if (sliderTemp != null) {
                  sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                  sliderVolumeCtrl.value = volumeLevel;
            }
      }

      void Start () {
            pauseMenuUI.SetActive(false);
			//ButtonPlayStory.SetActive(true);
			QuitButton.SetActive(true);
			StartButton.SetActive(true);
            GameisPaused = false;
            player = GameObject.FindWithTag("Player");
			playercolor=0;
			pageNumber=0;
            DefineGameLevels();
            SetGameLevel(1); // Start the game at level 1
            SetPlayerPosition(currentLevel.spawnPoint); // Define player starting point in level
			
				//NextButton.SetActive(false);
				//ButtonPlayStory.SetActive(true);
				//SceneManager.LoadScene("bookPage1");
			
			
      }
/*public void turnPage()
    {
		if (pageNumber==0)
		{
			SceneManager.LoadScene("bookPage1");
			pageNumber++;
		}
        if (pageNumber==1)
		{
			SceneManager.LoadScene("bookPage2");
			pageNumber++;
		}
		if (pageNumber==2)
		{
			SceneManager.LoadScene("bookPage3");
			pageNumber++;
		}
		if (pageNumber==3)
		{
			SceneManager.LoadScene("bookPage4");
			pageNumber++;
		}
		if (pageNumber==4)
		{
			SceneManager.LoadScene("bookPage5");
			pageNumber++;
		}
		if (pageNumber==5)
		{
			SceneManager.LoadScene("bookPage6");
			pageNumber++;
		}
		if (pageNumber==6)
		{
			SceneManager.LoadScene("bookPage7");
			pageNumber++;
		}
		if (pageNumber==7)
		{
			SceneManager.LoadScene("bookPage8");
			pageNumber++;
		}
		if (pageNumber==8)
		{
			SceneManager.LoadScene("scene1_OutsideTower");
			
		}
    }*/
      void Update () {
		  if (Input.GetKeyDown(KeyCode.Escape))
		  {
             if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
		  
          //  if(pageNumber==8)
			//{
				FallCheck();
		//	}

            if (Input.GetKeyDown(KeyCode.Escape)){
                  if (GameisPaused){
                        Resume();
                  }
                  else {
                        Pause();
                  }
            }
	  }
      

      void DefineGameLevels() {
            levels[0] = new Level(0, new Vector3(0,0,0)); // Not a real level, allows us to look up levels numbers by the index
            levels[1] = new Level(1, new Vector3(0,0,0));
      }

      // Has player fallen off map?
      void FallCheck() {
		  
		  
           // Transform transform = player.GetComponent<Transform>();

           // if (transform.position.y <= -10) {
              //   Debug.Log("Fell Off");

               //   SetPlayerPosition(currentLevel.spawnPoint);
            }
		  
      

      void Pause(){
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameisPaused = true;
      }

      public void Resume(){
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameisPaused = false;
      }

      public void SetAudioLevel (float sliderValue){
            mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
            volumeLevel = sliderValue;
      }

      public void SetGameLevel(int number) {
            currentLevel = levels[number];
      }

      public void SetPlayerPosition(Vector3 position) {
            Transform transform = player.GetComponent<Transform>();

            transform.position = position;
      }

      public static bool stairCaseUnlocked = false;
      //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

      private string sceneName;

     /* void Start(){
            player = GameObject.FindWithTag("Player");
            sceneName = SceneManager.GetActiveScene().name;
            //if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
                  playerHealth = StartPlayerHealth;
            //}
            updateStatsDisplay();
      }*/

      public void playerGetTokens(int newTokens){
            gotTokens += newTokens;
            updateStatsDisplay();
      }

      public void playerGetHit(int damage){
           if (isDefending == false){
                  playerHealth -= damage;
				  Debug.Log(playerHealth);
                  if (playerHealth >=0){
                        updateStatsDisplay();
                  }
                  player.GetComponent<PlayerHurt>().playerHit();
            }

           if (playerHealth >= StartPlayerHealth){
                  playerHealth = StartPlayerHealth;
            }

          if (playerHealth <= 0){
                  playerHealth = 0;
                  //Debug.Log("health is zero");
				  playerDies();
            }
      }

      public void updateStatsDisplay(){
            Text healthTextTemp = healthText.GetComponent<Text>();
            healthTextTemp.text = "HEALTH: " + playerHealth;

            Text tokensTextTemp = tokensText.GetComponent<Text>();
            tokensTextTemp.text = "GOLD: " + gotTokens;
      }

     public void playerDies(){
            player.GetComponent<PlayerHurt>().playerDead();
            StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
           // player.GetComponent<PlayerMove>().isAlive = false;
            //player.GetComponent<PlayerJump>().isAlive = false;
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("EndLose");
      }

      public void StartGame() {
            SceneManager.LoadScene("bookPage1");
      }

      public void RestartGame() {
		Time.timeScale = 1f;
            SceneManager.LoadScene("StartMenu");
            playerHealth = StartPlayerHealth;
      }

      public void QuitGame() {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }


public class Level {
      public int number;
      public Vector3 spawnPoint;

      public Level(int number, Vector3 spawnPoint) {
            this.number = number;
            this.spawnPoint = spawnPoint;
      }
}
}