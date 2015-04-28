using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using SURGE.Common;
using System.Data;
using System.Drawing;

namespace SURGE_iOS
{
	partial class AcceptTaskViewController : UIViewController
	{
		#region Declare Controls
		UILabel lblHeading, lblSubTitle, lblTitleCaption, lblJobTitle, lblProvidersCaption, lblBidAmountCaption, lblBidAmount;
		UIButton btnJobDetails, btnAcceptBid, btnRejectBid;
		UITableView tblProviders;
		UIScrollView scrollView;

		#endregion Declare Controls

		float h,w;
		public int JobId{ get; set; }
		public int ProviderId{ get; set; }
		DataTable dtProviders;

		#region Constructor
		public AcceptTaskViewController (IntPtr handle) : base (handle)
		{
			this.Title = "Acc/Dec Task";

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

			#region Instantiate Controls
			lblHeading = new UILabel(){Text = "Accept/Decline Task", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 20, w - 10, h) };
			lblSubTitle = new UILabel(){Text = "Take action on this task by accepting or declining", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 40, w - 10, h) };

			lblTitleCaption = new UILabel (){ Text = "Title", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 75, w - 10, h) };
			lblJobTitle = new UILabel(){Text="Job title goes here...", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 95, w - 10, h) };
			lblJobTitle.TextColor = UIColor.FromRGB (0, 44, 84);

			btnJobDetails = UIButton.FromType(UIButtonType.RoundedRect);
			btnJobDetails.Font = UIFont.FromName ("Helvetica", 14f);
			btnJobDetails.Frame = new RectangleF (10, 130, 140, h);
			btnJobDetails.SetTitle ("Click for full job details", UIControlState.Normal);

			lblBidAmountCaption = new UILabel(){ Text = "Your bid", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 165, w - 10, h) };
			lblBidAmount = new UILabel(){ Text = "$0", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 185, w - 10, h) };
			lblBidAmount.TextColor = UIColor.FromRGB (0, 44, 84);

			lblProvidersCaption = new UILabel (){ Text = "Other bids for this task", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 220, w - 10, h) };

			tblProviders = new UITableView(){ RowHeight=30, Frame = new RectangleF (0, 250, w - 10, 200)};

			btnAcceptBid = UIButton.FromType(UIButtonType.RoundedRect);
			btnAcceptBid.Font = UIFont.FromName ("Helvetica", 14f);
			btnAcceptBid.Frame = new RectangleF (10, 420, 90, h);
			btnAcceptBid.SetTitle ("Accept Task", UIControlState.Normal);

			btnRejectBid = UIButton.FromType(UIButtonType.RoundedRect);
			btnRejectBid.Font = UIFont.FromName ("Helvetica", 14f);
			btnRejectBid.Frame = new RectangleF (10, 455, 90, h);
			btnRejectBid.SetTitle ("Decline Task", UIControlState.Normal);

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
			scrollView.AddSubview(lblProvidersCaption);
			scrollView.AddSubview(tblProviders);
//			scrollView.AddSubview(btnAcceptBid);
//			scrollView.AddSubview(btnRejectBid);


			View.AddSubview(scrollView);


			#endregion Instantiate Controls

			#region Load Job Details

			DataTable dtJobDetails = new DataTable();
			dtJobDetails = BL.GetjobDetail(JobId);

			if(dtJobDetails.Rows.Count>0)
			{
				lblJobTitle.Text = dtJobDetails.Rows[0]["Title"].ToString();
			}

			tblProviders.Source = new ProviderTableSource(JobId, this, dtProviders);

			#endregion load job details
				
			#region Load his bid
			foreach(DataRow dr in dtProviders.Rows){
				if(ProviderId == Int32.Parse(dr["ProviderId"].ToString())){
					lblBidAmount.Text = "$" + dr["BidAmount"].ToString();
				}
			}
			#endregion Load his bid

			btnAcceptBid.TouchUpInside += (object sender, EventArgs e) => {
				UIAlertView av = new UIAlertView("Task Accepted",
					"Congrats on your new task", null, "OK");

				if(BL.ChangeJobStatus(JobId, "Inprogress")){
					av.Show();
				}

				ProviderJobsViewController providerJobsView = 
					(ProviderJobsViewController) this.Storyboard.InstantiateViewController("ProviderJobsViewController"); 

				this.NavigationController.PushViewController(providerJobsView, true);
			};

			btnRejectBid.TouchUpInside+= (object sender, EventArgs e) => {
				UIAlertView av = new UIAlertView("Task Declined",
					"No Problem, we will let you know other tasks", null, "OK");

				if(BL.ChangeJobStatus(JobId, "New")){
					av.Show();
				}

				ProviderJobsViewController providerJobsView = 
					(ProviderJobsViewController) this.Storyboard.InstantiateViewController("ProviderJobsViewController"); 

				this.NavigationController.PushViewController(providerJobsView, true);
			};

			btnJobDetails.TouchUpInside+= (object sender, EventArgs e) => {
				TaskDetailsViewController taskDetailsView = (TaskDetailsViewController) this.Storyboard.InstantiateViewController("TaskDetailsViewController");
				taskDetailsView.JobId = JobId;
				this.NavigationController.PushViewController(taskDetailsView, true);
			};

			//Set Navigationcontroller tab bar
			this.SetToolbarItems( new UIBarButtonItem[] {
				new UIBarButtonItem("Accept Task", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) => {
					UIAlertView av = new UIAlertView("Task Status",
						"Task has been accepted", null, "OK");

					if(BL.ChangeJobStatus(JobId, "Inprogress")){
						av.Show();
					}

					ProviderJobsViewController providerJobsView = 
						(ProviderJobsViewController) this.Storyboard.InstantiateViewController("ProviderJobsViewController"); 

					this.NavigationController.PushViewController(providerJobsView, true);
				})
				, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) {  Width = 30 }
				, new UIBarButtonItem("Decline Task", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) => {
					UIAlertView av = new UIAlertView("Task Status",
						"Task has been declined", null, "OK");

					if(BL.ChangeJobStatus(JobId, "New")){
						av.Show();
					}

					ProviderJobsViewController providerJobsView = 
						(ProviderJobsViewController) this.Storyboard.InstantiateViewController("ProviderJobsViewController"); 

					this.NavigationController.PushViewController(providerJobsView, true);
				})
			}, false);

			this.NavigationController.ToolbarHidden = false;
		}	

		class ProviderTableSource: UITableViewSource
		{
			DataTable dtProvidersTagged;
			string cellIdentifier = "TableCell";


			public ProviderTableSource(int _jobId, UIViewController _parent, DataTable _dtProviders)
			{
					this.dtProvidersTagged = _dtProviders;
			}

			#region implemented abstract members of UITableViewSource
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);

				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Value1, cellIdentifier);

				cell.ImageView.Image = UIImage.FromBundle (dtProvidersTagged.Rows [indexPath.Row] ["ProfilePic"].ToString ());
				cell.TextLabel.Text = dtProvidersTagged.Rows [indexPath.Row] ["Name"].ToString ();
				cell.DetailTextLabel.Text = "$" + dtProvidersTagged.Rows [indexPath.Row] ["BidAmount"].ToString ();

				return cell;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return dtProvidersTagged.Rows.Count;
			}

			#endregion
		}
	}
}
