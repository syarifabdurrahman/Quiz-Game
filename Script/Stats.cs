using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    #region StatsVariable
    public int Playermaxhealth;
    public int PlayerCurrentHealth = 7;
    #endregion

    public Sprite[] HealthBar;

    public Image HearthUI;

    public Stats PlayerStats;

    private void Start()
    {
        Playermaxhealth = PlayerCurrentHealth;
        PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }

    private void Update()
    {
        HearthUI.sprite = HealthBar[PlayerStats.PlayerCurrentHealth];
    }

    public void Damage(int Dmg)
    {
        PlayerCurrentHealth -= Dmg;
    }

}
