using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    public GameObject item, textContainer;
    public TextMeshProUGUI pickupText;

    private PlayerObject p = PlayerObject.GetInstance();

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            textContainer.SetActive(true);
            if (item.name == "Ham")
            {
                pickupText.text = "Press[c] to pick up a meat";
            }
            else if (item.name == "Potion")
            {
                pickupText.text = "Press[c] to pick up a potion";
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (item.name == "Ham")
                {
                    Debug.Log("Meat"+p.GetMeat());
                    p.SetMeat(p.GetMeat() + 1);
                    Destroy(item);
                    textContainer.SetActive(false);

                }
                else if (item.name == "Potion")
                {
                    Debug.Log("Hp"+p.GetHealthPotion());
                    p.SetHealthPotion(p.GetHealthPotion() + 1);
                    Destroy(item);
                    textContainer.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        textContainer.SetActive(false);
    }

}
