using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float spawntime;
    private float last;
    public Rect area;
    public float safeRadious;
    public GameObject player;

    private GameObject[] startGui;
    private GameObject[] ingameGui;
    private GameObject[] finishGui;

    public enum GameState
    {
        Start,
        Mainloop,
        End
    }
    public GameState state;

    void Start()
    {
        startGui = GameObject.FindGameObjectsWithTag("Start");
        ingameGui = GameObject.FindGameObjectsWithTag("Ingame");
        finishGui = GameObject.FindGameObjectsWithTag("Finish");

        player.SetActive(false);
        foreach (var obj in ingameGui)
        {
            obj.SetActiveRecursively(false);
        }
        foreach (var obj in finishGui)
        {
            obj.SetActiveRecursively(false);
        }
    }

    // Update is called once per frame
    void Update() {
        if (state == GameState.Mainloop)
        {
            if (Time.time - last > spawntime)
            {
                Vector3 genpos;
                do
                {
                    genpos = new Vector3(Random.Range(area.xMin, area.xMax),
                                         1,
                                         Random.Range(area.yMin, area.yMax));
                } while ((player.transform.position - genpos).magnitude < safeRadious);

                Instantiate(enemyPrefab, genpos, Quaternion.identity);
                last = Time.time;
            }
        }
    }

    public void StartGame()
    {
        foreach (var obj in startGui)
        {
            obj.SetActiveRecursively(false);
        }
        foreach (var obj in ingameGui)
        {
            obj.SetActiveRecursively(true);
        }
        
        GameObject.FindObjectOfType<ScoreCount>().Reset();
        player.SetActive(true);
        state = GameState.Mainloop;
    }

    public void FinishGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        foreach (var obj in ingameGui)
        {
            obj.SetActiveRecursively(false);
        }

        foreach (var obj in finishGui)
        {
            obj.SetActiveRecursively(true);
        }
        state = GameState.End;
    }

    public void Reload()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadScene(index);
        SceneManager.LoadScene(index);
    }
}
