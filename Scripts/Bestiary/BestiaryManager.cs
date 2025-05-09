using System;
using System.Collections.Generic;
using UnityEngine;

public class BestiaryManager : MonoBehaviour
{

    public static event Action<BestiaryScriptableObject> BestiaryEntryAdded;

    static int totalToFind = 11;
    static int totalFound = 0;
    public static int GetTotalToFind => totalToFind;
    public static int GetTotalFound => totalFound;

    static List<BestiaryScriptableObject> entriesChronological = new List<BestiaryScriptableObject>();
    static List<BestiaryScriptableObject> entriesSorted = new List<BestiaryScriptableObject>();
    static public List<BestiaryScriptableObject> GetEntries => entriesSorted;

    private void Awake()
    {
        entriesChronological.Clear();
        entriesSorted.Clear();
        totalFound = 0;
    }

    static public BestiaryScriptableObject GetMostRecentEntry()
    {
        if (entriesChronological.Count > 0)
        {
            int lastInList = entriesChronological.Count;
            return entriesChronological[lastInList - 1];
        }
        return null;
    }

    static public BestiaryScriptableObject GetReferenceFromTitle(string title)
    {
        foreach (var entry in entriesSorted)
        {
            if (entry.title == title)
            {
                return entry;
            }
        }
        throw new Exception("Title not found in currently found entries");
    }

    static public int GetEntryNumber(BestiaryScriptableObject reference)
    {
        for (int i = 0; i < BestiaryManager.GetEntries.Count; i++)
        {
            if (BestiaryManager.GetEntries[i] == reference)
            {
                return i;
            }
        }
        throw new Exception("Request number not found in current entries");
        //return -1l
    } 

    static public void AddEntry(BestiaryScriptableObject reference)
    {
        if (!IsEntryFound(reference))
        {
            entriesChronological.Add(reference);
            totalFound++;

            entriesSorted.Add(reference);
            entriesSorted.Sort((a, b) => string.Compare(a.title, b.title, StringComparison.OrdinalIgnoreCase));
            
            BestiaryEntryAdded?.Invoke(reference);
            Debug.Log("Entry added: " + reference.title);
        }
        else
        {
            Debug.Log("Entry already found: " + reference.title + ". Cannot add");
        }
    }

    static bool IsEntryFound(BestiaryScriptableObject reference)
    {
        foreach (BestiaryScriptableObject foundEntry in entriesSorted)
        {
            if (reference == foundEntry) return true;
        }
        return false;
    }

    static public string GetEntriesList()
    {
        string textList = "";

        for (int i = 0; i < entriesSorted.Count; i++)
        {
            textList += entriesSorted[i].title.ToString();

            if (i != entriesSorted.Count - 1)
            {
            textList += "\n";
            }
        }
        return textList;
    }


}
