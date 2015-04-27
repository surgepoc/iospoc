using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using SURGE.Common;
using System.Data;
using System.Drawing;

namespace SURGE_iOS
{
	partial class ReviewProviderViewController : UIViewController
	{
		#region Declare Controls
		UILabel lblHeading, lblSubTitle, lblTitleCaption, lblJobTitle, lblProvidersCaption;
		UIButton btnJobDetails, btnTaskList, btnTagProviders, btnNewTask;
		UITableView tblProviders;
		UIScrollView scrollView;

		#endregion Declare Controls

		float h,w;
		public int JobId{ get; set; }

		#region Constructor
		public ReviewProviderViewController (IntPtr handle) : base (handle)
		{
			this.Title = "Review Providers";
		}
		#endregion Constructor

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			#region tempCode
			if(this.JobId ==0){
				this.JobId = 33;
			}

			//			this.Title=this.JobId.ToString();
			#endregion tempCode

			h = 30;
			w = float.Parse (View.Frame.Width.ToString ());

			#region Instantiate Controls
			lblHeading = new UILabel(){Text = "Review Providers", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 20, w - 10, h) };
			lblSubTitle = new UILabel(){Text = "View Providers interested in this task", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 40, w - 10, h) };

			lblTitleCaption = new UILabel (){ Text = "Title", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 75, w - 10, h) };
			lblJobTitle = new UILabel(){Text="Job title goes here...", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 95, w - 10, h) };
			lblJobTitle.TextColor = UIColor.FromRGB (0, 44, 84);

			btnJobDetails = UIButton.FromType(UIButtonType.RoundedRect);
			btnJobDetails.Font = UIFont.FromName ("Helvetica", 14f);
			btnJobDetails.Frame = new RectangleF (10, 130, 140, h);
			btnJobDetails.SetTitle ("Click for full job details", UIControlState.Normal);

			lblProvidersCaption = new UILabel (){ Text = "Click on below provider to award", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 165, w - 10, h) };

			tblProviders = new UITableView(){ RowHeight=30, Frame = new RectangleF (0, 200, w - 10, 200)};

			btnTaskList = UIButton.FromType(UIButtonType.RoundedRect);
			btnTaskList.Font = UIFont.FromName ("Helvetica", 14f);
			btnTaskList.Frame = new RectangleF (10, 400, 110, h);
			btnTaskList.SetTitle ("Back to Task List", UIControlState.Normal);

			btnTagProviders = UIButton.FromType(UIButtonType.RoundedRect);
			btnTagProviders.Font = UIFont.FromName ("Helvetica", 14f);
			btnTagProviders.Frame = new RectangleF (10, 435, 90, h);
			btnTagProviders.SetTitle ("Tag Providers", UIControlState.Normal);

			btnNewTask = UIButton.FromType(UIButtonType.RoundedRect);
			btnNewTask.Font = UIFont.FromName ("Helvetica", 14f);
			btnNewTask.Frame = new RectangleF (10, 470, 65, h);
			btnNewTask.SetTitle ("New Task", UIControlState.Normal);

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
			scrollView.AddSubview(btnTagProviders);
			scrollView.AddSubview(btnNewTask);

			View.AddSubview(scrollView);


			#endregion Instantiate Controls

			#region Load Job Details

			DataTable dtJobDetails = new DataTable();
			dtJobDetails = BL.GetjobDetail(JobId);

			if(dtJobDetails.Rows.Count>0)
			{
				lblJobTitle.Text = dtJobDetails.Rows[0]["Title"].ToString();
			}

			tblProviders.Source = new ProviderTableSource(JobId, this);

			#endregion load job details

			btnTaskList.TouchUpInside+= (object sender, EventArgs e) => {

				AdminJobsViewController adminJobsView = (AdminJobsViewController) this.Storyboard.InstantiateViewController("AdminJobsViewController");
				this.NavigationController.PushViewController(adminJobsView, true);
			};

			btnTagProviders.TouchUpInside+= (object sender, EventArgs e) => {
				TagProviderViewController tagProvider = (TagProviderViewController)this.Storyboard.InstantiateViewController("TagProviderViewController");
				tagProvider.JobId = JobId;
				this.NavigationController.PushViewController(tagProvider, true);
			};

			btnNewTask.TouchUpInside+= (object sender, EventArgs e) => {
				PostJobViewController postNewTask = (PostJobViewController) this.Storyboard.InstantiateViewController("PostJobViewController");
				this.NavigationController.PushViewController(postNewTask, true);
			};

			btnJobDetails.TouchUpInside+= (object sender, EventArgs e) => {
				TaskDetailsViewController taskDetailsView = (TaskDetailsViewController) this.Storyboard.InstantiateViewController("TaskDetailsViewController");
				taskDetailsView.JobId = JobId;
				this.NavigationController.PushViewController(taskDetailsView, true);
			};

			//Set Navigationcontroller tab bar
			this.SetToolbarItems( new UIBarButtonItem[] {
				new UIBarButtonItem("Tag Providers", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) => {
					TagProviderViewController tagProviders = (TagProviderViewController) this.Storyboard.InstantiateViewController("TagProviderViewController");
					tagProviders.JobId = JobId;
					this.NavigationController.PushViewController(tagProviders, true);
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
			DataTable dtProvidersTagged;
			string cellIdentifier = "TableCell";
			int jobId;
			UIViewController parent;

			public ProviderTableSource(int _jobId, UIViewController _parent)
			{
				this.dtProvidersTagged = BL.GetProvidersInterestedInJob(_jobId);
				this.jobId = _jobId;
				this.parent = _parent;
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

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				//Navigate to award page to award job to a provider
				AwardJobViewController awardJobView = parent.Storyboard.InstantiateViewController ("AwardJobViewController") as AwardJobViewController;
				awardJobView.JobId = jobId;
				awardJobView.ProviderId = Int32.Parse (dtProvidersTagged.Rows [indexPath.Row] ["ProviderId"].ToString ());

				parent.NavigationController.PushViewController (awardJobView, true);
			}
			#endregion
		}

	}
}
