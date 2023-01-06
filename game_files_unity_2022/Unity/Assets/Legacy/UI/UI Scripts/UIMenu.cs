using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Enable and disable a menu
public class UIMenu : MonoBehaviour
{
    private static UIMenu currentMenu;  // Used to access the current menu object from, say, a lower menu
    private Animator anim;
    [SerializeField]
    private bool subMenu = false;       // Use if the menu is a submenu, lets you open two menus at once
    [SerializeField]
    GameObject defaultButton;           // The first button to be selected when entering menu
    [SerializeField]
    public GameObject previousMenu;     // The menu used to get to current menu

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (anim != null)
        {
            anim.SetTrigger("Show");
        }
    }

    public void OpenMenu()
    {
        // If not currently in another menu, open the menu
        if (UIManager.inMenu == false || subMenu == true || gameObject == UIManager.current.pauseMenu)
        {
            gameObject.SetActive(true);
            UIManager.inMenu = true;
            if (previousMenu != null && previousMenu.activeInHierarchy == true)
            {
                previousMenu.GetComponent<UIMenu>().HideMenu(0.2f);
            }
            currentMenu = this;
        }
    }

    // Use when hiding a menu from above
    public void SelectMenu()
    {
        UIManager.currentMenu = gameObject;
        SelectButton();
    }

    public void HideMenu(float timeToDisable)
    {   // Time timeToDisable with the hide animation
        if (anim != null)
        {
            anim.SetTrigger("Hide");
        }
        StartCoroutine(DisableMenu(timeToDisable));
        UIManager.currentlyAnimating = true;
    }

    // Disable the panel after the animation plays
    IEnumerator DisableMenu(float timeToDisable)
    {
        yield return new WaitForSeconds(timeToDisable);
        // If not a submenu, disable
        if (subMenu == false)
        {
            UIManager.inMenu = false;
        }
        UIManager.currentlyAnimating = false;
        currentMenu.SelectMenu();
        gameObject.SetActive(false);
    }

    // Select a button in the canvas upon entering
    void SelectButton()
    {
         EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    // Set the default button to be the last one selected
    public void ChangeDefaultButton(GameObject button)
    {
        defaultButton = button;
    }
}
