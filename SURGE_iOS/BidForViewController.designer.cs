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
	[Register ("BidForViewController")]
	partial class BidForViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISegmentedControl btnAction { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnJobDetails { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblBidAmountCaption { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblBidForJobHeading { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tblReviewProviders { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtBidAmount { get; set; }

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
			if (lblBidAmountCaption != null) {
				lblBidAmountCaption.Dispose ();
				lblBidAmountCaption = null;
			}
			if (lblBidForJobHeading != null) {
				lblBidForJobHeading.Dispose ();
				lblBidForJobHeading = null;
			}
			if (tblReviewProviders != null) {
				tblReviewProviders.Dispose ();
				tblReviewProviders = null;
			}
			if (txtBidAmount != null) {
				txtBidAmount.Dispose ();
				txtBidAmount = null;
			}
		}
	}
}
