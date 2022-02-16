using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IEnumerator_Survive : MonoBehaviour
{
    [Header("Music")]
    public Slider startVolumeSlider;
    public Slider volumeSlider;
    public AudioClip[] musicClips;
    public AudioSource ass;

    [Header("Time")]
    public Slider timeSlider;
    public Text sliderValueText;

    [Header("Texts")]
    public Text dayText;
    public Text popsText;
    public Text foodText;
    public Text waterText;
    public Text goldText;
    public Text woodText;
    public Text ironText;
    public Text mineralsText;

    public Text notificationText;

    [Header("Resources")]
    public int day;
    public int pops;
    public int food;
    public int water;
    public int gold;
    public int wood;
    public int iron;
    public int minerals;

    [Header("Panels")]
    public GameObject startGamePanel;
    public GameObject gameOverPanel;
    public GameObject tradePanel;
    public GameObject arcanePanel;
    public GameObject heroesHallPanel;

    [Header("Buttons")]
    public Button warButton;
    public Button huntButton;
    public Button mineButton;
    public Button prayButton;
    public Button arcaneButton;
    public Button tradeButton;
    public Button taxesButton;
    public Button heroesButton;

    [Header("Buttons on Panels")]
    public Button sellYourSoulButton;
    public Button adventurersButton;

    [Header("Button Images")]
    public Image warButtonImage;
    public Image demonWarButtonImage;

    //[Header("City Sizes")]
    //string family = "Family"; //1 - 20
    //string hamlet = "Hamlet"; //20 - 50
    //string tribe = "Tribe"; //50 - 100
    //string village = "Village"; //100 - 1 000
    //string shire = "Shire"; //1 000 - 5 000
    //string town = "Town"; //5 000 - 10 000
    //string city = "City"; //10 000 - 50 000
    //string district = "District"; //50 000 - 100 000
    //string county = "County"; //100 000 - 500 000
    //string municipality = "Municipality"; //500 000 - 1 000 000
    //string globalCity = "Global City"; //1 000 000 - 2 500 000
    //string metropolis = "Metropolis"; //2 500 000 - 5 000 000
    //string megalopolis = "Megalopolis"; // 5 000 000 - 10 000 000
    //string gigalopolis = "Gigalopolis"; // > 10 000 000

    [Range(1, 14)]
    public int citySize;

    [Header("Background Images")]
    public GameObject familyBackgroundImage;
    public GameObject hamletBackgroundImage;
    public GameObject tribeBackgroundImage;
    public GameObject villageBackgroundImage;
    public GameObject shireBackgroundImage;
    public GameObject townBackgroundImage;
    public GameObject cityBackgroundImage;
    public GameObject districtBackgroundImage;
    public GameObject countyBackgroundImage;
    public GameObject municipalityBackgroundImage;
    public GameObject globalCityBackgroundImage;
    public GameObject metropolisBackgroundImage;
    public GameObject megalopolisBackgroundImage;
    public GameObject gigalopolisBackgroundImage;

    [Header("Trade")]
    public Button buyPopsButton;
    public Button sellPopsButton;
    public Button buyFoodButton;
    public Button sellFoodButton;
    public Button buyWaterButton;
    public Button sellWaterButton;
    public Button buyWoodButton;
    public Button sellWoodButton;
    public Button buyIronButton;
    public Button sellIronButton;


    [Header("Bools")]
    bool gameOver = false;
    public bool pauser = false;
    bool magesGuild = false;
    bool heroesArrive = false;
    bool sellYourSoul = false;

    private void Awake()
    {
        ass.GetComponent<AudioSource>();
        ass.loop = false;
    }

    public void Start()
    {
        startVolumeSlider.value = 0.5f;
        ass.volume = startVolumeSlider.value;

        day = 0;
        pops = 4;
        food = 500;
        water = 1000;
        gold = 50;
        wood = 20;
        iron = 5;
        minerals = 0;

        SizeChecker();
        BackgroundChanger();

        NewValues();

        timeSlider.value = 1;

        gameOver = false;
        pauser = false;
        magesGuild = false;
        heroesArrive = false;
        sellYourSoul = false;

        heroesButton.interactable = false;
        arcaneButton.interactable = false;

        warButtonImage.enabled = true;
        demonWarButtonImage.enabled = false;
    }

    private void Update()
    {
        sliderValueText.text = "<-faster   " + timeSlider.value.ToString("F2") + "   slower->";

        if (startGamePanel.activeSelf == true)
        {
            ass.volume = startVolumeSlider.value;
        }
        else
        {
            ass.volume = volumeSlider.value;
        }

        if (!ass.isPlaying)
        {
            SetAudioClip();
            ass.Play();
        }

        ResourceClamper();
        SizeChecker();
        BackgroundChanger();
        GameOverChecker();
    }

    //random izbor muzike koja ce svirati u playlisti
    public void SetAudioClip()
    {
        ass.clip = musicClips[Random.Range(0, musicClips.Length)];
    }

    //za pauziranje igre
    public void Pauser()
    {
        if (!pauser)
        {
            Time.timeScale = 0;
            warButton.interactable = false;
            huntButton.interactable = false;
            mineButton.interactable = false;
            prayButton.interactable = false;
            arcaneButton.interactable = false;
            tradeButton.interactable = false;
            heroesButton.interactable = false;
            taxesButton.interactable = false;

            pauser = true;
        }
        else
        {
            Time.timeScale = 1;
            warButton.interactable = true;
            huntButton.interactable = true;
            mineButton.interactable = true;
            prayButton.interactable = true;
            arcaneButton.interactable = true;
            tradeButton.interactable = true;
            heroesButton.interactable = true;
            taxesButton.interactable = true;

            pauser = false;
        }

    }

    //ispis vrijednosti resursa
    public void NewValues()
    {
        woodText.text = wood + " m";
        foodText.text = food + " kg";
        goldText.text = gold + " crowns";
        popsText.text = pops.ToString();
        mineralsText.text = minerals + " g";
        waterText.text = water + " l";
        ironText.text = iron + " bars";
        BackgroundChanger();
    }

    void NewNotificationGain(int gainValue, string jedinica)
    {
        notificationText.text = day + ". New " + gainValue + " " + jedinica + "\n" + notificationText.text;
    }

    void NewNotificationLoss(int lossValue, string jedinica)
    {
        notificationText.text = day + ". New " + lossValue + " " + jedinica + "\n" + notificationText.text;
    }

    //za mijenjanje slike pozadine
    //MIJENJANJE BOJA OVISNO O PLATFORMI JE OBAVEZNO RUCNO / TRENUTNO POSTAVLJENO ZA MOBILE VERZIJU
    public void BackgroundChanger()
    {
        if (citySize == 1)
        {
            familyBackgroundImage.SetActive(true);
            dayText.color = Color.black;
            popsText.color = Color.black;
            foodText.color = Color.white;
            waterText.color = Color.white;
            goldText.color = Color.white;
            woodText.color = Color.black;
            ironText.color = Color.white;
            mineralsText.color = Color.white;
            hamletBackgroundImage.SetActive(false);
        }
        else if (citySize == 2)
        {
            familyBackgroundImage.SetActive(false);
            hamletBackgroundImage.SetActive(true);
            dayText.color = Color.white;
            popsText.color = Color.white;
            foodText.color = Color.white;
            waterText.color = Color.white;
            goldText.color = Color.white;
            woodText.color = Color.white;
            ironText.color = Color.white;
            mineralsText.color = Color.white;
            tribeBackgroundImage.SetActive(false);
        }
        else if (citySize == 3)
        {
            hamletBackgroundImage.SetActive(false);
            tribeBackgroundImage.SetActive(true);
            dayText.color = Color.white;
            popsText.color = Color.white;
            foodText.color = Color.white;
            waterText.color = Color.white;
            goldText.color = Color.white;
            woodText.color = Color.white;
            ironText.color = Color.white;
            mineralsText.color = Color.white;
            villageBackgroundImage.SetActive(false);
        }
        else if (citySize == 4)
        {
            tribeBackgroundImage.SetActive(false);
            villageBackgroundImage.SetActive(true);
            dayText.color = Color.black;
            popsText.color = Color.black;
            foodText.color = Color.black;
            waterText.color = Color.white;
            goldText.color = Color.white;
            woodText.color = Color.white;
            ironText.color = Color.white;
            mineralsText.color = Color.white;
            shireBackgroundImage.SetActive(false);
        }
        else if (citySize == 5)
        {
            villageBackgroundImage.SetActive(false);
            shireBackgroundImage.SetActive(true);
            dayText.color = Color.black;
            popsText.color = Color.white;
            foodText.color = Color.white;
            waterText.color = Color.black;
            goldText.color = Color.black;
            woodText.color = Color.black;
            ironText.color = Color.white;
            mineralsText.color = Color.white;
            townBackgroundImage.SetActive(false);
        }
        else if (citySize == 6)
        {
            shireBackgroundImage.SetActive(false);
            townBackgroundImage.SetActive(true);
            dayText.color = Color.white;
            popsText.color = Color.white;
            foodText.color = Color.white;
            waterText.color = Color.black;
            goldText.color = Color.black;
            woodText.color = Color.black;
            ironText.color = Color.white;
            mineralsText.color = Color.white;
            cityBackgroundImage.SetActive(false);
        }
        else if (citySize == 7)
        {
            townBackgroundImage.SetActive(false);
            cityBackgroundImage.SetActive(true);
            dayText.color = Color.white;
            popsText.color = Color.white;
            foodText.color = Color.white;
            waterText.color = Color.white;
            goldText.color = Color.black;
            woodText.color = Color.black;
            ironText.color = Color.white;
            mineralsText.color = Color.black;
            districtBackgroundImage.SetActive(false);
        }
        else if (citySize == 8)
        {
            cityBackgroundImage.SetActive(false);
            districtBackgroundImage.SetActive(true);
            dayText.color = Color.white;
            popsText.color = Color.black;
            foodText.color = Color.white;
            waterText.color = Color.white;
            goldText.color = Color.white;
            woodText.color = Color.black;
            ironText.color = Color.black;
            mineralsText.color = Color.black;
            countyBackgroundImage.SetActive(false);
        }
        else if (citySize == 9)
        {
            districtBackgroundImage.SetActive(false);
            countyBackgroundImage.SetActive(true);
            dayText.color = Color.white;
            popsText.color = Color.white;
            foodText.color = Color.white;
            waterText.color = Color.black;
            goldText.color = Color.white;
            woodText.color = Color.black;
            ironText.color = Color.black;
            mineralsText.color = Color.black;
            municipalityBackgroundImage.SetActive(false);
        }
        else if (citySize == 10)
        {
            countyBackgroundImage.SetActive(false);
            municipalityBackgroundImage.SetActive(true);
            dayText.color = Color.white;
            popsText.color = Color.white;
            foodText.color = Color.white;
            waterText.color = Color.white;
            goldText.color = Color.white;
            woodText.color = Color.white;
            ironText.color = Color.white;
            mineralsText.color = Color.white;
            globalCityBackgroundImage.SetActive(false);
        }
        else if (citySize == 11)
        {
            municipalityBackgroundImage.SetActive(false);
            globalCityBackgroundImage.SetActive(true);
            dayText.color = Color.white;
            popsText.color = Color.white;
            foodText.color = Color.white;
            waterText.color = Color.white;
            goldText.color = Color.white;
            woodText.color = Color.white;
            ironText.color = Color.white;
            mineralsText.color = Color.white;
            metropolisBackgroundImage.SetActive(false);
        }
        else if (citySize == 12)
        {
            globalCityBackgroundImage.SetActive(false);
            metropolisBackgroundImage.SetActive(true);
            dayText.color = Color.black;
            popsText.color = Color.black;
            foodText.color = Color.black;
            waterText.color = Color.white;
            goldText.color = Color.white;
            woodText.color = Color.white;
            ironText.color = Color.white;
            mineralsText.color = Color.white;
            megalopolisBackgroundImage.SetActive(false);
        }
        else if (citySize == 13)
        {
            metropolisBackgroundImage.SetActive(false);
            megalopolisBackgroundImage.SetActive(true);
            dayText.color = Color.white;
            popsText.color = Color.white;
            foodText.color = Color.white;
            waterText.color = Color.white;
            goldText.color = Color.white;
            woodText.color = Color.white;
            ironText.color = Color.white;
            mineralsText.color = Color.white;
            gigalopolisBackgroundImage.SetActive(false);
        }
        else if (citySize == 14)
        {
            megalopolisBackgroundImage.SetActive(false);
            gigalopolisBackgroundImage.SetActive(true);
            dayText.color = Color.white;
            popsText.color = Color.white;
            foodText.color = Color.white;
            waterText.color = Color.white;
            goldText.color = Color.white;
            woodText.color = Color.white;
            ironText.color = Color.white;
            mineralsText.color = Color.white;
        }

    }

    //za mijenjanje kategorije velicine naselja
    public void SizeChecker()
    {
        if (pops > 0 && pops <= 20)
        {
            citySize = 1;
        }
        else if (pops > 20 && pops <= 50)
        {
            citySize = 2;
        }
        else if (pops > 50 && pops <= 100)
        {
            citySize = 3;
        }
        else if (pops > 100 && pops <= 1000)
        {
            citySize = 4;
        }
        else if (pops > 1000 && pops <= 5000)
        {
            citySize = 5;
        }
        else if (pops > 5000 && pops <= 10000)
        {
            citySize = 6;
        }
        else if (pops > 10000 && pops <= 50000)
        {
            citySize = 7;
        }
        else if (pops > 50000 && pops <= 100000)
        {
            citySize = 8;
        }
        else if (pops > 100000 && pops <= 500000)
        {
            citySize = 9;
        }
        else if (pops > 500000 && pops <= 1000000)
        {
            citySize = 10;
        }
        else if (pops > 1000000 && pops <= 2500000)
        {
            citySize = 11;
        }
        else if (pops > 2500000 && pops <= 5000000)
        {
            citySize = 12;
        }
        else if (pops > 5000000 && pops <= 10000000)
        {
            citySize = 13;
        }
        else if (pops > 10000000)
        {
            citySize = 14;
        }
    }

    //provjera za game over
    public void GameOverChecker()
    {
        if (pops <= 0)
        {
            gameOver = true;
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }

    //provjera da sipisane vrijednosti resursa ne padnu ispod 0
    public void ResourceClamper()
    {
        if (food <= 0)
        {
            food = 0;
        }
        if (water <= 0)
        {
            water = 0;
        }
        if (gold <= 0)
        {
            gold = 0;
        }
        if (wood <= 0)
        {
            wood = 0;
        }
        if (iron <= 0)
        {
            iron = 0;
        }
        if (minerals <= 0)
        {
            minerals = 0;
        }
    }

    IEnumerator SliderText()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.03f);
            sliderValueText.text = "<-faster   " + timeSlider.value.ToString("F2") + "   slower->";
        }
    }

    //standardno svaki dan
    IEnumerator DayIncrease()
    {
        while (gameOver == false)
        {
            yield return new WaitForSeconds(timeSlider.value);
            day++;
            dayText.text = day + ". day";
        }
    }

    //gubimo hranu na dnevnoj bazi
    IEnumerator FoodLose()
    {
        while (gameOver != true)
        {
            yield return new WaitForSeconds(timeSlider.value);
            food -= (int)Random.Range(pops * 0.15f, pops * 0.5f);
            foodText.text = food + " kg";
        }
    }

    //dobijamo hranu svakih 6 do 18 dana
    IEnumerator FoodGain()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(Random.Range(timeSlider.value + (6 * timeSlider.value), timeSlider.value + (18 * timeSlider.value)));
            int gainFood = (int)Random.Range(pops * 2.7f, pops * 5.5f);
            food += gainFood;
            foodText.text = food + " kg";
            NewNotificationGain(gainFood, "kg");
        }
    }

    //gubimo vodu na dnevnoj bazi
    IEnumerator WaterLose()
    {
        while (gameOver != true)
        {
            yield return new WaitForSeconds(timeSlider.value);
            water -= (int)Random.Range(pops * 0.2f, pops * 0.5f);
            waterText.text = water + " l";
        }
    }

    //dobijamo vodu svakih 7 dana
    IEnumerator WaterGain()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(timeSlider.value * 7);
            int gainWater = (int)Random.Range(pops * 3.5f, pops * 4.5f);
            water += gainWater;
            waterText.text = water + " l";
            //NewNotificationGain(gainWater, "l");
        }
    }

    //dobijamo populaciju
    IEnumerator PopulationGain()
    {
        while (!gameOver)
        {
            if (food <= 0 || water <= 0)
            {
                yield return new WaitForSeconds(Random.Range(timeSlider.value + (15 * timeSlider.value), timeSlider.value + (45 * timeSlider.value)));
                if (pops >= 2 && pops <= 100)
                {
                    int popBoost = (int)Random.Range(1f, 4f);
                    pops += popBoost;
                    NewNotificationGain(popBoost, "people");
                }
                else if (pops > 100)
                {
                    int popBoost = (int)Random.Range(pops * 0.02f, pops * 0.05f);
                    pops += popBoost;
                    NewNotificationGain(popBoost, "people");
                }
            }
            else if (food > 0 || water > 0)
            {
                yield return new WaitForSeconds(Random.Range(timeSlider.value + (15 * timeSlider.value), timeSlider.value + (60 * timeSlider.value)));
                if (pops >= 2 && pops <= 100)
                {
                    int popBoost = Random.Range(1, 7);
                    pops += popBoost;
                    NewNotificationGain(popBoost, "people");
                }
                else if (pops > 100)
                {
                    int popBoost = (int)Random.Range(pops * 0.02f, pops * 0.1f);
                    pops += popBoost;
                    NewNotificationGain(popBoost, "people");
                }
            }
            
            popsText.text = pops.ToString();
        }
    }

    //gubimo populaciju
    IEnumerator PopulationLose()
    {
        while (!gameOver)
        {
            if (food > 0 || water > 0)
            {
                yield return new WaitForSeconds(Random.Range(timeSlider.value + (10 * timeSlider.value), timeSlider.value + (40 * timeSlider.value)));
                int populationDecrese = (int)Random.Range(pops * 0.01f, pops * 0.03f);
                pops -= populationDecrese;
                popsText.text = pops.ToString();
                NewNotificationLoss(populationDecrese, "people");
            }
            else if (food <= 0 || water <= 0)
            {
                yield return new WaitForSeconds(Random.Range(timeSlider.value + (10 * timeSlider.value), timeSlider.value + (20 * timeSlider.value)));
                int populationDecrese = (int)Random.Range(pops * 0.05f, pops * 0.1f);
                pops -= populationDecrese;
                popsText.text = pops.ToString();
                NewNotificationLoss(populationDecrese, "people");
            }
        }
    }

    //random eventi
    IEnumerator ExoticTrader()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(Random.Range(timeSlider.value + (150 * timeSlider.value), timeSlider.value + (350 * timeSlider.value)));
            int chance = Random.Range(0, 101);
            if (gold > 0 && chance > 80)
            {
                int goldspent = (int)(gold * 0.05f);
                gold -= goldspent;
                minerals += goldspent;

                notificationText.text = "On the " + day + ". day an exotic trader arrived and we traded: " +
                "\n" + "Gold: " + goldspent + " crowns " +
                "\n" + "for minerals: " + goldspent + " g." +
                "\n" + notificationText.text;
                NewValues();
            }
        }
    }
    IEnumerator Catastrophe()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(Random.Range(timeSlider.value + (50 * timeSlider.value), timeSlider.value + (100 * timeSlider.value)));
            int chanceToOccur = Random.Range(0, 101);
            int chanceForType = Random.Range(1, 4);
            if (chanceToOccur >= 50)
            {
                if (chanceForType == 1) //poplava
                {
                    int popsChange = (int)Random.Range(pops * 0.01f, pops * 0.25f);
                    pops -= popsChange;
                    int foodChange = (int)Random.Range(food * 0.05f, food * 0.25f);
                    food -= foodChange;
                    int woodChange = (int)Random.Range(wood * 0.01f, wood * 0.33f);
                    wood -= woodChange;

                    notificationText.text = "On the " + day + ". day we had a terrible flood and lost: " +
                "\n" + "Population: " + popsChange + " people." +
                "\n" + "Food: " + foodChange + " kg." +
                "\n" + "Wood: " + woodChange + " m." +
                "\n" + notificationText.text;
                    NewValues();
                }
                else if (chanceForType == 2) //pozar
                {
                    int popsChange = (int)Random.Range(pops * 0.05f, pops * 0.25f);
                    pops -= popsChange;
                    int foodChange = (int)Random.Range(food * 0.1f, food * 0.25f);
                    food -= foodChange;
                    int woodChange = (int)Random.Range(wood * 0.25f, wood * 0.5f);
                    wood -= woodChange;

                    notificationText.text = "On the " + day + ". day we had a monstrous fire and lost: " +
                "\n" + "Population: " + popsChange + " people." +
                "\n" + "Food: " + foodChange + " kg." +
                "\n" + "Wood: " + woodChange + " m." +
                "\n" + notificationText.text;
                    NewValues();
                }
                else if (chanceForType == 3) //potres
                {
                    int popsChange = (int)Random.Range(pops * 0.1f, pops * 0.25f);
                    pops -= popsChange;
                    int foodChange = (int)Random.Range(food * 0.15f, food * 0.33f);
                    food -= foodChange;
                    int woodChange = (int)Random.Range(wood * 0.2f, wood * 0.33f);
                    wood -= woodChange;

                    notificationText.text = "On the " + day + ". day we had a horrific earthquake and lost: " +
                "\n" + "Population: " + popsChange + " people." +
                "\n" + "Food: " + foodChange + " kg." +
                "\n" + "Wood: " + woodChange + " m." +
                "\n" + notificationText.text;
                    NewValues();
                }
            }
        }
    }
    IEnumerator BountifulHarvest()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(Random.Range(timeSlider.value + (122 * timeSlider.value), timeSlider.value + (244 * timeSlider.value)));
            int foodChange = (int)Random.Range(pops * 1.5f, pops * 2.5f);
            food += foodChange;

            notificationText.text = "On the " + day + ". day we had a bountiful harvest and gained: " +
                "\n" + "Food: " + foodChange + " kg." +
                "\n" + notificationText.text;
            NewValues();
        }
    }
    IEnumerator BabyBoom()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(Random.Range(timeSlider.value + (150 * timeSlider.value), timeSlider.value + (300 * timeSlider.value)));
            int chanceToHappen = Random.Range(1, 101);
            if (chanceToHappen >= 66)
            {
                int popsChange = (int)Random.Range(pops * 0.25f, pops * 0.75f);
                pops += popsChange;

                notificationText.text = "On the " + day + ". day we had a baby boom and gained: " +
                "\n" + "Population: " + popsChange + " people." +
                "\n" + notificationText.text;
                NewValues();
            }
        }
    }
    IEnumerator RetiredAdventurer()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(Random.Range(timeSlider.value + (50 * timeSlider.value), timeSlider.value + (100 * timeSlider.value)));
            int chanceToHappen = Random.Range(1, 101);
            if (chanceToHappen >= 66)
            {
                if (!heroesArrive)
                {
                    heroesArrive = true;
                    heroesButton.interactable = true;
                    int popsChange = 1;
                    pops += popsChange;
                    int goldChange = (int)Random.Range(gold * 0.2f, gold * 0.33f);
                    gold += goldChange;
                    int ironChange = (int)Random.Range(iron * 0.2f, iron * 0.33f);
                    iron += ironChange;
                    int mineralsChange = (int)Random.Range(minerals * 0.2f, minerals * 0.33f);
                    minerals += mineralsChange;

                    notificationText.text = "On the " + day + ". day a famous adventurer retired to our town and brought: " + 
                    "\n" + "Population: " + popsChange + " people." +
                    "\n" + "Gold: " + goldChange + " crowns." +
                    "\n" + "Iron: " + ironChange + " bars." +
                    "\n" + "Minerals: " + mineralsChange + " g." +
                    "\n" + notificationText.text;
                    NewValues();
                }
                else
                {
                    int popsChange = 1;
                    pops += popsChange;
                    int goldChange = (int)Random.Range(gold * 0.2f, gold * 0.33f);
                    gold += goldChange;
                    int ironChange = (int)Random.Range(iron * 0.2f, iron * 0.33f);
                    iron += ironChange;
                    int mineralsChange = (int)Random.Range(minerals * 0.2f, minerals * 0.33f);
                    minerals += mineralsChange;

                    notificationText.text = "On the " + day + ". day a famous adventurer retired to our town and brought: " +
                    "\n" + "Population: " + popsChange + " people." +
                    "\n" + "Gold: " + goldChange + " crowns." +
                    "\n" + "Iron: " + ironChange + " bars." +
                    "\n" + "Minerals: " + mineralsChange + " g." +
                    "\n" + notificationText.text;
                    NewValues();
                }
            }
        }

    }
    IEnumerator UnderAttack()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(Random.Range(timeSlider.value + (100 * timeSlider.value), timeSlider.value + (200 * timeSlider.value)));
            int chanceOfType = Random.Range(1, 101);
            if (chanceOfType > 25 && chanceOfType <= 75) //bandits
            {
                int popsChange = (int)Random.Range(pops * 0.02f, pops * 0.15f);
                pops -= popsChange;

                int foodChange = (int)Random.Range(food * 0.15f, food * 0.25f);
                food -= foodChange;

                int goldChange = (int)Random.Range(gold * 0.05f, food * 0.25f);
                gold -= goldChange;

                int ironChange = (int)Random.Range(iron * 0.1f, iron * 0.2f);
                iron -= ironChange;

                notificationText.text = "On the " + day + ". day we were attacked by bandits and lost: " +
                "\n" + "Population: " + popsChange + " people." +
                "\n" + "Food: " + foodChange + " kg." +
                "\n" + "Gold: " + goldChange + " crowns." +
                "\n" + "Iron: " + ironChange + " bars." +
                "\n" + notificationText.text;
                NewValues();
            }
            if (chanceOfType > 75 && chanceOfType <= 95) //goblins
            {
                int popsChange = (int)Random.Range(pops * 0.1f, pops * 0.25f);
                pops -= popsChange;

                int foodChange = (int)Random.Range(food * 0.1f, food * 0.2f);
                food -= foodChange;

                int goldChange = (int)Random.Range(gold * 0.1f, food * 0.2f);
                gold -= goldChange;

                int ironChange = (int)Random.Range(iron * 0.1f, iron * 0.15f);
                iron -= ironChange;

                int mineralsChange = (int)Random.Range(minerals * 0.01f, minerals * 0.1f);
                minerals -= mineralsChange;

                notificationText.text = "On the " + day + ". day we were attacked by goblins and lost: " +
                "\n" + "Population: " + popsChange + " people." +
                "\n" + "Food: " + foodChange + " kg." +
                "\n" + "Gold: " + goldChange + " crowns." +
                "\n" + "Iron: " + ironChange + " bars." +
                "\n" + "Minerals: " + mineralsChange + " g." +
                "\n" + notificationText.text;
                NewValues();
            }
            if (chanceOfType > 95) //undead
            {
                int popsChange = (int)Random.Range(pops * 0.1f, pops * 0.33f);
                pops -= popsChange;

                int goldChange = (int)Random.Range(gold * 0.05f, food * 0.15f);
                gold -= goldChange;

                int ironChange = (int)Random.Range(iron * 0.05f, iron * 0.15f);
                iron -= ironChange;

                int mineralsChange = (int)Random.Range(minerals * 0.1f, minerals * 0.33f);
                minerals -= mineralsChange;

                notificationText.text = "On the " + day + ". day we were attacked by an undead horde and lost: " +
                "\n" + "Population: " + popsChange + " people." +
                "\n" + "Gold: " + goldChange + " crowns." +
                "\n" + "Iron: " + ironChange + " bars." +
                "\n" + "Minerals: " + mineralsChange + " g." +
                "\n" + notificationText.text;
                NewValues();
            }
        }
    }
    IEnumerator MagesGuild()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(Random.Range(timeSlider.value + (200 * timeSlider.value), timeSlider.value + (350 * timeSlider.value)));
            if (!magesGuild)
            {
                magesGuild = true;
                arcaneButton.interactable = true;
                int mineralsChange = 5;
                minerals += mineralsChange;

                notificationText.text = "On the " + day + ". day the Mages Guild decided to open a branch in our town and gave us: " +
                "\n" + mineralsChange + " g of minerals." +
                "\n" + notificationText.text;
                NewValues();
            }
            else
            {
                int mineralsChange = (int)(minerals * 0.5f);
                minerals += mineralsChange;
                int goldChange = (int)(gold * 0.15f);
                gold += goldChange;

                notificationText.text = "On the " + day + ". day the Mages Guild held a conference in our town and gave us: " +
                "\n" + "Minerals " + mineralsChange + " g." +
                "\n" + "Gold " + goldChange + " crowns." +
                "\n" + notificationText.text;
                NewValues();
            }
        }
    }
    IEnumerator Tourney()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(Random.Range(timeSlider.value + (183 * timeSlider.value), timeSlider.value + (365 * timeSlider.value)));
            int goldChange = (int)(Random.Range(pops * 0.25f, pops * 0.4f));
            gold += goldChange;
            int foodChange = (int)(Random.Range(gold * 2f, gold * 5f));
            food += foodChange;

            notificationText.text = "On the " + day + ". day we held a grand tourney and earned: " +
                "\n" + "Food " + foodChange + " kg and " +
                "\n" + "Gold " + goldChange + " crowns." +
                "\n" + notificationText.text;
            NewValues();
        }
    }

    //gumbi
    //dobije se ili se izgubi kolicina resursa
    public void GoToWar()
    {
        warButton.interactable = false;
        Invoke("DeactivationTimerWar", timeSlider.value * 20);
        if (sellYourSoul)
        {
            warButtonImage.enabled = true;
            demonWarButtonImage.enabled = false;

            int popsChange = (int)Random.Range(pops * 0.1f, pops * 0.25f);
            pops += popsChange;

            int foodChange = (int)Random.Range(pops * 0.1f, pops * 0.25f);
            food += foodChange;

            int waterChange = (int)Random.Range(pops * 0.1f, pops * 0.25f);
            water += waterChange;

            int goldChange = (int)Random.Range(pops * 0.1f, pops * 0.25f);
            gold += goldChange;

            int woodChange = (int)Random.Range(pops * 0.1f, pops * 0.25f);
            wood += woodChange;

            int ironChange = (int)Random.Range(pops * 0.1f, pops * 0.25f);
            iron += ironChange;

            notificationText.text = "On the " + day + ". day we went to war and our armies fought like demons. We lost nothing and gained: " +
                "\n" + "Population: " + popsChange + " people " +
                "\n" + "Food: " + foodChange + " kg " +
                "\n" + "Water: " + waterChange + " l " +
                "\n" + "Gold: " + goldChange + " crowns " +
                "\n" + "Wood: " + woodChange + " m " +
                "\n" + "Iron: " + ironChange + " bars " +
                "\n" + notificationText.text;
            NewValues();
        }
        else
        {
            if(pops < 100 && gold >= 30 && food > pops * 1.2f && iron >= 50)
            {
                gold -= 30;
                food -= (int)(pops * 1.2f);
                iron -= 50;

                int popsChange = (int)Random.Range(pops * -0.1f, pops * 0.15f);
                pops += popsChange;

                int foodChange = (int)Random.Range(food * -0.05f, food * 0.2f);
                food += foodChange;

                int waterChange = (int)Random.Range(water * -0.1f, water * 0.33f);
                water += waterChange;

                int goldChange = (int)Random.Range(gold * 0f, gold * 0.3f);
                gold += goldChange;

                int woodChange = (int)Random.Range(wood * -0.1f, wood * 0.25f);
                wood += woodChange;

                int ironChange = (int)Random.Range(iron * -0.05f, iron * 0.25f);
                iron += ironChange;

                int mineralsChange = (int)Random.Range(minerals * 0f, minerals * 0.01f);
                minerals += mineralsChange;

                notificationText.text = "On the " + day + ". day we went to war and the result is: " +
                    "\n" + "Population: " + popsChange + " people " +
                    "\n" + "Food: " + foodChange + " kg " +
                    "\n" + "Water: " + waterChange + " l " +
                    "\n" + "Gold: " + goldChange + " crowns " +
                    "\n" + "Wood: " + woodChange + " m " +
                    "\n" + "Iron: " + ironChange + " bars " +
                    "\n" + "Minerals: " + mineralsChange + " g " +
                    "\n" + notificationText.text;
                NewValues();
            }
            else if (pops >= 100 && gold >= 30 && food > pops * 1.2f && iron >= 50)
            {
                gold -= 30;
                food -= (int)(pops * 1.2f);
                iron -= 50;

                int popsChange = (int)Random.Range(pops * -0.1f, pops * 0.15f);
                pops += popsChange;

                int foodChange = (int)Random.Range(food * -0.05f, food * 0.15f);
                food += foodChange;

                int waterChange = (int)Random.Range(water * -0.1f, water * 0.33f);
                water += waterChange;

                int goldChange = (int)Random.Range(gold * 0f, gold * 0.3f);
                gold += goldChange;

                int woodChange = (int)Random.Range(wood * -0.1f, wood * 0.25f);
                wood += woodChange;

                int ironChange = (int)Random.Range(iron * -0.15f, iron * 0.25f);
                iron += ironChange;

                int mineralsChange = (int)Random.Range(minerals * 0f, minerals * 0.01f);
                minerals += mineralsChange;

                notificationText.text = "On the " + day + ". day we went to war and the result is: " +
                    "\n" + "Population: " + popsChange + " people " +
                    "\n" + "Food: " + foodChange + " kg " +
                    "\n" + "Water: " + waterChange + " l " +
                    "\n" + "Gold: " + goldChange + " crowns " +
                    "\n" + "Wood: " + woodChange + " m " +
                    "\n" + "Iron: " + ironChange + " bars " +
                    "\n" + "Minerals: " + mineralsChange + " g " +
                    "\n" + notificationText.text;
                NewValues();
            }
            else if (pops >= 500000 && gold >= 5000 && food > pops * 1.5f && iron >= 5000)
            {
                gold -= 5000;
                food -= (int)(pops * 1.5f);
                iron -= 5000;

                int popsChange = (int)Random.Range(pops * -0.13f, pops * 0.2f);
                pops += popsChange;

                int foodChange = (int)Random.Range(food * -0.1f, food * 0.2f);
                food += foodChange;

                int waterChange = (int)Random.Range(water * -0.1f, water * 0.33f);
                water += waterChange;

                int goldChange = (int)Random.Range(gold * -0.05f, gold * 0.33f);
                gold += goldChange;

                int woodChange = (int)Random.Range(wood * -0.15f, wood * 0.33f);
                wood += woodChange;

                int ironChange = (int)Random.Range(iron * -0.15f, iron * 0.33f);
                iron += ironChange;

                int mineralsChange = (int)Random.Range(minerals * -0.01f, minerals * 0.02f);
                minerals += mineralsChange;

                notificationText.text = "On the " + day + ". day we went to war and the result is: " +
                    "\n" + "Population: " + popsChange + " people " +
                    "\n" + "Food: " + foodChange + " kg " +
                    "\n" + "Water: " + waterChange + " l " +
                    "\n" + "Gold: " + goldChange + " crowns " +
                    "\n" + "Wood: " + woodChange + " m " +
                    "\n" + "Iron: " + ironChange + " bars " +
                    "\n" + "Minerals: " + mineralsChange + " g " +
                    "\n" + notificationText.text;
                NewValues();
            }
            else if (pops >= 1000000 && gold >= 10000 && food > pops * 1.5f && iron >= 15000)
            {
                gold -= 10000;
                food -= (int)(pops * 1.5f);
                iron -= 15000;

                int popsChange = (int)Random.Range(pops * -0.15f, pops * 0.3f);
                pops += popsChange;

                int foodChange = (int)Random.Range(food * -0.15f, food * 0.25f);
                food += foodChange;

                int waterChange = (int)Random.Range(water * -0.1f, water * 0.33f);
                water += waterChange;

                int goldChange = (int)Random.Range(gold * -0.1f, gold * 0.35f);
                gold += goldChange;

                int woodChange = (int)Random.Range(wood * -0.15f, wood * 0.33f);
                wood += woodChange;

                int ironChange = (int)Random.Range(iron * -0.15f, iron * 0.35f);
                iron += ironChange;

                int mineralsChange = (int)Random.Range(minerals * -0.02f, minerals * 0.05f);
                minerals += mineralsChange;

                notificationText.text = "On the " + day + ". day we went to war and the result is: " +
                    "\n" + "Population: " + popsChange + " people " +
                    "\n" + "Food: " + foodChange + " kg " +
                    "\n" + "Water: " + waterChange + " l " +
                    "\n" + "Gold: " + goldChange + " crowns " +
                    "\n" + "Wood: " + woodChange + " m " +
                    "\n" + "Iron: " + ironChange + " bars " +
                    "\n" + "Minerals: " + mineralsChange + " g " +
                    "\n" + notificationText.text;
                NewValues();
            }
            else if (pops >= 5000000 && gold >= 50000 && food > pops * 1.5f && iron >= 50000)
            {
                gold -= 50000;
                food -= (int)(pops * 1.5f);
                iron -= 50000;

                int popsChange = (int)Random.Range(pops * -0.2f, pops * 0.33f);
                pops += popsChange;

                int foodChange = (int)Random.Range(food * -0.15f, food * 0.33f);
                food += foodChange;

                int waterChange = (int)Random.Range(water * 0.15f, water * 0.33f);
                water += waterChange;

                int goldChange = (int)Random.Range(gold * -0.15f, gold * 0.4f);
                gold += goldChange;

                int woodChange = (int)Random.Range(wood * -0.15f, wood * 0.35f);
                wood += woodChange;

                int ironChange = (int)Random.Range(iron * -0.15f, iron * 0.4f);
                iron += ironChange;

                int mineralsChange = (int)Random.Range(minerals * -0.02f, minerals * 0.1f);
                minerals += mineralsChange;

                notificationText.text = "On the " + day + ". day we went to war and the result is: " +
                    "\n" + "Population: " + popsChange + " people " +
                    "\n" + "Food: " + foodChange + " kg " +
                    "\n" + "Water: " + waterChange + " l " +
                    "\n" + "Gold: " + goldChange + " crowns " +
                    "\n" + "Wood: " + woodChange + " m " +
                    "\n" + "Iron: " + ironChange + " bars " +
                    "\n" + "Minerals: " + mineralsChange + " g " +
                    "\n" + notificationText.text;
                NewValues();
            }
            else if (gold < 30 || food <= pops * 1.2f || iron < 50)
            {
                notificationText.text = "You cannot go to war. You lack resources." +
                    "\n" + "You need at least 30 crowns worth of gold, 50 bars of iron and " + (int)(pops * 1.2f) + " kg of food." +
                    "\n" + notificationText.text;
            }
        }
        
    }

    //dobije se hrana, voda i drvo, i potencijalno se izgubi populacije
    public void HuntAndGather()
    {
        huntButton.interactable = false;
        Invoke("DeactivationTimerHunt", timeSlider.value * 10);
        if (citySize < 3) //family and hamlet
        {
            if (food <= pops)
            {
                int foodChange = pops * 2;
                food += foodChange;
                int waterChange = (int)(foodChange * 1.5f);
                water += waterChange;
                int woodChange = (int)(pops * 0.5f);
                wood += woodChange;

                notificationText.text = "On the " + day + ". day we went hunting and brought back: " +
                "\n" + "Food: " + foodChange + " kg" +
                "\n" + "Water: " + waterChange + " l" +
                "\n" + "Wood: " + woodChange + " m" +
                "\n" + notificationText.text;
                NewValues();
            }
            else
            {
                int foodChange = (int)Random.Range(pops * 0.75f, pops * 1.5f);
                food += foodChange;
                int waterChange = (int)(foodChange * 1.5f);
                water += waterChange;
                int woodChange = (int)(pops * 0.5f);
                wood += woodChange;

                notificationText.text = "On the " + day + ". day we went hunting and brought back: " +
                "\n" + "Food: " + foodChange + " kg" +
                "\n" + "Water: " + waterChange + " l" +
                "\n" + "Wood: " + woodChange + " m" +
                "\n" + notificationText.text;
                NewValues();
            }
        }
        else if (citySize == 3) //tribe
        {
            int foodChange = (int)Random.Range(pops * 0.5f, pops * 1.25f);
            food += foodChange;
            int popsChange = (int)Random.Range(pops * 0f, pops * 0.05f);
            pops -= popsChange;
            int waterChange = (int)(foodChange * 1.5f);
            water += waterChange;
            int woodChange = (int)Random.Range(pops * 0.05f, pops * 0.4f);
            wood += woodChange;

            notificationText.text = "On the " + day + ". day we went hunting and brought back: " +
                "\n" + "Food: " + foodChange + " kg" +
                "\n" + "Water: " + waterChange + " l" +
                "\n" + "Wood: " + woodChange + " m" +
                "\n" + "And we lost " + popsChange + " people" +
                "\n" + notificationText.text;
            NewValues();
        }
        else if (citySize > 3 && citySize < 6) //village and shire
        {
            int foodChange = (int)Random.Range(pops * 0.33f, pops * 1.25f);
            food += foodChange;
            int popsChange = (int)Random.Range(pops * 0f, pops * 0.05f);
            pops -= popsChange;
            int waterChange = (int)(foodChange * 1.5f);
            water += waterChange;
            int woodChange = (int)Random.Range(pops * 0.02f, pops * 0.4f);
            wood += woodChange;

            notificationText.text = "On the " + day + ". day we went hunting and brought back: " +
                "\n" + "Food: " + foodChange + " kg" +
                "\n" + "Water: " + waterChange + " l" +
                "\n" + "Wood: " + woodChange + " m" +
                "\n" + "And we lost " + popsChange + " people" +
                "\n" + notificationText.text;
            NewValues();
        }
        else if (citySize == 6) //town
        {
            int foodChange = (int)Random.Range(pops * 0.33f, pops * 1.15f);
            food += foodChange;
            int popsChange = (int)Random.Range(pops * 0f, pops * 0.1f);
            pops -= popsChange;
            int waterChange = (int)(foodChange * 1.5f);
            water += waterChange;
            int woodChange = (int)Random.Range(pops * 0.02f, pops * 0.4f);
            wood += woodChange;

            notificationText.text = "On the " + day + ". day we went hunting and brought back: " +
                "\n" + "Food: " + foodChange + " kg" +
                "\n" + "Water: " + waterChange + " l" +
                "\n" + "Wood: " + woodChange + " m" +
                "\n" + "And we lost " + popsChange + " people" +
                "\n" + notificationText.text;
            NewValues();
        }
        else if (citySize == 7) //city
        {
            int foodChange = (int)Random.Range(pops * 0.25f, pops);
            food += foodChange;
            int popsChange = (int)Random.Range(pops * 0.01f, pops * 0.15f);
            pops -= popsChange;
            int waterChange = (int)(foodChange * 1.5f);
            water += waterChange;
            int woodChange = (int)Random.Range(pops * 0.01f, pops * 0.33f);
            wood += woodChange;

            notificationText.text = "On the " + day + ". day we went hunting and brought back: " +
                "\n" + "Food: " + foodChange + " kg" +
                "\n" + "Water: " + waterChange + " l" +
                "\n" + "Wood: " + woodChange + " m" +
                "\n" + "And we lost " + popsChange + " people" +
                "\n" + notificationText.text;
            NewValues();
        }
        else if (citySize > 7 && citySize < 10) //district and county
        {
            int foodChange = (int)Random.Range(pops * 0.15f, pops * 0.75f);
            food += foodChange;
            int popsChange = (int)Random.Range(pops * 0.01f, pops * 0.15f);
            pops -= popsChange;
            int waterChange = (int)(foodChange * 1.5f);
            water += waterChange;
            int woodChange = (int)Random.Range(pops * 0.01f, pops * 0.33f);
            wood += woodChange;

            notificationText.text = "On the " + day + ". day we went hunting and brought back: " +
                "\n" + "Food: " + foodChange + " kg" +
                "\n" + "Water: " + waterChange + " l" +
                "\n" + "Wood: " + woodChange + " m" +
                "\n" + "And we lost " + popsChange + " people" +
                "\n" + notificationText.text;
            NewValues();
        }
        else if (citySize == 10) //municipality
        {
            int foodChange = (int)Random.Range(pops * 0.1f, pops * 0.66f);
            food += foodChange;
            int popsChange = (int)Random.Range(pops * 0.01f, pops * 0.15f);
            pops -= popsChange;
            int waterChange = (int)(foodChange * 1.5f);
            water += waterChange;
            int woodChange = (int)Random.Range(pops * 0.005f, pops * 0.15f);
            wood += woodChange;

            notificationText.text = "On the " + day + ". day we went hunting and brought back: " +
                "\n" + "Food: " + foodChange + " kg" +
                "\n" + "Water: " + waterChange + " l" +
                "\n" + "Wood: " + woodChange + " m" +
                "\n" + "And we lost " + popsChange + " people" +
                "\n" + notificationText.text;
            NewValues();
        }
        else if (citySize > 10 && citySize < 13) //global city and metropolis
        {
            int foodChange = (int)Random.Range(pops * 0.05f, pops * 0.5f);
            food += foodChange;
            int popsChange = (int)Random.Range(pops * 0.01f, pops * 0.15f);
            pops -= popsChange;
            int waterChange = (int)(foodChange * 1.5f);
            water += waterChange;
            int woodChange = (int)Random.Range(pops * 0.005f, pops * 0.1f);
            wood += woodChange;

            notificationText.text = "On the " + day + ". day we went hunting and brought back: " +
                "\n" + "Food: " + foodChange + " kg" +
                "\n" + "Water: " + waterChange + " l" +
                "\n" + "Wood: " + woodChange + " m" +
                "\n" + "And we lost " + popsChange + " people" +
                "\n" + notificationText.text;
            NewValues();
        }
        else if (citySize == 13) //megalopolis
        {
            int foodChange = (int)Random.Range(pops * 0.05f, pops * 0.33f);
            food += foodChange;
            int popsChange = (int)Random.Range(pops * 0.005f, pops * 0.05f);
            pops -= popsChange;
            int waterChange = (int)(foodChange * 1.5f);
            water += waterChange;
            int woodChange = (int)Random.Range(pops * 0.001f, pops * 0.05f);
            wood += woodChange;

            notificationText.text = "On the " + day + ". day we went hunting and brought back: " +
                "\n" + "Food: " + foodChange + " kg" +
                "\n" + "Water: " + waterChange + " l" +
                "\n" + "Wood: " + woodChange + " m" +
                "\n" + "And we lost " + popsChange + " people" +
                "\n" + notificationText.text;
            NewValues();
        }
        else if (citySize == 14) //gigalopolis
        {
            int foodChange = (int)Random.Range(pops * 0.01f, pops * 0.25f);
            food += foodChange;
            int popsChange = (int)Random.Range(pops * 0.005f, pops * 0.05f);
            pops -= popsChange;
            int waterChange = (int)(foodChange * 1.5f);
            water += waterChange;
            int woodChange = (int)Random.Range(pops * 0.001f, pops * 0.05f);
            wood += woodChange;

            notificationText.text = "On the " + day + ". day we went hunting and brought back: " +
                "\n" + "Food: " + foodChange + " kg" +
                "\n" + "Water: " + waterChange + " l" +
                "\n" + "Wood: " + woodChange + " m" +
                "\n" + "And we lost " + popsChange + " people" +
                "\n" + notificationText.text;
            NewValues();
        }
    }

    //dobije se iron i potencijalno minerals
    public void Mining()
    {
        mineButton.interactable = false;
        Invoke("DeactivationTimerMining", timeSlider.value * 25);

        if (citySize <= 2)
        {
            int ironChange = (int)Random.Range(pops * 0.1f, pops * 0.33f);
            iron += ironChange;
            int mineralsChance = Random.Range(0, 101);
            int mineralsChange = (int)Random.Range(1f, 3f);
            int popsChance = Random.Range(0, 101);
            int popsChange = (int)Random.Range(pops * 0.01f, pops * 0.05f);
            if (mineralsChance > 85)
            {
                if (popsChance > 75)
                {
                    minerals += mineralsChange;
                    pops -= popsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + mineralsChange + " g" +
                "\n" + "and we lost " + popsChange + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
                else
                {
                    minerals += mineralsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + mineralsChange + " g" +
                "\n" + "and we lost " + 0 + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
            }
            else
            {
                if (popsChance > 75)
                {
                    pops -= popsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + 0 + " g" +
                "\n" + "and we lost " + popsChange + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
                else
                {
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + 0 + " g" +
                "\n" + "and we lost " + 0 + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
            }
        }
        else if (citySize > 2 && citySize <= 4)
        {
            int ironChange = (int)Random.Range(pops * 0.01f, pops * 0.1f);
            iron += ironChange;
            int mineralsChance = Random.Range(0, 101);
            int mineralsChange = (int)Random.Range(minerals * 0.01f, minerals * 0.1f);
            int popsChance = Random.Range(0, 101);
            int popsChange = (int)Random.Range(pops * 0.01f, pops * 0.05f);
            if (mineralsChance > 85)
            {
                if (popsChance > 75)
                {
                    minerals += mineralsChange;
                    pops -= popsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + mineralsChange + " g" +
                "\n" + "and we lost " + popsChange + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
                else
                {
                    minerals += mineralsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + mineralsChange + " g" +
                "\n" + "and we lost " + 0 + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
            }
            else
            {
                if (popsChance > 75)
                {
                    pops -= popsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + 0 + " g" +
                "\n" + "and we lost " + popsChange + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
                else
                {
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + 0 + " g" +
                "\n" + "and we lost " + 0 + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
            }
        }
        else if (citySize > 4 && citySize <= 8)
        {
            int ironChange = (int)Random.Range(pops * 0.05f, pops * 0.15f);
            iron += ironChange;
            int mineralsChance = Random.Range(0, 101);
            int mineralsChange = (int)Random.Range(minerals * 0.01f, minerals * 0.1f);
            int popsChance = Random.Range(0, 101);
            int popsChange = (int)Random.Range(pops * 0.01f, pops * 0.05f);
            if (mineralsChance > 85)
            {
                if (popsChance > 75)
                {
                    minerals += mineralsChange;
                    pops -= popsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + mineralsChange + " g" +
                "\n" + "and we lost " + popsChange + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
                else
                {
                    minerals += mineralsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + mineralsChange + " g" +
                "\n" + "and we lost " + 0 + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
            }
            else
            {
                if (popsChance > 75)
                {
                    pops -= popsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + 0 + " g" +
                "\n" + "and we lost " + popsChange + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
                else
                {
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + 0 + " g" +
                "\n" + "and we lost " + 0 + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
            }
        }
        else if (citySize > 8 && citySize <= 14)
        {
            int ironChange = (int)Random.Range(pops * 0.1f, pops * 0.2f);
            iron += ironChange;
            int mineralsChance = Random.Range(0, 101);
            int mineralsChange = (int)Random.Range(minerals * 0.01f, minerals * 0.1f);
            int popsChance = Random.Range(0, 101);
            int popsChange = (int)Random.Range(pops * 0.01f, pops * 0.05f);
            if (mineralsChance > 85)
            {
                if (popsChance > 75)
                {
                    minerals += mineralsChange;
                    pops -= popsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + mineralsChange + " g" +
                "\n" + "and we lost " + popsChange + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
                else
                {
                    minerals += mineralsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + mineralsChange + " g" +
                "\n" + "and we lost " + 0 + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
            }
            else
            {
                if (popsChance > 75)
                {
                    pops -= popsChange;
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + 0 + " g" +
                "\n" + "and we lost " + popsChange + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
                else
                {
                    notificationText.text = "On the " + day + ". day we went minig and brought back: " +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + 0 + " g" +
                "\n" + "and we lost " + 0 + " people." +
                "\n" + notificationText.text;
                    RareOre();
                    NewValues();
                }
            }
        }
    }

    //poziva se u mining metodi kao chance za ekstra resursima
    public void RareOre()
    {
        int rareOreChance = Random.Range(1, 101);
        if (rareOreChance > 98)
        {
            int rareOre = (int)Random.Range(minerals * 0.33f, minerals * 0.5f);
            minerals += rareOre;
            iron += rareOre * 10;

            notificationText.text = "On the " + day + ". day we found a vein of rare ore while mining! We brought back: " +
                "\n" + "Iron: " + (int)(rareOre * 10) + " bars" +
                "\n" + "Minerals: " + rareOre + " g" +
                "\n" + notificationText.text;
            NewValues();
        }
    }

    //dobije se gold i potencijalno izgubi populacija
    public void Taxes()
    {
        taxesButton.interactable = false;
        Invoke("DeactivationTimerTaxes", timeSlider.value * 90);
        int goldChange = (int)(pops * 0.25f);
        gold += goldChange;
        int popsChange = (int)Random.Range(pops * 0f, pops * 0.25f);
        pops -= popsChange;

        notificationText.text = "On the " + day + ". day we collected taxes and brought in: " +
            "\n" + "Gold: " + goldChange + " crowns" +
            "\n" + "but lost " + popsChange + " people." +
            "\n" + notificationText.text;
        NewValues();
    }

    //25% chance da se dobije jedan random resurs u iznocu od 50% trenutnih zaliha
    public void PrayToTheGods()
    {
        prayButton.interactable = false;
        Invoke("DeactivationTimerPray", timeSlider.value * 60);
        int randomChance = Random.Range(1, 101);
        int randomResource = Random.Range(1, 8);
        if (randomChance > 75)
        {
            if (randomResource == 1) //pops
            {
                int popsChange = (int)(pops * 0.5f);
                pops += popsChange;

                notificationText.text = "On the " + day + ". day we prayed to the Gods and they answered! We gained: " +
            "\n" + "Population: " + popsChange + " people." +
            "\n" + notificationText.text;
                NewValues();
            }
            if (randomResource == 2) //food
            {
                int foodChange = (int)(food * 0.5f);
                food += foodChange;

                notificationText.text = "On the " + day + ". day we prayed to the Gods and they answered! We gained: " +
            "\n" + "Food: " + foodChange + " kg." +
            "\n" + notificationText.text;
                NewValues();
            }
            if (randomResource == 3) //water
            {
                int waterChange = (int)(water * 0.5f);
                water += waterChange;

                notificationText.text = "On the " + day + ". day we prayed to the Gods and they answered! We gained: " +
            "\n" + "Water: " + waterChange + " l." +
            "\n" + notificationText.text;
                NewValues();
            }
            if (randomResource == 4) //wood
            {
                int woodChange = (int)(wood * 0.5f);
                wood += woodChange;

                notificationText.text = "On the " + day + ". day we prayed to the Gods and they answered! We gained: " +
            "\n" + "Wood: " + woodChange + " m." +
            "\n" + notificationText.text;
                NewValues();
            }
            if (randomResource == 5) //iron
            {
                int ironChange = (int)(iron * 0.5f);
                iron += ironChange;

                notificationText.text = "On the " + day + ". day we prayed to the Gods and they answered! We gained: " +
            "\n" + "Iron: " + ironChange + " bars." +
            "\n" + notificationText.text;
                NewValues();
            }
            if (randomResource == 6) //minerals
            {
                int mineralsChange = (int)(minerals * 0.5f);
                minerals += mineralsChange;

                notificationText.text = "On the " + day + ". day we prayed to the Gods and they answered! We gained: " +
            "\n" + "Minerals: " + mineralsChange + " g." +
            "\n" + notificationText.text;
                NewValues();
            }
            if (randomResource == 7) //gold
            {
                int goldChange = (int)(gold * 0.5f);
                gold += goldChange;

                notificationText.text = "On the " + day + ". day we prayed to the Gods and they answered! We gained: " +
            "\n" + "Gold: " + goldChange + " crowns." +
            "\n" + notificationText.text;
                NewValues();
            }
        }
        else
        {
            notificationText.text = "On the " + day + ". day we prayed to the Gods but they didn't answer... " +
            "\n" + notificationText.text;
            NewValues();
        }
    }

    //gumbu u panelima (heroes i arcane)
    public void Adventurers()
    {
        adventurersButton.interactable = false;
        Invoke("DeactivationTimerAdventurers", timeSlider.value * 60);
        int chanceToHappen = Random.Range(1, 101);
        if (chanceToHappen >= 50)
        {
            int goldChange = (int)Random.Range(gold * 0.05f, gold * 0.25f);
            gold += goldChange;
            int ironChange = (int)Random.Range(iron * 0.05f, iron * 0.25f);
            iron += ironChange;
            int mineralsChange = (int)Random.Range(minerals * 0.05f, minerals * 0.25f);
            minerals += mineralsChange;

            notificationText.text = "On the " + day + ". day we sent an adventuring party on a quest and they brought back: " +
                "\n" + "Gold: " + goldChange + " crowns" +
                "\n" + "Iron: " + ironChange + " bars" +
                "\n" + "Minerals: " + mineralsChange + " g" +
                "\n" + notificationText.text;
            NewValues();
        }
        else
        {
            notificationText.text = "On the " + day + ". day we sent an adventuring party on a quest, but they never returned. " +
                "\n" + notificationText.text;
            NewValues();
        }
    }
    public void SellYourSoul()
    {
        if (minerals >= 250)
        {
            warButtonImage.enabled = false;
            demonWarButtonImage.enabled = true;
            minerals -= 250;
            sellYourSoul = true;
            sellYourSoulButton.interactable = false;
            Invoke("DeactivationTimerSellYourSoul", timeSlider.value * 365);
        }
        ArcaneButtonChecker();
    }

    //tmetode za aktivaciju buttona nakon što istekne njihov interni timer i oni opet smiju biti aktivni
    public void DeactivationTimerPray()
    {
        prayButton.interactable = true;
    }
    public void DeactivationTimerWar()
    {
        warButton.interactable = true;
    }
    public void DeactivationTimerTaxes()
    {
        taxesButton.interactable = true;
    }
    public void DeactivationTimerMining()
    {
        mineButton.interactable = true;
    }
    public void DeactivationTimerHunt()
    {
        huntButton.interactable = true;
    }
    public void DeactivationTimerAdventurers()
    {
        adventurersButton.interactable = true;
    }
    public void DeactivationTimerSellYourSoul()
    {
        sellYourSoulButton.interactable = true;
    }

    //za provjeru mogu li buttoni u trade panelu biti aktivini ili ne
    public void TradeButtonChecker()
    {
        if (gold >= 200)
        {
            buyFoodButton.interactable = true;
            buyWaterButton.interactable = true;
            buyWoodButton.interactable = true;
            buyIronButton.interactable = true;
        }
        else
        {
            buyFoodButton.interactable = false;
            buyWaterButton.interactable = false;
            buyWoodButton.interactable = false;
            buyIronButton.interactable = false;
        }
        if (food >= 500)
        {
            sellFoodButton.interactable = true;
        }
        else
        {
            sellFoodButton.interactable = false;
        }
        if (water >= 750)
        {
            sellWaterButton.interactable = true;
        }
        else
        {
            sellWaterButton.interactable = false;
        }
        if (wood >= 350)
        {
            sellWoodButton.interactable = true;
        }
        else
        {
            sellWoodButton.interactable = false;
        }
        if (iron >= 100)
        {
            sellIronButton.interactable = true;
        }
        else
        {
            sellIronButton.interactable = false;
        }
    }

    //za provjeru mogu li buttoni u arcane panelu biti aktivini ili ne
    public void ArcaneButtonChecker()
    {
        if (minerals >= 1)
        {
            buyPopsButton.interactable = true;
        }
        else
        {
            buyPopsButton.interactable = false;
        }
        if (pops > 50)
        {
            sellPopsButton.interactable = true;
        }
        else
        {
            sellPopsButton.interactable = false;
        }

        if(minerals >= 250)
        {
            sellYourSoulButton.interactable = true;
        }
        else
        {
            sellYourSoulButton.interactable = false;
        }
    }

    //za aktivaciju panela
    public void TradePanelActivation()
    {
        TradeButtonChecker();
        tradePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ArcanePanelActivation()
    {
        ArcaneButtonChecker();
        arcanePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void HeroesHallPanelActivation()
    {
        heroesHallPanel.SetActive(true);
        Time.timeScale = 0;
    }

    //za back buttone
    public void BackHeroes()
    {
        heroesHallPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void BackArcane()
    {
        arcanePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void BackTrade()
    {
        tradePanel.SetActive(false);
        Time.timeScale = 1;
    }

    //za buttone na trade i arcane panelima
    public void BuyPops()
    {
        if (minerals >= 1)
        {
            minerals -= 1;
            pops += 50;
            NewValues();
        }
        ArcaneButtonChecker();
    }
    public void BuyFood()
    {
        if (gold >= 200)
        {
            gold -= 200;
            food += 500;
            NewValues();
        }
        TradeButtonChecker();
    }
    public void BuyWater()
    {
        if (gold >= 200)
        {
            gold -= 200;
            water += 750;
            NewValues();
        }
        TradeButtonChecker();
    }
    public void BuyWood()
    {
        if (gold >= 200)
        {
            gold -= 200;
            wood += 350;
            NewValues();
        }
        TradeButtonChecker();
    }
    public void BuyIron()
    {
        if (gold >= 200)
        {
            gold -= 200;
            iron += 100;
            NewValues();
        }
        TradeButtonChecker();
    }
    public void SellPops()
    {
        if (pops > 50)
        {
            minerals += 1;
            pops -= 50;
            NewValues();
        }
        ArcaneButtonChecker();
    }
    public void SellFood()
    {
        if (food >= 500)
        {
            gold += 200;
            food -= 500;
            NewValues();
        }
        TradeButtonChecker();
    }
    public void SellWater()
    {
        if (water >= 750)
        {
            gold += 200;
            water -= 750;
            NewValues();
        }
        TradeButtonChecker();
    }
    public void SellWood()
    {
        if (wood >= 350)
        {
            gold += 200;
            wood -= 350;
            NewValues();
        }
        TradeButtonChecker();
    }
    public void SellIron()
    {
        if (iron >= 100)
        {
            gold += 200;
            iron -= 100;
            NewValues();
        }
        TradeButtonChecker();
    }

    //za pokretanje igre
    public void StartGame()
    {
        volumeSlider.value = startVolumeSlider.value;
        startGamePanel.SetActive(false);
        notificationText.text = " ";
        NewValues();
        StartCoroutine(DayIncrease());
        StartCoroutine(FoodGain());
        StartCoroutine(FoodLose());
        StartCoroutine(WaterGain());
        StartCoroutine(WaterLose());
        StartCoroutine(PopulationGain());
        StartCoroutine(PopulationLose());
        StartCoroutine(ExoticTrader());
        StartCoroutine(Catastrophe());
        StartCoroutine(BountifulHarvest());
        StartCoroutine(BabyBoom());
        StartCoroutine(RetiredAdventurer());
        StartCoroutine(UnderAttack());
        StartCoroutine(MagesGuild());
        StartCoroutine(Tourney());
        BackgroundChanger();
        //StartCoroutine(SliderText());
    }

    //za izlazak iz igre
    public void QuitGame()
    {
        Application.Quit();
    }

    //za resetiranje igre
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
