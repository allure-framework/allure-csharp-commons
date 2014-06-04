namespace AllureCSharpCommons
{
	public interface IEvent<T>
	{
		void process(T context);
	}
}