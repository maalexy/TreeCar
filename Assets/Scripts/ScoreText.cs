using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

    public ScoreCount score;
    public Text text;
	
    void Awake()
    {
        if(text == null) text = GetComponent<Text>();
        if (score == null) score = FindObjectOfType<ScoreCount>();
    }

	// Update is called once per frame
	void Update () {
        text.text = score.ToString();
	}
}
