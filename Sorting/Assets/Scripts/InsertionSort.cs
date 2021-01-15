using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSort : MonoBehaviour
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
        if (ObjectsHandler.objs.restartSort && notSorting == false)
        {
            PauseSort(false);
            ObjectsHandler.objs.restartSort = false;
            PauseSort(true);
        }
        if (sort && notSorting && !ObjectsHandler.objs.sorted)
        {
            
            notSorting = false;
            co = StartCoroutine(Insertion());
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

    IEnumerator Insertion()
    {
        ObjectsHandler.objs.ToggleIndicators();
        int n = ObjectsHandler.objs.images.Length;

        for (int i = currPos; i < n - 1; i++)
        {
            // Find the minimum element in array 
            int min = i;
            for (int j = i + 1; j < n; j++)
            {
                if (ObjectsHandler.objs.images[j].transform.localScale.y < ObjectsHandler.objs.images[min].transform.localScale.y)
                    min = j;
                ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[i].transform.position.x, ObjectsHandler.objs.images[j].transform.position.x);
                if (ObjectsHandler.objs.delayValue > 0.15f)
                {
                    yield return new WaitForSeconds(0.25f);
                }
            }

            ObjectsHandler.objs.SwitchPositions(i, min);
            ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[i].transform.position.x, ObjectsHandler.objs.images[min].transform.position.x);
            yield return new WaitForSeconds(ObjectsHandler.objs.delayValue);
            currPos = i;
        }
        ObjectsHandler.objs.sorted = true;
        notSorting = true;
        currPos = 0;
        ObjectsHandler.objs.ToggleIndicators();
    }
}
