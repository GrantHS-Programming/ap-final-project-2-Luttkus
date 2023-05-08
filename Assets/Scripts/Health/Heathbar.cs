using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heathbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image MaxHealthbar;
    [SerializeField] private Image CurrentHealthbar;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
}
