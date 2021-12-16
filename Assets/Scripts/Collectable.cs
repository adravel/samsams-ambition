using UnityEngine;

public class Collectable : Interactable
{
    public string collectableName;
    public bool isCollected = false;
    private GameObject me;

    private void Start()
    {
        me = gameObject;
    }
    
    public override void Interact()
    {
        if (!isCollected)
        {
            me.SetActive(false);
            isCollected = true;

            Debug.Log("Collected " + collectableName + ".");
        }
    }
}
