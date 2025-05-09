using System;
using UnityEngine;


namespace NewFeatures
{
    [RequireComponent (typeof (ParticleSystem))]
    public class ShellScent : MonoBehaviour
    {
        public static event Action NewShellVisible;
        
        [HideInInspector] public ParticleSystem scentParticle;
        ShellCheck shellCheck;
        ShellEquip shellEquip;
        GameObject shellObject;

        public bool triggered;
        bool worn;

        private void Awake()
        {
            shellObject = GetComponentInParent<ShellReference>().gameObject;
            scentParticle = GetComponent<ParticleSystem>();
            shellCheck = FindAnyObjectByType<ShellCheck>();
            shellEquip = FindAnyObjectByType<ShellEquip>();

        }

        private void OnEnable()
        {
            //foodSystem.ShellFoodChanged += ToggleLooseShellScentBasedOnFood;
            shellEquip.ShellChanged += ToggleLooseShellScentBasedOnSize;
        }

        private void OnDisable()
        {
            //foodSystem.ShellFoodChanged -= ToggleLooseShellScentBasedOnFood;
            shellEquip.ShellChanged -= ToggleLooseShellScentBasedOnSize;
        }
        private void Start()
        {
            ToggleLooseShellScentBasedOnSize();
        }

        void ToggleLooseShellScentBasedOnSize()
        {
            if (shellCheck.CanShellBeEquipped(shellObject))
            {
                scentParticle.Play();
                Debug.Log(shellObject.name + " scent switched on");
                
                if (!triggered)
                {
                    triggered = true;
                    NewShellVisible?.Invoke();
                }
            }
            else
            {
                scentParticle.Stop();
                Debug.Log(shellObject.name + " scent switched off");
            }
        }


    }
}

