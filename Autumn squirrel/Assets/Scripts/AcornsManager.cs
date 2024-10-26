using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AcornsManager : MonoBehaviour
{
    public int StoredAcorns => _storedAcornsNum;
    [SerializeField] int _levelIndex;
    [SerializeField] List<Acorn> _acorns=new List<Acorn>();
    [SerializeField] TMP_Text _acronCollectText;
    [SerializeField] TMP_Text _acronStoredText;
    List<Acorn> _collectedAcorns = new List<Acorn>();
    private int _storedAcornsNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<_acorns.Count;i++) 
        {
            _acorns[i].SetIndex(i);
            _acorns[i].OnAcornCollected += CollectAcorn;
        }
        _acronCollectText.text = $"{_collectedAcorns.Count}/{GameStats.acornsInLevels[_levelIndex]}";
    }
    private void CollectAcorn(Acorn acorn)
    {
        _collectedAcorns.Add(acorn);
        _acronCollectText.text = $"{_collectedAcorns.Count}/{GameStats.acornsInLevels[_levelIndex]}";
        
    }
    public void StoreAcornsInHole()
    {
        for(int i=0;i<_collectedAcorns.Count;i++)
        {
            GameStats.CollectAcorn(_levelIndex, _collectedAcorns[i].Index);
        }
        _storedAcornsNum += _collectedAcorns.Count;
        _collectedAcorns.Clear();
        _acronStoredText.text = $"{_storedAcornsNum}/{GameStats.acornsInLevels[_levelIndex]}";
        _acronCollectText.text = $"{_collectedAcorns.Count}/{GameStats.acornsInLevels[_levelIndex]}";
    }
}
