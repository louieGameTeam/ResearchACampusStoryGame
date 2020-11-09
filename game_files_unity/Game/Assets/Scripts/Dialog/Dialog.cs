using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;


public class Dialog {

	// The type of node, based on the XML node name
	public enum NodeType {Page, Say, Reply, Finish, Other};

	// The XmlNode that the chat is currently using
	public XmlNode current = null;

	// The string for the current page
	public string page;

	// The strings for each available response
	public List<string> options;

	// The zero-based index of the currently selected chat reply
	public int selectedResponse = 0;

	// Automatically retrieve the type of the current node
	public NodeType currentType { get { return GetType (current); } }

    // Potential universal variable properties that can be used to dynamically show dialog
    public static Dictionary<string, string> playerProperties {
        get {
            if (playerPropertiesPrivate == null) {
                SaveData sd = SaveData.ReadGameSave();
                if (sd == null)
                    playerPropertiesPrivate = new Dictionary<string, string>();
                else
                    playerPropertiesPrivate = sd.playerMemory;
            }
            return playerPropertiesPrivate;
        }
        set {
            playerPropertiesPrivate = value;
        }
    }
    private static Dictionary<string, string> playerPropertiesPrivate;

    // Determines whether or not this chat session can be exited using the escape key
    [HideInInspector] public bool escapable = true;

	// The Chattable object that this dialog instance represents
	[HideInInspector] public Chattable host;




	// *** PRIVATE VARIABLES ***

	// The XML document generated from the given TextAsset
	XmlDocument doc;

	// The root XmlNode, (dialog)
	XmlNode root;

	// All nodes in the XmlDocument
	List<XmlNode> allNodes;

	// Constructor - Generate a Dialog object from the given TextAsset of XML
    public Dialog (TextAsset input) {

		// Set doc to be a new XmlDocument from the given TextAsset
		doc = new XmlDocument ();
		doc.LoadXml (input.text);

		// Store a reference to a root node
		root = doc.SelectSingleNode ("dialog");

		// Initialize the list of all available options
		options = new List<string> ();

		// Recursively iterate through every node in the XmlDocument
		allNodes = new List<XmlNode> ();
		List<XmlNode> hasKids = new List<XmlNode> ();
		hasKids.Add (root);
		while (hasKids.Count > 0) {
			List<XmlNode> newList = new List<XmlNode> ();
			foreach (XmlNode item in hasKids) {
				foreach (XmlNode child in item) {
					allNodes.Add (child);
					if (child.HasChildNodes)
						newList.Add (child);
				}
			}
			hasKids.Clear ();
			foreach (XmlNode item in newList)
				hasKids.Add (item);
		}
	}

    public Dialog (TextAsset input, Chattable _host) : this(input) {

		// Initialize the host to the given Chattable instance
		host = _host;
    }

	// Set the current node back to the beginning
	public void ToBeginning () {
		current = null;
        escapable = true;
	}

	// Determine the next appropriate node and progress to it
	public NodeType Next () {

        // All node types that don't require an immediate redirect
        string[] destinationNodeTypes = {
            "page",
            "say",
            "reply",
            "finish"
        };

		// If this dialog instance has just begun and current is not yet assigned, start it at the first node
		if (current == null) {
			current = root.ChildNodes [0];

		// If the current NodeType is a Page or Reply, move to the next node. If there are no NextSibling nodes, recursively get the NextSibling of the parent node
        } else if (current.Name == "page" || current.Name == "reply" || current.Name == "say") {

			// Specifically if the current NodeType is a Reply, set the current node to the contained <say> node of the selected option, as defined by selectedResponse, before moving to the next node
			if (current.Name == "reply")
				current = current.SelectNodes ("option") [selectedResponse].SelectSingleNode ("say");

			if (MoveToNextNode () == NodeType.Finish)
				return NodeType.Finish;
		}

        bool isOnDestinationType = false;
        foreach (string item in destinationNodeTypes)
            if (current.Name == item)
                isOnDestinationType = true;

		// Continue progressing the node until a non-redirect type is reached
        while (!isOnDestinationType) {
			
			// Randomly pick a child node to move to
			if (current.Name == "random") {
                float selection = Random.value;
				XmlNodeList allOptions = current.SelectNodes ("option");
                float defined = 0;
                int definedCount = 0;
                foreach (XmlNode node in allOptions) {
                    XmlNode w = node.Attributes.GetNamedItem("weight");
                    if (w != null) {
                        defined += float.Parse(w.Value);
                        definedCount++;
                    }
                }
                float undefined = (1f - defined) / (float)(allOptions.Count - definedCount);
                defined = 0;
                bool assigned = false;
                foreach (XmlNode node in allOptions) {
                    if (assigned) continue;
					float i = undefined;
					XmlNode w = node.Attributes.GetNamedItem("weight");
					if (w != null) i = float.Parse(w.Value);
                    defined += i;
                    if (defined > selection) {
						current = node.ChildNodes[0];
                        assigned = true;
					}
                }
                if (!assigned)
                    Debug.LogError("Error: Random option could not be selected. Check node syntax.");

			// If the current node is a goto, redirect it to its destination
			} else if (current.Name == "goto") {

				// Save the tag that this goto is looking for
				string gotoTag = current.Attributes.GetNamedItem ("tag").Value;

				// Iterate through all nodes in the document until one with the appropriate tag is found, and move there
				foreach (XmlNode item in allNodes) {
					if (item.Attributes != null) {
						XmlNode tagAttr = item.Attributes.GetNamedItem ("tag");
						if (item.Name != "goto" && tagAttr != null && tagAttr.Value == gotoTag)
							current = item;
					}
				}

			// Set the specified property to the specified value
			} else if (current.Name == "set") {

				// Store the key and values to be entered into properties
				string propertyName = current.Attributes.GetNamedItem ("var").Value;
				XmlNode valueNode = current.Attributes.GetNamedItem ("value");
				string value = string.Empty;
				if (valueNode != null) {
					value = DialogueVar.DataAccess.GetResult<DialogueVar.Raw>(valueNode.Value);
				}

				// Edit the existing entry, or add a new one if necessary
				DialogueVar.DataAccess.SetResult<DialogueVar.NPC> (propertyName, value);
					
				// Progress to the next node
				if (MoveToNextNode () == NodeType.Finish)
					return NodeType.Finish;

			// Move to the contents of the "true" child node if the given property matches the given value
			} else if (current.Name == "if") {

                // Get the operator type
                XmlNode caseNode = current.Attributes.GetNamedItem("case");
                string caseText = "E";
                if (caseNode != null)
                    caseText = caseNode.Value;

				// Store the key and values to be entered into properties
				string propertyName = current.Attributes.GetNamedItem ("var").Value;
                string result = DialogueVar.DataAccess.GetResult<DialogueVar.NPC>(propertyName);
                XmlNode valueNode = current.Attributes.GetNamedItem ("value");
				string value = string.Empty;
                if (valueNode != null/* Not sure why I put this in: && valueNode.Value.ToLower () != "true"*/)
					value = DialogueVar.DataAccess.GetResult<DialogueVar.Raw>(valueNode.Value);

                bool eitherString = false;
                float r = 0;
                bool outBool;
                if (bool.TryParse(result, out outBool))
                    r = outBool ? 1f : 0f;
                else if (!float.TryParse(result, out r))
                    eitherString = true;
                float v = 0;
                if (bool.TryParse(value, out outBool))
                    v = outBool ? 1f : 0f;
                else if (!float.TryParse(value, out v))
                    eitherString = true;

				// Determine if 'if' statement returns 'true' based on values and operator type
				bool isTrue = true;
				if (eitherString) {
					switch (caseText.ToUpper()) {
						case "NE":
							isTrue = result != value;
							break;
						case "E":
							isTrue = result == value;
							break;
                        default:
                            Debug.LogError("Error: Operator type unknown or unsupported for strings");
                            break;
					}
                } else {
					switch (caseText.ToUpper()) {
						case "GT":
							isTrue = r > v;
							break;
						case "LT":
							isTrue = r < v;
							break;
						case "GTE":
							isTrue = r >= v;
							break;
						case "LTE":
							isTrue = r <= v;
							break;
						case "NE":
							isTrue = r != v;
							break;
						case "E":
							isTrue = r == v;
							break;
                        default:
                            Debug.LogError("Error: Unknown operator type");
                            break;
					}
                }



				// Progress to the appropriate child node, or to the next one if not specified
				bool nodeMissing = false;
				if (isTrue) {
					XmlNode trueNode = current.SelectSingleNode ("true");
					if (trueNode != null)
						current = trueNode.ChildNodes [0];
					else
						nodeMissing = true;
				} else {
					XmlNode falseNode = current.SelectSingleNode ("false");
					if (falseNode != null)
						current = falseNode.ChildNodes [0];
					else
						nodeMissing = true;
				}
				if (nodeMissing) {
					if (MoveToNextNode () == NodeType.Finish)
						return NodeType.Finish;
				}
			}

            // Add or remove items from player's inventory
            else if (current.Name == "give" || current.Name == "take") {

                // Get the item name and count to add or remove
                string itemName = current.Attributes.GetNamedItem("item").Value;
                int itemCount = 1;
                XmlNode c = current.Attributes.GetNamedItem("count");
                if (c != null)
                    itemCount = int.Parse(c.Value);

                // Add or remove it
                if (current.Name == "give")
                    Inventory.Add(itemName, itemCount);
                else if (current.Name == "take")
                    Inventory.Remove(itemName, itemCount);

                // Progress to the next node
                if (MoveToNextNode () == NodeType.Finish)
                    return NodeType.Finish;

            // Advance the current task's next step
            } else if (current.Name == "advance") {

                Tasks.Advance();

                // Progress to the next node
                if (MoveToNextNode () == NodeType.Finish)
                    return NodeType.Finish;

            // Set the escapability of the chat session
            } else if (current.Name == "escape") {
                
                if (current.Attributes.Count > 0) {
                    escapable = bool.Parse(current.Attributes.GetNamedItem("on").Value);
                } else {
                    escapable = !escapable;
                }

				// Progress to the next node
				if (MoveToNextNode() == NodeType.Finish)
					return NodeType.Finish;

				// Call the method with the given name on a class inherited from Chattable object
			} else if (current.Name == "call") {

                // Parse method name and arguments
                string methodName = current.Attributes.GetNamedItem("method").Value;
                List<string> allArguments = new List<string>();
                foreach (XmlAttribute attr in current.Attributes) {
                    if (attr.Name != "method" && attr.Name != "tag")
                        allArguments.Add(attr.Value);
                }
                string[] strArray = new string[allArguments.Count];
                for (int index = 0; index < allArguments.Count; index++)
                    strArray[index] = allArguments[index];
                object[] args = { strArray };
				if (strArray.Length == 0)
					args = null;

				// Call the method on the host Chattable object
				System.Type npcType = host.GetType();
				MethodInfo theMethod = npcType.GetMethod(methodName);
                theMethod.Invoke(host, args);

				// Progress to the next node
				if (MoveToNextNode() == NodeType.Finish)
					return NodeType.Finish;
            }

			foreach (string item in destinationNodeTypes)
				if (current.Name == item)
					isOnDestinationType = true;
		}

		// Update values and return the new current node
		NodeType type = GetType (current);
		UpdateValues (type);
		return type;
	}

	// Move 'current' to the next available node destination
	NodeType MoveToNextNode () {
		XmlNode item = current;
		while (item.NextSibling == null) {

			// Ascend two levels, because any node that's a child of a non-root node will be nested in two, eg: <option> and <reply>
			item = item.ParentNode;

			// If this is the root node, which has no siblings, NodeType is Finish
			if (item == root)
				return NodeType.Finish;
			else
				item = item.ParentNode;
		}
		current = item.NextSibling;
		return GetType (current);
	}

	// Update values for page, options, and selectedResponse
	void UpdateValues (NodeType type) {
		selectedResponse = 0;
		switch (type) {
        case NodeType.Say:
		case NodeType.Page:
			page = current.InnerXml;
			break;
		case NodeType.Reply:
			XmlNodeList responses = current.SelectNodes ("option");
			options.Clear ();
			foreach (XmlNode response in responses) {
				string say = response.SelectSingleNode ("say").InnerText;
				options.Add (say);
			}
			break;
		default:
			break;
		}
	}

	// Return the NodeType based on the name of the given XmlNode
	NodeType GetType (XmlNode item) {
		switch (item.Name) {
		case "page":
			return NodeType.Page;
        case "say":
            return NodeType.Say;
		case "reply":
			return NodeType.Reply;
		case "finish":
			return NodeType.Finish;
		default:
			return NodeType.Other;
		}
	}
}