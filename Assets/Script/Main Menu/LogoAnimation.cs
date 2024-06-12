using UnityEngine;
using DG.Tweening;

public class LogoAnimation : MonoBehaviour
{
    private Tweener tweener;
    [SerializeField] private RectTransform _logo;
    [SerializeField] private float _scaleTo;
    private void Start()
    {
        _logo.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        tweener = transform.DOScale(_scaleTo, 2f)
        .SetEase(Ease.InOutSine)
        .OnComplete(onBounce);

    }

    private void onBounce()
    {
        tweener = transform.DOScale(_scaleTo + .2f, 5)
        .SetLoops(-1, LoopType.Yoyo);
    }

    public void menuMode()
    {
        tweener.Kill();
        tweener = transform.DOScale(.6f, 2f);
        _logo.DOAnchorPos(new Vector2(600, 360), 2f)
        .OnComplete(() =>
        {
            transform.DOScale(.6f + .05f, 5f)
            .SetLoops(-1, LoopType.Yoyo);
        });

    }
}
