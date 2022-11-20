using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Code Based Requirements (Frameworks)
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class njljaysvkbhsvbhk : MonoBehaviour
{
    // ------------------- ----------------------- ----------------------- -------------------- ---------------------- -------------------- [Declaration]

    // Game Objects pulled from assets as prefabs

    // use for all private GameObjects and Variables used further down in the code. [SerializeField]

    // Further declaration will be done using the current MonoBehaviour Status field in comment form. 

    //GameObjects currently in use ------------------ [GameObjects contained within the DEMO version 0.1]

    // Canvas to World Transformation
    private int canvasXOutsideScreenSpace;
    private int canvasYMax;
    private int canvasYMin;

    //VillageGoGo prefab pooling;
    private GameObject car1;
    private GameObject car2;
    private GameObject car3;
    private GameObject car4;
    private GameObject car5;
    private GameObject car6;
    private GameObject car7;
    private GameObject car8;
    private GameObject coin;
    public GameObject trackPiece;

    //Level1 prefab pooling;
    private GameObject tomato;
    private GameObject seeds;
    private GameObject apple;
    private GameObject carrot;

    //MainMenu SelectionScripting ------------------------------------------------------- [Scenes]

    //All scenes are loaded as public scenes in the beginning of the program´s runtime.

    /*
     * Scene(1) == MainMenuScene;
     * Scene(2) == LevelSelectScreen;
     * Sceen(3) == Level1Scene;
     * Scene(4) == MiniGameScene;
     * 
     *  Subdivisions of scenes are divided as GameObjects within the hierarchy;
     *          Scene(4.1) == VillageGoGo;
     */

    // Currently in use are only three private serialized text objects.

    private Text scoreInCurrentRunText;
    private Text coinsInCurrentRunText;
    private Text livesText;

    // Corresponding Variables defined as floats and/or integers;

    private int coins;
    private int lives;
    private float timeInCurrentRun; // Defined as score; Also kept as a separate variable for ease of access;

    // Tweening Script Variables (Highly Interchangeable and Modular)
    private float tweenSpeed;
    private int xDir;
    private int yDir;
    private float timeMod;

    private float timeToTween;
    private float currentTimeToTween;

    //CarScript Variables (Used Specifically for the purpose of simulating movement in the "VillageGoGo" minigame.

    private float speed;
    private float speedOfAi;
    private float speedOfAiX;
    private float speedOfAiY;   
    public GameObject miniGameController;
    public GameObject player;
    private GameObject endGame;
    private int carToSpawn;

    //public TypeOfScript (Player) playerController;
    //public TypeOfScript (AiCar) miniGameControllerScript;

    // ------------------------------------------------------------------ //

    //Level1 Mini Game "Command Shuffle"

    //All Objects within the scene have a RigidBody2D attached to them with a BoxCollider2D and a PhysicsMaterial2D with the "Bouncines" set to 1;

    public GameObject obj1String;
    public GameObject obj2String;
    public GameObject obj3String;
    public GameObject obj4String;
    public GameObject obj5String;
    // By default all 5 are set to activeSelf == false;


    // All Void Functions and callbacks in the current DEMO version --------------------- [ FUNCTIONS ] -----------------------------------

    private bool isDragable = false;
    private bool dragActive = false;
    private Vector2 screenPos;
    private Vector3 worldPos;
    
    //private TypeOfScript (TouchScreen) Dragable;

    // ------------------------------------------------------- [ OBJECT SPAWNER ] --------------------------------------------

    public float currentTimeToSpawnCar;
    public float currentTimeToSpawnCoin;
    public float timeToSpawnCar;
    public float timeToSpawnCoin;

    public void Awake()
    {
        // VillageGoGo Mini Game
        miniGameController = GameObject.Find("MiniGameController");
        //miniGameControllerScript = miniGameControllerScript.GetComponent<TypeOfScript>();

        player = GameObject.Find("PlayerController");
        //playerController = player.GetComponent<TypeOfScript>();
    }

    public void Start()
    {
        lives = 3;
        coins = 0;
    }
    
    public void Update()
    {
        // UI Update Functions

        livesText.text = lives.ToString();
        coinsInCurrentRunText.text = coins.ToString();

        scoreInCurrentRunText.text = timeInCurrentRun.ToString("F0");

        // -------------------------------

        timeInCurrentRun += Time.deltaTime;

        //AiCar

        speedOfAiX += 40 + Time.deltaTime;
        speedOfAiY += 50 + Time.deltaTime;

        if(timeInCurrentRun > 10 && timeInCurrentRun < 20)
        {
            speedOfAi = Random.Range(1, 5);
        }
        else if(timeInCurrentRun > 20 && timeInCurrentRun < 30)
        {
            speedOfAi = Random.Range(10, 15);
        }
        else if(timeInCurrentRun > 30 && timeInCurrentRun < 60)
        {
            speedOfAi = Random.Range(20, 40);
        }
        else
        {
            speedOfAi = Random.Range(speedOfAiX, speedOfAiY);
        }

        transform.Translate(Vector2.right /*Defined as (x = 1, y = 0)*/ * -speedOfAi * Time.deltaTime * 10);

        //Physics based operations are done through the GUI (Based on the RigidBody2D Component and the "Bouncy" 
        //multiplier being set to 1, of the PhysicsMaterial attached to this GameObject).

        /* Rigidbodies in general are set to :
         * Mass 1
         * Start Awake
         * Interpolate 
         * Gravity Scale 0
         */

        // VillageGoGo FUNCTIONS INSERTED IN THE MAIN GAMEPLAY LOOP 

        // -----------------------------------------------------------------------------

        // TWEENING FUNCTION INSERTED IN THE MAIN GAMEPLAY LOOP 

        currentTimeToTween -= Time.deltaTime;

        if(currentTimeToTween >  currentTimeToTween / 2)
        {
            Tween();
        }
        else if(currentTimeToTween < currentTimeToTween / 2)
        {
            TweeNegative();
        }
        else
        {
            currentTimeToTween = timeToTween;
        }

        // --------------------------------------------------------- [ Touch Input Method] ---------------------

        // ----------------------------------------------- DO NOT TOUCH ---------------------------------

        // Update Touch Inputs 

        if(Input.touchCount > 0)
        {
            screenPos = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        if(isDragable)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null)
            {
                /*Dragable "draggable" = hit.transform.gameObject.GetComponent<Draggable>();
                if(draggable != null)
                {
                    lastDragged = draggable;
                    InitDrag();
                }
                */
            }
        }
    }

    void InitDrag()
    {
        isDragable = true;
    }

    void Drag()
    {
        //Dragable.transform.position = new Vector2(worldPos.x, worldPos.y);
    }

    void Drop()
    {
        isDragable = false; 
    }

    // ------------------------ DO NOT TOUCH ------------------------------------------------------

    public void FixedUpdate()
    {
        currentTimeToSpawnCar -= Time.deltaTime;
        currentTimeToSpawnCoin -= Time.deltaTime;

        if(currentTimeToSpawnCar < 0)
        {
            carToSpawn = Random.Range(1, 9);

            switch(carToSpawn)
            {
                case 8:
                    Instantiate(car8, new Vector3(canvasXOutsideScreenSpace, Random.Range(canvasYMin, canvasYMax), 0 /*2D Space doesn´t have a third dimension so we default to 0*/), Quaternion.identity);
                    break;
                case 7:
                    Instantiate(car7, new Vector3(canvasXOutsideScreenSpace, Random.Range(canvasYMin, canvasYMax), 0 /*2D Space doesn´t have a third dimension so we default to 0*/), Quaternion.identity);
                    break;
                case 6:
                    Instantiate(car6, new Vector3(canvasXOutsideScreenSpace, Random.Range(canvasYMin, canvasYMax), 0 /*2D Space doesn´t have a third dimension so we default to 0*/), Quaternion.identity);
                    break;
                case 5:
                    Instantiate(car5, new Vector3(canvasXOutsideScreenSpace, Random.Range(canvasYMin, canvasYMax), 0 /*2D Space doesn´t have a third dimension so we default to 0*/), Quaternion.identity);
                    break; 
                case 4:
                    Instantiate(car4, new Vector3(canvasXOutsideScreenSpace, Random.Range(canvasYMin, canvasYMax), 0 /*2D Space doesn´t have a third dimension so we default to 0*/), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(car3, new Vector3(canvasXOutsideScreenSpace, Random.Range(canvasYMin, canvasYMax), 0 /*2D Space doesn´t have a third dimension so we default to 0*/), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(car2, new Vector3(canvasXOutsideScreenSpace, Random.Range(canvasYMin, canvasYMax), 0 /*2D Space doesn´t have a third dimension so we default to 0*/), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(car1, new Vector3(canvasXOutsideScreenSpace, Random.Range(canvasYMin, canvasYMax), 0 /*2D Space doesn´t have a third dimension so we default to 0*/), Quaternion.identity);
                    break;
            }

            currentTimeToSpawnCar = timeToSpawnCar;
        }

        if(currentTimeToSpawnCoin < 0)
        {
            Instantiate(coin, new Vector3(canvasXOutsideScreenSpace, Random.Range(canvasYMin, canvasYMax), 0 /*2D Space doesn´t have a third dimension so we default to 0*/), Quaternion.identity); 

            currentTimeToSpawnCoin = timeToSpawnCoin;
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Function defined by tag. Tags are limited but include certain functional capabilities.
        /* Tags are as follows :
         * Coins
         * aiCar
         * cout
         * <<
         * Text
         * da
         * PlayerController //(Dana in this, case function caller)
         */

        if (other.tag == "<<")
        {
            obj1String.SetActive(true);
        }
        else if(other.tag == "cout")
        {
            obj2String.SetActive(true);
        }
        else if(other.tag == "Text")
        {
            obj3String.SetActive(true);
        }
        else if(other.tag == "da")
        {
            if(obj4String.activeSelf != true)
            {
                obj4String.SetActive(true);
            }
            else
            {
                obj5String.SetActive(true);
            }
        }

        if(other.tag == "aiCar")
        {
            lives--;
        }

        if(other.tag == "Coins")
        {
            coins++; 
        }
    }

    public void Tween()
    {
        transform.Translate(new Vector3(xDir, yDir, 0) * tweenSpeed * Time.deltaTime * timeMod);
    }

    public void TweeNegative()
    {
        transform.Translate(new Vector3(xDir, yDir, 0) * -tweenSpeed * Time.deltaTime * timeMod);
    }

    // BUTTON FUNCTIONS -------------------------------------------- Called through the GUI; 

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadMiniGameScene()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(3);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.1f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    // BUTTON FUNCTIONS -----------------------------------------------------

    public void EndGame()
    {
        endGame.SetActive(true);
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
}
