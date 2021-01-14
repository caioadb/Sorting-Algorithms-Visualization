using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSort : MonoBehaviour
{

    bool sort;
    bool notSorting;
    Coroutine co;
    int currPos;

    // Start is called before the first frame update
    void Start()
    {
        sort = false;
        notSorting = true;
        currPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (sort && notSorting && !ObjectsHandler.objs.sorted)
        {
            notSorting = false;
            co = StartCoroutine(Selection());
        }
    }

    // Starts or stops sorting depending on trigger value
    public void PauseSort(bool value)
    {
        sort = value;
        if (value == false)
        {
            StopCoroutine(co);
            notSorting = true;
            ObjectsHandler.objs.ToggleIndicators();
        }
    }

    IEnumerator Selection()
    {
        ObjectsHandler.objs.ToggleIndicators();
        int  min;
        int n = ObjectsHandler.objs.images.Length;

        for (int i = currPos; i < n - 1; i++)
        {
            // Find the minimum element in unsorted array  
            min = i;
            for (int j = i + 1; j < n; j++)
            {
                ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[min].transform.position.x, ObjectsHandler.objs.images[j].transform.position.x);
                if (ObjectsHandler.objs.delayValue > 0.15f)
                {
                    yield return new WaitForSeconds(0.25f);
                }

                if (ObjectsHandler.objs.images[j].transform.localScale.y < ObjectsHandler.objs.images[min].transform.localScale.y)
                {
                    min = j;
                }
                   
            }

            ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[min].transform.position.x, ObjectsHandler.objs.images[i].transform.position.x);
            // Swap the found minimum element with the first element  
            ObjectsHandler.objs.SwitchPositions(min, i);
            yield return new WaitForSeconds(ObjectsHandler.objs.delayValue);
            currPos = i;
        }
        ObjectsHandler.objs.sorted = true;
        notSorting = true;
        currPos = 0;
        ObjectsHandler.objs.ToggleIndicators();
    }
}
