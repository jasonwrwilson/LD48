using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject shopPanel;
    public GameObject weaponPanel;
    public DeathPanel deathPanel;
    public GameObject enterPrompt;
    public GameObject quitPanel;
    public IntroBar introPanel;

    public DudeController dude;

    private UpgradeManager upgradeManager;

    public TileManager tileManager;

    private int recordDepth = 0;

    public enum DeathType
    {
        Fish,
        Air
    }

    private string[] fishQuips = { "You're sleeping with the fishes!", "Fish breath is killer!", "The one that got away!" };
    private string[] airQuips = { "Forgot to come up for air!", "Couldn't hold your breath!", "Out of breath!" };

    // Start is called before the first frame update
    void Start()
    {
        upgradeManager = GetComponent<UpgradeManager>();
        LoadRecord();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        PauseGame(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        PauseGame(false);
    }

    public void OpenWeapons()
    {
        weaponPanel.SetActive(true);
        PauseGame(true);
    }

    public void CloseWeapons()
    {
        weaponPanel.SetActive(false);
        PauseGame(false);
    }

    public void ShowEnterPrompt(bool show)
    {
        enterPrompt.SetActive(show);
    }

    public void Death(int currentDepth, DeathType deathType)
    {
        deathPanel.gameObject.SetActive(true);
        deathPanel.ShowDeathPanel(GetQuip(deathType), currentDepth, recordDepth);

        if ( currentDepth > recordDepth )
        {
            recordDepth = currentDepth;
            SaveRecord();
        }

        PauseGame(true);
    }

    public void CloseDeathPanel()
    {
        deathPanel.gameObject.SetActive(false);

        tileManager.ResetMap();
        dude.ResetDude();
        PauseGame(false);

        introPanel.gameObject.SetActive(true);
        introPanel.ResetIntro();
    }

    private string GetQuip(DeathType deathType)
    {
        switch (deathType)
        {
            case DeathType.Fish:
            {
                int randomQuip = Random.Range(0, fishQuips.Length);
                return fishQuips[randomQuip];

                break;
            }
            case DeathType.Air:
            {
                int randomQuip = Random.Range(0, airQuips.Length);
                return airQuips[randomQuip];

                break;
            }
        }

        return "Couldn't keep your head above water!";
    }

    public void OpenQuitPanel()
    {
        quitPanel.SetActive(true);
        PauseGame(true);
    }

    public void CloseQuitPanel()
    {
        quitPanel.SetActive(false);
        PauseGame(false);
    }

    public void PauseGame(bool pause)
    {
        isPaused = pause;
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void SaveRecord()
    {
        PlayerPrefs.SetInt("RecordDepth", recordDepth);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    private void LoadRecord()
    {
        if (PlayerPrefs.HasKey("RecordDepth"))
        {
            recordDepth = PlayerPrefs.GetInt("RecordDepth");
        }
    }

    public int GetRecordDepth()
    {
        return recordDepth;
    }
}
