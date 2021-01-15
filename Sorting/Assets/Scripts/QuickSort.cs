using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : MonoBehaviour
{

    bool sort;
    bool notSorting;

    // Start is called before the first frame update
    void Start()
    {
        sort = false;
        notSorting = true;
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
            StartCoroutine(StartSort());
        }
    }

    // Starts or stops sorting depending on trigger value
    public void PauseSort(bool value)
    {
        sort = value;
        if (value == false)
        {
            StopAllCoroutines();
            notSorting = true;
            ObjectsHandler.objs.ToggleIndicators();
        }
    }

    IEnumerator StartSort()
    {
        ObjectsHandler.objs.ToggleIndicators();
        yield return Quick(0, ObjectsHandler.objs.images.Length - 1);
        ObjectsHandler.objs.sorted = true;
        notSorting = true;
        ObjectsHandler.objs.ToggleIndicators();
    }

    /* low --> Starting index, 
    high --> Ending index */
    IEnumerator Quick(int low, int high)
    {
        if (low < high)
        {

            CoroutineWithData cd = new CoroutineWithData(this, Partition(low, high));
            /* pi is partitioning index, arr[pi] is  
            now at right place */
            yield return cd.coroutine;
            int pi = (int)cd.result;

            // Recursively sort elements before 
            // partition and after partition 
            yield return StartCoroutine(Quick(low, pi - 1));
            yield return StartCoroutine(Quick(pi + 1, high));
        }
    }

    IEnumerator Partition(int low, int high)
    {
        float pivot = ObjectsHandler.objs.images[high].transform.localScale.y;

        // index of smaller element 
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            if (i > 0)
            {
                ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[j].transform.position.x, ObjectsHandler.objs.images[i].transform.position.x);
                if (ObjectsHandler.objs.delayValue > 0.15f)
                {
                    yield return new WaitForSeconds(0.25f);
                }
            }

            // If current element is smaller  
            // than the pivot 
            if (ObjectsHandler.objs.images[j].transform.localScale.y < pivot)
            {
                i++;

                // swap arr[i] and arr[j] 
                ObjectsHandler.objs.SwitchPositions(i, j);
                yield return new WaitForSeconds(ObjectsHandler.objs.delayValue);
            }
        }
        ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[i+1].transform.position.x, ObjectsHandler.objs.images[high].transform.position.x);
        // swap arr[i+1] and arr[high] (or pivot) 
        ObjectsHandler.objs.SwitchPositions(i+1, high);
        yield return new WaitForSeconds(ObjectsHandler.objs.delayValue);

        yield return i + 1;
    }
}

// Utility class for invoking coroutines
// Allows access to coroutine return value
class CoroutineWithData
{
    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator target;
    public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
    {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (target.MoveNext())
        {
            result = target.Current;
            yield return result;
        }
    }
}