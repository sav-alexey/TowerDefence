using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] int playerLife = 10;
    [SerializeField] int damageCount = 1;

    [SerializeField] Text playerLifeText;

    void Start()
    {
        playerLifeText.text = playerLife.ToString();
    }


    public void TakeDamage()
    {
        playerLife -= damageCount;
        playerLifeText.text = playerLife.ToString();
        if (playerLife <= 0)
        {
            Destroy(gameObject);
        }
    }





}
