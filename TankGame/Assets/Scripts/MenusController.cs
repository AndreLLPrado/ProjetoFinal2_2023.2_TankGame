using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusController : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject upgradeMenu;
    [SerializeField]
    private GameObject howToPlayMenu;
    [SerializeField]
    private GameObject creditsMenu;
    public void playGame()
    {
        SceneManager.LoadScene("game");
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        upgradeMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void OpenUpgradeMenu()
    {
        mainMenu.SetActive(false);
        upgradeMenu.SetActive(true);
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void OpenHowToPlayMenu()
    {
        mainMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void OpenCreditsMenu()
    {
        mainMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
}
