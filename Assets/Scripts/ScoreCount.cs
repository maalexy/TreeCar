using UnityEngine;
using System.Collections;

public class ScoreCount : MonoBehaviour {

    public int score;

    public void Reset()
    {
        score = 0;
    }

    public void AddScore()
    {
        ++score;
    }

    public override string ToString()
    {
        return "Score: " + score.ToString();
    }
}
