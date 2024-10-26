using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour,ICollectable
{
    public int Index=>_acornIndex;
    public Action<Acorn> OnAcornCollected;
    [SerializeField] int _acornIndex;
    public void SetIndex(int index)
    {
        _acornIndex = index;
    }
    public void Collect()
    {
        OnAcornCollected?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
    }
}
