using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSort : MonoBehaviour
{

    bool sort;
    bool notSorting;
    Coroutine co;


    // Start is called before the first frame update
    void Start()
    {
        sort = false;
        notSorting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sort && notSorting && !ObjectsHandler.objs.sorted)
        {
            notSorting = false;
            co = StartCoroutine(Shell());
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

    IEnumerator Shell()
    {
        ObjectsHandler.objs.ToggleIndicators();
        int n = ObjectsHandler.objs.images.Length;

        // Start with a big gap,  
        // then reduce the gap 
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            // Do a gapped insertion sort for this gap size. 
            // The first gap elements a[0..gap-1] are already 
            // in gapped order keep adding one more element 
            // until the entire array is gap sorted 
            for (int i = gap; i < n; i += 1)
            {
                // add a[i] to the elements that have 
                // been gap sorted save a[i] in temp and 
                // make a hole at position i 
                float temp = ObjectsHandler.objs.images[i].transform.localScale.y;
                int tempPos = i;

                // shift earlier gap-sorted elements up until 
                // the correct location for a[i] is found 
                int j;
                for (j = i; j >= gap && ObjectsHandler.objs.images[j - gap].transform.localScale.y > temp; j -= gap)
                {
                    ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[tempPos].transform.position.x, ObjectsHandler.objs.images[j].transform.position.x);
                    ObjectsHandler.objs.images[j].transform.localScale = ObjectsHandler.objs.images[j - gap].transform.localScale;
                    yield return new WaitForSeconds(ObjectsHandler.objs.delayValue);
                }

                ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[i].transform.position.x, ObjectsHandler.objs.images[j].transform.position.x);
                // put temp (the original a[i])  
                // in its correct location 
                ObjectsHandler.objs.images[j].transform.localScale = new Vector3(ObjectsHandler.objs.images[j].transform.localScale.x, temp);
                yield return new WaitForSeconds(ObjectsHandler.objs.delayValue);
            }
        }

        ObjectsHandler.objs.sorted = true;
        notSorting = true;
        ObjectsHandler.objs.ToggleIndicators();
    }

}
