using System;

namespace ClippySharp
{
    public class AgentView 
		:
		//UIImageView
		// System.Net.Mime.MediaTypeNames.Image		// static
		// Windows.UI.Xaml.Controls.Image			// sealed
		Windows.UI.Xaml.FrameworkElement,
		IDisposable
	{
		public event EventHandler TapPressed;
		public event EventHandler TapUnPressed;

		public Agent Agent { get; private set; }

		public void ConfigureAgent (Agent agent)
		{
			if (this.Agent != null) {
				this.Agent.NeedsRender -= Agent_NeedsRender;
				this.Agent.Dispose ();
			}

			this.Agent = agent;
			this.Agent.NeedsRender += Agent_NeedsRender;
			this.Agent.RefreshImage ();
		}

		public AgentView ()
		{
			Initialize ();
		}

		//public AgentView (NSCoder coder) : base (coder)
		//{
		//}

		//protected AgentView (NSObjectFlag t) : base (t)
		//{
		//}

		protected internal AgentView (IntPtr handle) : base () //(handle)
		{
		}

		//public AgentView(CGRect frame) : base(frame)
		//{
		//	Initialize();
		//}

		//public override void TouchesBegan (NSSet touches, UIEvent evt)
		//{
		//	TapPressed?.Invoke (this, EventArgs.Empty);
		//}

		//public override void TouchesEnded (NSSet touches, UIEvent evt)
		//{
		//	TapUnPressed?.Invoke (this, EventArgs.Empty);
		//}

		void Initialize()
		{
			//UserInteractionEnabled = true;
		}


		void Agent_NeedsRender(object sender, EventArgs e)
        {
			// Essentials
            //InvokeOnMainThread(() =>
            //{
            //    Image = Agent.GetCurrentImage()?.NativeObject as UIImage;
            //});
        }

        public void Dispose(/*bool disposing*/)
        {
			if (Agent != null) {
				Agent.NeedsRender -= Agent_NeedsRender;
			}

			//base.Dispose(disposing);

			return;
        }
    }
}
