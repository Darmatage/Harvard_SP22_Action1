using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour {
      private PlayerBars bars;
      public Level currentLevel;
	public static bool GameisPaused = false;
      public bool canOpenDoor = true;
      Level[] levels = new Level[5]; // When adding more levels, increase size
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
	 
	public GameObject StartButton;
	
	public GameObject QuitButton;
      private new Transform transform;

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
		QuitButton.SetActive(true);
		StartButton.SetActive(true);
            GameisPaused = false;
		playercolor = 0;

            if (!IsGameLevel()) {
                  return;
            }

            player = GameObject.FindWithTag("Player");
            bars = player.GetComponent<PlayerBars>();
            transform = player.GetComponent<Transform>();
			
            DefineGameLevels();
            SetGameLevel(1); // Start the game at level 1
           // SetPlayerPosition(currentLevel.spawnPoint); // Define player starting point in level
			
      }

      void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
                  if (GameisPaused) {
                        Resume();
                  } else {
                        Pause();
                  }
            }

            if (!IsGameLevel()) {
                  return;
            }

		FallCheck();	
	}
      

      void DefineGameLevels() {
            levels[0] = new Level(0, new Vector3(0,0,0)); // Not a real level, allows us to look up levels numbers by the index
            levels[1] = new Level(1, new Vector3(0,0,0));
            levels[2] = new Level(2, new Vector3(0,0,0));
            levels[3] = new Level(3, new Vector3(0,0,0));
            levels[4] = new Level(4, new Vector3(0,0,0));
      }

      // Are we on a game level or a cutscene / menu
      bool IsGameLevel() {
            if (GameObject.FindWithTag("Player") != null) {
                  return true;
            }

            return false;
      }

      // Has player fallen off map?
      void FallCheck() {
            if (transform.position.y <= -10) {
                  Debug.Log("Fell Off");

                  SetPlayerPosition(currentLevel.spawnPoint);
                  bars.Reset();
            }
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
					  
                        //updateStatsDisplay();
						
                  }
                  player.GetComponent<PlayerHurt>().playerHit();
            }

           if (playerHealth >= StartPlayerHealth){
                  playerHealth = StartPlayerHealth;
            }

          if (playerHealth <= 0){
                  playerHealth = 0;
                  //Debug.Log("health is zero");
				//  playerDies();
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

      public void MainMenu() {
            SceneManager.LoadScene("StartMenu");
      }

      public void StartGame() {
            SceneManager.LoadScene("bookPage1");
      }

      public void RestartGame(int levelChoice) {
            Time.timeScale = 1f;
            playerHealth = StartPlayerHealth;

            if (levelChoice == 0) {
                  SceneManager.LoadScene("StartMenu");
            } else if (levelChoice == 1) {
                  SceneManager.LoadScene("scene1_LowerTower_Salwa");
            } else if (levelChoice == 2) {
                  SceneManager.LoadScene("scene2_MidTower_James");
            } else if (levelChoice == 3) {
                  SceneManager.LoadScene("scene3_UpperTower_Chris");
            } else if (levelChoice == 4) {
                  SceneManager.LoadScene("scene4_RooftopJellyDragonBoss_James");
            } else if (levelChoice == 5) {
                  SceneManager.LoadScene("scene5_JellyDragonBossHard_James");
            } else {
                  SceneManager.LoadScene("Credits");
            }
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
}

public class Level {
      public int number;
      public Vector3 spawnPoint;

      public Level(int number, Vector3 spawnPoint) {
            this.number = number;
            this.spawnPoint = spawnPoint;
      }
}
