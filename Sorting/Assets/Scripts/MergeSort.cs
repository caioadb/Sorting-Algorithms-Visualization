using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSort : MonoBehaviour
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

    IEnumerator Merge(int l, int m, int r)
    {
        // Find sizes of two
        // subarrays to be merged
        int n1 = m - l + 1;
        int n2 = r - m;

        // Create temp arrays
        float[] L = new float[n1];
        float[] R = new float[n2];
        int i, j;

        // Copy data to temp arrays
        for (i = 0; i < n1; ++i)
            L[i] = ObjectsHandler.objs.images[l + i].transform.localScale.y;
        for (j = 0; j < n2; ++j)
            R[j] = ObjectsHandler.objs.images[m + 1 + j].transform.localScale.y;

        // Merge the temp arrays
        i = 0;
        j = 0;

        int k = l;
        while (i < n1 && j < n2)
        {

            if (L[i] <= R[j])
            {
                ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[k].transform.position.x, ObjectsHandler.objs.images[i].transform.position.x);
                if (ObjectsHandler.objs.delayValue > 0.15f)
                {
                    yield return new WaitForSeconds(0.25f);
                }
                ObjectsHandler.objs.images[k].transform.localScale = new Vector3(ObjectsHandler.objs.images[k].transform.localScale.x, L[i]);
                i++;
            }

            else
            {            
                ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[k].transform.position.x, ObjectsHandler.objs.images[j].transform.position.x);
                if (ObjectsHandler.objs.delayValue > 0.15f)
                {
                    yield return new WaitForSeconds(0.25f);
                }
                ObjectsHandler.objs.images[k].transform.localScale = new Vector3(ObjectsHandler.objs.images[k].transform.localScale.x, R[j]);
                j++;
            }
            k++;
            yield return new WaitForSeconds(ObjectsHandler.objs.delayValue);
        }

        // Copy remaining elements
        // of L[] if any
        while (i < n1)
        {
            ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[k].transform.position.x, ObjectsHandler.objs.images[i].transform.position.x);
            if (ObjectsHandler.objs.delayValue > 0.15f)
            {
                yield return new WaitForSeconds(0.25f);
            }
            ObjectsHandler.objs.images[k].transform.localScale = new Vector3(ObjectsHandler.objs.images[k].transform.localScale.x, L[i]);
            i++;
            k++;
            yield return new WaitForSeconds(ObjectsHandler.objs.delayValue);
        }

        // Copy remaining elements
        // of R[] if any
        while (j < n2)
        {
            ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[k].transform.position.x, ObjectsHandler.objs.images[j].transform.position.x);
            if (ObjectsHandler.objs.delayValue > 0.15f)
            {
                yield return new WaitForSeconds(0.25f);
            }
            ObjectsHandler.objs.images[k].transform.localScale = new Vector3(ObjectsHandler.objs.images[k].transform.localScale.x, R[j]);
            j++;
            k++;
            yield return new WaitForSeconds(ObjectsHandler.objs.delayValue);
        }
    }

    // Main function that
    // sorts arr[l..r] using
    // merge()
    IEnumerator Sort(int l, int r)
    {
        if (l < r)
        {
            // Find the middle
            // point
            int m = (l + r) / 2;

            // Sort first and
            // second halves
            yield return StartCoroutine(Sort(l, m));
            yield return StartCoroutine(Sort(m + 1, r));

            // Merge the sorted halves
            yield return StartCoroutine(Merge(l, m, r));
        }
    }

    IEnumerator StartSort()
    {
        ObjectsHandler.objs.ToggleIndicators();
        ObjectsHandler.objs.indicators[1].SetActive(false);
        yield return StartCoroutine(Sort(0, ObjectsHandler.objs.images.Length - 1));
        ObjectsHandler.objs.sorted = true;
        notSorting = true;
        ObjectsHandler.objs.ToggleIndicators();
    }
}
