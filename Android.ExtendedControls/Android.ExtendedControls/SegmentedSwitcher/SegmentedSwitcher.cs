using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Runtime;
using Android;
using Android.Graphics.Drawables;
using System.Drawing;
using Android.Views;

using Res = Android.ExtendedControls.Resource;

namespace Android.ExtendedControls {
	public class SegmentedSwitcher : RadioGroup {
		static StateListDrawable emptyStateList = new StateListDrawable ();

		int leftResourceId;
		int middleResourceId;
		int rightResourceId;
		int solidResourceId;

		public int LeftButtonRes { 
			get { return leftResourceId; } 
			set { leftResourceId = value; UpdateButtonsImages (); }
		}

		public int MiddleButtonRes { 
			get { return middleResourceId; } 
			set { middleResourceId = value; UpdateButtonsImages (); }
		}

		public int RightButtonRes { 
			get { return rightResourceId; } 
			set { rightResourceId = value; UpdateButtonsImages (); }
		}

		public int SolidButtonRes { 
			get { return solidResourceId; } 
			set { solidResourceId = value; UpdateButtonsImages (); }
		}

		public SegmentedSwitcher (Context context) : base (context)
		{
			Init ();
		}

		public SegmentedSwitcher (Context context, IAttributeSet attrs) : base (context, attrs)
		{
			Init ();
		}

		void Init () 
		{
			Orientation = Orientation.Horizontal;

			leftResourceId = Res.Drawable.SegmentRadioLeft;
			middleResourceId = Res.Drawable.SegmentRadioMiddle;
			rightResourceId = Res.Drawable.SegmentRadioRight;

			solidResourceId = Res.Drawable.SegmentButton;
		}

		public void SetButtonsResources (int solidResId = Res.Drawable.SegmentButton, int leftResId = Res.Drawable.SegmentRadioLeft, int middleResId = Res.Drawable.SegmentRadioMiddle, int rightResId = Res.Drawable.SegmentRadioRight)
		{
			leftResourceId = leftResId;
			middleResourceId = middleResId;
			rightResourceId = rightResId;

			solidResourceId = middleResId;

			UpdateButtonsImages ();
		}

		public RadioButton AddTab (Context context, int id, string text = "", int index = -1)
		{
			var tab = GetTab (context, id, text);

			if (index != -1)
				AddView (tab, index);
			else
				AddView (tab);

			return tab;
		}

		public static RadioButton GetTab (Context context, int id, string text = "")
		{
			var tab = new RadioButton (context) { Id = id, Text = text, LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent) };
			tab.SetButtonDrawable (emptyStateList);

			return tab;
		}

		protected override void OnSizeChanged (int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged (w, h, oldw, oldh);

			Console.WriteLine ("OnSizeChanged");

			UpdateButtonsImages ();
		}

		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure (widthMeasureSpec, heightMeasureSpec);

			Console.WriteLine ("w: {0} h: {1}", MeasuredWidth, MeasuredHeight);

			var tabWidth = MeasuredWidth / ChildCount;

			for (var i = 0; i < ChildCount; i++) {
				var tab = GetChildAt (i);
				tab.Measure (MeasureSpec.MakeMeasureSpec(tabWidth, MeasureSpecMode.Exactly), heightMeasureSpec);
			}
		}

		void UpdateButtonsImages () 
		{
			if (ChildCount == 0)
				return;

			var tab = GetChildAt (0);
			
			if (ChildCount == 1) {
				tab.SetBackgroundResource (SolidButtonRes);

				return;
			}
			
			tab.SetBackgroundResource (LeftButtonRes);
			
			for (var i = 1; i < ChildCount - 1; i++) {
				tab = GetChildAt (i);
				tab.SetBackgroundResource (MiddleButtonRes);
			}

			tab = GetChildAt (ChildCount - 1);
			tab.SetBackgroundResource (RightButtonRes);
		}
	}
}

	