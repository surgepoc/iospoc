using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using SURGE.Common;
using System.Data;
using System.Drawing;

namespace SURGE_iOS
{
	partial class TagProviderViewController : UIViewController
	{
		#region Declare Controls
		UILabel lblHeading, lblSubTitle, lblTitleCaption, lblJobTitle, lblProvidersCaption;
		UIButton btnJobDetails, btnTaskList;
		UITableView tblProviders;
		UIScrollView scrollView;

		#endregion Declare Controls

		float h,w;
		public int JobId{ get; set; }

		#region Constructor
		public TagProviderViewController (IntPtr handle) : base (handle)
		{
			this.Title = "Tag Providers";
		}
		#endregion Constructor

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			#region tempCode
			if(this.JobId ==0){
				this.JobId = 1;
			}

//			this.Title=this.JobId.ToString();
			#endregion tempCode

			h = 30;
			w = float.Parse (View.Frame.Width.ToString ());

			#region Instantiate Controls
			lblHeading = new UILabel(){Text = "Tag Providers", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 20, w - 10, h) };
			lblSubTitle = new UILabel(){Text = "Invite eligible Providers to bid for this task", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 40, w - 10, h) };

			lblTitleCaption = new UILabel (){ Text = "Title", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 75, w - 10, h) };
			lblJobTitle = new UILabel(){Text="Job title goes here...", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 95, w - 10, h) };
			lblJobTitle.TextColor = UIColor.FromRGB (0, 44, 84);

			btnJobDetails = UIButton.FromType(UIButtonType.RoundedRect);
			btnJobDetails.Font = UIFont.FromName ("Helvetica", 14f);
			btnJobDetails.Frame = new RectangleF (10, 130, 140, h);
			btnJobDetails.SetTitle ("Click for full job details", UIControlState.Normal);

			lblProvidersCaption = new UILabel (){ Text = "Click on below providers to tag", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 165, w - 10, h) };

			tblProviders = new UITableView(){ RowHeight=30, Frame = new RectangleF (0, 200, w - 10, 200)};

			btnTaskList = UIButton.FromType(UIButtonType.RoundedRect);
			btnTaskList.Font = UIFont.FromName ("Helvetica", 14f);
			btnTaskList.Frame = new RectangleF (10, 400, 140, h);
			btnTaskList.SetTitle ("Back to Task List", UIControlState.Normal);

			scrollView = new UIScrollView () {
				Frame = new RectangleF (0, 0, float.Parse (View.Frame.Width.ToString ()), float.Parse ((View.Frame.Height - 44).ToString ())),
				ContentSize = new CoreGraphics.CGSize(View.Frame.Width, 800)
			};

			scrollView.AddSubview(lblHeading);
			scrollView.AddSubview(lblSubTitle);
			scrollView.AddSubview(lblTitleCaption);
			scrollView.AddSubview(lblJobTitle);
			scrollView.AddSubview(btnJobDetails);
			scrollView.AddSubview(lblProvidersCaption);
			scrollView.AddSubview(tblProviders);
			scrollView.AddSubview(btnTaskList);

			View.AddSubview(scrollView);


			#endregion Instantiate Controls

			#region Load Job Details

			DataTable dtJobDetails = new DataTable();
			dtJobDetails = BL.GetjobDetail(JobId);

			if(dtJobDetails.Rows.Count>0)
			{
				lblJobTitle.Text = dtJobDetails.Rows[0]["Title"].ToString();
			}

			DataTable dtProviders = new DataTable();
			dtProviders = BL.GetProvidersToTag();

			tblProviders.Source = new ProviderTableSource(dtProviders, JobId);

			#endregion load job details

			btnTaskList.TouchUpInside+= (object sender, EventArgs e) => {

				AdminJobsViewController adminJobsView = (AdminJobsViewController) this.Storyboard.InstantiateViewController("AdminJobsViewController");
				this.NavigationController.PushViewController(adminJobsView, true);
			};

			btnJobDetails.TouchUpInside+= (object sender, EventArgs e) => {
				TaskDetailsViewController taskDetailsView = (TaskDetailsViewController) this.Storyboard.InstantiateViewController("TaskDetailsViewController");
				taskDetailsView.JobId = JobId;
				this.NavigationController.PushViewController(taskDetailsView, true);
			};

			//Set Navigationcontroller tab bar
			this.SetToolbarItems( new UIBarButtonItem[] {
				new UIBarButtonItem("Review Providers", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) => {
					ReviewProviderViewController reviewProviders = (ReviewProviderViewController) this.Storyboard.InstantiateViewController("ReviewProviderViewController");
					reviewProviders.JobId = JobId;
					this.NavigationController.PushViewController(reviewProviders, true);
				})
				, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) {  Width = 30 }
				, new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (s,e) => {
					HomeViewController homeView = (HomeViewController) this.Storyboard.InstantiateViewController("HomeViewController");
					this.NavigationController.PushViewController(homeView, true);
				})
			}, false);

			this.NavigationController.ToolbarHidden = false;
		}

		class ProviderTableSource: UITableViewSource
		{
			DataTable dtProviders, dtProvidersTagged;
			int jobId;
			string cellIdentifier = "TableCell";

			public ProviderTableSource(DataTable _providers, int _jobId)
			{
				this.dtProviders = _providers;
				this.dtProvidersTagged = BL.GetProvidersTaggedForJob(_jobId);
				jobId = _jobId;
			}

			#region implemented abstract members of UITableViewSource
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);

				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Value1, cellIdentifier);

				cell.ImageView.Image = UIImage.FromBundle (dtProviders.Rows [indexPath.Row] ["ProfilePic"].ToString ());
				cell.TextLabel.Text = dtProviders.Rows [indexPath.Row] ["Name"].ToString ();
				cell.DetailTextLabel.Text = GetRating(Int32.Parse(dtProviders.Rows [indexPath.Row] ["Rating"].ToString ()));
				cell.Accessory = UITableViewCellAccessory.Checkmark;

				if (IsProviderMapped (Int32.Parse (dtProviders.Rows [indexPath.Row] ["ID"].ToString ()))) {
					cell.TintColor = UIColor.Blue;
				} else {
					cell.TintColor = UIColor.LightGray;
				}

				return cell;
			}

		 	bool IsProviderMapped(int providerId){
				foreach (DataRow dr in dtProvidersTagged.Rows) {
					if (Int32.Parse (dr ["providerId"].ToString ()) == providerId) {
						return true;
					}
				}

				return false;
			}

			string GetRating(int rateCount){
				string rating = "";
				for (int i = 0; i < rateCount; i++) {
					rating += "*";
				}
				return rating;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return dtProviders.Rows.Count;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				
				UIAlertView av = new UIAlertView ("Provider Tagged",
					                 dtProviders.Rows [indexPath.Row] ["Id"].ToString () + "." + dtProviders.Rows [indexPath.Row] ["Name"].ToString (),
					                 null,
					                 "OK");

				//Tag provider
				if (BL.TagProviderForJob (jobId, Int32.Parse (dtProviders.Rows [indexPath.Row] ["Id"].ToString ()))) {
					av.Show ();
				}
				UITableViewCell cell = tableView.CellAt (indexPath);
				cell.Accessory = UITableViewCellAccessory.Checkmark;
				cell.TintColor = UIColor.Blue;
				tableView.DeselectRow (indexPath, true);
			}
			#endregion
		}
	}
}
