using System;
using System.Collections;
using DG.Tweening;
using Minimalist.Player;
using Minimilist.Player.PlayerActions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Minimalist.DialogSystem
{
    public class DialogManager : MonoBehaviour
    {
        [Header("Assets Used")]
        [SerializeField] private TextMeshProUGUI dialogTMP;
        [SerializeField] private TextMeshProUGUI speakerTMP;
        [SerializeField] private TextMeshProUGUI skipInstructionsTMP;

        [Header("Debugging")]
        [SerializeField] public DialogObject dialogObject;

        private bool showing = false;

        [SerializeField] private CanvasGroup canvasGroup;
        public UnityEvent OnDialogueCompleted;

        private PlayerControls inputActions;
        private Coroutine dialoguesRoutine;

        private void Awake()
        {
            inputActions = new PlayerControls();
        }

        private void OnEnable()
        {
            inputActions.Dialogue.Enable();

            inputActions.Dialogue.SkipInstructions.performed += ctx =>
            {
                showing = true;
                Fade(() => 
                {
                    if (dialoguesRoutine != null)
                        StopCoroutine(dialoguesRoutine);
                    
                    OnDialogueCompleted?.Invoke();
                });
            };
        }

        public void SetSkipInstructionText(bool isGamepad)
        {
            const string currentInstructions = "Hold {X} to skip instructions";
            const string ButtonNamePlaceHolder = "{X}";
            int bindingIndex = isGamepad ? 1 : 0;
            skipInstructionsTMP.text = currentInstructions.Replace(ButtonNamePlaceHolder, inputActions.Dialogue.SkipInstructions.bindings[bindingIndex].ToDisplayString());
            Debug.Log(skipInstructionsTMP.text);
        }

        private void OnDisable()
        {
            inputActions.Dialogue.Disable();
            inputActions.Dispose();
        }

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void StartDialog(DialogObject dialogObject)
        {
            DialogMessage dialogMessage = dialogObject.messages[0];
            dialogTMP.text = "";
            speakerTMP.text = dialogMessage.speaker;

            Fade(() => RunDialogObject(dialogObject, 0, () => Fade(() => { OnDialogueCompleted?.Invoke(); })));
        }

        private void Fade(Action callback, float duration = 1)
        {
            canvasGroup.DOFade(showing ? 0 : 1, duration).OnComplete(() => { showing = canvasGroup.alpha >= 0.5f; callback?.Invoke(); });
        }

        private void RunDialogObject(DialogObject dialogObject, int index, Action endCallback)
        {
            if (index >= dialogObject.messages.Count)
            {

                endCallback.Invoke();
                return;
            }
            dialogTMP.text = "";
            speakerTMP.text = "";

            DialogMessage dm = dialogObject.messages[index];
            speakerTMP.text = dm.speaker;

            if (dialoguesRoutine != null)
            {
                StopCoroutine(dialoguesRoutine);
            }

            dialoguesRoutine = StartCoroutine(
                ShowDialog(dm,
                () =>
                {
                    RunDialogObject(
                        dialogObject,
                        index + 1,
                        endCallback);
                }
                ));
        }

        IEnumerator ShowDialog(DialogMessage currentMessage, Action nextMessageCallback)
        {
            string currentText = "";
            foreach(char a in currentMessage.text)
            {
                currentText += a;
                dialogTMP.text = currentText;
                yield return new WaitForSeconds(currentMessage.timeTillNextCharacter);
            }
            yield return new WaitForSeconds(currentMessage.timeToNext);
            nextMessageCallback.Invoke();
        }
    }
}

