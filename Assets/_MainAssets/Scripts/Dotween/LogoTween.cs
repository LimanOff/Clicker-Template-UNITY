using UnityEngine;
using DG.Tweening;

public class LogoTween : MonoBehaviour
{
    public Transform Logo;

    private Tweener _tweener;
    private void Awake()
    {
        _tweener = Logo.DOShakeScale(.7f,1,5,90,true).SetLoops(-1);
    }
    
    private void OnDestroy()
    {
        _tweener.Kill();
    }
}
