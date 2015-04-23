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
		UIButton btnJobDetails;
		UITableView tblProviders;
		UIScrollView scrollView;
		event EventHandler<ProviderSelected> AwardRowSelected;

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
				this.JobId = 1;
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
			lblJobTitle.TextColor = UIColor.FromRGB (81, 125, 137);

			btnJobDetails = UIButton.FromType(UIButtonType.RoundedRect);
			btnJobDetails.Font = UIFont.FromName ("Helvetica", 14f);
			btnJobDetails.Frame = new RectangleF (10, 130, 140, h);
			btnJobDetails.SetTitle ("Click for full job details", UIControlState.Normal);

			lblProvidersCaption = new UILabel (){ Text = "Click on below provider to award", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 165, w - 10, h) };

			tblProviders = new UITableView(){ RowHeight=30, Frame = new RectangleF (0, 200, w - 10, 200)};

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

				cell.ImageView.Image = UIImage.FromBundle ("ProfileDefault.png");
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

		class ProviderSelected: EventArgs{
			public int ProviderId{ get; set;}
		}
	}
}
