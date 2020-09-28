using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class gameManager : MonoBehaviour 
{

    bool gameHasEnded = false;

    public GameObject completeLevelUI;

    public float restartDelay = 2f;

    GameObject player;
    public GameObject Turtle;

    [Header("Replay")]
    bool instantReplay = false;
    float replayStart;
    public GameObject Replay;
    public Transform ReplayPosition;
    

    [Header("Change Ground Color")]
    public GameObject gr1;
    public GameObject gr2;

    private void OnEnable()
    {
        playerScore.OnHitCertainPoint += ChangeGroundColor;
        playerCollision.OnHitObstacle += endGame;
        playerScore.SecretEnding += TheTorpidTurtle;
    }

    private void OnDisable()
    {
        playerScore.OnHitCertainPoint -= ChangeGroundColor;
        playerCollision.OnHitObstacle -= endGame;
        playerScore.SecretEnding -= TheTorpidTurtle;
    }

    void Start()
    {
        Replay.SetActive(false);
        playerMovement PlayerMovement = FindObjectOfType<playerMovement>();
        player = PlayerMovement.gameObject;

        if (CommandLog.commands.Count > 0)
        {
            instantReplay = true;
            Replay.SetActive(true);
            replayStart = Time.timeSinceLevelLoad;
        }

        
    }

    void Update()
    {
        if (instantReplay)
        {
            InstantReplay();
        }
    }

    public void completeLevel()
    {
        completeLevelUI.SetActive(true);
    }

    public void endGame(Collision collisionInfo)
    {
        player.GetComponent<playerMovement>().enabled = false;

        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
        } 
        
    }

    public void ChangeGroundColor()
    {
        playerScore.OnHitCertainPoint -= ChangeGroundColor;
        gr1.GetComponent<Renderer>().material.color = Color.red;
        gr2.GetComponent<Renderer>().material.color = Color.blue;

    }

    public void TheTorpidTurtle()
    {
        playerScore.SecretEnding -= TheTorpidTurtle;
        Turtle.SetActive(true);
    }

    void Restart()
    {
        player.GetComponent<playerMovement>().enabled = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.transform.position = ReplayPosition.transform.position;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.transform.eulerAngles = new Vector3(0, 180, 0);
        gameHasEnded = false;
    }

    void InstantReplay()
    {
        if (CommandLog.commands.Count == 0)
        {
            return;
        }

        Command command = CommandLog.commands.Peek();
        if (Time.timeSinceLevelLoad >= command.time)
        {
            command = CommandLog.commands.Dequeue();
            command.Player = player.GetComponent<Rigidbody>();
            Invoker invoker = new Invoker();
            invoker.disableLog = true;
            invoker.SetCommand(command);
            invoker.ExeCommand();
        }
    }

}
