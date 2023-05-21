using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    public int price;
    //public bool itemPurchased;
    public Stats stats;
    public CharacterController characterController;
    public GameObject player;
    public Shooting shooting;
    public TextMeshProUGUI displayText;
    public string customText;


    public enum ItemType
    {
        AttackSpeed,
        Ammo,
        PointsIncrease,
        HealthUp,
        RollCooldown,
        RollDistance,
        ReloadSpeed
        // Add more item types as needed
    }

    public ItemType itemType;

    private bool inRadius;

    private void Awake()
    {
        stats = FindObjectOfType<Stats>();
        characterController = FindObjectOfType<CharacterController>();
        shooting = FindObjectOfType<Shooting>();
        player = GameObject.FindGameObjectWithTag("Player");
        displayText = GetComponentInChildren<TextMeshProUGUI>();
        displayText.text = "";

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRadius = true;
        displayText.text = customText;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRadius = false;
        displayText.text = "";

    }

    private void Update()
    {
        if (inRadius && Input.GetKeyDown(KeyCode.X))
        {
            if (player.GetComponent<Stats>().MaxPoints >= price)
            {
                PurchaseItem();
                displayText.text = "you killed for that...";
                StartCoroutine(resetText());
                
            }
            else
            {
                displayText.text = "lol poor";
                StartCoroutine(resetText());
            }
        }
    }

    private void PurchaseItem()
    {
        player.GetComponent<Stats>().MaxPoints -= price;

        // Apply the effect based on the purchased item type
        switch (itemType)
        {
            case ItemType.AttackSpeed:
                // Apply attack speed increase logic
                shooting.GetComponent<Shooting>().ShootDelay -= 0.1f;
                break;
            case ItemType.Ammo:
                // Apply ammo increase logic
                shooting.GetComponent<Shooting>().MaxBullets += 10;
                break;
            case ItemType.PointsIncrease:
                characterController.GetComponent<Stats>().PointsMultiplier += 1;
                break;
            case ItemType.HealthUp:
                characterController.GetComponent<Stats>().MaxHealth += 50;
                break;
            case ItemType.RollCooldown:
                characterController.GetComponent<CharacterController>().DashCooldown -= 1.5f;
                break;
            case ItemType.RollDistance:
                characterController.GetComponent<CharacterController>().DashSpeedMultiplier += 2.5f;
                break;
            case ItemType.ReloadSpeed:
                shooting.GetComponent<Shooting>().ReloadSpeed -= 1.5f;
                break;
                // Add more cases for other item types
        }

        //itemPurchased = true;
    }
    IEnumerator resetText()
    {
        yield return new WaitForSeconds(2);
        while (inRadius)
        {
            displayText.text = customText;
        }
    }
  
 }
