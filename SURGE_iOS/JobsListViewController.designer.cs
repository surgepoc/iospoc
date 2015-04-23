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
	[Register ("JobsListViewController")]
	partial class JobsListViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnNewPost { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblJobsListHeading { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tblJobsList { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnNewPost != null) {
				btnNewPost.Dispose ();
				btnNewPost = null;
			}
			if (lblJobsListHeading != null) {
				lblJobsListHeading.Dispose ();
				lblJobsListHeading = null;
			}
			if (tblJobsList != null) {
				tblJobsList.Dispose ();
				tblJobsList = null;
			}
		}
	}
}
