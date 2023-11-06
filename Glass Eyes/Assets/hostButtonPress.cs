using UnityEngine;
using UnityEngine.SceneManagement;

public class hostButtonPress : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject hostUI;
    public GameObject roomList;
    public GameObject options;
    public void hostMenu()
    {
        Debug.Log("hostMenu called");
        hostUI.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void backButton()
    {
        hostUI.SetActive(false);
        roomList.SetActive(false);
        options.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void optionsButton()
    {
        options.SetActive(true);
        mainMenu.SetActive(false);
    }

}
