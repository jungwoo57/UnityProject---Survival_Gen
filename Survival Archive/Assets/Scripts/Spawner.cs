using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    int level;
    float timer;
    float bossTimer;
    public float spawnTime;
    public GameObject bossHPSlider;
    public Transform canvasTransform;
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        if (!GameManager.instance.isLive) 
            return;
        timer += Time.deltaTime;
        bossTimer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime /10f);
        if(level > 1) {
             level = Random.Range(0, 2); 
        }
        if (timer > spawnData[level].spawnTime){
            timer = 0;
            Spawn();
        }
        if(bossTimer > spawnData[2].spawnTime && GameManager.instance.kill % 10 == 0)
        {
            bossTimer = 0;
            BossSpawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
    public void BossSpawn() {
        GameObject boss = GameManager.instance.pool.Get(0);
        boss.AddComponent<Boss>();
        boss.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        boss.GetComponent<Enemy>().Init(spawnData[2]);
        boss.GetComponent<BoxCollider2D>().size = new Vector2(9,9);
        SpawnbossHPSlider(boss);
    }

    private void SpawnbossHPSlider(GameObject boss)
    {
        GameObject sliderClone = Instantiate(bossHPSlider);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;
        sliderClone.GetComponent<BossHpFollow>().SetUp(boss.transform);
        sliderClone.GetComponentInChildren<BossHPSlider>().Init(boss);
        sliderClone.SetActive(true);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int hp;
    public float speed;
}