using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    Transform[] targets; //Transform arrays that show enemy movement direction
    private int index = 0; //the index of the target that the enemy is moving towards
    private float speed=20;

    private float health;
    public float maxHealth;
    public Slider healthSlider; //health slider

    private bool isCalled; //make sure DecreaseHealth function only called once.


    private void Start()
    {
        targets = WayPoints.positions;
        health = maxHealth;
        isCalled = false;
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (index == targets.Length) return;
        transform.position = Vector3.MoveTowards(transform.position, targets[index].transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targets[index].transform.position) < 0.01f)
        {
            transform.position = targets[index].transform.position;
            index++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("End"))
        {
            Destroy(gameObject);
            GameObject.Find("GameManager").GetComponentInChildren<GameManager>().LoseGame();
        }
    }

    public void DecreaseHealth(float amount)
    {
        health=Mathf.Clamp(health-amount,0,maxHealth);
        if (health <= 0 && !isCalled)
        {
            isCalled = true;
            Destroy(gameObject);
           
            EnemySpawner.instance.DecreaseEnemyCount();
        }
        updateHealthUI();
    }

    private void updateHealthUI()
    {
        healthSlider.value = health / maxHealth;
    }

}
