using DG.Tweening;
using Models.ServerAnswers.Breeds;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScenes.FactPanels.FactContainerViews
{
    public class FactListView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _number;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private RectTransform _progressRectTrasnform;

        [Header("Animation")]
        [SerializeField] private float _rotateAnimationTime;

        private ReactiveCommand<FactListView> _onSelect = new();
        private IDisposable _disposed;
        private Tween _tweenLoading;
        private Breed _breed;

        public IObservable<FactListView> OnSelect => _onSelect;
        public string BreedId => _breed.Id;

        private void Awake()
        {
            _disposed = _button.OnClickAsObservable().Subscribe(_ => _onSelect.Execute(this));
        }

        public void SetData(int index, Breed breed)
        {
            _breed = breed;
            _number.text = $"{index + 1}";
            _name.text = breed.Attributes.Name;
        }

        public void ShowLoadingStatus()
        {
            _progressRectTrasnform.gameObject.SetActive(true);
            _tweenLoading.Kill();
            _tweenLoading = DOTween.Sequence()
                .Append
                (
                    _progressRectTrasnform.DORotate
                    (
                        new Vector3(0, 0, -360),
                        _rotateAnimationTime,
                        RotateMode.FastBeyond360
                    )
                    .SetEase(Ease.Linear)
                )
                .SetLoops(-1);
        }

        public void HideLoadingStatus()
        {
            _tweenLoading.Kill();
            _progressRectTrasnform.gameObject.SetActive(false);
            _progressRectTrasnform.rotation = Quaternion.identity;
        }

        private void OnDestroy()
        {
            _tweenLoading.Kill();
            _disposed?.Dispose();
        }
    }
}
