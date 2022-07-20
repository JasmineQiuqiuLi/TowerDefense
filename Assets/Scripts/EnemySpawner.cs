using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public EnemyWave[] enemyWaves;
    public GameObject startPosition;
    public static GameObject[] aliveEnemiesInHierachy;
    private int index;
    public static EnemySpawner instance;

    //the count of enemy alive
    private int enemyAliveCount;

    private void Awake()
    {
        instance = this;
        aliveEnemiesInHierachy = GameObject.FindGameObjectsWithTag("Enemy");
        enemyAliveCount = 10;
        index = 0;
    }
    

    void Update()
    {
        if (enemyAliveCount == 0)
        {
            GameManager.instance.WinGame();

            //this code doesn't make sense, but it makes the C# stop calling the WinGame() method.
            enemyAliveCount--;
           
        }

        if (!GameManager.instance.IsGameOver)
        {
            aliveEnemiesInHierachy = GameObject.FindGameObjectsWithTag("Enemy");
            if (index >= 4) return;
            if (aliveEnemiesInHierachy.Length == 0 && index < 4)
            {
                StartCoroutine(CreateEnemy(enemyWaves[index]));
                index++;
            }
        }
    }

    IEnumerator CreateEnemy(EnemyWave enemyWave)
    {
        for(int i = 0; i < enemyWave.enemyCount; i++)
        {
            Instantiate(enemyWave.enemyPreafb, startPosition.transform.position, enemyWave.enemyPreafb.transform.rotation);
            
            yield return new WaitForSeconds(0.5f);
            
        }
    }

    public void DecreaseEnemyCount()
    {
        enemyAliveCount--;
    }

    public void STOPAllCoroutine()
    {
        StopAllCoroutines();
    }


}
