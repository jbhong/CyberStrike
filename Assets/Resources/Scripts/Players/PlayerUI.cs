using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    [Header("Pause menu reference")]
    [SerializeField] private PauseMenu pauseMenu;

    [Header("Graphics")]
    [SerializeField] RectTransform healthAmount;
    [SerializeField] Text timeText;
    [SerializeField] Text ammoText;
    [SerializeField] Text laptopsText;
    [SerializeField] Text promptText;
    [SerializeField] GameObject prompt;
    [SerializeField] Image rewardType;
    [SerializeField] GameObject rewardPopup;
    [SerializeField] Sprite healthSprite;
    [SerializeField] Sprite ammoSprite;
    [SerializeField] Sprite timeSprite;

    /* Game object references */
    private GameManager gameManager;
    private Player player;

    private GameObject box;
    private GameObject laptop;
    private GameObject MainMonitor;

    private ArrayList laptopsFound = new ArrayList();

    /* Start method */

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        prompt.SetActive(false);
        rewardPopup.SetActive(false);
    }

    /* Update method */

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }
        if(box != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                OpenBox();
            }
        }
        if(laptop != null)
        {
            laptop.GetComponent<MeshRenderer>().material.color = Color.green;
            if (Input.GetKeyDown(KeyCode.F))
            {
                LaptopDisplay();
            }
        }
        if (MainMonitor != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (gameManager.laptopsCollectedCounter == gameManager.laptopSpawnNumber) {
                    StartQuiz();
                }
            }
        }
        SetHealthAmount(player.GetHealth());
        SetTime(gameManager.GetTime());
        SetAmmoAmount(player.GetComponent<WeaponManager>().GetCurrentWeapon().bullets, player.GetComponent<WeaponManager>().GetCurrentWeapon().ammoLeft);
        SetLaptopText(gameManager.laptopsCollectedCounter, gameManager.laptopSpawnNumber);

       

    }

    private void SetHealthAmount(int _amount)
    {
        healthAmount.localScale = new Vector3(Mathf.Clamp(_amount / 100f, 0, 1), 1f, 1f);
    }

    private void SetTime(float _secondsLeft)
    {
        string seconds = (_secondsLeft % 60f).ToString();
        if(Convert.ToDouble(seconds) < 10.0f)
        {
            seconds = "0" + seconds;
        }
        string minutes = Mathf.FloorToInt(_secondsLeft / 60f).ToString();
        timeText.text = minutes + ":" + seconds;
    }

    private void SetAmmoAmount(int _ammo, int _ammoLeft)
    {
        ammoText.text = _ammo.ToString() + " / " + _ammoLeft.ToString();
    }

    private void SetLaptopText(int _collectedLaptops, int _laptopNumber)
    {
        laptopsText.text = _collectedLaptops.ToString() + " / " + _laptopNumber.ToString();
    }

    /* Public functions */
    public void SetBox(GameObject _box)
    { 
        box = _box;
        if (box != null)
        {
            string boxType = _box.tag;
            if (boxType == "AmmoBox")
            {
                promptText.text = "Press the F key to get more ammo.";
            }
            else if (boxType == "HealthBox")
            {
                promptText.text = "Press the F key to get more health.";
            }
            else if (boxType == "TimeBox")
            {
                promptText.text = "Press the F key to get more time.";
            }
            prompt.SetActive(true);
        }
        else
        {
            prompt.SetActive(false);
        }
    }

    public void SetLaptop(GameObject _laptop)
    {
        laptop = _laptop;
        if (laptop != null)
        {
            promptText.text = "Press the F key to get more information.";
            prompt.SetActive(true);
        }
        else
        {
            prompt.SetActive(false);
        }
    }

    public void SetMainMonitor(GameObject _MainMonitor)
    {
        MainMonitor = _MainMonitor;
        if (MainMonitor != null)
        {
            if (gameManager.laptopsCollectedCounter == gameManager.laptopSpawnNumber)
            {
                MainMonitor.GetComponent<MeshRenderer>().material.color = Color.green;
                promptText.text = "You have found all the information from the laptops, press F to start the quiz.";
                prompt.SetActive(true);
            }
            else
            {
                promptText.text = "You cannot start the quiz until you have collected the information from all laptops. ";
                prompt.SetActive(true);
            }
        }
        else
        {
            prompt.SetActive(false);
        }
        
    }

    public void StartQuiz()
    {
        pauseMenu.SetPauseType("question");
        pauseMenu.SetQuestionText(MainMonitor, "Quiz");
        prompt.SetActive(false);
    }

    public void LaptopDisplay()
    {
        if (laptopsFound.IndexOf(laptop.transform.name) == -1)
        {
            gameManager.PlayCollectionSound();
            IncrementLaptopsCollected();
            laptopsFound.Add(laptop.transform.name);
            
        }
        ShowInformation(laptop.GetComponent<LaptopData>().GetLaptopData());
        prompt.SetActive(false);
    } //LaptopDisplay()

    public void OpenBox()
    {
        gameManager.OpenBox(box);
        string _boxType = box.transform.tag;
        SetBox(null);
        if(_boxType == "AmmoBox")
        {
            rewardType.sprite = ammoSprite;
        }
        if (_boxType == "TimeBox")
        {
            rewardType.sprite = timeSprite;
        }
        if (_boxType == "HealthBox")
        {
            rewardType.sprite = healthSprite;
        }
        rewardPopup.SetActive(true);
        StartCoroutine(ClosePopup());
    } // OpenBox()

    public IEnumerator ClosePopup()
    {
        yield return new WaitForSeconds(2.0f);
        rewardPopup.SetActive(false);
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
    }

    public void IncrementLaptopsCollected()
    {
        gameManager.incrementLaptopCounter();
    }

    public void ShowInformation(string _information)
    {
        pauseMenu.SetPauseType("info");
        pauseMenu.SetInfoText(_information);
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetPauseType("regular");
    }
}
