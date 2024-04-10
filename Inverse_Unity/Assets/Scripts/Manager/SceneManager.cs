using LighthouseGames.UI.Effects;

using Minimalist.Scene.Transition;

using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace Minimalist.Manager
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        [SerializeField] private GameObject _transitionContainer;
        [SerializeField] private SceneTransition[] _transitions;

        [SerializeField] private LightBeamColorChanger _lightBeamColorChanger;
        [SerializeField] private Slider _progressBar;

        [SerializeField] private float _minLoadTimeInSeconds = 3f;
        private bool _loading = false;

        public string ActiveScene { get => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; }

        private void Awake()
        {
            if (Instance == null) // Singleto
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _transitions = _transitionContainer.GetComponentsInChildren<SceneTransition>();
        }

        public void LoadScene(string sceneName, string transitionName, bool showInverseAnimation = true)
        {
            if (!_loading)
                StartCoroutine(LoadSceneAsync(sceneName, transitionName, showInverseAnimation: showInverseAnimation));
        }

        private IEnumerator LoadSceneAsync(string sceneName, string transitionName, float delay = 0.5f, bool showInverseAnimation = true)
        {
            _loading = true;
            yield return new WaitForSeconds(delay);

            var loadTime = Time.time;

            SceneTransition transition = _transitions.First(element => element.name.Equals(transitionName));

            AsyncOperation scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;

            yield return transition.AnimateTransitionIn();

            _progressBar.gameObject.SetActive(true);

            if (_lightBeamColorChanger != null && showInverseAnimation)
            {
                _lightBeamColorChanger.TriggerEffect();
            }

            do
            {
                //_progressBar.value = scene.progress;
                _progressBar.value = Mathf.SmoothStep(_progressBar.value, scene.progress, .05f);
                yield return null;

            } while ((scene.progress < .9f || _progressBar.value < 0.875f) || (Time.time < (loadTime + _minLoadTimeInSeconds) && showInverseAnimation));
            //} while (scene.progress < 0.9f);
            //} while (_progressBar.value < 0.9f);

            if (_lightBeamColorChanger != null && showInverseAnimation)
            {
                _lightBeamColorChanger.StopEffect();
            }

            scene.allowSceneActivation = true;
            yield return new WaitForSeconds(1.5f);

            _progressBar.gameObject.SetActive(false);

            yield return transition.AnimateTransitionOut();
            _loading = false;
        }
    }
}
