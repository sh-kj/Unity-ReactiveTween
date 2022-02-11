using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;


namespace radiants.ReactiveTween
{
	public static partial class Fader
	{
		public static async UniTask Fade(float time,
			System.Action<float> onNext, System.Action onComplete = null,
			bool useUnscaledTime = false,
			CancellationToken cancellationToken = default)
		{
			float elapsedTime = 0f;
			while (elapsedTime < time)
			{
				float scaledTime = elapsedTime / time;
				onNext(scaledTime);

				await UniTask.Yield();

				elapsedTime += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

				if(cancellationToken.IsCancellationRequested)
				{
					onComplete?.Invoke();
					return;
				}
			}
			onNext(1f);
			onComplete?.Invoke();
		}
	}
}