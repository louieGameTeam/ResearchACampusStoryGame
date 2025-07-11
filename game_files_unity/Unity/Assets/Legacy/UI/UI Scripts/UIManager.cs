using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Manages the UI and menus
public class UIManager : MonoBehaviour
{
    public static UIManager current;
    public static bool inMenu = false;
    public static bool paused = false;
    public static bool currentlyAnimating = false;  // Track if currently showing/hiding a menu (prevents overlap)
    public static GameObject currentMenu;           // Track which menu is currently open
    [SerializeField]
    public GameObject pauseMenu;                    // The menu that pops up when "Pause" is entered
    [SerializeField]
    GameObject buttonPointer;                       // The pointer to the current highlighted button

    private void Awake() {
        current = this;
    }

    private void OnEnable() {
        paused = false;
        inMenu = false;
        currentlyAnimating = false;
        buttonPointer.SetActive(false);
    }

    void Update()  {
        CheckInput();
        // Move the button pointer to current highlighted button
        if (buttonPointer.activeInHierarchy == true) {
            buttonPointer.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
        }
    }

    void CheckInput() {
        if (Input.GetButtonDown("Cancel")) {
            Pause();
        }
    }

    public void Pause() {
        
        // If not animating another menu transition
        if (currentlyAnimating == false) {
            if (paused == false) {
                paused = true;
                pauseMenu.GetComponent<UIMenu>().OpenMenu();
                inMenu = false; // Only opening pause menu
                pauseMenu.GetComponent<UIMenu>().SelectMenu();
                Camera.main.GetComponent<Animator>().SetTrigger("Pause");
                buttonPointer.SetActive(true);

            } else {
                
                // Return to previous menu
                GameObject menuToDisable = currentMenu;
                if (currentMenu.GetComponent<UIMenu>().previousMenu != null) {
                    currentMenu.GetComponent<UIMenu>().previousMenu.GetComponent<UIMenu>().OpenMenu();
                }
                menuToDisable.GetComponent<UIMenu>().HideMenu(0.2f);
                if (menuToDisable == pauseMenu)
                {   // If false, current menu is the base pause menu, so unpause
                    inMenu = false;
                    paused = false;
                    buttonPointer.SetActive(false);
                    Camera.main.GetComponent<Animator>().SetTrigger("Unpause");
                }
            }
        }
    }
}