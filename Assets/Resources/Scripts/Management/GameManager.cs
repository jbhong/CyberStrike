using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject closedBoxPrefab;
    [SerializeField] private GameObject openBoxPrefab;
    [SerializeField] private GameObject laptopCollectablePrefab;

    [Header("Spawn Points")]
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] boxesSpawnPoints;
    [SerializeField] private GameObject[] laptopSpawnPoints;

    [Header("Game Object References")]
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject mainMonitor;
    [SerializeField] private GameObject miniMap;
    [SerializeField] private GameObject gameOverRestartButton;

    [Header("Graphics")]
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private Text gameOverPrompt;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private AudioClip collectionSound;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;
    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioSource audioSource;

    [HideInInspector] public int numQuestions;
    [HideInInspector] public int laptopSpawnNumber;
    [HideInInspector] public int boxesSpawnNumber;
    [HideInInspector] public GameObject[] spawnedLaptops;
    [HideInInspector] public int laptopsCollectedCounter;

    private float MinutesToPlay;
    private float EnemySpawnRate; //in enemies per minute
    private int EnemyHealth;
    private int EnemyDamage;

    /* Predefined names */
    private string playerName = "Police Man";
    private string ammoBoxTag = "AmmoBox";
    private string healthBoxTag = "HealthBox";
    private string timeBoxTag = "TimeBox";

    /* Spawned items */
    private GameObject playerIns;
    private GameObject playerUIInstance;
    private ArrayList enemies = new ArrayList();
    private GameObject[] closedSpawnedBoxes;

    /* Other variables */
    private int score; // holds players score
    private bool repeating = false; //checks if enemies are spawning
    private float timeLeft; //holds time left
    private Dictionary<string, object> user = new Dictionary<string, object>(); //holds user info
    private HandleTextFile textController; // reference to the text reader
    private List<int> boxesRandomNumberList = new List<int>();
    private List<int> laptopRandomNumberList = new List<int>();
    private List<string> boxTags = new List<string>();
    private List<string> tagsAdded = new List<string>();
    private bool isGameOver;
    private Dictionary<string, string> variables = new Dictionary<string, string>();

    private float lastCalled;
    private float nextCall;

    /* Start method */

    private void Start()
    {
        gameOver.SetActive(false);

        variables = Difficulty.GetVariables();

        textController = FindObjectOfType<HandleTextFile>();
        textController.GenerateData(variables["infoPath"], variables["questionPath"]);

        boxTags.Add(ammoBoxTag);
        boxTags.Add(healthBoxTag);
        boxTags.Add(timeBoxTag);

        laptopSpawnNumber = Convert.ToInt32(variables["laptopSpawnNumber"]);
        boxesSpawnNumber = Convert.ToInt32(variables["boxesSpawnNumber"]);
        numQuestions = Convert.ToInt32(variables["numQuestions"]);
        MinutesToPlay = (float)Convert.ToDouble(variables["minutes"]);
        EnemySpawnRate = (float)Convert.ToDouble(variables["enemyspawn"]);
        EnemyDamage = Convert.ToInt32(variables["enemydamage"]);
        EnemyHealth = Convert.ToInt32(variables["enemyhealth"]);

        SpawnPlayer();

        InvokeRepeating("SpawnEnemy", 60f / EnemySpawnRate, 60f/EnemySpawnRate);
        repeating = true;

        spawnedLaptops = new GameObject[laptopSpawnNumber];
        
        SpawnLaptop();

        closedSpawnedBoxes = new GameObject[boxesSpawnNumber];
        SpawnBoxes();

        SetQuizQuestions();

        timeLeft = Mathf.Round(MinutesToPlay * 60.0f);
        lastCalled = (int)timeLeft;
        StartCoroutine(Countdown());

        gameOverRestartButton.SetActive(true);
    }

    /* Update method */

    private void Update()
    {
        if (PauseMenu.IsOn)
        {
            if (repeating == true) {
                int nowSec = (int)timeLeft;
                nextCall = 60 / (int)EnemySpawnRate - (lastCalled - nowSec);
                CancelInvoke("SpawnEnemy");
                repeating = false;
            }
            return;
        }
        else
        {
            if (!repeating && !isGameOver)
            {
                InvokeRepeating("SpawnEnemy", nextCall, 60f / EnemySpawnRate);
                repeating = true;
            }
        }

    }

    /* Countdown coroutine */

    private IEnumerator Countdown()
    {
        while(timeLeft > 0.0f)
        {
            yield return new WaitForSeconds(1.0f);
            if(!PauseMenu.IsOn)
                timeLeft--;
        }
    }

    private void SetCameraState(bool _state)
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(_state);
        }
    }

    private void SetQuizQuestions()
    {
        QuestionManager questions = mainMonitor.GetComponent<QuestionManager>();
       
        questions.SetNumQuestions(numQuestions);
        for(int i = 0; i< numQuestions; i++)
        {
            Question question = textController.GetRandomQuestion();
            questions.SetQuestionData(i, question.QuestionString, question.Option1, question.Option2, question.Option3, question.Correct);
        }

    }

    /* Spawn functions */

    private void SpawnLaptop()
    {
        laptopRandomNumberList = new List<int>(new int[laptopSpawnPoints.Length]);

        //for (int i = 0; i < laptopSpawnNumber; i++)
        for (int i = 0; i < laptopSpawnNumber; i++)
        {
            int laptopSpawnPoint = UnityEngine.Random.Range(0, laptopSpawnPoints.Length);
           
            while (laptopRandomNumberList.Contains(laptopSpawnPoint))
            {
                laptopSpawnPoint = UnityEngine.Random.Range(0, laptopSpawnPoints.Length);
            }

            laptopRandomNumberList[i] = laptopSpawnPoint;
           
            GameObject laptop = (GameObject)Instantiate(laptopCollectablePrefab, laptopSpawnPoints[laptopSpawnPoint].transform.position, laptopSpawnPoints[laptopSpawnPoint].transform.rotation);
            spawnedLaptops[i] = laptop;
            spawnedLaptops[i].name = "Laptop" + i;
            laptop.GetComponent<LaptopData>().SetLaptopData(textController.information[i].Split(':')[0]);
            laptop.GetComponent<LaptopData>().SetLaptopData(textController.information[i].Split(':')[0]);
            
        }
        StartCoroutine(FreezeLaptops());
    }

    public IEnumerator FreezeLaptops()
    {
        yield return new WaitForSeconds(2f);
        foreach(GameObject _laptop in spawnedLaptops)
        {
            _laptop.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void SpawnBoxes()
    {
        boxesRandomNumberList = new List<int>(new int[boxesSpawnPoints.Length]);

        for (int jj = 0; jj < boxesSpawnNumber; jj++)
        {
            int boxSpawnPoint = UnityEngine.Random.Range(0, boxesSpawnPoints.Length);
           
            while (boxesRandomNumberList.Contains(boxSpawnPoint) )
            {
                boxSpawnPoint = UnityEngine.Random.Range(0, boxesSpawnPoints.Length);
            }

            boxesRandomNumberList[jj] = boxSpawnPoint;

            GameObject closedBox = (GameObject)Instantiate(closedBoxPrefab, boxesSpawnPoints[boxSpawnPoint].transform.position, boxesSpawnPoints[boxSpawnPoint].transform.rotation);

            closedBox.name = "ClosedBox" + jj;

            closedBox.tag = RandomTag();

            closedSpawnedBoxes[jj] = closedBox;
        } 
    }

    private string RandomTag()
    {
        string _tag = boxTags[UnityEngine.Random.Range(0, boxTags.Count)];
        while (tagsAdded.Contains(_tag))
        {
            _tag = boxTags[UnityEngine.Random.Range(0, boxTags.Count)];
        }
        tagsAdded.Add(_tag);
        if(boxTags.Count == tagsAdded.Count)
        {
            tagsAdded.Clear();
        }
        return _tag;

    }

    private void SpawnPlayer()
    {
        GameObject _spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        playerIns = (GameObject)Instantiate(playerPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
        SetCameraState(false);

        playerIns.name = playerName;
        CreateSpawnEffect(playerIns.transform.position);
    }

    private void SpawnEnemy()
    {
        GameObject _enemySpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        GameObject _enemyIns = (GameObject)Instantiate(enemyPrefab, _enemySpawnPoint.transform.position, _enemySpawnPoint.transform.rotation);
        _enemyIns.name = "Enemy";
        _enemyIns.GetComponent<Enemy>().SetID(_enemyIns.name);
        _enemyIns.GetComponent<Enemy>().SetHealth(EnemyHealth);
        _enemyIns.GetComponent<EnemyController>().SetDamage(EnemyDamage);
        enemies.Add(_enemyIns);
        CreateSpawnEffect(_enemyIns.transform.position);
        lastCalled = (int)timeLeft;
    }

    /* Effect functions */

    private void CreateSpawnEffect(Vector3 _position)
    {
        GameObject _spawnIns = (GameObject)Instantiate(spawnEffect, _position, Quaternion.identity);
        Destroy(_spawnIns, 2f);
    }

    private void CreateDeathEffect(Vector3 _position)
    {
        GameObject _destroyIns = (GameObject)Instantiate(deathEffect, _position, Quaternion.identity);
        audioSource.clip = explosion;
        audioSource.Play();
        Destroy(_destroyIns, 2f);
    }


    /* Public functions */


    public void incrementLaptopCounter()
    {
        laptopsCollectedCounter++;
    }

    /* Enemy death functions */

    public void KillEnemy(GameObject _enemy)
    {
        CreateDeathEffect(_enemy.transform.position);
        Destroy(_enemy);
        enemies.Remove(_enemy);
    }

    public void SetBox(GameObject _box)
    {
        playerUIInstance.GetComponent<PlayerUI>().SetBox(_box);
    }

    public void SetLaptop(GameObject _laptop)
    {
        playerUIInstance.GetComponent<PlayerUI>().SetLaptop(_laptop);     
    }

    public void SetMainMonitor(GameObject _MainMonitor)
    {
        playerUIInstance.GetComponent<PlayerUI>().SetMainMonitor(_MainMonitor);
    }

    public void OpenBox(GameObject _collisionObject)
    {
        GiveBoxReward(_collisionObject.transform.tag);
        Vector3 _position = _collisionObject.transform.position;
        Quaternion _rotation = _collisionObject.transform.rotation;
        Destroy(_collisionObject);
        Instantiate(openBoxPrefab, _position, _rotation);
        PlayCollectionSound();
    }

    public void GiveBoxReward(string _type)
    {
        if(_type == "AmmoBox")
        {
            playerIns.GetComponent<WeaponManager>().GetCurrentWeapon().ammoLeft += 2 * playerIns.GetComponent<WeaponManager>().GetCurrentWeapon().clipSize;
        }
        else if (_type == "TimeBox")
        {
            timeLeft += 1.0f * 60.0f;
        }
        else if (_type == "HealthBox")
        {
            playerIns.GetComponent<Player>().AddHealth(50);
        }
    }

    public void PlayCollectionSound()
    {
        audioSource.clip = collectionSound;
        audioSource.Play();
    }

    public void PlayQuizCorrectSound()
    {
        audioSource.clip = correctSound;
        audioSource.Play();
    }


    public void PlayQuizWrongSound()
    {
        audioSource.clip = wrongSound;
        audioSource.Play();
    }

    public void SetPlayerUIInstance(GameObject _playerUIInstance)
    {
        playerUIInstance = _playerUIInstance;
    }

    public GameObject GetPlayerInstance()
    {
        return playerIns;
    }

    public int GetScore()
    {
        score = (int)timeLeft * (Difficulty.difficulty+1) + Difficulty.cumulativeScore;
        return score;
    }

    public float GetTime()
    {
        return timeLeft;
    }

    public void SetGameOverType(string _type, int _questions)
    {
        switch (_type)
        {
            case "incorrect":
                {
                    gameOverPrompt.text = "Unfortunately, you only got "+_questions+"/"+numQuestions+"  on the quiz, the hacker got away.";
                    score = (int)timeLeft/(numQuestions - _questions + 1) * (Difficulty.difficulty + 1) + Difficulty.cumulativeScore;
                    break;
                }
            case "complete":
                {
                    gameOverPrompt.text = ScenarioManager.GetEndQuote();
                    score = (int)timeLeft * (Difficulty.difficulty + 1) + Difficulty.cumulativeScore;
                    gameOverRestartButton.SetActive(false);
                    break;
                }
            case "died":
                {
                    gameOverPrompt.text = "Unfortunately you did not find the hacker, hit restart to try that level again.";
                    score = (int)(MinutesToPlay*60f-timeLeft) * (Difficulty.difficulty + 1) + Difficulty.cumulativeScore;
                    break;
                }
        }
        GameOver();
    }

    private void GameOver()
    {
        StopAllCoroutines();
        user = Utility.GetUserLoggedIn();

        isGameOver = true;
        SetCameraState(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //deactivate mini map
        miniMap.SetActive(false);

        //stop spawning enemies
        CancelInvoke("SpawnEnemy");
        repeating = false;

        //kill all enemies
        for (int i = 0; i< enemies.Count; i++)
        {
            Destroy((GameObject)enemies[i]);
        }

        //destroy player user interface
        if (playerUIInstance != null)
        {
            Destroy(playerUIInstance);
        }

        //kill player
        CreateDeathEffect(playerIns.gameObject.transform.position);
        Destroy(playerIns);

        scoreText.text = score.ToString();

        if (score > Convert.ToInt32(user["highscore"]))
        {
            highScoreText.text = score.ToString();
        }
        else
        {
            highScoreText.text = user["highscore"].ToString();
        }

        //show game over menu
        gameOver.SetActive(true);
    }
}
