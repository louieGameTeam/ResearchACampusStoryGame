using UnityEngine;

[CreateAssetMenu(fileName = "FirebaseSettings", menuName = "Firebase/Settings")]
public class FirebaseSettings : ScriptableObject 
{
    [SerializeField]
    string _apiKey = "AIzaSyBnnuST5vGjDMWPMQNnwPI0JpfIkWvd_hk";
    [SerializeField]
    string _database = "https://university-game-default-rtdb.firebaseio.com/";

    public string apiKey => _apiKey;
    public string database => _database;
}