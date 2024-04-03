# Unity ReactiveTween

*THIS LIBRARY was DEPRECATED, see https://github.com/sh-kj/ReactiveTween*

Fast, less-memory-alloc Tween system using UniRx and Microcoroutine, or UniTask.

## Requirement

- Unity 2017 or higher(can be compatible with older version)
- UniRx https://github.com/neuecc/UniRx
- UniTask https://github.com/Cysharp/UniTask

## Usage[UniRx]

have to `using UniRx;`

```
Fader.Fade(0.5f)
	.Subscribe(value => DoSomething(value));
```
After Subscribe, `Fader.Fade` dispatches `value` every frame until 0.5(argument value) second.  
`value` interpolates 0 to 1 while argument second, so you can do anything using normalized time.

```
var fade = Fader.Fade(2.0f)
	.Subscribe(value => DoSomething(value), () => DoComplete());

fade.Dispose();
```

You can cancel stream to `Dispose()`.  
`onCompleted` action will be called when it canceled too.

## Usage[UniTask]

have to `using Cysharp.Threading.Tasks;`

```
await Fader.Fade(0.5f, value => DoSomething(value));
```
`Fader.Fade` returns awaitable UniTask object.  

```
System.Threading.CancellationTokenSource source = new System.Threading.CancellationTokenSource();
var task = Fader.Fade(0.5f, value => DoSomething(value), () => DoComplete(), false, source.Token);
source.Cancel();
```
You can cancel task by CancellationToken.  
`onCompleted` action will be called when it canceled too.

## Easing

`Easing` contains some easing equations to use, like this;

```
Fader.Fade(0.5f)
	.Subscribe(value => transform.position = Easing.QuadIn.Ease(
		new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), value));
```

## License

MIT