using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorybookMemory : MemoryTextModel
{
    public GameObject panel;
    public List<GameObject> pages = new List<GameObject>();
    public float pageTurnPoint = 0.5f;
    bool isBookOpen => panel.activeSelf;

    GameObject currentPage;

    MemoryTextSelection textSelection;

    protected override void Awake()
    {
        base.Awake();

        textSelection = FindAnyObjectByType<MemoryTextSelection>();
    }

    private void Update()
    {
        if (isBookOpen)
        {
            float percentageThroughText = textSelection.GetPercentageThroughText();

            if (percentageThroughText < pageTurnPoint)
            {
                OpenPage(pages[0]);
            }
            else
            {
                OpenPage(pages[1]);
            }
        }
    }
    public override void Interact()
    {
        base.Interact();

        OpenBook(true);
    }

    public override void CloseMemory()
    {
        base.CloseMemory();

        CloseBookAndPages();
    }
    void OpenBook(bool value)
    {
        panel.SetActive(value);
    }

    bool IsPageOpen(GameObject chosenPage) => currentPage == chosenPage;

    void OpenPage(GameObject chosenPage)
    {
        if (IsPageOpen(chosenPage) == false)
        {
            foreach (var page in pages)
            {
                if (page == chosenPage)
                {
                    page.SetActive(true);
                    currentPage = page;
                }
                else
                {
                    page.SetActive(false);
                }
            }
        }
    }


    void CloseBookAndPages()
    {
        foreach (var page in pages)
        {
            if (page.activeSelf)
            {
                page.SetActive(false);
            }
        }
        currentPage = null;
        OpenBook(false);
    }

  


    //private void Start()
    //{
    //    //StartCoroutine(DelayedTestOpenCoroutine());
      
    //}

    //IEnumerator DelayedTestOpenCoroutine()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    Interact();
    //}

}