using UnityEngine;
using DG.Tweening;

public class ClickTween : MonoBehaviour
{
    public Transform ObjForShaking;
    public Ease ease;

    private Sequence _seq;
    private Vector3 _startSize;

    private void Awake()
    {
        _startSize = ObjForShaking.localScale;
    }

    public void ClickShake()
    {
        _seq = DOTween.Sequence();   
        
        if(ObjForShaking.localScale != _startSize)
        {
            _seq.Prepend(ObjForShaking.DOScale(_startSize,.1f));
        }

        _seq.Append(ObjForShaking.DOShakeScale(1f,.5f,8,50,true,ShakeRandomnessMode.Harmonic)).SetEase(ease)
            .Append(ObjForShaking.DOScale(_startSize,.1f));
    }

    private void OnDestroy()
    {
        _seq.Kill();    
    }
}
