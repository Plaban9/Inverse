using DG.Tweening;
using Minimalist.Manager;
using Minimilist.Player.PlayerActions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoryTeller : MonoBehaviour
{
    [SerializeField] private RectTransform loreText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI skipText;
    [SerializeField, Range(0f, 1f)] private float dialogueWriteTime = 0.05f;
    [SerializeField] private List<StorySO> story;
    [SerializeField] private AudioSource source;

    private bool isSkipHidden;
    private PlayerControls inputs;

    private void Awake()
    {
        inputs = new PlayerControls();
        inputs.Story.Enable();
    }

    private void OnDestroy()
    {
        inputs.Story.Disable();
    }

    private void OnEnable()
    {
        inputs.Story.Skip.performed += HandleSkip;
        inputs.Story.ShowSkip.performed += HandleShowSkip;
    }

    private void OnDisable()
    {
        inputs.Story.Skip.performed -= HandleSkip;
        inputs.Story.ShowSkip.performed -= HandleShowSkip;
    }

    private void Start()
    {
        StartStory();
    }

    private void HandleSkip(InputAction.CallbackContext ctx)
    {
        Debug.Log("Story Completed!");
        source.Pause();
        SceneManager.Instance.LoadScene("Level1", "CircleWipe");
    }

    private void HandleShowSkip(InputAction.CallbackContext ctx)
    {
        skipText.DOFade(1, 1).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            isSkipHidden = false;
            skipText.DOFade(0, 1f).SetDelay(2f).SetEase(Ease.OutQuint).OnComplete(() => isSkipHidden = true);
        });
    }

    public void StartStory()
    {
        StartCoroutine(ShowDialogues());
    }

    private IEnumerator ShowDialogues()
    {
        loreText.DOAnchorPosY(0, 1f).SetEase(Ease.OutBack);
        skipText.DOFade(0, 1f).SetDelay(1f).SetEase(Ease.OutQuint).OnComplete(() => isSkipHidden = true);
        yield return new WaitForSeconds(3f);

        source.Play();

        for (int i = 0; i < story.Count; i++)
        {
            var storySo = story[i];
            yield return StartCoroutine(WriteDialogue(storySo));
        }

        Debug.Log("Story Completed!");
        SceneManager.Instance.LoadScene("Level1", "CircleWipe");
    }

    private IEnumerator WriteDialogue(StorySO storySO)
    {
        var textsize = storySO.textSize;
        var dialogue = storySO.dialogue.ToCharArray();

        dialogueText.text = string.Empty;
        dialogueText.fontSize = textsize;

        foreach (var character in dialogue)
        {
            yield return new WaitForSeconds(dialogueWriteTime * storySO.writeTimeMultiplier);
            dialogueText.text += character;
        }
        yield return new WaitForSeconds(storySO.waitTime); // Can be changed to press any key to continue

    }
}
