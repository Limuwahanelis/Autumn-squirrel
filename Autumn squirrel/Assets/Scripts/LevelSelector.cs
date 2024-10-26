using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] int _levelindex;
    [SerializeField] int _levelToLoadIndex;
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] TMP_Text _acornText;
    [SerializeField] Vector3 _bigScale;
    [SerializeField] float _textShowSpeed;
    [SerializeField] float _textFadeSpeed;
    Vector3 _scale;
    Vector3 _orignalScale;
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
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(_levelToLoadIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        _orignalScale = _rectTransform.localScale;
        _acornText.text = $"{GameStats.LevelDatas[_levelindex].collectedAcornsIndex.Count}/{GameStats.acornsInLevels[_levelindex]}";
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
