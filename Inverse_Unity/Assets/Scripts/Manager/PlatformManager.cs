using System.Collections.Generic;

using UnityEngine;

namespace Minimalist.Manager
{
    public enum PlatformAction
    {
        Activate,
        Deactivate
    }

    public enum PlatformType
    {
        Mobile,
        Desktop,
        WebGL
    }

    [System.Serializable]
    public class PlatformSpecificObjects
    {
        [SerializeField] private string _name;
        [SerializeField] private PlatformAction _platformAction;
        [SerializeField] private PlatformType _platformType;
        [SerializeField] private List<GameObject> _platformObjects;

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public List<GameObject> PlatformObjects
        {
            get
            {
                return _platformObjects;
            }
        }

        public PlatformAction PlatformAction
        {
            get
            {
                return _platformAction;
            }
        }

        public PlatformType PlatformType
        {
            get
            {
                return _platformType;
            }
        }
    }

    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private PlatformType currentPlatformType = PlatformType.Desktop;
        [SerializeField] List<PlatformSpecificObjects> _platformSpecificObjects = new List<PlatformSpecificObjects>();

        PlatformType GetCurrentPlatformType()
        {
            return currentPlatformType;
        }

        private void SetCurrentPlatformType()
        {
            if (IsWebMobilePlatform())
            {
                currentPlatformType = PlatformType.Mobile;
            }
            else if (IsMobilePlatform())
            {
                currentPlatformType = PlatformType.Mobile;
            }
            else if (IsDesktopPlatform())
            {
                currentPlatformType = PlatformType.Desktop;
            }
            else if (IsWebPlatform())
            {
                currentPlatformType = PlatformType.WebGL;
            }
        }

        void Awake()
        {
            SetCurrentPlatformType();
        }

        void Start()
        {
            foreach (PlatformSpecificObjects platformSpecificObject in _platformSpecificObjects)
            {
                if (platformSpecificObject.PlatformType == currentPlatformType)
                {
                    foreach (GameObject obj in platformSpecificObject.PlatformObjects)
                    {
                        obj.SetActive(platformSpecificObject.PlatformAction == PlatformAction.Activate);
                    }
                }
            }
        }

        private bool IsMobilePlatform()
        {
            return Application.platform == RuntimePlatform.IPhonePlayer ||
                   Application.platform == RuntimePlatform.Android;
        }

        private bool IsDesktopPlatform()
        {
            return Application.platform == RuntimePlatform.WindowsPlayer ||
                   Application.platform == RuntimePlatform.OSXPlayer ||
                   Application.platform == RuntimePlatform.LinuxPlayer ||
                   Application.platform == RuntimePlatform.WindowsEditor ||
                   Application.platform == RuntimePlatform.OSXEditor;
        }

        private bool IsWebPlatform()
        {
            return Application.platform == RuntimePlatform.WebGLPlayer;
        }

        private bool IsWebMobilePlatform()
        {
            return IsWebPlatform() && (IsMobilePlatform());
        }
    }
}
