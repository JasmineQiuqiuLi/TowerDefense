using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public Dictionary<GameObject, GameObject> cubes = new Dictionary<GameObject, GameObject>();
    public Turrets[] turrets;
    public static TurretSpawner instance;

    float money;
    public float Money { get { return money; } }

    public ParticleSystem buildEffect;



    private void Awake()
    {
        instance = this;
        money = 250;

        int cubeCount = transform.childCount;
        for(int i = 0; i < cubeCount; i++)
        {
            cubes.Add(transform.GetChild(i).gameObject, null);
        }

    }

    private void Start()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].costUpgraded = 80 + i * 20;
        }
    }

    public  void  SpawnTurret(GameObject obj)
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i].name == UIManager.choseTurret.ToString())
            {
                if (cubes[Raycast.currHitCube] == null )
                {
                    if(turrets[i].Cost <= Money)
                    {
                        GameObject newTurret = Instantiate(turrets[i].turretPrefab);
                        if (turrets[i].name == "Laser") newTurret.transform.position = obj.transform.position;
                        else if (turrets[i].name == "Missile") { newTurret.transform.position = obj.transform.position + new Vector3(0, 1, 0) * 0.35f; }
                        else { newTurret.transform.position = obj.transform.position + new Vector3(0, 1, 0) * 0.5f; }

                        UpgradeMoneyUI(turrets[i].Cost);

                        newTurret.transform.localScale = new Vector3(2f, 2f, 2f);

                        cubes[Raycast.currHitCube] = newTurret;//update the cubes list;
                    }
                    else //does not have enough money
                    {
                       
                        Money_Left.instance.PlayTextEffect();

                    }

                }
                else //upgrate the turret
                {   
                    Vector3 pos = Raycast.currHitCube.transform.position;
                    UpgradeCanvas.instance.ShowCanvas(pos);

                }

            }
        }
    }

    public void DismantleTurret()
    {
        Destroy(cubes[Raycast.currHitCube]);
      
        UpgradeCanvas.instance.HideCanvas();
    }

    public void UpgradeTurret()
    {

        GameObject currTurret = cubes[Raycast.currHitCube];
        string tagName = currTurret.tag;
        Destroy(cubes[Raycast.currHitCube]);
        foreach(Turrets turret in turrets)
        {
            if (tagName == turret.name)
            {
                GameObject newTurret=Instantiate(turret.turretUpgradePrefab, Raycast.currHitCube.transform.position, Quaternion.identity);
                newTurret.transform.localScale = new Vector3(2f, 2f, 2f);
                cubes[Raycast.currHitCube] = newTurret;

                UpgradeMoneyUI(turret.costUpgraded);

            }
        }
        UpgradeCanvas.instance.HideCanvas();

    }

    void UpgradeMoneyUI(float cost)
    {
        money -= cost;
        //Debug.Log("turret's costupgraded is " + turret.costUpgraded);
        Money_Left.instance.UpdateTextUI();
    }
    
}
