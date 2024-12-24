using Models.ServerAnswers.Breeds;

namespace UI.MainScenes.FactPanels.FactDataPanels
{
    public interface IFactDataPanelController
    {
        public void ShowData(BreedByIndexServerIndexAnswer breed);
    }
}
