using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public enum TurretType
{
    Laser,
    Missile,
    Standard
}
public class UIManager : MonoBehaviour
{
    //private ToggleGroup toggleGroup;
    //public IEnumerable activeToggle;
    public TurretType turret;
    public static TurretType choseTurret;

    private void Awake()
    {
        choseTurret = TurretType.Laser;
    }

    public void toggleOn() {

        Toggle toggle = gameObject.GetComponent<Toggle>();
        
        if (toggle.isOn)
        {
            choseTurret = turret;
        }
    }


}
