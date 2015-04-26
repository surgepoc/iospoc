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
		UILabel lblHeading, lblSubTitle, lblTitleCaption, lblJobTitle, lblRemarksCaption, lblBidAmountCaption, lblBidAmount;
		UIButton btnJobDetails, btnAction, btnCancel;
		UITextField txtRemarks;
		UIScrollView scrollView;

		#endregion Declare Controls

		float h,w;
		public int JobId{ get; set; }
		public int ProviderId{ get; set; }
		DataTable dtProviders;

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
				this.JobId = 1;
			}
			ProviderId=1;

			#endregion tempCode

			h = 30;
			w = float.Parse (View.Frame.Width.ToString ());
			dtProviders = BL.GetProvidersInterestedInJob (JobId);

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

			lblRemarksCaption = new UILabel (){ Text = "Remarks", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 220, w - 10, h) };

			txtRemarks = new UITextField(){ Placeholder="Enter your remarks here", Frame = new RectangleF (10, 250, w - 10, h)};

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
			scrollView.AddSubview(lblRemarksCaption);
			scrollView.AddSubview(txtRemarks);
			scrollView.AddSubview(btnAction);
			scrollView.AddSubview(btnCancel);


			View.AddSubview(scrollView);


			#endregion Instantiate Controls

			#region Load his bid
			foreach(DataRow dr in dtProviders.Rows){
				if(ProviderId == Int32.Parse(dr["ProviderId"].ToString())){
					lblBidAmount.Text = "$" + dr["BidAmount"].ToString();
				}
			}
			#endregion Load his bid

			btnAction.TouchUpInside+= (object sender, EventArgs e) => {

				UIAlertView av = new UIAlertView("Task Submitted",
					"Congrats on submitting your task",null, "OK");

				if((BL.ChangeJobStatus(JobId, "Submitted"))){
					av.Show();
				}

				ProviderJobsViewController providerJobsView = 
					(ProviderJobsViewController) this.Storyboard.InstantiateViewController("ProviderJobsViewController"); 

				this.NavigationController.PushViewController(providerJobsView, true);
			};

			txtRemarks.ShouldReturn += ((textField) => { 
				textField.ResignFirstResponder ();
				return true; 
			});
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

				cell.ImageView.Image = UIImage.FromBundle ("ProfilePic.jpg");
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
