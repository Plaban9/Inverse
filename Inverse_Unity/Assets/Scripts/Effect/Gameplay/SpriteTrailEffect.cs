using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteTrailEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer trailPrefab;
    [SerializeField] private float trailTime = .5f;
    [SerializeField] private float spawnInterval = .1f;

    private bool isEmitting;
    private float spawnTime;
    private SpriteRenderer spriteRenderer;
    private Queue<SpriteRenderer> trailPool;

    private void Awake()
    {
        trailPool = new Queue<SpriteRenderer>(20);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isEmitting)
            return;

        spawnTime += Time.deltaTime;

        if (spawnTime >= spawnInterval)
        {
            spawnTime = 0;
            StartTrail();
        }
    }

    public void StartEmitting() => isEmitting = true;

    public void StopEmitting() => isEmitting = false;

    public void StartTrail()
    {
        var SR = GetTrail();
        SR.sprite = spriteRenderer.sprite;
        SR.flipX = spriteRenderer.flipX;
        SR.color = spriteRenderer.color;
        SR.transform.SetPositionAndRotation(transform.position, transform.rotation);
        SR.DOFade(0, trailTime).OnComplete(() =>
        {
            ResetTrail(SR);
        });
    }

    private SpriteRenderer GetTrail()
    {
        if (trailPool.Count == 0)
            FillPool();

        var trail = trailPool.Dequeue();
        trail.gameObject.SetActive(true);
        return trail;
    }

    private void FillPool()
    {
        for (int i = 0; i < 20; i++)
        {
            var GO = Instantiate(trailPrefab.gameObject);
            GO.SetActive(false);
            trailPool.Enqueue(GO.GetComponent<SpriteRenderer>());
        }
    }

    private void ResetTrail(SpriteRenderer sr)
    {
        sr.gameObject.SetActive(false);
        trailPool.Enqueue(sr);
    }
}
