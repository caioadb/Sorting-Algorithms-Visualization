using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : MonoBehaviour
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
            co = StartCoroutine(Bubble());
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

    //bubble sort
    private IEnumerator Bubble()
    {
        ObjectsHandler.objs.ToggleIndicators();

        for (int i = 0; i < ObjectsHandler.objs.images.Length - 1; i++)
        {
            for (int j = 0; j < ObjectsHandler.objs.images.Length - i - 1; j++)
            {
                ObjectsHandler.objs.MoveIndicators(ObjectsHandler.objs.images[j].transform.position.x, ObjectsHandler.objs.images[j + 1].transform.position.x);
                if (ObjectsHandler.objs.delayValue > 0.15f)
                {
                    yield return new WaitForSeconds(0.25f);
                }

                if (ObjectsHandler.objs.images[j].transform.localScale.y > ObjectsHandler.objs.images[j + 1].transform.localScale.y)
                {
                    ObjectsHandler.objs.SwitchPositions(j, j + 1);
                    yield return new WaitForSeconds(ObjectsHandler.objs.delayValue);
                }
            }   
        }
        ObjectsHandler.objs.sorted = true;
        notSorting = true;
        ObjectsHandler.objs.ToggleIndicators();
        
    }
}
