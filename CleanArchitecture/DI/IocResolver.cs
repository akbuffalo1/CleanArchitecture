using System;
namespace CleanArchitecture.DI
{
	public class IocResolver
	{
		private IocContainer _container;
		public IocResolver(IocContainer container)
		{
			_container = container;
		}

		public T Resolve<T>() {
			return (T)_container.Get<T>();
		}

		public void Register<T>(Func<object> creator) {
			_container.Add<T>(creator);
		}

		public void Register<T>(object instance)
		{
			_container.Add<T>(instance);
		}
	}
}

