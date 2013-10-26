using System;

public class AbstractPage : FContainer
{
	public AbstractPage ()
	{
	}

	virtual public void Start()
	{	
	}
	
	virtual public void Destroy()
	{	
	}

	
	virtual public void ZoomThenStart() {
		this.scale = 0.5f;
		this.SetPosition(Futile.screen.halfWidth, Futile.screen.halfHeight);
		Go.to(this, 1.5f, new TweenConfig().setEaseType(EaseType.SineInOut).floatProp("scale", 1).onComplete(HandleZoomFinished));
	}

	virtual public void HandleZoomFinished(AbstractTween tween) {
		Start();
	}
}

