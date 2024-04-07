using DG.Tweening;
using Minimalist.Interfaces;
using Minimalist.Level;
using Minimalist.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInputUIController : MonoBehaviour, ILevelListener<LevelType>
{
    private const float SwapDuration = .5f;

    [Header("Movement")]
    [SerializeField] private Image moveImg;
    [SerializeField] private RectTransform moveHandleRect;

    [Header("Icon Parents")]
    [SerializeField] private Image jumpParentImg;
    [SerializeField] private Image dashParentImg;
    [SerializeField] private Image realmChangeParentImg;

    [Header("Icons")]
    [SerializeField] private Image jumpImg;
    [SerializeField] private Image dashImg;
    [SerializeField] private Image realmChangeImg;

    private void Awake()
    {
#if !UNITY_ANDROID
    GetComponentInParent<Canvas>().gameObject.SetActive(false);
#endif
    }

    private void Start()
    {
        LevelManager.Instance.RealmManager.AddListener(this);
    }

    private void Update()
    {
        moveHandleRect.anchoredPosition = new Vector2(moveHandleRect.anchoredPosition.x, 0);
    }

    public void OnNotify(LevelType enums)
    {
        switch (enums)
        {
            case LevelType.Dark:    HandleDarkRealm();  break;
            case LevelType.Light:   HandleLightRealm(); break;
        }
    }

    private void HandleDarkRealm()
    {
        moveImg.DOColor(Color.black, SwapDuration);

        jumpImg.DOColor(Color.white, SwapDuration);
        dashImg.DOColor(Color.white, SwapDuration);
        realmChangeImg.DOColor(Color.white, SwapDuration);

        jumpParentImg.DOColor(Color.black, SwapDuration);
        dashParentImg.DOColor(Color.black, SwapDuration);
        realmChangeParentImg.DOColor(Color.black, SwapDuration);
    }

    private void HandleLightRealm()
    {
        moveImg.DOColor(Color.white, SwapDuration);

        jumpImg.DOColor(Color.black, SwapDuration);
        dashImg.DOColor(Color.black, SwapDuration);
        realmChangeImg.DOColor(Color.black, SwapDuration);

        jumpParentImg.DOColor(Color.white, SwapDuration);
        dashParentImg.DOColor(Color.white, SwapDuration);
        realmChangeParentImg.DOColor(Color.white, SwapDuration);
    }
}
