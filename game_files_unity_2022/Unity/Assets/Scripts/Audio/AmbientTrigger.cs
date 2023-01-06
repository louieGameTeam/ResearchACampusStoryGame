using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AudioSystem {

    public class AmbientTrigger : MonoBehaviour {

        // Values used when script is added manually in editor
        [SerializeField] string clipName;
        [SerializeField] float rolloff;

		public AmbientClip ac;
		
        private bool inside = false;
        private Collider2D collider;
        private static Collider2D player;

        void Start() {
            collider = GetComponent<Collider2D>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
			if (ac == null) {
				ac = new AmbientClip(clipName, collider, rolloff);
			}
        }

        void Update() {

			if (inside) {
                float volume = GetVolume();
				AudioManager.ambients.Add(ac, volume);
            }
        }

        float GetVolume() {
            float dist = collider.Distance(player).distance;
            dist = Mathf.Abs(dist);
            float volume = dist / ac.rolloff;
            volume = Mathf.Clamp01(volume);
            return volume;
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) inside = true;
        }

        void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) inside = false;
        }
    }
}
