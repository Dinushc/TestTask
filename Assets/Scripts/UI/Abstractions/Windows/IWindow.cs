namespace UI.Abstractions.Windows
{
    public interface IWindow
    {
        void Open();
        void Close();
        string Name { get; }
    }
}
