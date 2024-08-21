using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# GAME CONTROL")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("GAME OBJECT")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;
    public Item item;
    [Header("# PLAYER INFO")]
    public int playerId;
    public int level;
    public float health;
    public float maxHealth = 100;
    public int kill;
    public int exp;
    public int[] nextExp;


    /*private void Start()
    {
        GameStart(playerId);
    }*/
    private void Awake()
    {
        instance = this;
    }

    public void GameStart(int id) 
    {
        playerId = id;
        health = maxHealth;
        item.CharInit(playerId);
        player.gameObject.SetActive(true);
        uiLevelUp.Select(0); // 나중에 보완
        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true) ;
        uiResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }
    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
    private void Update()
    {
        if (!GameManager.instance.isLive) 
            return;

        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
            GameVictory();
        }       
    }

    public void GetExp()
    {
        if (!isLive) return;
        exp++;
        if(exp >= nextExp[Mathf.Min(level, nextExp.Length-1)]) {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
