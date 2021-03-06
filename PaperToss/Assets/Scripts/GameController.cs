using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SocialPlatforms.Impl;
using Random=UnityEngine.Random;
using RenderSettings = UnityEngine.RenderSettings;
using UnityEngine.InputSystem;

public enum GameMode
{
    Arcade,
    Campaign
}
public enum LightMode
{
    Day,
    Night,
    Space
}
public class GameController : MonoBehaviour {

    public static GameController instance { get; private set; }

    public bool gameIsRunning;
    public bool gameIsPaused;
    public LightMode lightMode;
    public GameMode mode;
    public float gameDifficulty = 0;
    public float gameDifficultyDelta = 0;
    public int gameLength;

    public Fan fan;
    public Holster holster;
    public TrashCan trashCan;
    
    public GameObject menu;
    public GameObject pauseMenu;
    public GameObject highScoreUI;
    public GameObject campaignMenu;
    public GameObject campaignInstructionsUI;
    public GameObject cheatCollider;
    public ScoreCounter scoreboard;
    public TimerCountdown timerCountdown;
    public GameCountdown gameStartCountdown;
    public GameObject howToPlayIUI;
    public GameObject spaceObjects;
    public GameObject officeObjects;
    public GameObject tableOne;
    public GameObject tableTwo;
    public GameObject characters;

    public HighScoreTextAnimator highScoreAnmator;
    public GameObject fireworks;

    public AudioClip pointClip;
    public AudioClip goldPointClip;
    public AudioClip firePointClip;
    public AudioClip noFireClip;
    public AudioClip hesOnFireClip;
    public AudioClip arcadeOverClip;
    public AudioClip winBellClip;
    public AudioClip paranormalClip;
    public AudioSource backgroundMusic;
    public AudioClip lightMusic;
    public AudioClip darkMusic;

    private bool gotNewHighScore;
    private Coroutine fadeInOutNightLightCoroutine;

    [SerializeField] private Material nightSkyBox;
    [SerializeField] private Material daySkyBox;
    [SerializeField] private Material spaceSkyBox;
    [SerializeField] private Material metalicTableMaterial;
    [SerializeField] private Material redTableMaterial;
    [SerializeField] private Light nightLight;
    [SerializeField] private Light directionalLight;

    private int currentLevelSelected;
    private int currentStageSelected;
    public int scoreRequiredToComplete = 10;
    public int arcadeTimeLength = 60;
    public int campaignTimeLength = 60;
    public int currentStreak = 0;
    
    public int pointsNeededForFire  = 5;
    public int pointsNeededForTrashToMove  = 20;

    private bool trashIsAtWaypoint1 = false; 
    private bool trashIsAtWaypoint2 = false; 
    private bool trashIsAtWaypoint3 = false; 

    public InputActionReference togglePauseMenueRefernce = null;
    
    private List<MovingCharacter> charactersToWalk;

    private int firstCharacterTime; 
    private int secondCharacterTime; 


    // Use this for initialization
    void Awake () {
        // PlayerPrefs.DeleteAll();
        togglePauseMenueRefernce.action.started += TogglePause;

        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);  
        }
        DontDestroyOnLoad (gameObject);
    }
    
    private void OnDestroy()
    {
        togglePauseMenueRefernce.action.started -= TogglePause;
    }

    private void Start()
    {
        fan = GameObject.FindGameObjectWithTag("Fan").GetComponent<Fan>();;
        holster = GameObject.FindGameObjectWithTag("Holster").GetComponent<Holster>();
        menu = GameObject.FindGameObjectWithTag("MainMenu");
        gameStartCountdown = GameObject.FindGameObjectWithTag("GameStartCountdown").GetComponent<GameCountdown>();
        timerCountdown = GameObject.FindGameObjectWithTag("TimerCountdown").GetComponent<TimerCountdown>();
        highScoreUI = GameObject.FindGameObjectWithTag("HighScoreUI");
        scoreboard = GameObject.FindGameObjectWithTag("Scoreboard").GetComponent<ScoreCounter>();
        trashCan = GameObject.FindGameObjectWithTag("TrashCan").GetComponent<TrashCan>();
        fireworks = GameObject.FindGameObjectWithTag("Fireworks");
        campaignMenu = GameObject.FindGameObjectWithTag("CampaignMenu");
        characters = GameObject.FindGameObjectWithTag("Characters");
        nightLight = GameObject.FindGameObjectWithTag("NightLight").GetComponent<Light>();
        fireworks.SetActive(false);
        highScoreAnmator.SetVisible(false);
        cheatCollider.SetActive(false);
        spaceObjects.SetActive(false);
        lightMode = LightMode.Day;
        RenderSettings.skybox = daySkyBox;

        int readGameInstructions = PlayerPrefs.GetInt("ReadGameInstructions", 0);
        if (readGameInstructions == 0)
        {
            PlayerPrefs.SetInt("ReadGameInstructions", 1);
            howToPlayIUI.SetActive(true);
            menu.SetActive(false);   
        }
        else
        {
            howToPlayIUI.SetActive(false);
            menu.SetActive(true);
        }
    }

    public void SetUpGame(GameMode gameMode)
    {
        mode = gameMode;
        if (gameMode == GameMode.Arcade)
        {
            gameDifficulty = 0;
            gameLength = arcadeTimeLength;
            timerCountdown.UpdateGameLength();
            gameDifficultyDelta = 0.7f;
        }
        else if (gameMode == GameMode.Campaign)
        {
            gameLength = campaignTimeLength;
            timerCountdown.UpdateGameLength();
            gameDifficultyDelta = 1.0f;
            gameDifficulty = 0.8f;
            gameDifficulty = gameDifficulty * currentLevelSelected;
        }
        scoreboard.ResetScore();
        holster.SetHolsterPosition();
        if (currentStageSelected >= 2 && currentLevelSelected <= 4)
        {
            trashCan.StartAnimating(AnimationPathStyle.Line);
        } else if (currentStageSelected >= 2 && currentLevelSelected > 4)
        {
            trashCan.StartAnimating(AnimationPathStyle.Hexagon);
        }
        if (currentStageSelected == 3)
        {
            trashCan.speed = .55f;
        }
        GetNewFanSpeed();
        firstCharacterTime = Random.Range(42, 53);
        secondCharacterTime = Random.Range(15, 30);

    }
    
    public void SetUpGame(GameMode gameMode, int stage, int level)
    {
        currentLevelSelected = level;
        currentStageSelected = stage;
        SetUpGame(gameMode);
    }
    
    void TogglePause(InputAction.CallbackContext context)
    {
        Debug.Log("toggle pause menu");
        if (gameIsRunning == true && gameIsPaused)
        {
            pauseMenu.SetActive(false);
            GameController.instance.ResumeGame();
        }
        else if (gameIsRunning == true )
        {
            pauseMenu.SetActive(true);
            GameController.instance.PauseGame();
        }
    }


    public void StartGameCountdown()
    {
        if (mode == GameMode.Arcade || currentStageSelected == 1)
        {
            changeSceneLight(LightMode.Day);
            spaceObjects.SetActive(false);
            officeObjects.SetActive(true);
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().mass = 2f;
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().drag = 2f;
            Physics.gravity = new Vector3(0,-7.81f,0); 
            if (backgroundMusic.clip != lightMusic)
            {
                backgroundMusic.clip = lightMusic;
                backgroundMusic.Play();                
            }
            trashCan.StopAnimating();
            trashCan.hideGlow();
            characters.SetActive(false);
        }
        else if (currentStageSelected == 2)
        {
            changeSceneLight(LightMode.Night);
            spaceObjects.SetActive(false);
            officeObjects.SetActive(true);
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().mass = 2f;
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().drag = 2f;
            Physics.gravity = new Vector3(0,-7.81f,0); 
            if (backgroundMusic.clip != darkMusic)
            {
                backgroundMusic.clip = darkMusic;
                backgroundMusic.Play();     
            }
            SoundManager.Instance.Play(paranormalClip);
            trashCan.showGlow();;
            characters.SetActive(false);
        } 
        else if (currentStageSelected == 3)
        {
            characters.SetActive(false);
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().mass = 5f;
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().drag = 1.5f;
            Physics.gravity = new Vector3(0,-1.62f,0);
            spaceObjects.SetActive(true);
            officeObjects.SetActive(false);

            changeSceneLight(LightMode.Space);
            if (backgroundMusic.clip != darkMusic)
            {
                backgroundMusic.clip = darkMusic;
                backgroundMusic.Play();     
            }
            SoundManager.Instance.Play(paranormalClip);
            trashCan.showGlow();;
        
        }

        if (mode == GameMode.Campaign)
        {
            campaignInstructionsUI.SetActive(false);
            highScoreUI.SetActive(false);
        }
        else
        {
            highScoreUI.SetActive(true);
        }
        fireworks.SetActive(false);
        highScoreAnmator.SetVisible(false);
        fan.SetVisible(true);
        menu.SetActive(false);
        holster.SetVisible(true);
        trashCan.SetVisible(true);
        cheatCollider.SetActive(true);
        trashCan.ResetPosition();
        fan.ResetFanPosition();
        gameStartCountdown.StartCountdown();
        SetUpCharactersToWalk();
        // SetTableMaterials(); // turns it silver
    }

    public void ShowInstructions()
    {
        campaignMenu.SetActive(false);
        campaignInstructionsUI.SetActive(true);
    }
    public void StartArcade()
    {
        gameIsPaused = false;
        gameIsRunning = true;
        timerCountdown.BeginCountdown();
    }

    public void GameFinished()
    {
        if (mode == GameMode.Arcade)
        {
            SoundManager.Instance.Play(arcadeOverClip);
        } else 
        {
            if (scoreboard.score >= scoreRequiredToComplete)
            {
                SoundManager.Instance.Play(winBellClip);
            }
            else
            {
                SoundManager.Instance.Play(arcadeOverClip);
            }
        }
        cheatCollider.SetActive(false);
        timerCountdown.Reset();
        DestoryAllBalls();
        gameIsPaused = false;
        gameIsRunning = false;
        holster.SetOnFire(false);

        holster.SetVisible(false);
        fan.SetVisible(false);
        trashCan.SetVisible(false);
        
        if (mode == GameMode.Arcade)
        {
            if (gotNewHighScore == true)
            {
                OculusLeaderboardManager.instance.SubmitScore(scoreboard.score);
                EventManager.TriggerEvent("NewHighScore");
                runHighScoreAnimations();
            }
            menu.SetActive(true);
        }
        else if (mode == GameMode.Campaign)
        {
            bool levelCompleted = checkFinalScore();
            campaignMenu.SetActive(true);
            if(levelCompleted && (currentStageSelected == 2 && currentLevelSelected == 9))
            {
                runBeatGame();
            }

        }
        trashCan.StopAnimating();
        trashCan.hideGlow();
        trashIsAtWaypoint3 = false;
        trashIsAtWaypoint2 = false;
        trashIsAtWaypoint1 = false;
        currentStreak = 0;
        PTCharacterController.instance.ResetAllCharacters();
    }

    private bool checkFinalScore()
    {
        if (currentLevelSelected != 9)
        {
            if (scoreboard.score >= scoreRequiredToComplete)
            {
                CompleteLevel(currentStageSelected, currentLevelSelected );
                UnlockLevel(currentStageSelected, currentLevelSelected + 1);
            }   
        } else if (scoreboard.score >= scoreRequiredToComplete)
        {
            CompleteLevel(currentStageSelected, currentLevelSelected);
            UnlockLevel(currentStageSelected+1, 1);
        }

        if (scoreboard.score >= scoreRequiredToComplete)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void UnlockLevel(int stage, int level)
    {
        Debug.Log("unlocking level: " + stage.ToString() +"-" + level.ToString());
        PlayerPrefs.SetInt(stage.ToString() +"-" + level.ToString(), 1);
        EventManager.TriggerEvent("CheckLevelUnlocks");
    }
    
    private void CompleteLevel(int stage, int level)
    {
        Debug.Log("completed level: " + stage.ToString() +"-" + level.ToString());

        PlayerPrefs.SetInt(stage.ToString() +"-" + level.ToString()+"complete", 1);
        EventManager.TriggerEvent("CheckLevelCompletes");
    }
    private void runHighScoreAnimations()
    {
        fireworks.SetActive(true);
        gotNewHighScore = false;
        highScoreAnmator.SetVisible(true);
        highScoreAnmator.startAnimation();
        changeSceneLight(LightMode.Night);
    }
    private void runBeatGame()
    {
        fireworks.SetActive(true);
        changeSceneLight(LightMode.Night);
    }

    private void changeSceneLight(LightMode newLightMode)
    {
        if (newLightMode == lightMode)
        {
            return;
        }
        if (newLightMode == LightMode.Day)
        {
            directionalLight.intensity = 1.67f;
            RenderSettings.skybox = daySkyBox;
            if (fadeInOutNightLightCoroutine != null)
            {
                StopCoroutine(fadeInOutNightLightCoroutine);
            }
            StartCoroutine(fadeInOutNightLight(nightLight, false, 0.5f));
        }
        else if (newLightMode == LightMode.Night)
        {
            directionalLight.intensity = 1.67f;
            RenderSettings.skybox = nightSkyBox;
            fadeInOutNightLightCoroutine = StartCoroutine(fadeInOutNightLight(nightLight, true, 1.5f));
        }
        else if (newLightMode == LightMode.Space)
        {
            directionalLight.intensity = 2.0f;
            RenderSettings.skybox = spaceSkyBox;
        }
        lightMode = newLightMode;

    }
    IEnumerator fadeInOutNightLight(Light lightToFade, bool fadeIn, float duration)
    {
        float minLuminosity = 0.0f; // min intensity
        float maxLuminosity = 20.0f; // max intensity
        float counter = 0.0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;

        if (fadeIn)
        {
            a = minLuminosity;
            b = maxLuminosity;
        } else
        {
            a = maxLuminosity;
            b = minLuminosity;
        }

        float currentIntensity = lightToFade.intensity;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);
            yield return null;
        }
    }
    
    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
    
    public void PlayerDidScore(bool gold, bool fire)
    {
        
        if (gold)
        {
            SoundManager.Instance.Play(goldPointClip);
            scoreboard.PlayerScored(true, false);
        }
        if (currentStreak == pointsNeededForFire-1)
        {
            SoundManager.Instance.Play(hesOnFireClip);
            holster.SetOnFire(true);
        }  else if (fire)
        {
            scoreboard.PlayerScored(false, true);
            SoundManager.Instance.Play(firePointClip);
        } 
        else if (!gold)
        {
            SoundManager.Instance.Play(pointClip);
            scoreboard.PlayerScored(false, false);            
        }
        /////// SCORE CHANGED /////
        if (mode == GameMode.Campaign && scoreboard.score >= scoreRequiredToComplete)
        {
            GameFinished();
        }
        else
        {
            if (mode == GameMode.Arcade)
            {
                ShouldMoveToLongerWaypointStage();
                IncreaseGameDifficulty();
                bool gotNewHighScore = CheckSaveHighScore(scoreboard.score);
            }
            if (ShouldShowGoldBall())
            {
                holster.SetNextBallToBeGold(ShouldShowGoldBall());
            }
            UpdateFanSpeedAndLocation();
            currentStreak++;
        }
    }

    void UpdateFanSpeedAndLocation()
    {
        GetNewFanSpeed();
        if (trashIsAtWaypoint1 || trashIsAtWaypoint2 || trashIsAtWaypoint3)
        {
            fan.ChangeFanPosition(true);
        }
        else
        {
            fan.ChangeFanPosition(false);
        }

    }

    void ShouldMoveToLongerWaypointStage()
    {
        
        
        if (trashIsAtWaypoint3 == true)
        {
            trashCan.ResetPosition();
            trashIsAtWaypoint3 = false;
            trashIsAtWaypoint2 = false;
            trashIsAtWaypoint1 = false;
        } else if (trashIsAtWaypoint2 == true)
        {
            trashCan.MoveToLongerWaypoint(3);
            trashIsAtWaypoint3 = true;
            trashIsAtWaypoint2 = false;
        } else if (trashIsAtWaypoint1 == true)
        {
            trashCan.MoveToLongerWaypoint(2);
            trashIsAtWaypoint2 = true;
            trashIsAtWaypoint1 = false;
        }  else if (scoreboard.score >= pointsNeededForTrashToMove)
        {
            trashCan.MoveToLongerWaypoint(1);
            trashIsAtWaypoint1 = true;
        }

    }

    private bool ShouldShowGoldBall()
    {
        int r = UnityEngine.Random.Range(0, 3);
        if (r == 2)
        {
            return true;
        }
        return false;
    }
    

    private void IncreaseGameDifficulty()
    {
        gameDifficulty += 0.4f;
    }

    private void GetNewFanSpeed()
    {
        fan.currentFanSpeed = Random.Range(Mathf.Clamp(gameDifficulty - gameDifficultyDelta, 0.1f, 10.0f), Mathf.Clamp(gameDifficulty + gameDifficultyDelta, 0.1f, 10.0f));
        fan.UpdateFanStrength();

    }
    
    private bool CheckSaveHighScore(int newScore)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        bool gotNewHighScore = newScore > highScore;

        if (gotNewHighScore)
        {
            PlayerPrefs.SetInt("HighScore", newScore);
            PlayerPrefs.Save();
            this.gotNewHighScore = true;
        }

        return gotNewHighScore;
    }
    
    void DestoryAllBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        for(var i = 0 ; i < balls.Length ; i ++)
        {
            if (!balls[i].GetComponent<Ball>().isInHolster)
            {
                Destroy(balls[i]);
            }
        }
    }

    public float currentFanSpeed()
    {
        return fan.currentFanSpeed;
    }

    public void PauseGame()
    {
        timerCountdown.PauseCountdown();
        gameIsPaused = true;
        
        fan.SetVisible(false);
        holster.SetVisible(false);
        trashCan.SetVisible(false);
        cheatCollider.SetActive(false);

    }
    
    public void ResumeGame()
    {
        gameIsPaused = false;
        timerCountdown.BeginCountdown();
        
        holster.SetVisible(true);
        fan.SetVisible(true);
        trashCan.SetVisible(true);
        cheatCollider.SetActive(true);

    }

    public void ReadHowToPlayInstructions()
    {
        PlayerPrefs.SetInt("ReadGameInstructions", 1);
        howToPlayIUI.SetActive(false);
        menu.SetActive(true);
    }

    public void MissOccured()
    {
        if (currentStreak >= pointsNeededForFire)
        {
            SoundManager.Instance.Play(noFireClip);
        }
        currentStreak = 0;
        holster.SetOnFire(false);
        
    }

    public void TimeUpdated(){
        if (currentStageSelected != 3)
        {
            if (timerCountdown.secondsLeft == firstCharacterTime)
            {
                PTCharacterController.instance.StartWalk(charactersToWalk[0]);
            }
            if (timerCountdown.secondsLeft == secondCharacterTime)
            {
                PTCharacterController.instance.StartWalk(charactersToWalk[1]);
            }   
        }
    }

    private void SetUpCharactersToWalk()
    {
        charactersToWalk = PTCharacterController.instance.GetRandomChracterList(lightMode);
    }

    private void SetTableMaterials()
    {
        if (lightMode == LightMode.Space)
        {
            Material[] matArray = tableOne.GetComponent<Renderer>().materials;
            matArray[1] = metalicTableMaterial;
            tableOne.GetComponent<Renderer>().materials = matArray;
            tableTwo.GetComponent<Renderer>().materials = matArray;
            GameObject[] tvs = GameObject.FindGameObjectsWithTag(@"TV");
            foreach (GameObject tv in  tvs)
            {
                Material[] tvMatArray = tv.GetComponent<Renderer>().materials;
                tvMatArray[2] = metalicTableMaterial;
                tv.GetComponent<Renderer>().materials = tvMatArray;
            }
        }
        else
        {
            Material[] matArray = tableOne.GetComponent<Renderer>().materials;
            matArray[1] = redTableMaterial;
            tableOne.GetComponent<Renderer>().materials = matArray;
            tableTwo.GetComponent<Renderer>().materials = matArray;
            GameObject[] tvs = GameObject.FindGameObjectsWithTag(@"TV");
            foreach (GameObject tv in  tvs)
            {
                Material[] tvMatArray = tv.GetComponent<Renderer>().materials;
                tvMatArray[2] = redTableMaterial;
                tv.GetComponent<Renderer>().materials = tvMatArray;
            }
        }
    }



}