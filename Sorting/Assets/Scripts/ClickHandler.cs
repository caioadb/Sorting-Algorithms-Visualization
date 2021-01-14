using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    GameObject Height = null;
    [SerializeField]
    TextMeshProUGUI HeightText = null;

    void Start()
    {
        Height.SetActive(false);
    }

    //Detect current clicks on the GameObject (the one with the script attached)
    public void OnPointerDown(PointerEventData pointerEventData)
    {

        //Output the name of the GameObject that is being clicked
        Debug.Log(name + "Game Object Click in Progress");
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log(name + "No longer being clicked");
        if (!Height.activeSelf)
        {
            StartCoroutine(EnableHeight());
        }
        
    }

    IEnumerator EnableHeight()
    {

        Height.transform.position = new Vector2(this.transform.position.x - 7, 0);
        Height.GetComponent<RectTransform>().localPosition = new Vector2(Height.GetComponent<RectTransform>().localPosition.x, this.transform.localPosition.y + (this.GetComponent<RectTransform>().localScale.y * 10) + 10);
        Debug.Log(this.transform.position + "  " + this.GetComponent<RectTransform>().localPosition.y + "  " + this.GetComponent<RectTransform>().localScale.y + " height " + Height.transform.position);

        string value = string.Format("{0:0.##}", this.transform.localScale.y);
        HeightText.text = value;
        Height.SetActive(true);

        yield return new WaitForSeconds(1f);
        Height.SetActive(false);
    }
    
}
