using System.Collections;
using UnityEngine;

public class SFXShellChange : OneShotTrack
{
    ShellEquip shellEquip;

    bool active = false;

  

    protected override void Awake()
    {
        base.Awake();
        shellEquip = FindAnyObjectByType<ShellEquip>();
    }
    private void OnEnable()
    {
        shellEquip.ShellChanged += OnShellChanged;
    }
    private void OnDisable()
    {
        shellEquip.ShellChanged -= OnShellChanged;
    }

    private void Start()
    {
        StartCoroutine(OpeningDelayCoroutine());
    }

    void OnShellChanged()
    {
        if (active)
        {
            PlayTrack();
        }
    }

    IEnumerator OpeningDelayCoroutine()
    {
        yield return new WaitForSeconds(1f);
        active = true;
    }


}
