namespace UI.Abstractions.Controllers
{
    public interface IUiController
    {
        void Show();
        void Hide();
        bool IsAutoShow { get; }
    }
}
