using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    public GameObject bulletPrfab;
    public float bulletForce;//force apply to the bullet

    private float timer;
    private float interval = 1.0f; //the standard turret and missile fire bullet every 1 second.

    
    public List<GameObject> enemyWithinRange;
    
    //the head of the turret will look at the first enemy within range
    public Transform headTransform;

    //the attack mechanic of Laser is different from other two turrets
    public bool isLaser;

    //used to renderer the laser ray;
    public LineRenderer laserLineRender;

    //the damage per second caused by Laser turret
    public float LaserDamage;

    //the start transform is a empty GameObject in the prefab that specify the start position of bullet/laser instance
    public Transform startTransform;

    private void Awake()
    {
        timer = 1.0f;//when the first enemy first enter the range, the turret will attack it immediately.
        enemyWithinRange = new List<GameObject>();

    }


    private void Update()
    {
        if (!GameManager.instance.IsGameOver)
        {
            timer += Time.deltaTime;

            if (enemyWithinRange.Count != 0 && enemyWithinRange[0] != null)
            {
                Vector3 targetPosition = enemyWithinRange[0].transform.position;
                targetPosition.y = headTransform.position.y;
                headTransform.LookAt(targetPosition);

                if (isLaser)//if the gameObject is a Laser, it will attack the enemy all the way until the enemy is out of range
                {
                    if (laserLineRender.enabled == false) laserLineRender.enabled = true;
                    //create Laser for laser turret
                    laserLineRender.positionCount = 2;

                    Vector3[] positions = new Vector3[2];
                    positions[0] = startTransform.position;
                    positions[1] = enemyWithinRange[0].transform.position;
                    laserLineRender.SetPositions(positions);

                    enemyWithinRange[0].GetComponent<Enemy>().DecreaseHealth(LaserDamage * Time.deltaTime);

                }
            }


            if (timer >= interval)
            {
                if (enemyWithinRange.Count != 0 && enemyWithinRange[0] != null)
                {
                    if (!isLaser)//if the turret is none laser, it will attack the enemy every 1 seccond.
                    {
                        GameObject bullet = Instantiate(bulletPrfab,startTransform.position, headTransform.rotation); //it will follow the position of the head
                        
                        Vector3 flyDirection = enemyWithinRange[0].transform.position - bullet.transform.position;
                        
                        Vector3.Normalize(flyDirection);

                        bullet.GetComponent<Bullets>().Attack(flyDirection, bulletForce);
                    }

                }

                timer = 0f;
            }

        }
    }

    private void LateUpdate()
    {   //when an enemy is attack to death, it will be destroyed, and the reference in the enemyWithinRange list will be missing.
        for (var i = enemyWithinRange.Count - 1; i > -1; i--)
        {
            if (enemyWithinRange[i] == null)
                enemyWithinRange.RemoveAt(i);
        }

        if (isLaser)
        {
            if (enemyWithinRange.Count == 0)
            {
                laserLineRender.enabled = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyWithinRange.Add(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyWithinRange.Remove(other.gameObject);
        }
    }



    




}
