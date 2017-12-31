using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreGetter : MonoBehaviour {
    private string secretKey = "JabocIsCool"; // Edit this value and make sure it's the same as the one stored on the server
    public string addScoreURL = "http://jaboc.tech/NarwhalSimulator/addscore.php?"; //be sure to add a ? to your url
    public string highscoreURL = "http://jaboc.tech/NarwhalSimulator/display.php";

    void Start() {
        StartCoroutine(GetScores());
    }
    
    IEnumerator PostScores(string name, int score) {
        string hash = Jmath.Md5Sum(name + score + secretKey);

        string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
        
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null) {
            print("There was an error posting the high score: " + hs_post.error);
        }
    }

    IEnumerator GetScores() {
        GetComponent<Text>().text = "Loading Scores";
        WWW hs_get = new WWW(highscoreURL);
        yield return hs_get;

        if (hs_get.error != null) {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else {
            GetComponent<Text>().text = hs_get.text;
        }
    }
}