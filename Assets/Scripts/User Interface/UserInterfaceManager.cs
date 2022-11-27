using Lindon.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UserInterfaceShortcuts))]
public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager instance;

    [Header("Panels, Dialogs & Elements")]
    private List<UIPage> allPages;
    private List<UIElement> allElements;

    private Stack<UIPage> openPagesStack;

    private void Awake()
    {
        Initialization();
    }

    public void Initialization()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);

            LoadDatas();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadDatas()
    {
        openPagesStack = new Stack<UIPage>();

        allElements = new List<UIElement>(GetComponentsInChildren<UIElement>(true));

        PagesInitialization();

        Open<StarterPage>();
    }

    private void Start()
    {
        foreach (var page in allPages)
        {
            // LOAD
            page.Load(LoadTiming.InStart);
        }
    }

    /// <summary>
    /// Initial setup of pages
    /// </summary>
    private void PagesInitialization()
    {
        // GET
        allPages = new List<UIPage>(GetComponentsInChildren<UIPage>(true));
        foreach (UIPage page in allPages)
        {
            // LOAD
            page.Load(LoadTiming.InAwake);

            // CLOSE
            page.SetActive(false);
        }
    }


    #region Pages's Function

    public static UIPage Open<T>() where T : UIPage
    {
        //UIPage resault = null;
        //var isPage = false;
        //foreach (var page in instance.allPages)
        //{
        //    if (page is T)
        //    {
        //        instance.openPagesStack.Push(page);
        //        page.SetActive(true);
        //        isPage = page is not UIDialog;
        //        resault = page;
        //    }
        //}
        //if (isPage)
        //{
        //    instance.TurnOffTopPage();
        //}
        //return resault;
        return Open(GetPageOfType<T>());
    }

    public static UIPage Open(UIPage newPage)
    {
        if (!(newPage is UIDialog))
        {
            instance.TurnOffTopPage();
        }

        foreach (var page in instance.allPages)
        {
            if (page == newPage)
            {
                instance.openPagesStack.Push(page);
                page.SetActive(true);
                return page;
            }
        }
        return null;
    }

    public static T GetPageOfType<T>() where T : UIPage
    {
        for (int i = 0; i < instance.allPages.Count; i++)
        {
            if (instance.allPages[i] is T t)
                return t;
        }
        return null;
    }

#if UNITY_EDITOR
    public static List<UIPage> GetAllPage()
    {
        return new List<UIPage>(FindObjectsOfType<UIPage>());
    }
#endif

    //#region Dialog's Functions

    //public static UIDialog OpenDialog<T>() where T : UIDialog
    //{
    //    foreach (var dialog in instance.allDialogs)
    //    {
    //        if (dialog is T)
    //        {
    //            dialog.gameObject.SetActive(true);
    //            dialog.SetValues();
    //            dialog.SetActiveElements(true);
    //            instance.openPagesStack.Push(dialog);
    //            return dialog;
    //        }
    //    }
    //    return null;
    //}

    //public static UIDialog GetDialogOfType<T>() where T : UIDialog
    //{
    //    for (int i = 0; i < instance.allDialogs.Count; i++)
    //    {
    //        if (instance.allDialogs[i] is T t)
    //        {
    //            return t;
    //        }
    //    }
    //    return null;
    //}

    //#endregion

    private bool CloseTopPage()
    {
        if (openPagesStack.Count > 0)
        {
            var top = openPagesStack.Pop();
            top.SetActive(false);
            return true;
        }

        return false;
    }

    //public static bool Close(UIPage oldPage)
    //{
    //    if (instance.openPagesStack.Count > 0 && instance.openPagesStack.Peek() == oldPage)
    //    {
    //        var top = instance.openPagesStack.Pop();
    //        top.gameObject.SetActive(false);
    //        top.SetActiveElements(false);
    //        Debug.Log("CLOSE: " + instance.openPagesStack.Count);
    //        return true;
    //    }

    //    return false;
    //}

    #endregion

    #region Elements's functions

    public static UIElement GetElementOfType<T>() where T : UIElement
    {
        for (int i = 0; i < instance.allElements.Count; i++)
        {
            if (instance.allElements[i] is T t)
                return t;
        }
        return null;
    }

    #endregion

    public static void OnBackPressed()
    {
        if (instance.openPagesStack.Count > 0)
        {
            if (instance.openPagesStack.Peek() == GetPageOfType<StarterPage>())
            {
                Application.Quit();
            }
            else
            {
                if (instance.CloseTopPage())
                {
                    if (instance.openPagesStack.Count < 1)
                    {
                        Open<StarterPage>();
                    }
                    else
                    {
                        Open(instance.openPagesStack.Pop());
                    }
                }
                else
                {
                    Debug.Log("ERORR!!");
                }
            }
        }
    }

    private void TurnOffTopPage()
    {
        if (openPagesStack.Count > 0)
        {
            var top = openPagesStack.Peek();
            top.SetActive(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>return active page/dialog title</returns>
    public static string GetTitle() => instance.openPagesStack.Count > 0 ? instance.openPagesStack.Peek().Title : string.Empty;

#if UNITY_EDITOR
    [EasyButtons.Button("Back Pressed", Mode = EasyButtons.ButtonMode.EnabledInPlayMode)]
    private void Back()
    {
        OnBackPressed();
    }
#endif
}
