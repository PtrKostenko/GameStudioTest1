namespace GameStudioTest1
{
    public interface IMemorizable
    {
        string ID { get; }
        Memento MakeMemento();
        void SetFromMemento(Memento memento);
    }
}
