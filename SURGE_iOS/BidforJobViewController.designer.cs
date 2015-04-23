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
	[Register ("BidforJobViewController")]
	partial class BidforJobViewController
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
		UILabel lblOtherCaption { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblTagJobHeading { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tblTaggedProviders { get; set; }

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
			if (lblOtherCaption != null) {
				lblOtherCaption.Dispose ();
				lblOtherCaption = null;
			}
			if (lblTagJobHeading != null) {
				lblTagJobHeading.Dispose ();
				lblTagJobHeading = null;
			}
			if (tblTaggedProviders != null) {
				tblTaggedProviders.Dispose ();
				tblTaggedProviders = null;
			}
			if (txtBidAmount != null) {
				txtBidAmount.Dispose ();
				txtBidAmount = null;
			}
		}
	}
}
