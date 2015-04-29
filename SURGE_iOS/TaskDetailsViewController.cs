using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using SURGE.Common;
using System.Data;
using System.Drawing;


namespace SURGE_iOS
{
	partial class TaskDetailsViewController : UIViewController
	{
		#region Declare Controls
		UILabel lblHeading, lblSubTitle, lblTitleCaption, lblJobDescCaption, lblJobDateCaption, lblJobFromTimeCaption, lblJobToTimeCaption, lblBudgetCaption, lblForBusinessCaption, lblByInviteCaption;
		UILabel lblTitle, lblJobDesc, lblJobDate, lblFromTime, lblToTime, lblBudget;
		UISwitch swtchForBusiness, swtchByInvite;
		UIScrollView scrollView;
		#endregion Declare Controls

		float h,w;
		DataTable dtJobDetails;

		public int JobId{ get; set; }

		#region Constructor
		public TaskDetailsViewController (IntPtr handle) : base (handle)
		{
		}
		#endregion Constructor

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			h = 30;
			w = float.Parse (View.Frame.Width.ToString ());
			dtJobDetails = BL.GetjobDetail (JobId);

			#region Instantiate Controls
			lblHeading = new UILabel () {
				Text = "Task Details",
				Font = UIFont.FromName ("Helvetica", 16f),
				Frame = new RectangleF (10, 20, w - 10, h)
			};
			lblSubTitle = new UILabel () {
				Text = "View complete Task Details here",
				Font = UIFont.FromName ("Helvetica", 12f),
				Frame = new RectangleF (10, 40, w - 10, h)
			};

			lblTitleCaption = new UILabel () {
				Text = "Title",
				Font = UIFont.FromName ("Helvetica", 12f),
				Frame = new RectangleF (10, 75, w - 10, h)
			};
			lblTitle = new UILabel () {
				Text = "Job title goes here...",
				Font = UIFont.FromName ("Helvetica", 16f),
				Frame = new RectangleF (10, 95, w - 10, h)
			};
			lblTitle.TextColor = UIColor.FromRGB (0, 44, 84);

			lblJobDescCaption = new UILabel () {
				Text = "Description",
				Font = UIFont.FromName ("Helvetica", 12f),
				Frame = new RectangleF (10, 130, w - 10, h)
			};
			lblJobDesc = new UILabel {
				Text = "Job Desc  goes here",
				Font = UIFont.FromName ("Helvetica", 16f),
				Frame = new RectangleF (10, 150, w - 10, h)
			};
			lblJobDesc.TextColor = UIColor.FromRGB (0, 44, 84);

			lblJobDateCaption = new UILabel () {
				Text = "Job Date",
				Font = UIFont.FromName ("Helvetica", 12f),
				Frame = new RectangleF (10, 185, w - 10, h)
			};
			lblJobDate = new UILabel () {
				Text = "mm/dd/yyyy",
				Font = UIFont.FromName ("Helvetica", 16f),
				Frame = new RectangleF (10, 205, w - 10, h)
			};
			lblJobDate.TextColor = UIColor.FromRGB (0, 44, 84);

			lblJobFromTimeCaption = new UILabel () {
				Text = "From Time",
				Font = UIFont.FromName ("Helvetica", 12f),
				Frame = new RectangleF (10, 240, w - 10, h)
			};
			lblFromTime = new UILabel () {
				Text = "00:00am",
				Font = UIFont.FromName ("Helvetica", 16f),
				Frame = new RectangleF (10, 260, w - 10, h)
			};
			lblFromTime.TextColor = UIColor.FromRGB (0, 44, 84);

			lblJobToTimeCaption = new UILabel () {
				Text = "To Time",
				Font = UIFont.FromName ("Helvetica", 12f),
				Frame = new RectangleF (10, 295, w - 10, h)
			};
			lblToTime = new UILabel {
				Text = "00:00pm",
				Font = UIFont.FromName ("Helvetica", 16f),
				Frame = new RectangleF (10, 315, w - 10, h)
			};
			lblToTime.TextColor = UIColor.FromRGB (0, 44, 84);

			lblBudgetCaption = new UILabel () {
				Text = "Budget",
				Font = UIFont.FromName ("Helvetica", 12f),
				Frame = new RectangleF (10, 350, w - 10, h)
			};
			lblBudget = new UILabel () {
				Text = "$00.00",
				Font = UIFont.FromName ("Helvetica", 16f),
				Frame = new RectangleF (10, 370, w - 10, h)
			};
			lblBudget.TextColor = UIColor.FromRGB (0, 44, 84);

			lblForBusinessCaption = new UILabel () {
				Text = "For Business",
				Font = UIFont.FromName ("Helvetica", 12f),
				Frame = new RectangleF (10, 405, w - 10, h)
			};
			swtchForBusiness = new UISwitch{ On = true, Frame = new RectangleF (10, 430, w - 10, h) };

			lblByInviteCaption = new UILabel () {
				Text = "Show this task to Invitees only",
				Font = UIFont.FromName ("Helvetica", 12f),
				Frame = new RectangleF (10, 465, w - 10, h)
			};
			swtchByInvite = new UISwitch{ On = true, Frame = new RectangleF (10, 490, w - 10, h) };

			#endregion Instantiate controls

			#region ScrollView 
			scrollView = new UIScrollView () {
				Frame = new RectangleF (0, 0, float.Parse (View.Frame.Width.ToString ()), float.Parse ((View.Frame.Height - 44).ToString ())),
				ContentSize = new CoreGraphics.CGSize (View.Frame.Width, 800)
			};

			//Adding controls to Scroll view as subviews
			scrollView.AddSubview (lblHeading);
			scrollView.AddSubview(lblSubTitle);
			scrollView.AddSubview (lblTitleCaption);
			scrollView.AddSubview (lblTitle);
			scrollView.AddSubview (lblJobDescCaption);
			scrollView.AddSubview (lblJobDesc);
			scrollView.AddSubview (lblJobDateCaption);
			scrollView.AddSubview (lblJobDate);
			scrollView.AddSubview (lblJobFromTimeCaption);
			scrollView.AddSubview (lblFromTime);
			scrollView.AddSubview (lblJobToTimeCaption);
			scrollView.AddSubview (lblToTime);
			scrollView.AddSubview (lblBudgetCaption);
			scrollView.AddSubview (lblBudget);
			scrollView.AddSubview (swtchForBusiness);
			scrollView.AddSubview (lblForBusinessCaption);
			scrollView.AddSubview(lblByInviteCaption);
			scrollView.AddSubview(swtchByInvite);

			View.AddSubview (scrollView);
			#endregion Scroll View

			#region Load Job Details

			if(dtJobDetails.Rows.Count>0){
				lblTitle.Text = dtJobDetails.Rows[0]["Title"].ToString();
				lblJobDesc.Text = dtJobDetails.Rows[0]["JobDesc"].ToString();
				lblJobDate.Text = dtJobDetails.Rows[0]["JobStartDate"].ToString();
				lblFromTime.Text= Convert.ToDateTime( dtJobDetails.Rows[0]["JobStartTime"].ToString()).ToShortTimeString();
				lblToTime.Text =Convert.ToDateTime( dtJobDetails.Rows[0]["JobEndTime"].ToString()).ToShortTimeString();
				lblBudget.Text = "$" + dtJobDetails.Rows[0]["Budget"].ToString();
				if(bool.Parse(dtJobDetails.Rows[0]["ForHospital"].ToString())){
					swtchForBusiness.On = true;
				}
				else{
					swtchForBusiness.On=false;
				}
				if(bool.Parse(dtJobDetails.Rows[0]["ByInvite"].ToString())){
					swtchByInvite.On = true;
				}
				else{
					swtchByInvite.On=false;
				}
			}

			#endregion Load Job Details

			//Set Navigationcontroller tab bar
			this.SetToolbarItems( new UIBarButtonItem[] {
				new UIBarButtonItem("Done", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) => {
					this.NavigationController.PopViewController(true);
				})
				, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) {  Width = 30 }
				, new UIBarButtonItem("", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) => {
					})
			}, false);

			this.NavigationController.ToolbarHidden = false;
		}


	}
}
