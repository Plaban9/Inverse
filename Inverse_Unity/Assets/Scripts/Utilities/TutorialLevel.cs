using Minimalist.DialogSystem;
using Minimalist.Player;
using Minimalist.SaveSystem;
using Minimalist.Utilities;
using Minimilist.Player.PlayerActions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{
    [SerializeField] private DialogObject keyboardTutorialDialogues;
    [SerializeField] private DialogObject gamepadTutorialDialogues;

    [SerializeField] private MyPlayerInput inputs;
    [SerializeField] private ExitPortal portal;
    [SerializeField] private DialogManager dialogManager;

    private void Awake()
    {
        Debug.Log(SaveManager.ReadData(Minimalist.Inverse.Constants.SaveSystem.STAT_TUTORIAL_COMPLETED, false));
    }

    private void OnEnable()
    {
        portal.OnLevelCompleted += () => SaveManager.SaveData(Minimalist.Inverse.Constants.SaveSystem.STAT_TUTORIAL_COMPLETED, true);

        inputs.OnDie += () => dialogManager.gameObject.SetActive(false);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        bool isGamepad = inputs.GetCurrentControlScheme() == "Gamepad";
        dialogManager.SetSkipInstructionText(isGamepad);
        dialogManager.StartDialog(isGamepad ? gamepadTutorialDialogues : keyboardTutorialDialogues);
    }
}
