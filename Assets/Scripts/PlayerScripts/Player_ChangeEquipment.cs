using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ChangeEquipment : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public Player_Combat combat;
    public Player_Bow bow;
    void Update()
    {
        if (Input.GetButtonDown("ChangeEquipment"))
        {
            combat.enabled = !combat.enabled;
            bow.enabled = !combat.enabled;
        }
    }
}
