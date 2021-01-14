using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapSort : MonoBehaviour
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
            StartCoroutine(Heap());
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

    //heap sort
    IEnumerator Heap()
    {
        ObjectsHandler.objs.ToggleIndicators();
        int n = ObjectsHandler.objs.images.Length;

        // Build heap (rearrange array)
        for (int i = n / 2 - 1; i >= 0; i--)
            yield return StartCoroutine(Heapify(n, i));

        // One by one extract an element from heap
        for (int i = n - 1; i > 0; i--)
        {
            
            // Move current root to end
            yield return StartCoroutine(Swap(0, i));

            // call max heapify on the reduced heap
            yield return StartCoroutine(Heapify(i, 0));

        }
        ObjectsHandler.objs.sorted = true;
        notSorting = true;
        ObjectsHandler.objs.ToggleIndicators();

    }

    // To heapify a subtree rooted with node i which is
    // an index in arr[]. n is size of heap
    IEnumerator Heapify(int n, int i)
    {
        
        int largest = i; // Initialize largest as root
        int l = 2 * i + 1; // left = 2*i + 1
        int r = 2 * i + 2; // right = 2*i + 2

       
        // If left child is larger than root
        if (l < n)
        {
            ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[largest].transform.position.x, ObjectsHandler.objs.images[l].transform.position.x);
            if (ObjectsHandler.objs.delayValue > 0.15f)
            {
                yield return new WaitForSeconds(0.25f);
            }
            
            if ( ObjectsHandler.objs.images[l].transform.localScale.y > ObjectsHandler.objs.images[largest].transform.localScale.y)
            {
                largest = l;
            }
        }

        
        // If right child is larger than largest so far
        if (r < n)
        {
            ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[largest].transform.position.x, ObjectsHandler.objs.images[r].transform.position.x);
            if (ObjectsHandler.objs.delayValue > 0.15f)
            {
                yield return new WaitForSeconds(0.25f);
            }
            if (ObjectsHandler.objs.images[r].transform.localScale.y > ObjectsHandler.objs.images[largest].transform.localScale.y)
            {
                largest = r;
            }      
        }

        // If largest is not root
        if (largest != i)
        {

            yield return StartCoroutine(Swap(i, largest));

            // Recursively heapify the affected sub-tree
            yield return StartCoroutine(Heapify(n, largest));
        }
    }

    IEnumerator Swap(int a, int b)
    {
        ObjectsHandler.objs.SwitchPositions(a, b);
        ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[a].transform.position.x, ObjectsHandler.objs.images[b].transform.position.x);
        yield return new WaitForSeconds(ObjectsHandler.objs.delayValue);
    }

}
