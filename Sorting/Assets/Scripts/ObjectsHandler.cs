using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ObjectsHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject[] images;

    public static ObjectsHandler objs;
    [SerializeField]
    TextMeshProUGUI delayText = null;

    [SerializeField]
    public GameObject[] indicators;

    bool fewUnique;
    bool nearlySorted;
    bool reversed;
    public bool sorted;
    public float delayValue;
    // Start is called before the first frame update
    void Start()
    {
        objs = this;
        fewUnique = false;
        nearlySorted = false;
        reversed = false;
        delayValue = 1f;
        indicators[0].SetActive(false);
        indicators[1].SetActive(false);
        Randomize();
    }

    public void UpdateDelay(System.Single n)
    {
        delayValue = (float)n;
        string value = string.Format("{0:0.##}", delayValue);
        delayText.text = "Delay Time: " + value + "s";

    }

    public void Unique(bool value)
    {
        fewUnique = value;
    }
    public void NearlySort(bool value)
    {
        nearlySorted = value;
    }
    public void Reverse(bool value)
    {
        reversed = value;
    }

    // switches position of two elements
    public void SwitchPositions(int a, int b)
    {
        Vector3 temp = new Vector3(images[a].transform.localScale.x, images[a].transform.localScale.y);
        images[a].transform.localScale = images[b].transform.localScale;
        images[b].transform.localScale = temp;

    }

    public void MoveIndicators(float a, float b)
    {
        indicators[0].transform.position = new Vector3(a - 5, indicators[0].transform.position.y);
        indicators[1].transform.position = new Vector3(b - 5, indicators[1].transform.position.y);

    }

    public void ToggleIndicators()
    {
        if (sorted)
        {
            indicators[0].SetActive(false);
            indicators[1].SetActive(false);
        }
        else
        {
            indicators[0].SetActive(true);
            indicators[1].SetActive(true);
        }

    }
    // randomizes size of images
    public void Randomize()
    {
        sorted = false;

        if (fewUnique)
        {
            int max = UnityEngine.Random.Range(4, 11);
            float[] values = new float[max];
            for (int i = 0; i < max; i++)
            {
                int value = UnityEngine.Random.Range(10, 590);
                values[i] = (float)value/10;
            }
            for (int i = 0; i < images.Length; i++)
            {
                images[i].transform.localScale = new Vector3(images[i].transform.localScale.x, (float)values[UnityEngine.Random.Range(0,max)]);
            }
        }
        else if (nearlySorted)
        {
            float randomScale = UnityEngine.Random.Range(1f,4f);
            for (int i = 0; i < images.Length; i++)
            {
                images[i].transform.localScale = new Vector3(images[i].transform.localScale.x, ((float)(i + 5) * 2 + 10) / 10f * randomScale);
            }
            int max = UnityEngine.Random.Range(3, 11);
            for (int i = 0; i < max; i++)
            {
                images[UnityEngine.Random.Range(0,images.Length)].transform.localScale = new Vector3(images[i].transform.localScale.x, (float)UnityEngine.Random.Range(10, 590) /10);
            }
        }
        else if (reversed)
        {
            float randomScale = UnityEngine.Random.Range(1f, 4f);
            for (int i = 0; i < images.Length; i++)
            {
                images[images.Length - i - 1].transform.localScale = new Vector3(images[images.Length - i - 1].transform.localScale.x, ((float)(i + 5) * 2 + 10) / 10f * randomScale);
            }
        }
        else
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].transform.localScale = new Vector3(images[i].transform.localScale.x, (float)UnityEngine.Random.Range(10, 590) /10);
            }
        }
    }

}
