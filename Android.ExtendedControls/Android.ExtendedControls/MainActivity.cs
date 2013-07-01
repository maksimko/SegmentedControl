using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;

namespace Android.ExtendedControls {
	[Activity (Label = "SegmentedControl.Sample", MainLauncher = true)]
	public class MainActivity : Activity {
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var layout = new LinearLayout (this);

			var switcher = new SegmentedSwitcher (this) { LayoutParameters = new LinearLayout.LayoutParams (LinearLayout.LayoutParams.MatchParent, 100) };
			var emptyStateList = new StateListDrawable ();

			switcher.AddTab (this, 1, "1");
			switcher.AddTab (this, 2, "2");
			switcher.AddTab (this, 3, "3");
			switcher.AddTab (this, 4, "4");
			switcher.AddTab (this, 5, "5");

			switcher.Check (1);

			switcher.CheckedChange += HandleCheckedChange;

			layout.AddView (switcher);

			SetContentView (layout);
		}

		void HandleCheckedChange (object sender, RadioGroup.CheckedChangeEventArgs e)
		{
			Console.WriteLine ("Check change " + e.CheckedId);
		}
	}
}


