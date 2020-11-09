using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// Style information for a character, contains all applied effects
public class TextStyle : ICloneable {

	// All available effects
	TextEffect[] allEffects;

    public char chars;

	// A list of effects that are currently active in this TextStyle
	public List<TextEffect> appliedEffects;

    // Index of the reply the character is part of - only used for response text
    public int replyIndex = 0;

	// Generate a style object with default text styling settings
	public TextStyle () {

		// To add a new text effect, add it to this list
		allEffects = new TextEffect[] {
			new TextColor()
		};
		appliedEffects = new List<TextEffect>();
	}

	// Generate a style object inheriting the settings of the previous
	public TextStyle (TextStyle former, XmlNode input) : this() {

		// Inherit all applied effects from the given TextStyle object
		foreach (TextEffect e in former.appliedEffects) {
			appliedEffects.Add (e);
		}

		// Iterate through all possible effects until finding one that matches the node name
		foreach (TextEffect e in allEffects) {
			if (input.Name == e.nodeName) {

				// If this type of effect is already present, remove the existing instance
				List<TextEffect> toRemove = new List<TextEffect>();
				foreach (TextEffect ef in appliedEffects) {
					if (ef.nodeName == e.nodeName)
						toRemove.Add (ef);
				}
				foreach (TextEffect ef in toRemove)
					appliedEffects.Remove (ef);

				// Update the new effect with the given node information and add it to appliedEffects
				e.SetProperties (input);
				appliedEffects.Add (e);
			}
		}
	}

	public void ApplyEffects (Text input) {
		
		// Apply each applied effect individually
		foreach (TextEffect e in appliedEffects) {
			e.ApplyEffect (input);
		}
	}

	public object Clone() {
        return this.MemberwiseClone();
	}
}

// Base class from which all new effects should inherit
public class TextEffect {

	// The name of the XML node defining this effect
	public string nodeName;

	public TextEffect () {}

	// Store the effect settings as derived from the given XmlNode
	public virtual void SetProperties (XmlNode input) {}

	// Apply this effect to the given Text object
	public virtual void ApplyEffect (Text input) {}
}

// Change the text to a given color
public class TextColor : TextEffect {

	public Color color = Color.white;

	public TextColor () {
		nodeName = "color";
	}

	public override void SetProperties (XmlNode input) {
		string hex = input.Attributes.GetNamedItem ("hex").Value;
		color = HEX2RGB (hex);
	}

	public override void ApplyEffect (Text input) {
		input.color = color;
	}

	static Color HEX2RGB (string hex) {

		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 255);
	}

	public static string RGB2HEX (Color input) {
		string hex = string.Format("{0:X2}{1:X2}{2:X2}", 
			(int)(input.r * 255), 
			(int)(input.g * 255), 
			(int)(input.b * 255));
		return hex;
	}
}