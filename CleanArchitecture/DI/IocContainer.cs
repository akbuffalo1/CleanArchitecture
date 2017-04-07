using System;
using System.Collections.Generic;
using CleanArchitecture.DI;

namespace CleanArchitecture.DI
{
	public class IocContainer
	{
		private Dictionary<Type, EntityHolder> hashMap = new Dictionary<Type, EntityHolder>();

		public object Get<T>() {
			if (hashMap.ContainsKey(typeof(T)))
			{
				var holder = hashMap[typeof(T)];
				return holder.Value ?? (holder.Value = holder?.Build());
			}
			return null;
		}

		public void Add<T>(Func<object> creator) {
			if (hashMap.ContainsKey(typeof(T)))
				throw new Exception("You cannot register more than one instance of Type: " + typeof(T));
			hashMap.Add(typeof(T), new EntityHolder(creator));
		}

		public void Add<T>(object instance)
		{
			if (hashMap.ContainsKey(typeof(T)))
				throw new Exception("You cannot register more than one instance of Type: " + typeof(T));
			hashMap.Add(typeof(T), new EntityHolder() { Value = instance });
		}
	}

	public class EntityHolder
	{
		private Func<object> _creator;
		public object Value { get; set; }

		public EntityHolder() { }

		public EntityHolder(Func<object> creator) {
			_creator = creator;
		}

		public object Build() {
			return _creator();
		}
	}
}

