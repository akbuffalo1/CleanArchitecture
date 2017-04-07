using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture
{
	public class AnimationExample
	{
		public static void Maini(string[] args) { 
			var interpolator = new FuncInterpolator(
				(iter, maxY) => {
					var maxX = Math.Sqrt(maxY);
					var step = maxX / maxY;
					return Math.Pow(step * iter, 2);
				}
			);
			var animation = new Animation(1000, 10, interpolator, (iter, val) => Console.WriteLine(String.Format("iter:{0}, val:{1}", iter, val)));

			animation.Start();
			Console.WriteLine("--------------------");
			animation.Back();
		}
	}

	public class Animation 
	{
		int _durationMillis;
		int _iterationCount;
		int currentPosition = 0;
		bool pause;
		List<int[]> _path;

		Interpolator _interpolator;
		Action<int, double> _onNext;

		public Animation(int count, int duration, Interpolator interpolator, Action<int, double> onNext) {
			_iterationCount = count;
			_durationMillis = duration;

			_interpolator = interpolator;
			_onNext = onNext;

			_path = new List<int[]>();
		}

		public void Start() {
			pause = false;
			Execute(true);
		}

		public void Stop() {
			pause = true;
		}

		public void Back() { 
			pause = false;
			Execute(false);
		}

		void Execute(bool direction) {
			if (direction)
			{
				iterateForward();
			}
			else 
			{
				iterateBack();
			}
		}

		void iterateForward() {
				var toSleep = _durationMillis / _iterationCount;
				for (int iter = currentPosition; iter <= _iterationCount; iter++)
				{
					if (!pause) {
						Thread.Sleep(toSleep);
						_onNext?.Invoke(iter, _interpolator.Interpolate(iter, _iterationCount));
						currentPosition = iter;
						if (iter == 500)
						{
							pause = true;
							break;
						}
					}
				}
		}

		void iterateBack() { 
			var toSleep = _durationMillis / _iterationCount;
			for (int iter = currentPosition; iter >= 0; iter--)
			{
				if (!pause)
				{
					Thread.Sleep(toSleep);
					_onNext?.Invoke(iter, _interpolator.Interpolate(iter, _iterationCount));
					currentPosition = iter;
				}
			}
		}

	}

	public interface Interpolator {
		double Interpolate(int iter, double max);
	}

	public class FuncInterpolator : Interpolator {
		Func<int, double, double> _interpolateFunc;
		public FuncInterpolator(Func<int, double, double> interpolateFunc) {
			_interpolateFunc = interpolateFunc;
		}

		public double Interpolate(int iter, double max)
		{
			return _interpolateFunc(iter, max);
		}
	}
}

