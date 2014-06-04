namespace AllureCSharpCommons.Events
{
	public interface IEvent<T>
	{
		void process(T context);
	}
}