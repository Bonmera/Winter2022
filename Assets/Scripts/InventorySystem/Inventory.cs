using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public int space = 10;
    public List<item> items = new List<item>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private void Awake()
    {
        if (instance != null) {
            return;
        }
        instance = this;
    }

    public bool Add(item item)
    {
        if (items.Count >= space)
        {
            return false;
        }

        items.Add(item);
        if(onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public bool CheckIfRopeExists()
    {
        if(items.Count > 0)
        {
            foreach(item i in items)
            {
                if (i.name.Equals("Rope"))
                {
                    return true;
                }
            }
        }
        
        return false;
        
    }

    //Function to update value in conversation
    public void UpdateHasRope()
    {
        bool value = CheckIfRopeExists();
        ConversationManager.Instance.SetBool("hasRope", value);
    }

}
