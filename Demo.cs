using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    private GameObject tomato;
    private GameObject seeds;
    private GameObject apple;
    private GameObject carrot;
    private float tweenSpeed;
    private int xDir;
    private int yDir;
    private float timeMod;
    private float timeToTween;
    private float currentTimeToTween;
    public GameObject obj1String;
    public GameObject obj2String;
    public GameObject obj3String;
    public GameObject obj4String;
    public GameObject obj5String;
    private bool isDragable = false;
    private bool dragActive = false;
    private Vector2 screenPos;
    private Vector3 worldPos;
    public void Start()
    public void Update()
    {if (currentTimeToTween > currentTimeToTween / 2)
        {Tween();}
        else if (currentTimeToTween < currentTimeToTween / 2)
        {TweeNegative();}
        else
        {currentTimeToTween = timeToTween;}
        if (Input.touchCount > 0)
        {screenPos = Input.GetTouch(0).position;}
        else
        {return;}
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        if (isDragable)
        {Drag();}
        else
        {RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null)
            {Dragable "draggable" = hit.transform.gameObject.GetComponent<Draggable>();
                if (draggable != null)
                {lastDragged = draggable;
                 InitDrag();}}}}
    void InitDrag()
    {isDragable = true;}
    void Drag()
    {Dragable.transform.position = new Vector2(worldPos.x, worldPos.y);}
    void Drop()
    {isDragable = false;}
    public void OnTriggerEnter2D(Collider2D other)
    { if (other.tag == "<<")
        {obj1String.SetActive(true);}
        else if (other.tag == "cout")
        {obj2String.SetActive(true);}
        else if (other.tag == "Text")
        {obj3String.SetActive(true);}
        else if (other.tag == "da")
        {if (obj4String.activeSelf != true)
            {obj4String.SetActive(true);}
            else
            {obj5String.SetActive(true);}}
        if (other.tag == "aiCar")
        {lives--;}
        if (other.tag == "Coins")
        {coins++;}}
    public void Tween()
    {transform.Translate(new Vector3(xDir, yDir, 0) * tweenSpeed * Time.deltaTime * timeMod);}
    public void TweeNegative()
    {transform.Translate(new Vector3(xDir, yDir, 0) * -tweenSpeed * Time.deltaTime * timeMod);}
    public void LoadMainMenu()
    {SceneManager.LoadScene(1);}
    public void LoadLevelSelect()
    {SceneManager.LoadScene(2);}
    public void LoadMiniGameScene()
    {SceneManager.LoadScene(4);}
    public void LoadLevel1()
    {SceneManager.LoadScene(3);}
    public void PauseGame()
    {Time.timeScale = 0.1f;}
    public void ResumeGame()
    {Time.timeScale = 1f;}
    public void EndGame()
    {endGame.SetActive(true);}
    public void ResetTimeScale()
    {Time.timeScale = 1f;}}

