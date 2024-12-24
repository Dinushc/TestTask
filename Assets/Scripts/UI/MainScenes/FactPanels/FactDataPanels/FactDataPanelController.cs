using Models.ServerAnswers.Breeds;
using System;
using UI.Abstractions.Controllers;
using UI.MainScenes.FactPanels.FactDataPanels;
using UniRx;
using Zenject;

namespace MainScenes.FactPanels.FactDataPanels
{
    public class FactDataPanelController : UiController<FactDetailsView>, IFactDataPanelController, IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposables = new();

        public void Initialize()
        {
            View.CloseButton.OnClickAsObservable().Subscribe(_ => Hide()).AddTo(_disposables);
            View.DimmedButton.OnClickAsObservable().Subscribe(_ => Hide()).AddTo(_disposables);
        }

        public void ShowData(BreedByIndexServerIndexAnswer answer)
        {
            View.MainLabel.text = answer.Data.Attributes.Name;
            View.Description.text = answer.Data.Attributes.Description;
            Show();
            View.SetDefaultScrollPosition();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
