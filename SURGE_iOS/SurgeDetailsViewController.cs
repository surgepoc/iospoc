using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using SURGE.Common;
using System.Data;
using System.Drawing;

namespace SURGE_iOS
{
	partial class SurgeDetailsViewController : UIViewController
	{
		#region Declare Controls
		UILabel lblHeading, lblSubTitle, lblTitleCaption, lblJobTitle, lblAwardToCaption, lblProviderNameCaption,
		lblProviderName, lblRating, lblBidAmountCaption, lblBidAmount, lblTaskStatusCaption, lblTaskStatus;
		UIImageView imgProvider;
		UIButton btnJobDetails, btnDynAction, btnCancel;
		UIScrollView scrollView;

		#endregion Declare Controls

		float h,w;
		public int JobId{ get; set; }
		public int ProviderId{ get; set; }
		DataTable dtJob, dtProvider, dtTagJob;
		string actionType;

		#region Constructor
		public SurgeDetailsViewController (IntPtr handle) : base (handle)
		{
			this.Title = "Surge Details";
		}
		#endregion Constructor

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			#region tempCode
			if(this.JobId ==0){
				this.JobId = 1; this.ProviderId=1;
			}

			//			this.Title=this.JobId.ToString();
			#endregion tempCode

			h = 30;
			w = float.Parse (View.Frame.Width.ToString ());
			dtJob = BL.GetjobDetail (JobId);
			dtTagJob = BL.GetProvidersInterestedInJob (JobId);
			//To get provider details who is either awarded, inprogress, submitted or completed
			foreach (DataRow drRow in dtTagJob.Rows) {
				if (bool.Parse (drRow ["IsAwarded"].ToString ())) {
					dtProvider = BL.GetProviderDetailsByJob (JobId, Int32.Parse (drRow ["ProviderId"].ToString ()));
				}
			}

			#region Instantiate Controls
			lblHeading = new UILabel(){Text = "Surge Details", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 20, w - 10, h) };
			lblSubTitle = new UILabel(){Text = "Check below current status of this Surge task", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 40, w - 10, h) };

			lblTitleCaption = new UILabel (){ Text = "Title", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 75, w - 10, h) };
			lblJobTitle = new UILabel(){Text="Job title goes here...", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 95, w - 10, h) };
			lblJobTitle.TextColor = UIColor.FromRGB (0, 44, 84);

			btnJobDetails = UIButton.FromType(UIButtonType.RoundedRect);
			btnJobDetails.Font = UIFont.FromName ("Helvetica", 14f);
			btnJobDetails.Frame = new RectangleF (10, 130, 140, h);
			btnJobDetails.SetTitle ("Click for full job details", UIControlState.Normal);

			lblAwardToCaption = new UILabel (){ Text = "to", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 165, w - 10, h) };

			imgProvider = new UIImageView() { Frame = new RectangleF (10, 200, 64, 64), Image = UIImage.FromBundle("ProfileDefault.png")};

			lblProviderNameCaption = new UILabel() { Text = "Name", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 265, w - 10, h) };

			lblProviderName = new UILabel() { Text = "Provider name comes here", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 285, w - 10, h) };
			lblProviderName.TextColor = UIColor.FromRGB (0, 44, 84);

			lblRating =  new UILabel() { Text = "***", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 305, w - 10, h) };

			lblBidAmountCaption =  new UILabel() { Text = "Bid Amount", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 340, w - 10, h) };

			lblBidAmount =  new UILabel() { Text = "$400", Font=UIFont.FromName("Helvetica", 16), Frame = new RectangleF (10, 360, w - 10, h) };
			lblBidAmount.TextColor = UIColor.FromRGB (0, 44, 84);

			lblTaskStatusCaption =  new UILabel() { Text = "Task Status", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 395, w - 10, h) };

			lblTaskStatus =  new UILabel() { Text = "Status goes here", Font=UIFont.FromName("Helvetica", 16), Frame = new RectangleF (10, 415, w - 10, h) };
			lblTaskStatus.TextColor = UIColor.FromRGB (0, 44, 84);

			btnDynAction = UIButton.FromType(UIButtonType.RoundedRect);
			btnDynAction.Font = UIFont.FromName ("Helvetica", 14f);
			btnDynAction.Frame = new RectangleF (10, 440, 80, h);
			btnDynAction.SetTitle ("Dynamic Action", UIControlState.Normal);

			btnCancel = UIButton.FromType(UIButtonType.RoundedRect);
			btnCancel.Font = UIFont.FromName ("Helvetica", 14f);
			btnCancel.Frame = new RectangleF (10, 470, 45, h);
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
			scrollView.AddSubview(lblAwardToCaption);
			scrollView.AddSubview(imgProvider);
			scrollView.AddSubview(lblProviderNameCaption);
			scrollView.AddSubview(lblProviderName);
			scrollView.AddSubview(lblRating);
			scrollView.AddSubview(lblBidAmountCaption);
			scrollView.AddSubview(lblBidAmount);
			scrollView.AddSubview(lblTaskStatusCaption);
			scrollView.AddSubview(lblTaskStatus);
			scrollView.AddSubview(btnDynAction);
			scrollView.AddSubview(btnCancel);

			View.AddSubview(scrollView);


			#endregion Instantiate Controls

			#region Load Job Details

			DataTable dtJobDetails = new DataTable();
			dtJobDetails = BL.GetjobDetail(JobId);

			if(dtJobDetails.Rows.Count>0)
			{
				lblJobTitle.Text = dtJobDetails.Rows[0]["Title"].ToString();

				lblTaskStatus.Text = dtJobDetails.Rows[0]["JobStatus"].ToString();

				string jobStatus = dtJobDetails.Rows[0]["JobStatus"].ToString();

				if(jobStatus == "Awarded" || jobStatus =="Inprogress"){
					actionType = "Cancel Task";
					btnDynAction.SetTitle (actionType, UIControlState.Normal);
					btnDynAction.Frame = new RectangleF (10, 440, 80, h);
				}
				else if(jobStatus=="Submitted"){
					actionType = "Approve Task";
					btnDynAction.SetTitle (actionType, UIControlState.Normal);
					btnDynAction.Frame = new RectangleF (10, 440, 90, h);
				}
				else if(jobStatus=="Completed"){
					actionType = "Rate Task";
					btnDynAction.SetTitle (actionType, UIControlState.Normal);
					btnDynAction.Frame = new RectangleF (10, 440, 80, h);
				}
			}

			#endregion Load Job Details

			#region Load Provider Details

//			DataTable dtProvider = new DataTable();
//			dtProvider = BL.GetProviderDetailsByJob(JobId, ProviderId);

			if(dtProvider.Rows.Count>0){

				if(dtProvider.Rows[0]["ProfilePic"].ToString()!=""){
					imgProvider.Image = UIImage.FromBundle(dtProvider.Rows[0]["ProfilePic"].ToString());
				}
				lblProviderName.Text = dtProvider.Rows[0]["Name"].ToString();

				lblRating.Text = GetRating(Int32.Parse(dtProvider.Rows[0]["Rating"].ToString()));

				lblBidAmount.Text = dtProvider.Rows[0]["BidAmount"].ToString();
			}
			#endregion Load Provider Details

			btnDynAction.TouchUpInside+= (object sender, EventArgs e) => {
				if(actionType == "Approve Task"){

					UIAlertView av = new UIAlertView("Task Status",
						"Task completed successfully", null, "OK");
					
					if(BL.ChangeJobStatus(JobId, "Completed")){
						av.Show();
					}

					AdminJobsViewController adminJobsView = (AdminJobsViewController) this.Storyboard.InstantiateViewController("AdminJobsViewController");

					this.NavigationController.PushViewController(adminJobsView, true);
				}
			};
		}

		string GetRating(int rateCount){
			string rating = "";
			for (int i = 0; i < rateCount; i++) {
				rating += "*";
			}
			return rating;
		}
	}
}
