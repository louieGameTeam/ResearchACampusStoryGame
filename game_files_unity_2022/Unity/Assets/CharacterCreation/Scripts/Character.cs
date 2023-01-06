using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Linq;

namespace CharacterCreation
{

    [RequireComponent(typeof(Animator))]
    [ExecuteInEditMode]
    public class Character : MonoBehaviour
    {

        public bool isPlayer = false;

        public CharacterSetting cs;

        //void OnValidate()
        //{
        //    if (!isPlayer)
        //    {
        //        foreach (var location in cs.locations)
        //            foreach (var layer in location.version.layers)
        //                foreach (var direction in layer.directions)
        //                    foreach (var frame in direction.frames)
        //                    {
        //                        if (frame != null && frame.sprite != null)
        //                        {
        //                            var f = Wardrobe.Singleton.frames.FirstOrDefault(x => x.sprite == frame.sprite);
        //                            if (f != null)
        //                            {
        //                                frame.id = f.id;
        //                            }
        //                            else
        //                            {
        //                                frame.id = Guid.NewGuid().ToString();
        //                                Wardrobe.Singleton.frames.Add(new Wardrobe.FrameData(frame.id, frame.sprite));
        //                            }
        //                        }
        //                    }

        //        UnityEditor.EditorUtility.SetDirty(this);
        //        UnityEditor.EditorUtility.SetDirty(Wardrobe.Singleton);
        //    }
        //}

        public void Awake()
        {
            InitializeCharacter();
        }

        void InitializeCharacter()
        {
            SaveData sd = SaveData.ReadGameSave();
            bool saveExists = sd != null && sd.character != null;

            if (isPlayer && !saveExists)
                cs = null;

            if (isPlayer && saveExists)
            {
                cs = sd.character;
            }
            else if (cs == null || cs.locations.Length == 0)
            {
                cs = new CharacterSetting(transform);
            }
            cs.host = transform;
            cs.Init();
            cs.SetIdle();
        }

        public void Next()
        {
            cs.Next();
        }

        public void SetIdle()
        {
            cs.SetIdle();
        }


        // This function is used to populate the AnimationEvent at the end of the cycle
        // so it isn't automatically trimmed to the end of the prior AnimationEvent
        public void Placeholder() { }
    }

    [Serializable]
    public class CharacterSetting
    {

        [NonSerialized] public Transform host;

        public string name;
        public FeatureSetting[] locations;

        public CharacterSetting(Transform _host)
        {
            host = _host;

            Wardrobe w = Wardrobe.Singleton;

            locations = new FeatureSetting[6];
            for (int i = 0; i < locations.Length; i++)
            {
                locations[i] = new FeatureSetting(w.locations[i]);
            }
            Init();
        }

        public void Init()
        {
            for (int i = 0; i < locations.Length; i++)
                locations[i].Init(host);
        }

        public void Next()
        {

            int dir = host.GetComponent<Animator>().GetInteger(Controller.directionHash);
            foreach (FeatureSetting item in locations)
                item.Next(dir);
        }

        public void SetIdle()
        {

            int dir = host.GetComponent<Animator>().GetInteger(Controller.directionHash);
            foreach (FeatureSetting item in locations)
                item.SetIdle(dir);
        }
    }

    [Serializable]
    public class FeatureSetting
    {

        public Version version;
        public Color tint;
        [NonSerialized] public int frame = 0;
        [NonSerialized] SpriteRenderer[] layers;
        public string locationName;


        public FeatureSetting(Location loc)
        {
            int versionIndex = UnityEngine.Random.Range(0, loc.versions.Count);
            version = loc.versions[versionIndex];
            tint = loc.locationPalette.colors[UnityEngine.Random.Range(0, loc.locationPalette.colors.Count)];
            locationName = loc.name;
        }

        public void Init(Transform host)
        {

            layers = new SpriteRenderer[version.layers.Count];
            string holderName = "Character Sprites";
            Transform spriteHolder = host.Find(holderName);
            if (spriteHolder == null)
            {
                spriteHolder = (new GameObject()).transform;
                spriteHolder.name = holderName;
                spriteHolder.SetParent(host);
                spriteHolder.localPosition = Vector3.zero;
            }
            Transform newLayer = spriteHolder.Find(locationName);
            if (newLayer == null)
            {
                newLayer = (new GameObject()).transform;
                newLayer.name = locationName;
                newLayer.SetParent(spriteHolder);
                newLayer.localPosition = Vector3.zero;
            }
            if (version.layers.Count == 1)
            {
                SpriteRenderer sr = newLayer.GetComponent<SpriteRenderer>();
                if (sr == null)
                    sr = newLayer.gameObject.AddComponent<SpriteRenderer>();
                layers[0] = sr;
            }
            else
            {
                for (int i = 0; i < version.layers.Count; i++)
                {
                    string layerName = "Layer " + i.ToString();
                    Transform subLayer = newLayer.transform.Find(layerName);
                    if (subLayer == null)
                    {
                        subLayer = (new GameObject()).transform;
                        subLayer.name = layerName;
                        subLayer.SetParent(newLayer.transform);
                        subLayer.localPosition = Vector3.zero;
                    }
                    SpriteRenderer sr = subLayer.GetComponent<SpriteRenderer>();
                    if (sr == null)
                        sr = subLayer.gameObject.AddComponent<SpriteRenderer>();
                    layers[i] = sr;
                }
            }
        }

        public void Next(int dir)
        {

            int cycle = version.frames;
            SetFrames(dir);
            frame++;
            if (frame >= cycle)
                frame = 0;
        }

        public void SetIdle(int dir)
        {
            frame = 0;
            SetFrames(dir);
        }

        private void SetFrames(int d)
        {
            for (int i = 0; i < version.layers.Count; i++)
            {
                Layer l = version.layers[i];
                Frame f = l.directions[d].frames[frame];
                bool flip = f.flip;
                Sprite sprite = f.sprite;
                if (d == 1 && l.leftMirrorsRight)
                {
                    sprite = l.directions[3].frames[frame].sprite;
                    flip = !flip;
                }
                else if (d == 2 && l.upMatchesDown)
                {
                    Frame up = l.directions[0].frames[frame];
                    sprite = up.sprite;
                    flip = up.flip;
                }
                layers[i].sprite = sprite;
                layers[i].flipX = flip;
                layers[i].color = l.applyTint ? tint : Color.white;
                layers[i].sortingOrder = l.layer;
                layers[i].sortingLayerName = "People";
            }
        }
    }
}