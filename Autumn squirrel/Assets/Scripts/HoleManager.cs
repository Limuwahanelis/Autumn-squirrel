using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    [SerializeField] TMP_Text _holeText;
    [SerializeField] float _textShowSpeed;
    [SerializeField] float _textFadeSpeed;
    [SerializeField] AcornsManager _acornsManager;
    [SerializeField] List<GameObject> _acornIcons=new List<GameObject>();
    private float _textAlpha;
    private float _time;
    private Coroutine _cor;
    private bool _isPlayerNear = false;
    private void Start()
    {
        
    }
    public void StoreAcorns()
    {
        if(_isPlayerNear)
        {
            _acornsManager.StoreAcornsInHole();
            for(int i=0; i< _acornsManager.StoredAcorns;i++)
            {
                _acornIcons[i].SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled) return;
        StartShowtextCor();
        _isPlayerNear = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!enabled) return;
        StartHidetextCor();
        _isPlayerNear = false;
    }
    private void StartShowtextCor()
    {
        if (_cor != null)
        {
            StopCoroutine(_cor);
        }
        _cor = StartCoroutine(ShowText());
    }
    private void StartHidetextCor()
    {
        if (_cor != null)
        {
            StopCoroutine(_cor);
        }
        _cor = StartCoroutine(HideText());
    }
    private IEnumerator ShowText()
    {
        while (_textAlpha < 1)
        {
            _time += Time.deltaTime * _textShowSpeed;
            _time = Mathf.Clamp(_time, 0, 1);
            _textAlpha = Mathf.Lerp(0, 1, _time);
            _holeText.alpha = _textAlpha;
            yield return null;
        }
    }
    private IEnumerator HideText()
    {
        while (_textAlpha > 0)
        {
            _time -= Time.deltaTime * _textFadeSpeed;
            _time = Mathf.Clamp(_time, 0, 1);
            _textAlpha = Mathf.Lerp(0, 1, _time);
            _holeText.alpha = _textAlpha;
            yield return null;
        }
    }
    private void OnEnable()
    {
        StartShowtextCor();
        _isPlayerNear = true;
    }
}
