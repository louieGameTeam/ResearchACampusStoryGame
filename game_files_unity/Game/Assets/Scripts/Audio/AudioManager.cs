using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioSystem;



namespace AudioSystem {

    public class AudioManager : MonoBehaviour {

        public List<SoundClip> sounds;
		public static Dictionary<AmbientClip, float> ambients = new Dictionary<AmbientClip, float>();
        private static List<AmbientClip> ambientsPlaying = new List<AmbientClip>();
		private IEnumerator musicTransition;
        public MusicClip currentMusic;

        void Awake() {
            sounds = new List<SoundClip>();
        }

        void Update() {

            // TODO: Test both of these
            UpdateSounds();
            UpdateAmbients();
        }

        void UpdateAmbients () {
            
            Dictionary<AmbientClip, float> ambientsFiltered = new Dictionary<AmbientClip, float>();
            foreach (KeyValuePair<AmbientClip, float> item in ambients) {

                bool overridden = false;
                List<string> overrides;
                foreach (KeyValuePair<AmbientClip, float> item2 in ambients) {
                    overrides = AudioDB.GetClip(item2.Key).overrides;
                    if (overrides.Contains(item.Key.name))
                        overridden = true;
                }
                if (overridden) continue;

				if (ambientsFiltered.ContainsKey(item.Key)) {
					float existing = ambientsFiltered[item.Key];
					ambientsFiltered[item.Key] = Mathf.Max(item.Value, existing);
				} else {
					ambientsFiltered.Add(item.Key, item.Value);
				}
            }
            ambients.Clear();

			// Remove ambient clips that have become out of range
			List<AmbientClip> toRemove = new List<AmbientClip>();
            foreach (AmbientClip item in ambientsPlaying) {
                if (!ambientsFiltered.ContainsKey(item))
                    toRemove.Add(item);
            }
            foreach (AmbientClip item in toRemove) {
				Transform source = transform.Find(item.name);
				if (source != null)
					Destroy(source.gameObject);
				ambientsPlaying.Remove(item);
            }

            foreach (KeyValuePair<AmbientClip, float> item in ambientsFiltered) {
                
                // Get or create AudioSource reference
                AudioSource ads;
                if (!ambientsPlaying.Contains(item.Key)) {
                    ambientsPlaying.Add(item.Key);
                    ads = NewAudioSource(item.Key);
                    ads.time = Random.Range(0, ads.clip.length);
                    ads.loop = true;
                    ads.Play();
                } else {
                    Transform source = transform.Find(item.Key.name);
                    ads = source.GetComponent<AudioSource>();
                }

                // Update the volume;
                ads.volume = item.Value * item.Key.volume;
            }
        }

        void UpdateSounds() {

            List<SoundClip> soundsFiltered = new List<SoundClip>();
            for (int i = 0; i < sounds.Count; i++) {
                
                bool repeated = false;
                for (int j = i + 1; j < sounds.Count; j++)
                    if (sounds[j] == sounds[i])
                        repeated = true;
                if (repeated) continue;

                bool overridden = false;
                List<string> overrides;
                for (int j = 0; j < sounds.Count; j++) {
                    overrides = AudioDB.GetClip(sounds[j]).overrides;
                    if (overrides.Contains(sounds[i].name))
                        overridden = true;
                }
                if (overridden) continue;
                
                soundsFiltered.Add(sounds[i]);
            }
            sounds.Clear();

            foreach (SoundClip clip in soundsFiltered) {
                AudioClip target = AudioDB.GetClip(clip).GetClip();
                AudioSource ads = NewAudioSource(clip);
                ads.volume = clip.volume;
                Object.Destroy(ads.gameObject, target.length + clip.delay);
                ads.PlayDelayed(clip.delay);
            }
        }

        public void ChangeMusic (MusicClip input) {
            if (input == currentMusic) return;
            currentMusic = input;
            if (musicTransition != null)
                StopCoroutine(musicTransition);
            musicTransition = FadeTracks(input);
            StartCoroutine(musicTransition);
        }

        IEnumerator FadeTracks (MusicClip input) {

            AudioSource ads = GetComponent<AudioSource>();

            // Fade out;
            if (!ads.isPlaying) ads.volume = 0;
            while (ads.volume > 0) {
                ads.volume -= Time.deltaTime * Audio.fadeSpeed;
                if (ads.volume < 0) ads.volume = 0;
                yield return null;
            }

            if (input == null) {
                ads.Pause();
                yield break;
            }

            // Transition and fade in
            ads.pitch = input.pitch;
            ads.clip = AudioDB.GetClip(input).GetClip();
            ads.time = input.startRandom ? Random.Range(0, ads.clip.length) : 0;
            ads.Play();
            while (ads.volume < input.volume) {
                ads.volume += Time.deltaTime * Audio.fadeSpeed;
                if (ads.volume > input.volume) ads.volume = input.volume;
                yield return null;
            }
        }

		public AudioSource NewAudioSource(ClipSettings clip) {
			GameObject player = new GameObject();
			AudioSource ads = player.AddComponent<AudioSource>();
			Clip ac = AudioDB.GetClip(clip);
			ads.clip = ac.GetClip();
			player.name = ac.name;
			ads.rolloffMode = AudioRolloffMode.Linear;
			player.transform.localPosition = Vector3.zero;
			ads.spatialBlend = 0;
			ads.pitch = clip.pitch;
			ads.playOnAwake = false;
			ads.transform.SetParent(transform);
			return ads;
		}
    }
}