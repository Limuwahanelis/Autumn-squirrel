using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] int _levelindex;
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] Vector3 _bigScale;
    [SerializeField] float _textShowSpeed;
    [SerializeField] float _textFadeSpeed;
    Vector3 _scale;
    Vector3 _orignalScale;
    private float _textAlpha;
    private float _time;
    private Coroutine _cor;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Logger.Log("enter");
        StartShowtextCor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Logger.Log("exit");
        StartHidetextCor();
    }

    // Start is called before the first frame update
    void Start()
    {
        _orignalScale = _rectTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        while (_scale.x < _bigScale.x)
        {
            _time += Time.deltaTime * _textShowSpeed;
            _time = Mathf.Clamp(_time, 0, 1);
            _scale = Vector3.Lerp(_orignalScale, _bigScale, _time);
            _rectTransform.localScale= _scale;
            yield return null;
        }
    }
    private IEnumerator HideText()
    {
        while (_scale.x > _orignalScale.x)
        {
            _time -= Time.deltaTime * _textFadeSpeed;
            _time = Mathf.Clamp(_time, 0, 1);
            _scale = Vector3.Lerp(_orignalScale,_bigScale,  _time);
            _rectTransform.localScale = _scale;
            yield return null;
        }
    }
}
