using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using SURGE.Common;
using System.Data;
using System.Drawing;

namespace SURGE_iOS
{
	partial class SubmitTaskViewController : UIViewController
	{
		#region Declare Controls
		UILabel lblHeading, lblSubTitle, lblTitleCaption, lblJobTitle, lblJobStatusCaption, lblJobStatus, lblBidAmountCaption, lblBidAmount;
		UIButton btnJobDetails, btnAction, btnCancel;
		UIScrollView scrollView;

		#endregion Declare Controls

		float h,w;
		public int JobId{ get; set; }
		public int ProviderId{ get; set; }
		DataTable dtProviders, dtJobDetails;

		#region Constructor
		public SubmitTaskViewController (IntPtr handle) : base (handle)
		{
			this.Title = "Submit Task";
		}
		#endregion Constructor

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			#region tempCode
			if(this.JobId ==0){
				this.JobId = 1;ProviderId=1;
			}

			#endregion tempCode

			h = 30;
			w = float.Parse (View.Frame.Width.ToString ());
			dtProviders = BL.GetProvidersInterestedInJob (JobId);
			dtJobDetails = BL.GetjobDetail (JobId);

			#region Instantiate Controls
			lblHeading = new UILabel(){Text = "Submit Task", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 20, w - 10, h) };
			lblSubTitle = new UILabel(){Text = "Submit your task after completion", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 40, w - 10, h) };

			lblTitleCaption = new UILabel (){ Text = "Title", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 75, w - 10, h) };
			lblJobTitle = new UILabel(){Text="Job title goes here...", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 95, w - 10, h) };
			lblJobTitle.TextColor = UIColor.FromRGB (0, 44, 84);

			btnJobDetails = UIButton.FromType(UIButtonType.RoundedRect);
			btnJobDetails.Font = UIFont.FromName ("Helvetica", 14f);
			btnJobDetails.Frame = new RectangleF (10, 130, 140, h);
			btnJobDetails.SetTitle ("Click for full job details", UIControlState.Normal);

			lblBidAmountCaption = new UILabel(){ Text = "Your bid amount", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 165, w - 10, h) };
			lblBidAmount = new UILabel(){ Text = "$0", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 185, w - 10, h) };
			lblBidAmount.TextColor = UIColor.FromRGB (81, 125, 137);

			lblJobStatusCaption = new UILabel (){ Text = "Task Status", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 220, w - 10, h) };

			lblJobStatus = new UILabel(){Text="Job Status goes here", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 240, w - 10, h) };
			lblJobStatus.TextColor = UIColor.FromRGB (0, 44, 84);

			btnAction = UIButton.FromType(UIButtonType.RoundedRect);
			btnAction.Font = UIFont.FromName ("Helvetica", 14f);
			btnAction.Frame = new RectangleF (10, 285, 80, h);
			btnAction.SetTitle ("Submit Task", UIControlState.Normal);

			btnCancel = UIButton.FromType(UIButtonType.RoundedRect);
			btnCancel.Font = UIFont.FromName ("Helvetica", 14f);
			btnCancel.Frame = new RectangleF (10, 320, 50, h);
			btnCancel.SetTitle ("Cancel", UIControlState.Normal);

			scrollView = new UIScrollView () {
				Frame = new RectangleF (0, 0, float.Parse (View.Frame.Width.ToString ()), float.Parse ((View.Frame.Height - 44).ToString ())),
				ContentSize = new CoreGraphics.CGSize(View.Frame.Width, 800)
			};

			scrollView.AddSubview(lblHeading);
			scrollView.AddSubview(lblSubTitle);
			scrollView.AddSubview(lblTitleCaption);
			scrollView.AddSubview(lblJobTitle);
			scrollView.AddSubview(btnJobDetails);
			scrollView.AddSubview(lblBidAmountCaption);
			scrollView.AddSubview(lblBidAmount);
			scrollView.AddSubview(lblJobStatusCaption);
			scrollView.AddSubview(lblJobStatus);
//			scrollView.AddSubview(btnAction);
//			scrollView.AddSubview(btnCancel);


			View.AddSubview(scrollView);


			#endregion Instantiate Controls

			#region Load Job Details
			string actionType;

			lblJobTitle.Text = dtJobDetails.Rows[0]["Title"].ToString();
			if(dtJobDetails.Rows[0]["jobStatus"].ToString() == "Completed Not Submitted"){
				actionType = "Submit Task";
				lblSubTitle.Text = "You can submit this task if completed";
			}
			else{
				actionType = "Rate Task";
				lblSubTitle.Text = "You can rate this task after completion";
			}

			this.Title = actionType;
			lblHeading.Text = actionType;
			#endregion

			#region Load his bid
			foreach(DataRow dr in dtProviders.Rows){
				if(ProviderId == Int32.Parse(dr["ProviderId"].ToString())){
					lblBidAmount.Text = "$" + dr["BidAmount"].ToString();
				}
			}
			#endregion Load his bid

			lblJobStatus.Text = dtJobDetails.Rows [0] ["JobStatus"].ToString ();

			btnAction.TouchUpInside+= (object sender, EventArgs e) => {

				UIAlertView av = new UIAlertView("Task Submitted",
					"Congrats on submitting your task",null, "OK");

				if((BL.ChangeJobStatus(JobId, "Submitted"))){
					av.Show();
				}

				ProviderJobsViewController providerJobsView = 
					(ProviderJobsViewController) this.Storyboard.InstantiateViewController("ProviderJobsViewController"); 

				this.NavigationController.PushViewController(providerJobsView, true);

//				this.DismissViewController(true, null);
			};

			btnJobDetails.TouchUpInside+= (object sender, EventArgs e) => {
				TaskDetailsViewController taskDetailsView = (TaskDetailsViewController) this.Storyboard.InstantiateViewController("TaskDetailsViewController");
				taskDetailsView.JobId = JobId;
				this.NavigationController.PushViewController(taskDetailsView, true);
			};
				

			//Set Navigationcontroller tab bar
			this.SetToolbarItems( new UIBarButtonItem[] {
				new UIBarButtonItem(actionType, UIBarButtonItemStyle.Plain, (object sender, EventArgs e) => {
					if(actionType=="Submit Task"){
					UIAlertView av = new UIAlertView("Task Status",
						"Task has been submitted",null, "OK");

					if((BL.ChangeJobStatus(JobId, "Submitted Not Approved"))){
						av.Show();
					}

					ProviderJobsViewController providerJobsView = 
						(ProviderJobsViewController) this.Storyboard.InstantiateViewController("ProviderJobsViewController"); 
						providerJobsView.ProviderId = ProviderId;
					this.NavigationController.PushViewController(providerJobsView, true);
					}
				})
				, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) {  Width = 30 }
				, new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (s,e) => {
					this.NavigationController.PopViewController(true);
				})
			}, false);

			this.NavigationController.ToolbarHidden = false;
		}

//		class ProviderTableSource: UITableViewSource
//		{
//			DataTable dtProvidersTagged;
//			string cellIdentifier = "TableCell";
//
//
//			public ProviderTableSource(int _jobId, UIViewController _parent, DataTable _dtProviders)
//			{
//				this.dtProvidersTagged = _dtProviders;
//			}
//
//			#region implemented abstract members of UITableViewSource
//			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
//			{
//				UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
//
//				if (cell == null)
//					cell = new UITableViewCell (UITableViewCellStyle.Value1, cellIdentifier);
//
//				cell.ImageView.Image = UIImage.FromBundle ("ProfilePic.jpg");
//				cell.TextLabel.Text = dtProvidersTagged.Rows [indexPath.Row] ["Name"].ToString ();
//				cell.DetailTextLabel.Text = "$" + dtProvidersTagged.Rows [indexPath.Row] ["BidAmount"].ToString ();
//
//				return cell;
//			}
//
//			public override nint RowsInSection (UITableView tableview, nint section)
//			{
//				return dtProvidersTagged.Rows.Count;
//			}
//
//			#endregion
//		}
	}
}
