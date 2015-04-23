// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SURGE_iOS
{
	[Register ("AwardJobViewController")]
	partial class AwardJobViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISegmentedControl btnAction { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnJobDetails { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblAwardJobHeading { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblBid { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblBidCaption { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblProviderName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblProviderNameCaption { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblRating { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblRatingCaption { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnAction != null) {
				btnAction.Dispose ();
				btnAction = null;
			}
			if (btnJobDetails != null) {
				btnJobDetails.Dispose ();
				btnJobDetails = null;
			}
			if (lblAwardJobHeading != null) {
				lblAwardJobHeading.Dispose ();
				lblAwardJobHeading = null;
			}
			if (lblBid != null) {
				lblBid.Dispose ();
				lblBid = null;
			}
			if (lblBidCaption != null) {
				lblBidCaption.Dispose ();
				lblBidCaption = null;
			}
			if (lblProviderName != null) {
				lblProviderName.Dispose ();
				lblProviderName = null;
			}
			if (lblProviderNameCaption != null) {
				lblProviderNameCaption.Dispose ();
				lblProviderNameCaption = null;
			}
			if (lblRating != null) {
				lblRating.Dispose ();
				lblRating = null;
			}
			if (lblRatingCaption != null) {
				lblRatingCaption.Dispose ();
				lblRatingCaption = null;
			}
		}
	}
}
