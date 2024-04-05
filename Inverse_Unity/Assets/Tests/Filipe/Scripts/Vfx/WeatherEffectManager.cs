using Managers.BWEffectManager;
using Minimalist.Effect;
using Minimalist.Interfaces;
using Minimalist.Level;
using Minimalist.Manager;
using UnityEngine;

public class WeatherEffectManager : MonoBehaviour, ILevelListener<LevelType>
{
    [Header("Effect Enabler")]
    [SerializeField] private bool useRain;
    [SerializeField] private bool useDust;
    [SerializeField] private bool UseStar;

    [Header("Required Objects")]
    [SerializeField] public BWEffectManager _bwManager;
    [SerializeField] private RealmManager _rmManager;


    [SerializeField]    private DuststormEffect _dsEffect;
    [SerializeField]    private Transform _starsEffect;
    [SerializeField]    private RainEffect _rainEffect;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponentInParent<Camera>();
    }

    void Start()
    {
        _rmManager.AddListener(this);

        _bwManager.AddFrontMaterial(_dsEffect.layer1Mat);
        _bwManager.AddFrontMaterial(_dsEffect.layer2Mat);
        _bwManager.AddFrontMaterial(_dsEffect.layer3Mat);
        _bwManager.AddFrontMaterial(_starsEffect.GetComponent<Renderer>().material);
        _bwManager.AddFrontMaterial(_rainEffect.material);

        SetRain(useRain);
        SetDuststorm(useDust);
        SetStars(UseStar);
    }

    public void Update()
    {
        if (_camera == null)
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
    }

    public void SetDuststorm(bool state)
    {
        _dsEffect.gameObject.SetActive(state);
    }

    public void SetStars(bool state)
    {
        _starsEffect.gameObject.SetActive(state);
    }

    public void SetRain(bool state)
    {
        _rainEffect.gameObject.SetActive(state);
    }

    private void SetRain(LevelType type)
    {
        _rainEffect.SetLightMode(type);
    }

    public void OnNotify(LevelType enums)
    {
        SetRain(enums);
    }
}
