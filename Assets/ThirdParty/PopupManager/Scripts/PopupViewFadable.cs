using DG.Tweening;
using UnityEngine;

namespace GG.Infrastructure.Popups
{
    public class PopupViewFadable : PopupViewBase
    {
        [SerializeField]
        protected float _fadeTime = 0.5f;

        [SerializeField]
        protected CanvasGroup _canvasGroup;

        protected bool _defaultInteractable = true;

        public override void Hide()
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            _canvasGroup.DOFade(0f, _fadeTime)
                .SetUpdate(true)
                .OnComplete(
                    () =>
                    {
                        OnHiddenEvent();
                        Destroy(gameObject);
                    });
        }

        public override void Show()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0f;
            gameObject.SetActive(true);
            _canvasGroup.DOFade(1f, _fadeTime)
                .SetUpdate(true)
                .OnComplete(
                    () =>
                    {
                        _canvasGroup.interactable = _defaultInteractable;
                        OnShownEvent();
                    });
        }
    }
}