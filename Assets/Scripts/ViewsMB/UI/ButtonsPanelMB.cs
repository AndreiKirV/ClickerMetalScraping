using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsPanelMB : MonoBehaviour
{
    [SerializeField] private Animator _animatorButtons;

    private bool _isOpen = false;

    public void PlayChangedAnimation()
    {
        if(_isOpen)
        PlayAnimClosed();
        else
        PlayAnimOpen();
    }

    public void PlayAnimOpen()
    {
        _animatorButtons.Play("ButtonsPanelOpen");
        _isOpen = true;
    }

    public void PlayAnimClosed()
    {
        _animatorButtons.Play("ButtonsPanelClosed");
        _isOpen = false;
    }
}