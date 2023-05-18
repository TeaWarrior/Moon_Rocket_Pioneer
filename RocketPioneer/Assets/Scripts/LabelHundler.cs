using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items.Container;

public class LabelHundler : MonoBehaviour
{

    [SerializeField] GameObject maxIndicator;
    [SerializeField] ItemsContainer container;
    private int capacity;
    private float containerSize;
    // Start is call
    // ed before the first frame update
    void Start()
    {

        container.OnFullInventory += Container_OnFullInventory;



    }

    void SetIndicatorPosition()
    {
      containerSize =   container._containerSize;
      capacity = container.Capacity;
        maxIndicator.transform.localPosition = new Vector3(0f, capacity * containerSize, 0f);
        Debug.Log("asdasdasd");
    }
    private void Container_OnFullInventory(object sender, bool value)
    {
        Indicator(value);
        SetIndicatorPosition();
    }

    private void Indicator(bool value)
    {
        maxIndicator.SetActive(value);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
