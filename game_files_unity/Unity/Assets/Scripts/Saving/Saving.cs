using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using Proyecto26;
using UnityEngine.Events;

public static class Saving {

    public static SaveData cached;
    private static bool saving = false;
    public static GameLog log;
    public static float lastTime = 0;
    public static void Save(SaveData obj) {
        if (!MainMenu.isOffline) {
            if (saving) return;
            saving = true;

            int offsetHours = Firebase.instance.IsPacificDaylightTime() ? -7 : -8;
            log = UpdateGamelog(log);
            Firebase.instance.SaveData(obj, () => {
                log = UpdateGamelog(log);
                for (int i = 0; i < Tasks.levels.Count; i++) {
                    float newProgress = Tasks.levels[i].progress;
                    if (newProgress > log.log[i].progress) {
                        var lastProgressPacific = Firebase.instance.currentTime.AddHours(offsetHours);
                        log.log[i].lastProgress = lastProgressPacific.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.GetCultureInfo("en-US"));
                    }
                    log.log[i].progress = newProgress;
                }

                Firebase.instance.SaveProgress(log, () => {
                    cached = obj;
                });

            });
            saving = false; // Needed in case SaveData fails (i.e. a momentary network drop). Otherwise will never attempt to save again.
        }
        else {
            Serialization.Serialize(obj);
        }
    }

    private static GameLog UpdateGamelog(GameLog log) {
        if (log == null || log.log.Count == 0) {
            log = new GameLog();
            foreach (Level item in Tasks.levels) {
                log.log.Add(new LevelLog(item.name, 0, 0, string.Empty));
            }
        }
        float toAdd = Time.realtimeSinceStartup - lastTime;
        lastTime = Time.realtimeSinceStartup;
        int index = Tasks.levels.IndexOf(Tasks.currentLevel);
        if (index < log.log.Count && index >= 0) {
            log.log[index].seconds += toAdd;
        }
        return log;
    }


    public static object GetSave() {
        object result = null;
        if (!MainMenu.isOffline) {
            result = cached;
        }
        else {
            result = Serialization.Deserialize();
        }

        return result;
    }

}

[System.Serializable]
public class LevelLog {

    public string level = string.Empty;
    public float seconds = 0;
    public float progress = 0;
    public string lastProgress = string.Empty;

    public LevelLog(string lvlName, float elapsed, float percent, string date) {
        level = lvlName;
        seconds = elapsed;
        progress = percent;
        lastProgress = date;
    }
}

[System.Serializable]
public class GameLog {

    public List<LevelLog> log;

    public GameLog() {
        log = new List<LevelLog>();
    }
}

// Wrapper for the player counter, which keeps a cumulative total of
// the number of students who have signed up to play the game
[System.Serializable]
public class PlayerCounter {
    public int counter;
    public PlayerCounter(int players) {
        counter = players;
    }

    public static PlayerCounter operator+ (PlayerCounter left, PlayerCounter right) {
        PlayerCounter summed = new PlayerCounter(left.counter);
        summed.counter += right.counter;
        return summed;
    }
    
    public static PlayerCounter operator+ (PlayerCounter left, int right) {
        return left + new PlayerCounter(right);
    }
}
