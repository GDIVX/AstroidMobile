using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameState state { get; private set; }
    public UnityEvent<GameState> onGameStateChanged;
    public void SetState(GameState gameState)
    {
        state = gameState;
        onGameStateChanged?.Invoke(state);
    }

    private void Awake()
    {
        if (instance == null) { instance = this; }
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        state = GameState.Paused;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
        state = GameState.Running;
    }

    public void EndGame()
    {
        if(state == GameState.Eneded)
            SceneManager.LoadScene(1);

    }
}

public enum GameState
{
    Paused,
    Running,
    Eneded
}
