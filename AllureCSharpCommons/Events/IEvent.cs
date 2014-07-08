namespace AllureCSharpCommons.Events
{
    public interface IEvent<in T>
    {
        void Process(T context);
    }
}