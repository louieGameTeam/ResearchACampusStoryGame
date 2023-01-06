using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Xml;
using CharacterCreation;



#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ChatManager))]
public class LevelScriptEditor : Editor 
{
	public override void OnInspectorGUI() {
		
		ChatManager myTarget = (ChatManager)target;

		EditorGUILayout.LabelField ("Formatting");
		EditorGUI.indentLevel++;
		myTarget.textFont = EditorGUILayout.ObjectField("Text Font", (Object)myTarget.textFont, typeof(Font), false) as Font;
		myTarget.fontSize = EditorGUILayout.IntField("Font Size", myTarget.fontSize);
		myTarget.lineSpacing = EditorGUILayout.IntField("Line Spacing", myTarget.lineSpacing);
		myTarget.color = EditorGUILayout.ColorField("Color", myTarget.color);
		myTarget.selectedColor = EditorGUILayout.ColorField("Selection Color", myTarget.selectedColor);
		EditorGUI.indentLevel--;

		myTarget.charactersPerSecond = EditorGUILayout.FloatField("Characters / sec", myTarget.charactersPerSecond);
		myTarget.anim = EditorGUILayout.ObjectField("Animator", (Object)myTarget.anim, typeof(Animator), false) as Animator;

		EditorGUILayout.LabelField ("Reply Overflow Buttons");
		EditorGUI.indentLevel++;
		myTarget.previousPage = EditorGUILayout.TextField ("Previous Page", myTarget.previousPage);
		myTarget.nextPage = EditorGUILayout.TextField ("Next Page", myTarget.nextPage);
		EditorGUI.indentLevel--;

        EditorGUILayout.LabelField ("Child Regions");
        EditorGUI.indentLevel++;
        myTarget.npcRegion = EditorGUILayout.ObjectField("NPC Text", (Object)myTarget.npcRegion, typeof(RectTransform), true) as RectTransform;
        myTarget.playerRegion = EditorGUILayout.ObjectField("Player Text", (Object)myTarget.playerRegion, typeof(RectTransform), true) as RectTransform;
		myTarget.npcHead = EditorGUILayout.ObjectField("NPC Icon", (Object)myTarget.npcHead, typeof(RectTransform), true) as RectTransform;
        myTarget.playerHead = EditorGUILayout.ObjectField("Player Icon", (Object)myTarget.playerHead, typeof(RectTransform), true) as RectTransform;
        myTarget.npcName = EditorGUILayout.ObjectField("NPC Name", (Object)myTarget.npcName, typeof(Text), true) as Text;
        myTarget.npcName.font = myTarget.textFont;
        EditorGUI.indentLevel--;

		myTarget.playAudio = EditorGUILayout.Toggle ("Play Audio", myTarget.playAudio);
		if (myTarget.playAudio) {
			EditorGUI.indentLevel++;
            myTarget.clip = EditorGUILayout.ObjectField("Chat SFX", (AudioClip)myTarget.clip, typeof(AudioClip), false) as AudioClip;
            myTarget.charactersPerClick = EditorGUILayout.IntField ("Characters / click", myTarget.charactersPerClick);
			EditorGUI.indentLevel--;
		}
		EditorGUILayout.LabelField ("Input Buttons");
		EditorGUI.indentLevel++;
		myTarget.cont = EditorGUILayout.TextField ("Continue", myTarget.cont);
		myTarget.choose = EditorGUILayout.TextField ("Selection Axis", myTarget.choose);
		myTarget.exit = EditorGUILayout.TextField ("Exit", myTarget.exit);
		EditorGUI.indentLevel--;
	}
}
#endif


[RequireComponent(typeof(AudioSource))]
public class ChatManager : MonoBehaviour {

	// Text settings
	public Font textFont;
	public int fontSize;
	public int lineSpacing;
	public Color color;
	public Color selectedColor;

	// How many characters will be displayed in one second
	public float charactersPerSecond;

    // The CanvasScaler attached to the Canvas that this operates under
    private CanvasScaler canvasScaler;

	// Bounding regions that the text will fit into under different circumstances
    public RectTransform npcRegion;
    public RectTransform playerRegion;

    // Image objects that represent the icons of the player and the chat target
    public RectTransform npcHead;
    public RectTransform playerHead;

    // Text object that displays the name of the chattable object during chat
    public Text npcName;

	// Animation controller that shows and hides the chat banner
	public Animator anim;

	// If this is false, audio will not play and the related properties will not be used
	public bool playAudio;

	// The AudioClip that will play as chat scrolls and on selected response change
	public AudioClip clip;

    // The attached AudioSource, used to play sound effects
    private AudioSource audioSource;

	// Names of buttons for reply overflow navigation
	public string previousPage;
	public string nextPage;

	// How many characters will be rendered before the next click plays
	public int charactersPerClick;

	// Names of input buttons as defined in InputManager
	public string cont;
	public string choose;
	public string exit;


	// *** NON-SERIALIZED VARIABLES ***

	// The NPC dialog that the player is currently engaged with
	static public Dialog chatting;

	// Animation related hashes to optimize state lookups
	static int showHash = Animator.StringToHash("Show");
	static int hideHash = Animator.StringToHash("Hide");

	// Bool representing the current text delivery
	static bool stringPlaying;

	// True only on the frame that a new chat starts, is used to prevent chat initialization input from also being used to move to the next page
	static bool startedChattingThisFrame = false;

	// True if a key that is part of the "Vertical" input axis was pressed this frame
	static bool verticalPressed = false;

	// The range of options displayed on the current page
	static Vector2 displayedOptions = Vector2.zero;

	// The currently selected option, compared to displayOptions to check for Previous and Next
	static int currentlySelected = 0;

	// The list of available options for the current page, potentially including Previous and Next
	static List<string> optionsWithOverflow;

	// Store already calculated overflow ranges in here for optimization and Previous
	static List<Vector2> pageRanges;

    // The region that is in use, defined by the current node type
    static RectTransform currentRegion;


    void Start () {
		anim.enabled = false;
		optionsWithOverflow = new List<string> ();
		pageRanges = new List<Vector2> ();
        canvasScaler = GetComponentInParent<CanvasScaler>();

        audioSource = GetComponent<AudioSource>();
        if (clip != null) audioSource.clip = clip;

        CharacterSetting cs = FindObjectOfType<PlayerControl>().GetComponent<Character>().cs;
        GenerateIcon(cs, playerHead);

        chatting = null;
	}

    public static void GenerateIcon (CharacterSetting input, RectTransform location) {
        Transform existing = location.Find("Character Sprites");
        if (existing)
            DestroyImmediate(existing.gameObject);
        
		string spritesName = "Character Sprites";
        int currentDir = input.host.GetComponent<Animator>().GetInteger(Controller.directionHash);
        input.host.GetComponent<Animator>().SetInteger(Controller.directionHash, 0);
        input.host.GetComponent<Character>().SetIdle();
        Transform allTransform = input.host.Find(spritesName);
        GameObject all = Instantiate(allTransform.gameObject) as GameObject;
        input.host.GetComponent<Controller>().Walk(currentDir);
		input.host.GetComponent<Character>().SetIdle();
		all.name = spritesName;
        all.transform.SetParent(location);
		all.transform.localPosition = Vector3.down * 35;

		Transform sprites = location.Find(spritesName);
		sprites.localScale = Vector3.one * 500;
		SpriteRenderer[] allSprites = sprites.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer item in allSprites) {
			if (item.sprite == null) continue;
			Image img = item.gameObject.AddComponent<Image>();
			img.color = item.color;
			img.sprite = item.sprite;
            Destroy(item);
		}
    }

	// Initialize the chat with a new NPC
	public static void StartChatting (Dialog dialog) {

		ChatManager cm = GetCM();
        if (cm.npcHead.childCount > 0)
		    DestroyImmediate(cm.npcHead.GetChild(0).gameObject);
		Image icon = cm.npcHead.GetComponent<Image>();
        icon.enabled = false;
		if (dialog.host.isNPC) {
            CharacterSetting cs = dialog.host.GetComponent<Character>().cs;
            GenerateIcon(cs, cm.npcHead);
            cm.npcName.text = cs.name;
        } else if (dialog.host.chatHead != null) {
            icon.enabled = true;
            icon.sprite = dialog.host.chatHead;
            cm.npcName.text = dialog.host.myName;
        }

		chatting = dialog;
		ShowBanner ();
		dialog.ToBeginning ();
		NextBanner ();
		startedChattingThisFrame = true;
	}

	void Update () {

        // Process and respond to input if the user is currently chatting
		if (chatting != null) {

			// Save various chat related inputs
			int v = -Mathf.RoundToInt (Input.GetAxis (choose));
			if (v == 0) {
				verticalPressed = false;
			} else if (v != 0) {
				if (!verticalPressed)
					verticalPressed = true;
				else
					v = 0;
			}
			bool enter = Input.GetButtonDown (cont);

			// Progress to the next chat page if the user presses the Submit button
			if (enter && !startedChattingThisFrame) {
                PickResponse();
			} else if (v != 0 && chatting.currentType == Dialog.NodeType.Reply) {
				displayedOptions = new Vector2 (displayedOptions.x, chatting.options.Count - 1);
				UpdateResponse (v);
			}

			// Close the chat session if the user presses Escape
			if (Input.GetButtonDown (exit) && chatting.escapable) {
				HideBanner ();
			}
		}
		startedChattingThisFrame = false;
	}

    // What happens when you click or press enter on a selected response
    void PickResponse () {
		bool onPrevious = currentlySelected == 0 && displayedOptions.x != 0;
		bool onMore = currentlySelected == optionsWithOverflow.Count - 1 && displayedOptions.y != chatting.options.Count - 1;
		if (chatting.currentType == Dialog.NodeType.Reply && (onPrevious || onMore)) {
			if (onMore) {
				displayedOptions = new Vector2(displayedOptions.y + 1, chatting.options.Count - 1);
			}
			else if (onPrevious) {
				int y = (int)displayedOptions.x - 1;
				foreach (Vector2 item in pageRanges)
					if (item.y == y)
						displayedOptions = item;
			}
			currentlySelected = 0;
			UpdateResponse(0);
		}
		else if (!stringPlaying) {
			NextBanner();
		}

        // Play the chat sound when the user progresses to a Reply node
        if (playAudio && chatting != null && chatting.currentType == Dialog.NodeType.Reply)
            //Audio.Play(clip);
            audioSource.Play();
	}

	// Get the next chat node and update the chat banner accordingly
	static void NextBanner () {

		// Save the NodeType of the next chat node
		Dialog.NodeType next = chatting.Next ();

        // Update the text region and icon
        SetTextRegion(next);
        SetIcon(next);

		// If the nodetype is a Page, simply display the text
		if (next == Dialog.NodeType.Page || next == Dialog.NodeType.Say) {
			PlayString (chatting.page);

		// If it's a Reply, populate the banner with the available responses
		} else if (next == Dialog.NodeType.Reply) {
			displayedOptions = Vector2.up * (chatting.options.Count - 1);
            pageRanges.Clear();
			currentlySelected = 0;
			UpdateResponse (0);

			// If the type is Finish, terminate the chat session and hide the banner
		} else if (next == Dialog.NodeType.Finish) {
			HideBanner ();
		}
	}

    // Update the current text region depending on the current node type
    static void SetTextRegion (Dialog.NodeType type) {
		if (type == Dialog.NodeType.Finish) return;
		ChatManager cm = GetCM();
        cm.npcRegion.gameObject.SetActive(false);
		cm.playerRegion.gameObject.SetActive(false);
        switch(type) {
            case Dialog.NodeType.Say:
            case Dialog.NodeType.Reply:
                cm.playerRegion.gameObject.SetActive(true);
                currentRegion = cm.playerRegion;
                break;
            case Dialog.NodeType.Page:
                cm.npcRegion.gameObject.SetActive(true);
                currentRegion = cm.npcRegion;
                break;
        }
    }

    // Update the current icon depending on the current node type
	static void SetIcon(Dialog.NodeType type) {
        if (type == Dialog.NodeType.Finish) return;
		ChatManager cm = GetCM();
        cm.playerHead.gameObject.SetActive(false);
        cm.npcHead.gameObject.SetActive(false);
        cm.npcName.gameObject.SetActive(false);
        if (type == Dialog.NodeType.Page) {
            cm.npcHead.gameObject.SetActive(true);
            cm.npcName.gameObject.SetActive(true);
        } else if (type == Dialog.NodeType.Reply || type == Dialog.NodeType.Say) {
            cm.playerHead.gameObject.SetActive(true);
        }
	}

	// Return the formatted string for all response options using the current selectedResponse value
	static void UpdateResponse (int input) {
		int oldResponse = currentlySelected;
		optionsWithOverflow.Clear ();
		if (displayedOptions.x != 0)
			optionsWithOverflow.Add (GetCM().previousPage);
		foreach (Vector2 item in pageRanges) {
			if (item.x == displayedOptions.x)
				displayedOptions = item;
		}
		for (int i = (int)displayedOptions.x; i <= (int)displayedOptions.y; i++)
            optionsWithOverflow.Add(chatting.options[i]);
		if (displayedOptions.y != chatting.options.Count - 1) {
			optionsWithOverflow.Add (GetCM().nextPage);
		}
		currentlySelected = Mathf.Clamp(currentlySelected + input, 0, optionsWithOverflow.Count - 1);
		chatting.selectedResponse = (int)displayedOptions.x + currentlySelected + (displayedOptions.x == 0 ? 0 : -1);
		bool changed = oldResponse != currentlySelected;
        if (changed && GetCM().playAudio)
            //Audio.Play(GetCM().clip);
            GetCM().audioSource.Play();
		string optionList = GetCM().OptionList ();
		PlayString (optionList, false);
	}

	// Return the string representing the list of current options within the given range
	string OptionList () {
		string optionList = string.Empty;
		string hex = TextColor.RGB2HEX (selectedColor);
        string nodeStart = "<color hex=\"" + hex + "\">" + " &gt; ";
		string nodeEnd = "</color>";
		for (int i = 0; i < optionsWithOverflow.Count; i++) {
			if (i == currentlySelected)
				optionList += nodeStart + optionsWithOverflow[i] + nodeEnd;
			else
				optionList += optionsWithOverflow [i];
			optionList += '\n';
		}
		return optionList;
	}

	// Show the chat banner
	static void ShowBanner () {
		GetCM().anim.enabled = true;
		GetCM().anim.SetTrigger (showHash);
	}

	// Hide the chat banner
	static void HideBanner () {
		chatting = null;
		GetCM().anim.SetTrigger (hideHash);
	}

	// Public function to start playing the given string, with scrolling text
	public static void PlayString (string input) {
		PlayString (input, true);
	}

	// Public function to start playing the given string, with a second boolean parameter to scroll text or not
	public static void PlayString (string input, bool scrolling) {
		IEnumerator playing = GetCM().PlayStringCoroutine (input, scrolling);
		GetCM().StartCoroutine (playing);
	}

	// Private coroutine to start playing the given string
	IEnumerator PlayStringCoroutine (string xml, bool scrolling) {

        // Store the input as an XmlDocument to access it as a node
		XmlDocument chat = new XmlDocument();
		string input = "<chat>" + xml + "</chat>";
		chat.LoadXml (input);

		// Set the input string to contain only the text, without additional xml formatting
		input = chat.InnerText;

        // Set an array for the styles for each of the included characters in 'input'
        List<TextStyle> styles = ParseStyleNode(input, xml);
        input = string.Empty;
        for (int i = 0; i < styles.Count; i++) {
            input += styles[i].chars;
        }

		// Set playing mode to true, as the animated scrolling effect is about to start
		stringPlaying = true;

		// Clear all existing characters from textRegion
		for (int i = currentRegion.childCount - 1; i >= 0; i--) {
			Destroy(currentRegion.GetChild(i).gameObject);
		}

		// Save the dimensions of textRegion in pixels
		Vector2 boxSize = currentRegion.rect.size;

		// The current starting point for the next letter
		Vector2 lead = Vector2.zero;

		// If the input takes more than one page to display, the remainder will be stored in this string
		string pageOverflow = string.Empty;

		// Iterate through each character in the input string
		for (int i = 0; i < input.Length; i++) {

			// Get the character at the current index
			char item = styles[i].chars;

			// Generate a new empty RectTransform, add and set up a new Text component, and parent it to textRegion
			RectTransform letter = (new GameObject()).AddComponent<RectTransform>();
			Text t = letter.gameObject.AddComponent<Text> ();
            t.fontSize = fontSize;
			t.font = textFont;
			t.text = item.ToString();
			t.color = color;
			styles[i].ApplyEffects(t);
			letter.name = item.ToString ();
			letter.SetParent (currentRegion);
            letter.localScale = Vector3.one;

            EventTrigger et = letter.gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry click = new EventTrigger.Entry();
			click.eventID = EventTriggerType.PointerClick;
			click.callback.AddListener((data) => {
				if (ChatManager.chatting.currentType == Dialog.NodeType.Reply) {
                    PickResponse();
                }
			});
			et.triggers.Add(click);
            if (chatting.currentType == Dialog.NodeType.Reply) {
                EventTrigger.Entry hover = new EventTrigger.Entry();
                hover.eventID = EventTriggerType.PointerEnter;
				int temp = i;
				hover.callback.AddListener((data) => {
					int index = styles[temp].replyIndex;
                    int diff = index - currentlySelected;
                    if (diff != 0)
					    UpdateResponse(diff);
				});
                et.triggers.Add(hover);
            }

			// Set both anchors to the top left of textRegion
			letter.anchorMin = Vector2.up;
			letter.anchorMax = Vector2.up;

			// Calculate the size of this character with the current font settings
			float x = LayoutUtility.GetPreferredWidth (letter);
			float y = LayoutUtility.GetPreferredHeight (letter);

			// Keep all characters off until they turn on one at a time below
			t.enabled = !scrolling;

			// Set the offsets to be between the end of the last letter and the end of this letter
			letter.offsetMin = new Vector2 (lead.x, lead.y - y);
            letter.offsetMax = new Vector2 (Mathf.CeilToInt(lead.x + x + 1), lead.y);

			// Update the lead value for the next character, drop to the next line after a line break or after the last fittable word
			if (lead.x > boxSize.x || item == '\n') {
				string existing = input.Substring (0, i + 1);
				int lastSpace = existing.LastIndexOf (' ');
				int lastNewLine = existing.LastIndexOf ('\n');
				int last = Mathf.Max (lastSpace, lastNewLine);
				if (last >= 0) {
					int starting = currentRegion.childCount - 1;
					for (int j = starting; j >= starting - (i - last); j--) {
						DestroyImmediate (currentRegion.GetChild (j).gameObject);
					}
					i = last;
				}
				lead = new Vector2 (0, lead.y - (fontSize + lineSpacing));
				if (lead.y < -boxSize.y) {
					if (chatting.currentType == Dialog.NodeType.Page || chatting.currentType == Dialog.NodeType.Say) {
						pageOverflow = input.Substring (i + 1, input.Length - i - 1);
						i = input.Length;
						continue;
					} else if (chatting.currentType == Dialog.NodeType.Reply) {
						displayedOptions -= Vector2.up * 1;
						optionsWithOverflow.RemoveAt (optionsWithOverflow.Count - 2);
						if (optionsWithOverflow [optionsWithOverflow.Count - 1] != nextPage) {
							optionsWithOverflow [optionsWithOverflow.Count - 1] = nextPage;
							displayedOptions -= Vector2.up;
						}
						string newList = OptionList ();
						PlayString (newList, false);
						stringPlaying = false;

						currentlySelected = Mathf.Clamp(currentlySelected, 0, optionsWithOverflow.Count - 1);
						chatting.selectedResponse = (int)displayedOptions.x + currentlySelected + (displayedOptions.x == 0 ? 0 : -1);

						Vector2 range = displayedOptions;
						bool replaced = false;
						for (int k = 0; k < pageRanges.Count; k++) {
							if (pageRanges [k].x == range.x) {
								pageRanges [k] = range;
								replaced = true;
							}
						}
						if (!replaced) {
							pageRanges.Add (displayedOptions);
						}

						yield break;
					}
				}
			} else {
				lead += x * Vector2.right;
			}
		}

		// Iterate through all characters to enable them separately
		yield return null;
		for (int i = 0; i < currentRegion.childCount; i++) {
			
			// Wait for secondsPerCharacter until process repeats for next character
			Text t = currentRegion.GetChild (i).GetComponent<Text> ();
			if (scrolling) {
				float time = 1f / charactersPerSecond;
				while (time > 0) {
                    if (chatting == null) yield break;
					t.enabled = true;
					if (Input.GetButtonDown (cont))
						scrolling = false;

					time -= Time.unscaledDeltaTime;

                    // Play the 'Chat Scroll' sound effect every charactersPerClick characters
                    if (i % charactersPerClick == 0 && playAudio) {
                        //Audio.Play(clip);
                        audioSource.Play();
                    }
					
					yield return null;
				}
			} else {
				t.enabled = true;
			}
		}

		// If there is overflow to the next page, recursively call this function, passing whatever remaining string is left into it
		if (!string.IsNullOrEmpty (pageOverflow)) {

			// Wait until the user presses the Submit button
			while (!Input.GetButtonDown (cont))
				yield return null;
			
			PlayString (pageOverflow);

		// If there is no overflow, end stringPlaying mode so the user can progress naturally to the next dialog stage
		} else {
			stringPlaying = false;
		}
	}

	// Return an array of TextStyle information for each character from 'input', using the style nodes in 'xml'
	List<TextStyle> ParseStyleNode (string input, string xml) {

        // List of all style information for each character in the input string
        List<TextStyle> styles = new List<TextStyle>();

        // List of TextStyles with an entry for each node change
		List<TextStyle> nodeStyles = new List<TextStyle> ();

		// Add the default style information to the nodeStyles list
		TextStyle defaultStyle = new TextStyle ();
		nodeStyles.Add (defaultStyle);

		// The index of the current character in the displayed dialog string
		int inputMarker = 0;

		// The index of the current character in the dialog XML string
		int xmlMarker = 0;

		// Make special characters only take up one arbitrary character to match 'input'
		xml = xml.Replace ("&gt;", ".");
		xml = xml.Replace ("&#62;", ".");
		xml = xml.Replace ("&#60;", ".");
		xml = xml.Replace ("&lt;", ".");
		xml = xml.Replace ("&#38;", ".");
		xml = xml.Replace ("&#39;", ".");
		xml = xml.Replace ("&#34;", ".");

        // Keep iterating through each node tag until xmlMarker has reached the end
        while (xmlMarker < xml.Length) {

            // Generate a string of all the characters that have not been iterated over yet
			string remaining = xml.Substring (xmlMarker, xml.Length - xmlMarker);

			// Find the beginning of the next node tag, index 0 being at xmlMarker
			int nextTag = remaining.IndexOf ('<');

			// If there are no nodes left, set nextTag to the end of the string instead
			if (nextTag < 0)
				nextTag = xml.Length - xmlMarker;

			// Apply TextStyle information to all the characters between the current xmlMarker and the next node tag
			for (int j = 0; j < nextTag && inputMarker < input.Length; j++) {
                TextStyle newChar = nodeStyles[nodeStyles.Count - 1].Clone() as TextStyle;
                styles.Add(newChar);
                styles[inputMarker].chars = input[inputMarker];
				inputMarker++;
			}

			// If there are no nodes left, immediately end the loop after adding style information to the remaining characters
			if (nextTag == (xml.Length - xmlMarker)) {
				xmlMarker = xml.Length;
				continue;
			}

			// Find the end of the next node tag
			int nextTagEnd = remaining.IndexOf ('>');

			// Determine if the next tag is a start or end tag by checking for a '/' after the beginning '<'
			bool isEndTag = remaining [nextTag + 1] == '/';

            // True if the next tag is self-closing (used for text inserts)
            bool isInsertTag = remaining.IndexOf('/') == nextTagEnd - 1;

            // If it's an end tag, signalling the close of the node, revert to the previous TextStyle setting
            if (isEndTag) {
                nodeStyles.RemoveAt(nodeStyles.Count - 1);

				// Move the xmlMarker to be after the end of this tag
				xmlMarker += nextTagEnd + 1;

            // If it's a self-closing tag, insert the text into the strings 'input' and 'xml'
            } else if (isInsertTag) {

                string node = remaining.Substring(nextTag, nextTagEnd - nextTag + 1);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(node);
                XmlNode root = doc.ChildNodes[0];
                string property = root.Name + ":" + root.Attributes[0].Value;
                string toInsert = DialogueVar.DataAccess.ToString(property);

                input = input.Substring(0, inputMarker) + toInsert + input.Substring(inputMarker, input.Length - inputMarker);
                int nt = nextTag + (xml.Length - remaining.Length);
                int nte = nextTagEnd + (xml.Length - remaining.Length);
                xml = xml.Substring(0, nt) + toInsert + xml.Substring(nte + 1, xml.Length - nte - 1);

                // Move the xmlMarker up to the beginning of the inserted text
                xmlMarker += nextTag;

			// If it's a start tag, isolate the node and its contents, and add a new TextStyle setting based on that node
			} else {

				// How many start and end tags have been located
				int startCount = 0;
				int endCount = 0;

				// The number of characters remaining after the most recently evaluated tag
				int remainingLength = 0;

				// The string of remaining characters in 'xml' after 'xmlMarker'
				int endTagMarker = xmlMarker + nextTagEnd + 1;
				string substring = xml.Substring (endTagMarker, xml.Length - endTagMarker);

                // Keep iterating over node tags until it's located more end tags than start tags
                while (startCount >= endCount) {

                    // The index of the start of the next node tag
					int openIndex = substring.IndexOf ('<');

					// Find the end of this tag
					int endIndex = substring.IndexOf('>');

					// Is the next node tag a start or end tag?
					bool isEnd = substring [openIndex + 1] == '/';

                    // Is the node self-closing? (insert node)
                    bool selfClosing = substring[endIndex - 1] == '/';

					// Incremement the appropriate counter
					if (isEnd)
						endCount++;
					else if (!selfClosing)
						startCount++;

					// Set remainingLength to the difference between the current substring length and endIndex
					remainingLength = substring.Length - endIndex;

					// Update the value of substring to contain all characters after the recently iterated tag
					substring = substring.Substring (endIndex + 1, substring.Length - endIndex - 1);
				}

				// Create a substring starting at the beginning of the start tag and ending at the end of the end tag
				string node = remaining.Substring (nextTag, remaining.Length - remainingLength - nextTag + 1);

				// Generate an XmlNode from the string 'node'
				XmlDocument doc = new XmlDocument ();
				doc.LoadXml (node);
				XmlNode root = doc.ChildNodes [0];

				// Create a new TextStyle from the node properties and the previous style settings, and add it to the top of the list
				TextStyle toAdd = new TextStyle (nodeStyles [nodeStyles.Count - 1], root);
				nodeStyles.Add (toAdd);

				// Move the xmlMarker to be after the end of this tag
				xmlMarker += nextTagEnd + 1;
			}
   		}
        if (chatting.currentType == Dialog.NodeType.Reply) {
	        int start = 0;
	        int count = 0;
	        foreach (string item in optionsWithOverflow) {
                int l = 1 + item.Length;
                if (item == optionsWithOverflow[currentlySelected])
                    l += 2;
	            for (int i = 0; i < l; i++) {
	                styles[start + i].replyIndex = count;
	            }
	            start += l;
	            count++;
            }
        }
		return styles;
	}

    public static ChatManager GetCM () {
        return FindObjectOfType<ChatManager>();
    }
}