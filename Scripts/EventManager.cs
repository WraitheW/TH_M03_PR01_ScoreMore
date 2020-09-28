using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public delegate void Click();
    public static event Click onClicked;

    [SerializeField] List<GameObject> Obstacles;

    List<Transform> ObstacleList;

    public Button menuButton;

    void Awake()
    {
        menuButton = GameObject.Find("Menu").GetComponent<Button>();
        AutoFillObstacles();
        //int i = 0;
        //foreach (GameObject obj in Obstacles)
        //{
        //    ObstacleList[i] = obj.transform;
        //    i++;
        //}
    }

    void Start()
    {
        menuButton.onClick.AddListener(onButtonClicked);
    }

    void onButtonClicked()
    {
        onClicked?.Invoke();
    }

    public void onEnable()
    {
        //playerCollision.OnHitObstacle += incrementTries;
        //playerCollision.OnHitObstacle += removeObject;
    }

    [ContextMenu("AutoFill Obstacles")]
    void AutoFillObstacles()
    {
        foreach(GameObject obst in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obst.name.Contains("traffic"))
            {
                Obstacles.Add(obst);
                DontDestroyOnLoad(obst);
            }
        }
        
    }

    public void removeObject(Collision collisionInfo)
    {
        Obstacles.Remove(collisionInfo.gameObject);
    }

}
