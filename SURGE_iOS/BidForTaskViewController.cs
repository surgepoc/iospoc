using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using SURGE.Common;
using System.Data;
using System.Drawing;

namespace SURGE_iOS
{
	partial class BidForTaskViewController : UIViewController
	{
		#region Declare Controls
		UILabel lblHeading, lblSubTitle, lblTitleCaption, lblJobTitle, lblProvidersCaption, lblBidAmountCaption;
		UITextField txtBidAmount;
		UIButton btnJobDetails, btnSubmitBid, btnCancel;
		UITableView tblProviders;
		UIScrollView scrollView;

		#endregion Declare Controls

		float h,w;
		public int JobId{ get; set; }
		public int ProviderId{ get; set; }

		#region Constructor
		public BidForTaskViewController (IntPtr handle) : base (handle)
		{
			this.Title = "Bid Task";
		}
		#endregion Constructor

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			#region tempCode
			if(this.JobId ==0){
				this.JobId = 33;ProviderId=1;
			}

			#endregion tempCode

			h = 30;
			w = float.Parse (View.Frame.Width.ToString ());

			#region Instantiate Controls
			lblHeading = new UILabel(){Text = "Bid Task", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 20, w - 10, h) };
			lblSubTitle = new UILabel(){Text = "Submit your bid for this task", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 40, w - 10, h) };

			lblTitleCaption = new UILabel (){ Text = "Title", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 75, w - 10, h) };
			lblJobTitle = new UILabel(){Text="Job title goes here...", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 95, w - 10, h) };
			lblJobTitle.TextColor = UIColor.FromRGB (0, 44, 84);

			btnJobDetails = UIButton.FromType(UIButtonType.RoundedRect);
			btnJobDetails.Font = UIFont.FromName ("Helvetica", 14f);
			btnJobDetails.Frame = new RectangleF (10, 130, 140, h);
			btnJobDetails.SetTitle ("Click for full job details", UIControlState.Normal);

			lblBidAmountCaption = new UILabel(){ Text = "Enter your bid", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 165, w - 10, h) };
			txtBidAmount = new UITextField(){ Placeholder = "$0", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 185, w - 10, h) };
			txtBidAmount.TextColor = UIColor.FromRGB (0, 44, 84);

			lblProvidersCaption = new UILabel (){ Text = "Other bids for this task", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 220, w - 10, h) };

			tblProviders = new UITableView(){ RowHeight=30, Frame = new RectangleF (0, 250, w - 10, 170)};

			btnSubmitBid = UIButton.FromType(UIButtonType.RoundedRect);
			btnSubmitBid.Font = UIFont.FromName ("Helvetica", 14f);
			btnSubmitBid.Frame = new RectangleF (10, 420, 70, h);
			btnSubmitBid.SetTitle ("Submit Bid", UIControlState.Normal);

			btnCancel = UIButton.FromType(UIButtonType.RoundedRect);
			btnCancel.Font = UIFont.FromName ("Helvetica", 14f);
			btnCancel.Frame = new RectangleF (10, 455, 50, h);
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
			scrollView.AddSubview(txtBidAmount);
			scrollView.AddSubview(lblProvidersCaption);
			scrollView.AddSubview(tblProviders);
//			scrollView.AddSubview(btnSubmitBid);
//			scrollView.AddSubview(btnCancel);


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

			txtBidAmount.ShouldReturn += ((textField) => { 
				textField.ResignFirstResponder ();
				return true; 
			});

			btnSubmitBid.TouchUpInside+= (object sender, EventArgs e) => {
				UIAlertView av = new UIAlertView("Bid for Task",
					"Congrats, you successfully bid for this task for $" + txtBidAmount.Text,null, "OK");

				if(	BL.BidTaskByProvider(JobId, ProviderId, Int32.Parse(txtBidAmount.Text))){
					av.Show();

					ProviderJobsViewController providerJobsView = 
						(ProviderJobsViewController) this.Storyboard.InstantiateViewController("ProviderJobsViewController");

					providerJobsView.ProviderId = ProviderId;

					this.NavigationController.PushViewController(providerJobsView, true);
				}
			};

			btnJobDetails.TouchUpInside+= (object sender, EventArgs e) => {
				TaskDetailsViewController taskDetailsView = (TaskDetailsViewController) this.Storyboard.InstantiateViewController("TaskDetailsViewController");
				taskDetailsView.JobId = JobId;
				this.NavigationController.PushViewController(taskDetailsView, true);
			};

			//Set Navigationcontroller tab bar
			this.SetToolbarItems( new UIBarButtonItem[] {
				new UIBarButtonItem("Bid Task", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) => {
					UIAlertView av = new UIAlertView("Task Status",
						"Tash has been bid",null, "OK");

					if(	BL.BidTaskByProvider(JobId, ProviderId, Int32.Parse(txtBidAmount.Text))){
						av.Show();

					}

					ProviderJobsViewController providerJobsView = 
						(ProviderJobsViewController) this.Storyboard.InstantiateViewController("ProviderJobsViewController");

					providerJobsView.ProviderId = ProviderId;

					this.NavigationController.PushViewController(providerJobsView, true);
				})
				, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) {  Width = 30 }
				, new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (s,e) => {
					this.NavigationController.PopViewController(true);
				})
			}, false);

			this.NavigationController.ToolbarHidden = false;
		}	

		class ProviderTableSource: UITableViewSource
		{
			DataTable dtProvidersTagged;
			string cellIdentifier = "TableCell";


			public ProviderTableSource(int _jobId, UIViewController _parent)
			{
				this.dtProvidersTagged = BL.GetProvidersInterestedInJob(_jobId);
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
