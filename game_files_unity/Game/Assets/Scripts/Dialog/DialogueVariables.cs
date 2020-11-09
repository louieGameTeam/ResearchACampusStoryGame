using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueVar {

    // Higher level class to determine the data type and call appropriate function
    public class DataAccess {

        public static DataSource[] allTypes = {
            new Raw(),
            new Player(),
            new NPC(),
            new Inventory(),
            new Tasks()
        };

        public static string GetResult<T> (string input) {
            
			// Separate the information type indicator and the input
			int start = input.IndexOf(':');
			if (start == input.Length - 1)
				Debug.LogError("Error: No input after information type");
			char c = input[0];
			input = input.Substring(start + 1);
            if (start < 0) {
                foreach (DataSource item in allTypes) {
                    if (item is T) {
                        c = item.type;
                    }
                }
            }
            foreach (DataSource item in allTypes) {
                if (c == item.type) {
                    return item.Get(input);
                }
            }
            Debug.LogError("Error: Unknown data type");
            return string.Empty;
        }

        public static void SetResult<T>(string property, string input) {

			// Separate the information type indicator and the input
			int start = property.IndexOf(':');
			if (start == property.Length - 1)
				Debug.LogError("Error: No input after information type");
			char c = property[0];
			property = property.Substring(start + 1);
			if (start < 0) {
				foreach (DataSource item in allTypes) {
					if (item is T) {
						c = item.type;
					}
				}
			}
			foreach (DataSource item in allTypes) {
				if (c == item.type) {
					item.Set(property, input);
				}
			}
        }

        public static string ToString (string property) {

			// Separate the information type indicator and the input
			int start = property.IndexOf(':');
			if (start == property.Length - 1)
				Debug.LogError("Error: No input after information type");
			char c = property[0];
			property = property.Substring(start + 1);
			foreach (DataSource item in allTypes) {
				if (c == item.type) {
                    return item.ToString(property);
				}
			}
            Debug.LogError("Error: Unknown data type");
            return string.Empty;
        }
    }

    // Base class for all Data Sources
    public class DataSource {

        public char type;

        public virtual string Get (string input) {
            return string.Empty;
        }

        public virtual void Set (string property, string input) {}

        public virtual string ToString(string input) {
            return Get(input);
        }

    }

    public class Raw : DataSource {

        public Raw () {
            type = 'R';
        }

        public override string Get (string input) {
            return input;
        }
    }

	public class Player : DataSource {

        public Player () {
            type = 'P';
        }

        public override string Get (string input) {
            string result = string.Empty;
            Dialog.playerProperties.TryGetValue(input, out result);
            if (result == null) result = "false";
            return result;
        }

        public override void Set (string property, string input) {
			if (!Dialog.playerProperties.ContainsKey(property))
				Dialog.playerProperties.Add(property, input);
			else
				Dialog.playerProperties[property] = input;
        }

	}

	public class NPC : DataSource {

        public NPC () {
            type = 'N';
        }

		public override string Get(string input) {
            Chattable d = ChatManager.chatting.host;
			string result = string.Empty;
            d.properties.TryGetValue(input, out result);
            if (result == null) result = "false";
            return result;
		}

		public override void Set(string property, string input) {
            Chattable d = ChatManager.chatting.host;
			if (!d.properties.ContainsKey(property))
				d.properties.Add(property, input);
			else
				d.properties[property] = input;
		}

	}

	public class Inventory : DataSource {

        public Inventory () {
            type = 'I';
        }

        public override string Get(string input) {
            return global::Inventory.Count(input).ToString();
        }

        public override void Set (string property, string index) {
            Item i = global::Inventory.itemsByName[property];
            if (global::Inventory.backpack.ContainsKey(i)) {
                global::Inventory.backpack[i] = int.Parse(index);
            } else {
                global::Inventory.Add(property, int.Parse(index));
            }
        }

	}

	public class Tasks : DataSource {

        public Tasks () {
            type = 'T';
        }

        public override string Get (string input) {
			if (input == "current") {
				return global::Tasks.currentLevel.currentTask.currentStep.GetIndex().ToString();
			} else {
				if (input.EndsWith("/"))
					input = input.Substring(0, input.Length - 1);
				int slash1 = input.IndexOf('/');
				string levelName = string.Empty;
				string taskName = string.Empty;
				int stepIndex = 0;
				if (slash1 < 0) {
					levelName = input;
				} else {
					levelName = input.Substring(0, slash1);
					int slash2 = input.LastIndexOf('/');
					if (slash2 > slash1) {
						taskName = input.Substring(slash1 + 1, slash2 - slash1 - 1);
						if (slash2 < input.Length - 1)
							stepIndex = int.Parse(input.Substring(slash2 + 1, input.Length - slash2 - 1));
					} else {
						taskName = input.Substring(slash1 + 1, input.Length - slash1 - 1);
					}
				}
				Level theLevel = global::Tasks.levels[0];
				foreach (Level l in global::Tasks.levels) {
					if (l.name == levelName || l.scene == levelName)
						theLevel = l;
				}
				Task theTask = theLevel.tasks[0];
				if (!string.IsNullOrEmpty(taskName)) {
					foreach (Task t in theLevel.tasks)
						if (t.name == taskName)
							theTask = t;
				}
				return theTask.steps[stepIndex].GetIndex().ToString();
			}
        }

        public override string ToString (string property) {
            int index = int.Parse(Get(property));
            Task latestTask = global::Tasks.levels[0].tasks[0];
            foreach (Level l in global::Tasks.levels) {
                foreach (Task t in l.tasks) {
                    if (t.steps[0].GetIndex() > index)
                        return latestTask.name;
                    else
                        latestTask = t;
                }
            }
            Debug.LogError("Error: Unknown task");
            return string.Empty;
        }

	}
}