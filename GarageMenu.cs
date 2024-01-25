//controls garage menu options and fucntions
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;

public class GarageMenu : MonoBehaviour
{

    //set HUD and functional elements
    public Text levelText;
    public TextMeshProUGUI coinText;
    private int coins;
    private int level;
    private int car;
    private int lastCar;
    private int c;
    public GameObject carobj;

    void Start()
    {
        //get playerprefs
        coins = PlayerPrefs.GetInt("coins");
        level = PlayerPrefs.GetInt("level");
        car = PlayerPrefs.GetInt("car");
        //find the correct car to show
        for (c = 0; c < 10; c++)
        {
            if (c == car)
            {
                carobj = FindInActiveObjectByName(c.ToString());
                carobj.SetActive(true);
            }
            else
            {

                carobj = FindInActiveObjectByName(c.ToString());
                carobj.SetActive(false);
            }
        }
        //sets player level to 1 if its still 0 like default
        if (level == 0)
        {
            level++;
            PlayerPrefs.SetInt("level", level);

        }
        //shows that car is maxed out or not
        if (level == 10)
        {
            levelText.text = "Level: " + level.ToString() + "(Max)";
        }
        else
        {
            levelText.text = "Level: " + level.ToString();
        }
        //display coins amount
        coinText.text = "Coins: " + coins.ToString();

    }

    //goes to main game scene
    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

    // function to get inactive gameobjects by name (by checking through transform)
    private GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    //function that upgrades cars level
    public void UpgradeCar()
    {
        //check if maxed
        if (level < 10)
        {
            //check if has enough coins
            if (coins >= 10)
            {
                level++;
                PlayerPrefs.SetInt("level", level);
                coins -= 10;
                PlayerPrefs.SetInt("coins", coins);
                //shows maxed or not
                if (level == 10)
                {
                    levelText.text = "Level: " + level.ToString() + "(Max)";
                }
                else
                {
                    levelText.text = "Level: " + level.ToString();
                }
                //show new coins balance
                coinText.text = "Coins: " + coins.ToString();
            }

        }
    }

    //changes car skin to random new one
    public void ChangeCarSkin()
    {
        //check if has enough coins
        if (coins >= 20)
        {
            //makes sure they won't get the same car again (not guaranteed still, but low chances)
            lastCar = car;
            car = Random.Range(0, 10);
            if (lastCar == car)
            {
                car = Random.Range(0, 10);
            }
            //change the players car
            PlayerPrefs.SetInt("car", car);
            coins -= 20;
            PlayerPrefs.SetInt("coins", coins);
            coinText.text = "Coins: " + coins.ToString();
            // set new car active and old one inactive
            for (c = 0; c < 10; c++)
            {
                if (c == car)
                {
                    GameObject carobj = FindInActiveObjectByName(c.ToString());
                    carobj.SetActive(true);
                }
                else
                {
                    GameObject carobj = FindInActiveObjectByName(c.ToString());
                    carobj.SetActive(false);
                }
            }
        }
    }

    //set car skin back to origional
    public void DefaultCarSkin()
    {
        //check if has enough coins
        if (coins >= 10)
        {
            //sets car to default
            car = 0;
            PlayerPrefs.SetInt("car", car);
            coins -= 10;
            PlayerPrefs.SetInt("coins", coins);
            coinText.text = "Coins: " + coins.ToString();
            //sets new car and removes old one
            for (c = 0; c < 10; c++)
            {
                if (c == car)
                {
                    GameObject carobj = FindInActiveObjectByName(c.ToString());
                    carobj.SetActive(true);
                }
                else
                {
                    GameObject carobj = FindInActiveObjectByName(c.ToString());
                    carobj.SetActive(false);
                }
            }
        }
    }
}

