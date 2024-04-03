using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minimalist.DialogSystem
{
    public class DialogManager : MonoBehaviour
    {


        [Header("Assets Used")]
        [SerializeField]  private TextMeshProUGUI dialogTMP;
        [SerializeField]  private TextMeshProUGUI speakerTMP;

        [Header("Debugging")]
        [SerializeField] public DialogObject dialogObject;

        private bool showing = false;

        [SerializeField] private CanvasGroup canvasGroup;

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void StartDialog(DialogObject dialogObject)
        {
            DialogMessage dialogMessage = dialogObject.messages[0];
            dialogTMP.text = " ";
            speakerTMP.text = dialogMessage.speaker;
            StartCoroutine(
                Fade(1,
                () =>
                {

                    RunDialogObject(
                        dialogObject, 
                        0,
                        () =>
                        {
                            StartCoroutine(
                                Fade(1, () => { })
                            );
                        });
                })
                );
        }

        private IEnumerator Fade(float amountOfFrames, Action callback)
        {
            if (showing)
            {
                for (float i = amountOfFrames; i >= 0; i -= Time.deltaTime)
                {
                    canvasGroup.alpha = i/amountOfFrames;
                    yield return null;
                }
            }
            else
            {
                for (float i = 0; i <= amountOfFrames; i += Time.deltaTime)
                {
                    canvasGroup.alpha = i/amountOfFrames;
                    yield return null;
                }
            }
            if (canvasGroup.alpha >= 0.5) { showing = true; } else { showing = false; }
            callback.Invoke();
        }


        private void RunDialogObject(DialogObject dialogObject, int index, Action endCallback)
        {
            if(index >= dialogObject.messages.Count)
            {

                endCallback.Invoke();
                return;
            }
            dialogTMP.text = "";
            speakerTMP.text = "";

            DialogMessage dm = dialogObject.messages[index];
            speakerTMP.text = dm.speaker;
            
            StartCoroutine(
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
                yield return new WaitForSecondsRealtime(currentMessage.timeTillNextCharacter);
            }
            yield return new WaitForSecondsRealtime(currentMessage.timeToNext);
            nextMessageCallback.Invoke();
        }
    }
}

