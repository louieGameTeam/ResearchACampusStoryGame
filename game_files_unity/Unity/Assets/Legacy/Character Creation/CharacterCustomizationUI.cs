/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

public class CharacterCustomizationUI : MonoBehaviour {
    
    private SpriteRenderer currentSpriteRenderer, handsSpriteRenderer, hairSpriteRenderer;
    private Image currSelection;
    public Sprite[] hairStyles, pigtailDirections, shortHairDirections;
    public int currHairStyle;
    private GameObject skinColorPreset, eyeColorPreset, topSelection, pantsSelection, shoesSelection;
    private Animator anim;
    private int animationNum;
    [SerializeField]
    GameObject hairPanel;
    [SerializeField]
    GameObject clothesPanel;
    [SerializeField]
    GameObject skinEyesPanel;

//<<<<<<< Updated upstream:Game/Assets/CharacterCreation/CharacterCustomizationUI.cs
    //private void Awake()
    //{
//=======
    private void Start() {
        
//>>>>>>> Stashed changes:Game/Assets/CharacterCreation/Legacy/CharacterCustomizationUI.cs
        //Everything starts in the hair tab
        // initially set hair style to be pigtails (0)
        currHairStyle = 0;
        hairSpriteRenderer = GameObject.Find("hair").GetComponent<SpriteRenderer>();
        currentSpriteRenderer = hairSpriteRenderer;
        currentSpriteRenderer.sprite = hairStyles[currHairStyle]; // set the actual pigtails sprite
        handsSpriteRenderer = GameObject.Find("side_hand_walk").GetComponent<SpriteRenderer>();

        skinColorPreset = GameObject.Find("SkinColorPreset");
        eyeColorPreset = GameObject.Find("EyeColorPreset");

        topSelection = GameObject.Find("TopSelectionBorder");
        pantsSelection = GameObject.Find("PantsSelectionBorder");
        shoesSelection = GameObject.Find("ShoesSelectionBorder");

        currSelection = GameObject.Find("HairSelectionBorder").GetComponent<Image>();

        hairPanel = GameObject.Find("HairPanel");
        clothesPanel = GameObject.Find("ClothesPanel");
        skinEyesPanel = GameObject.Find("SkinEyesPanel");

        anim = GameObject.Find("Player").GetComponent<Animator>();
        animationNum = 0;
    }

    // ****************** Set all panels off except hair tab ***********************
    private void OnEnable()
    {
        hairPanel.SetActive(true);
        clothesPanel.SetActive(false);
        skinEyesPanel.SetActive(false);
    }

    // ****************** Hair tab UI ***********************
    public void HairClicked()
    {
        hairPanel.SetActive(true);
        clothesPanel.SetActive(false);
        skinEyesPanel.SetActive(false);

        currentSpriteRenderer = GameObject.Find("hair").GetComponent<SpriteRenderer>();
        currSelection = GameObject.Find("HairSelectionBorder").GetComponent<Image>();
    }

    public void HairRightArrowClicked()
    {
        if (currHairStyle < hairStyles.Length-1)
        {
            ++currHairStyle;
            currentSpriteRenderer.sprite = hairStyles[currHairStyle];
            GameObject.Find("HairText").GetComponent<Text>().text = "Hair " + (currHairStyle+1).ToString();
        } 
    }

    public void HairLeftArrowClicked()
    {
        if (currHairStyle > 0)
        {
            --currHairStyle;
            currentSpriteRenderer.sprite = hairStyles[currHairStyle];
            GameObject.Find("HairText").GetComponent<Text>().text = "Hair " + (currHairStyle + 1).ToString();
        }
    }

    // ****************** Skin/eyes tab UI ***********************
    public void SkinClicked()
    {
        hairPanel.SetActive(false);
        clothesPanel.SetActive(false);
        skinEyesPanel.SetActive(true);

        // change button colors
        GameObject.Find("SkinButton").GetComponent<Image>().color = new Color32(0x00, 0x3E, 0xA3, 0xFF);
        GameObject.Find("EyesButton").GetComponent<Image>().color = new Color32(0x2D, 0x7D, 0xFF, 0xFF);

        skinColorPreset.SetActive(true);
        eyeColorPreset.SetActive(false);
        currSelection = GameObject.Find("SkinSelectionBorder").GetComponent<Image>();
        currentSpriteRenderer = GameObject.Find("body").GetComponent<SpriteRenderer>();
    }

    public void EyesClicked()
    {
        // change button colors
        GameObject.Find("SkinButton").GetComponent<Image>().color = new Color32(0x2D, 0x7D, 0xFF, 0xFF);
        GameObject.Find("EyesButton").GetComponent<Image>().color = new Color32(0x00, 0x3E, 0xA3, 0xFF);

        skinColorPreset.SetActive(false);
        eyeColorPreset.SetActive(true);
        currSelection = GameObject.Find("EyeSelectionBorder").GetComponent<Image>();
        currentSpriteRenderer = GameObject.Find("eye_texture").GetComponent<SpriteRenderer>();
    }

    // ****************** Clothes tab UI ***********************
    public void TopClicked()
    {
        hairPanel.SetActive(false);
        clothesPanel.SetActive(true);
        skinEyesPanel.SetActive(false);

        // change button colors
        GameObject.Find("TopButton").GetComponent<Image>().color = new Color32(0x00, 0x3E, 0xA3, 0xFF);
        GameObject.Find("PantsButton").GetComponent<Image>().color = new Color32(0x2D, 0x7D, 0xFF, 0xFF);
        GameObject.Find("ShoesButton").GetComponent<Image>().color = new Color32(0x2D, 0x7D, 0xFF, 0xFF);

        topSelection.SetActive(true);
        pantsSelection.SetActive(false);
        shoesSelection.SetActive(false);
        currSelection = GameObject.Find("TopSelectionBorder").GetComponent<Image>();
        currentSpriteRenderer = GameObject.Find("hoodie").GetComponent<SpriteRenderer>();
    }

    public void PantsClicked()
    {
        // change button colors
        GameObject.Find("PantsButton").GetComponent<Image>().color = new Color32(0x00, 0x3E, 0xA3, 0xFF);
        GameObject.Find("TopButton").GetComponent<Image>().color = new Color32(0x2D, 0x7D, 0xFF, 0xFF);
        GameObject.Find("ShoesButton").GetComponent<Image>().color = new Color32(0x2D, 0x7D, 0xFF, 0xFF);

        topSelection.SetActive(false);
        pantsSelection.SetActive(true);
        shoesSelection.SetActive(false);
        currSelection = GameObject.Find("PantsSelectionBorder").GetComponent<Image>();
        currentSpriteRenderer = GameObject.Find("pants_texture").GetComponent<SpriteRenderer>();
    }

    public void ShoesClicked()
    {
        // change button colors
        GameObject.Find("ShoesButton").GetComponent<Image>().color = new Color32(0x00, 0x3E, 0xA3, 0xFF);
        GameObject.Find("PantsButton").GetComponent<Image>().color = new Color32(0x2D, 0x7D, 0xFF, 0xFF);
        GameObject.Find("TopButton").GetComponent<Image>().color = new Color32(0x2D, 0x7D, 0xFF, 0xFF);

        topSelection.SetActive(false);
        pantsSelection.SetActive(false);
        shoesSelection.SetActive(true);
        currSelection = GameObject.Find("ShoesSelectionBorder").GetComponent<Image>();
        currentSpriteRenderer = GameObject.Find("shoe").GetComponent<SpriteRenderer>();
    }

    public void ColorClicked()
    {
        currSelection.rectTransform.position = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().rectTransform.position;
        currentSpriteRenderer.color = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;

        if (skinColorPreset.activeInHierarchy)
            handsSpriteRenderer.color = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
    }

    private void SwitchAnimation()
    {
        Sprite[] hairDir;
        if (currHairStyle == 0)
            hairDir = pigtailDirections;
        else
            hairDir = shortHairDirections;

        if (animationNum == 0)
            anim.SetTrigger("Down");
        else if (animationNum == 1)
            anim.SetTrigger("Right");
        else if (animationNum == 2)
            anim.SetTrigger("Up");
        else if (animationNum == 3)
            anim.SetTrigger("Left");

        if (animationNum == 3)
        {
            hairSpriteRenderer.sprite = hairDir[1];
            hairSpriteRenderer.flipX = true;
        } 
        else
        {
            hairSpriteRenderer.flipX = false;
            hairSpriteRenderer.sprite = hairDir[animationNum];
        }
    }

    public void AnimationRightClicked()
    {
        if (animationNum < 3)
            animationNum++;
        else
            animationNum = 0;

        SwitchAnimation();
    }

    public void AnimationLeftClicked()
    {
        if (animationNum > 0)
            animationNum--;
        else
            animationNum = 3; 

        SwitchAnimation();
    }

    // Saves character data
    public void SaveCharacter()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            // Get data
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open, FileAccess.ReadWrite);
            SavedData data = (SavedData)bf.Deserialize(file);
            file.Close();
            file = File.Create(Application.persistentDataPath + "/saveData.dat");

            // Save data
            data.playerData.hairStyle = currHairStyle;
            data.playerData.hairColor = GameObject.Find("hair").GetComponent<SpriteRenderer>().color;
            data.playerData.skinColor = GameObject.Find("body").GetComponent<SpriteRenderer>().color;
            data.playerData.hoodieColor = GameObject.Find("hoodie").GetComponent<SpriteRenderer>().color;
            data.playerData.pantsColor = GameObject.Find("pants/pants_texture").GetComponent<SpriteRenderer>().color;
            data.playerData.shoesColor = GameObject.Find("shoes").GetComponent<SpriteRenderer>().color;
            data.playerData.eyeColor = GameObject.Find("eyes/eye_texture").GetComponent<SpriteRenderer>().color;

            // Write to file
            bf.Serialize(file, data);
            file.Close();
        }
    }
}*/