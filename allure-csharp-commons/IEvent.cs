using System;

namespace allurecsharpcommons
{
	public interface IEvent<T>
	{
		void process(T context);
	}
}