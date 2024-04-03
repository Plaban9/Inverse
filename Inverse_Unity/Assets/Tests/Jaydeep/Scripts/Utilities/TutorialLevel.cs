using Minimalist.DialogSystem;
using Minimalist.SaveSystem;
using Minimalist.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{
    [SerializeField] private DialogObject tutorialDialogues;

    private ExitPortal portal;
    private DialogManager dialogManager;

    private void Awake()
    {
        Debug.Log(SaveManager.ReadData(Minimalist.Inverse.Constants.SaveSystem.STAT_TUTORIAL_COMPLETED, false));
        portal = FindObjectOfType<ExitPortal>();
        dialogManager = FindAnyObjectByType<DialogManager>();
    }

    private void OnEnable()
    {
        portal.OnLevelCompleted += () =>
        {
            SaveManager.SaveData(Minimalist.Inverse.Constants.SaveSystem.STAT_TUTORIAL_COMPLETED, true);
            Debug.Log("Save Completed!");
        };
    }

    private void Start()
    {
        dialogManager.StartDialog(tutorialDialogues);
    }
}
