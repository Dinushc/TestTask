using Cysharp.Threading.Tasks;
using DataSenders.Managers;
using DataSenders.Requests;
using Models.ServerAnswers.Breeds;
using Models.SO.NetworkSettings;
using System;
using System.Threading;
using DataSenders.Requests;
using UI.Abstractions.Controllers;
using UI.MainScenes.FactPanels.FactContainerViews;
using UI.MainScenes.FactPanels.FactDataPanels;
using UI.Misc.Pools;
using UniRx;
using Utils.AsyncExtensions;
using Zenject;

namespace UI.MainScenes.FactPanels.MainPanels
{
    public class FactsPanelController : UiController<FactsPanel>, IInitializable, IDisposable
    {
        private readonly IRequestsController _requestsController;
        private readonly IFactDataPanelController _factDataPanelController;
        private readonly INetworkSettingSo _networkSettingSo;

        private readonly CompositeDisposable _disposables = new();

        private DynamicUiList<FactListView> _factsContainerPool;
        private CancellationTokenSource _cancellationTokenSource;
        private IDisposable _disposable;
        private FactListView _currentFactLoading;

        public FactsPanelController(
            IRequestsController requestsController,
            INetworkSettingSo networkSettingSo,
            IFactDataPanelController factDataPanelController
            )
        {
            _requestsController = requestsController;
            _factDataPanelController = factDataPanelController;
            _networkSettingSo = networkSettingSo;
        }

        public void Initialize()
        {
            _factsContainerPool = new DynamicUiList<FactListView>(
                View.FactListPrefab,
                OnCreateFactContainer,
                View.Scroll.content
                );
        }

        public override void Show()
        {
            ClearAll();
            StartGetFacts();
            base.Show();
        }

        public override void Hide()
        {
            ClearAll();
            _factsContainerPool.ClearList();
            _currentFactLoading?.HideLoadingStatus();
            HideAllPanels();
            base.Hide();
        }

        private void StartGetFacts()
        {
            View.LoadingPanel.gameObject.SetActive(true);
            _cancellationTokenSource = new();
            var factsListRequest = new GetFactsRequest(_cancellationTokenSource);
            _requestsController.AddRequest(factsListRequest);
            _disposable = factsListRequest.OnRequestDone.Subscribe(OnRequestDone);
        }

        private void OnRequestDone(BreedsServerAnswer answer)
        {
            View.LoadingPanel.gameObject.SetActive(false);
            ClearAll();
            var countFact = Math.Min(_networkSettingSo.FactContainersShowCount, answer.Data.Count);
            _factsContainerPool.GetNewViews(countFact, out var views);

            for (var i = 0; i < answer.Data.Count; i++)
            {
                views[i].SetData(i, answer.Data[i]);
            }
            View.ScrollPanel.gameObject.SetActive(true);
        }

        private void OnCreateFactContainer(FactListView view)
        {
            view.OnSelect.Subscribe(OnSelectFactContainer).AddTo(_disposables);
        }

        private void OnSelectFactContainer(FactListView view)
        {
            ClearAll();
            if (_currentFactLoading != null)
                _currentFactLoading.HideLoadingStatus();

            _currentFactLoading = view;
            _currentFactLoading.ShowLoadingStatus();

            _cancellationTokenSource = new();
            var breedByIndexDataRequest = new GetBreedByIndexDataRequest(view.BreedId, _cancellationTokenSource);
            _requestsController.AddRequest(breedByIndexDataRequest);
            _disposable = breedByIndexDataRequest.OnRequestDone.Subscribe(OnBreedByIdRequestDone);
        }

        private void OnBreedByIdRequestDone(BreedByIndexServerIndexAnswer answer)
        {
            _currentFactLoading.HideLoadingStatus();
            _currentFactLoading = null;
            _factDataPanelController.ShowData(answer);
        }

        private void HideAllPanels()
        {
            View.ScrollPanel.gameObject.SetActive(false);
            View.LoadingPanel.gameObject.SetActive(false);
        }

        private void ClearAll()
        {
            if (_disposable == null)
            {
                _disposable?.Dispose();
                _disposable = null;
            }

            _cancellationTokenSource.TryCancel();
        }

        public void Dispose()
        {
            ClearAll();
        }
    }
}