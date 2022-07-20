using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "New BallWave")]
public class EnemyWave : ScriptableObject

{
    public GameObject enemyPreafb;
    public float speed;
    public int enemyCount;
}
