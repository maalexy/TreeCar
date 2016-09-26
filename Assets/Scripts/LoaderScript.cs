using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class LoaderScript : MonoBehaviour {

    public Slider slider;
    public Color decColor = Color.white;
    public Color emptyColor = Color.white;
    public Color loadColor = Color.white;
    public Color fullColor = Color.white;
    public float loadSpeed = 1;
    public float decSpeed = 4;
    public enum LoadState
    {
        Empty, Loading, Full, Decrease
    }
    public LoadState state;

    public UnityEvent onEmptied;
    public UnityEvent onFullLoad;

	// Use this for initialization
	void Awake () {
        if (slider == null) slider = gameObject.GetComponent<Slider>();
        Color c;
        switch (state)
        {
            case LoadState.Empty:
                c = emptyColor;
                break;
            case LoadState.Loading:
                c = loadColor;
                break;
            case LoadState.Full:
                c = fullColor;
                break;
            case LoadState.Decrease:
                c = decColor;
                break;
            default:
                c = Color.white;
                break;
        }
        foreach (var im in GetComponentsInChildren<Image>())
        {
            im.color = c;
        }
    }

    public void StartLoad()
    {
        if (state != LoadState.Full && state != LoadState.Loading)
        {
            state = LoadState.Loading;
            foreach (var im in GetComponentsInChildren<Image>())
            {
                im.color = loadColor;
            }
        }
    }	

    public void StartDecrease()
    {
        if (state != LoadState.Empty && state != LoadState.Decrease)
        {
            state = LoadState.Decrease;
            foreach (var im in GetComponentsInChildren<Image>())
            {
                im.color = decColor;
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if(state == LoadState.Loading)
        {
            slider.value += loadSpeed * Time.deltaTime;
            if (slider.value == slider.maxValue)
            {
                onFullLoad.Invoke();
                state = LoadState.Full;
                foreach (var im in GetComponentsInChildren<Image>())
                {
                    im.color = fullColor;
                }
            }
        }
        else if(state == LoadState.Decrease)
        {
            slider.value -= decSpeed * Time.deltaTime;
            if (slider.value == slider.minValue)
            {
                onEmptied.Invoke();
                Debug.Log("AFDGJAOFD");
                state = LoadState.Empty;
                foreach (var im in GetComponentsInChildren<Image>())
                {
                    im.color = emptyColor;
                }
            }
        }
	}

    void OnDestroy()
    {

    }
}
