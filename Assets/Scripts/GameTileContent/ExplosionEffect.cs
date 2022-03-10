using UnityEngine;
using DG.Tweening;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [Space(10)]
    [SerializeField] private Color _startColor = new Color(1f, 1f, 1f, 0f);
    [SerializeField] private Color _midleColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private float _animationDuration = 0.5f;


    private void Awake()
    {
        Animate();
    }


    private void Animate()
    {
        Sequence animation = DOTween.Sequence();

        _renderer.color = _startColor;
        animation
            .Append(_renderer.DOColor(_midleColor, _animationDuration / 2))
            .Append(_renderer.DOColor(_startColor, _animationDuration / 2))
            .OnComplete(() => Destroy(gameObject));
    }
}
