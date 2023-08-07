using UnityEngine;
using DG.Tweening;

public class ClickTween : MonoBehaviour
{
    private Sequence _seq;
    public Transform ObjForShaking;
    public Ease ease;

    public void ClickShake()
    {
        _seq = DOTween.Sequence();   
        
        if(ObjForShaking.localScale != Vector3.one)
        {
            _seq.Prepend(ObjForShaking.DOScale(Vector3.one,.1f));
        }

        _seq.Append(ObjForShaking.DOShakeScale(1f,.5f,8,50,true,ShakeRandomnessMode.Harmonic)).SetEase(ease)
            .Append(ObjForShaking.DOScale(Vector3.one,.1f));
    }

    private void OnDestroy()
    {
        _seq.Kill();    
    }
}
