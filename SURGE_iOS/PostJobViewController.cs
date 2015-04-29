using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using SURGE.Common;
using System.Data;
using System.Drawing;

namespace SURGE_iOS
{
	partial class PostJobViewController : UIViewController
	{
		#region Declare Controls
		UILabel lblHeading, lblsubHeading, lblTitleCaption, lblJobDescCaption, lblJobDateCaption, lblJobFromTimeCaption, lblJobToTimeCaption, lblBudgetCaption, lblForBusinessCaption, lblByInviteCaption;
		UITextField txtTitle, txtJobDesc, txtJobDate, txtFromTime, txtToTime, txtBudget;
		UISwitch swtchForBusiness, swtchByInvite;
		UIScrollView scrollView;
		UIButton btnPostJob;
		UIDatePicker dpJobDate, dpJobFromTime, dpJobToTime;
		UIView pDateView, pFromTimeView, pToTimeView;
		UIToolbar myDateToolbar, myFromTimeToolbar, myToTimeToolbar;
		#endregion Declare Controls

		float h,w;

		#region Constructor
		public PostJobViewController (IntPtr handle) : base (handle)
		{
			this.Title = "Post Task";
		}
		#endregion Constructor

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			h = 30;
			w = float.Parse (View.Frame.Width.ToString ());

			#region Instantiate Controls
			lblHeading = new UILabel (){ Text = "Post New Task", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 20, w - 10, h) };
			lblsubHeading = new UILabel (){ Text = "New Surge Task", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 40, w - 10, h) };

			lblTitleCaption = new UILabel (){ Text = "Title", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 75, w - 10, h) };
			txtTitle = new UITextField{ Placeholder = "Enter job title", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 95, w - 10, h) };
			txtTitle.TextColor = UIColor.FromRGB (0, 44, 84);

			lblJobDescCaption = new UILabel (){ Text = "Description", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 130, w - 10, h) };
			txtJobDesc = new UITextField{ Placeholder = "Enter job desc", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 150, w - 10, h) };
			txtJobDesc.TextColor = UIColor.FromRGB (0, 44, 84);

			lblJobDateCaption = new UILabel (){ Text = "Task Date", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 185, w - 10, h) };
			txtJobDate = new UITextField{ Placeholder = "mm/dd/yyyy", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 205, w - 10, h) };
			txtJobDate.TextColor = UIColor.FromRGB (0, 44, 84);

			lblJobFromTimeCaption = new UILabel (){ Text = "Start Time", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 240, w - 10, h) };
			txtFromTime = new UITextField{ Placeholder = "00:00am", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 260, w - 10, h) };
			txtFromTime.TextColor = UIColor.FromRGB (0, 44, 84);

			lblJobToTimeCaption = new UILabel (){ Text = "End Time", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 295, w - 10, h) };
			txtToTime = new UITextField{ Placeholder = "00:00pm", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 315, w - 10, h) };
			txtToTime.TextColor = UIColor.FromRGB (0, 44, 84);

			lblBudgetCaption = new UILabel (){ Text = "Budget", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 340, w - 10, h) };
			txtBudget = new UITextField { Placeholder = "$0", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 360, w - 10, h) };
			txtBudget.TextColor = UIColor.FromRGB (0, 44, 84);

			lblForBusinessCaption = new UILabel (){ Text = "On Behalf of Hospital", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 395, w - 10, h) };
			swtchForBusiness = new UISwitch{ On = true, Frame = new RectangleF (10, 420,  w - 10, h) };

			lblByInviteCaption = new UILabel (){ Text = "Show this task to Invitees only", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 455, w - 10, h) };
			swtchByInvite = new UISwitch{ On = false, Frame = new RectangleF (10, 480,  w - 10, h) };

			btnPostJob = UIButton.FromType(UIButtonType.RoundedRect);
			btnPostJob.Font = UIFont.FromName ("Helvetica", 14f);
			btnPostJob.Frame = new RectangleF (10, 545, w - 10, h);
			btnPostJob.SetTitle ("Post Task", UIControlState.Normal);
			#endregion Instantiate controls

			#region Date Picker
			myDateToolbar = new UIToolbar(RectangleF.Empty);
			myDateToolbar.BarStyle = UIBarStyle.Default;
			myDateToolbar.Translucent = true;
			myDateToolbar.UserInteractionEnabled = true;
			myDateToolbar.SizeToFit();

			UIBarButtonItem btnDateCancel;
			UIBarButtonItem btnDateFlexibleSpace;
			UIBarButtonItem btnDateDone;
			UIBarButtonItem[] btnDateItems;
			btnDateCancel = new UIBarButtonItem(UIBarButtonSystemItem.Cancel, pickerDateCancel);
			btnDateFlexibleSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null);
			btnDateDone = new UIBarButtonItem(UIBarButtonSystemItem.Done, pickerDateDone);
			btnDateItems = new UIBarButtonItem[] { btnDateCancel, btnDateFlexibleSpace, btnDateDone }; 
			myDateToolbar.SetItems(btnDateItems, true);

			//Set Date Picker for Job Start Date

			dpJobDate = new UIDatePicker (new RectangleF (0, 44, w - 10, 216));
			dpJobDate.Mode = UIDatePickerMode.Date;
//			dpJobDate.MaximumDate = NSDate.Now;
			dpJobDate.TimeZone = NSTimeZone.LocalTimeZone;
			dpJobDate.UserInteractionEnabled = true;
			dpJobDate.BackgroundColor = UIColor.White;

			pDateView = new UIView (new RectangleF (0, 0, w - 10, 260));
			pDateView.AddSubview (myDateToolbar);
			pDateView.AddSubview (dpJobDate);

			txtJobDate.EditingDidBegin += delegate {
				txtJobDate.EndEditing(true);
				txtJobDate.InputView = pDateView;
			};
			#endregion Datepicker

			#region From Time Picker

			myFromTimeToolbar = new UIToolbar(RectangleF.Empty);
			myFromTimeToolbar.BarStyle = UIBarStyle.Default;
			myFromTimeToolbar.Translucent = true;
			myFromTimeToolbar.UserInteractionEnabled = true;
			myFromTimeToolbar.SizeToFit();

			UIBarButtonItem btnFromTimeCancel;
			UIBarButtonItem btnFromTimeFlexibleSpace;
			UIBarButtonItem btnFromTimeDone;
			UIBarButtonItem[] btnFromTimeItems;
			btnFromTimeCancel = new UIBarButtonItem(UIBarButtonSystemItem.Cancel, pickerFromTimeCancel);
			btnFromTimeFlexibleSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null);
			btnFromTimeDone = new UIBarButtonItem(UIBarButtonSystemItem.Done, pickerFromTimeDone);
			btnFromTimeItems = new UIBarButtonItem[] { btnFromTimeCancel, btnFromTimeFlexibleSpace, btnFromTimeDone }; 
			myFromTimeToolbar.SetItems(btnFromTimeItems, true);

			dpJobFromTime = new UIDatePicker (new RectangleF (0, 44, w - 10, 216));
			dpJobFromTime.Mode = UIDatePickerMode.Time;
			dpJobFromTime.TimeZone = NSTimeZone.LocalTimeZone;
			dpJobFromTime.UserInteractionEnabled = true;
			dpJobFromTime.BackgroundColor = UIColor.White;

			pFromTimeView = new UIView (new RectangleF (0, 0, w - 10, 260));
			pFromTimeView.AddSubview (myFromTimeToolbar);
			pFromTimeView.AddSubview (dpJobFromTime);

			txtFromTime.EditingDidBegin += delegate {
				txtFromTime.InputView = pFromTimeView;
			};
			#endregion From Time Picker

			#region To Time Picker

			myToTimeToolbar = new UIToolbar(RectangleF.Empty);
			myToTimeToolbar.BarStyle = UIBarStyle.Default;
			myToTimeToolbar.Translucent = true;
			myToTimeToolbar.UserInteractionEnabled = true;
			myToTimeToolbar.SizeToFit();

			UIBarButtonItem btnToTimeCancel;
			UIBarButtonItem btnToTimeFlexibleSpace;
			UIBarButtonItem btnToTimeDone;
			UIBarButtonItem[] btnToTimeItems;
			btnToTimeCancel = new UIBarButtonItem(UIBarButtonSystemItem.Cancel, pickerToTimeCancel);
			btnToTimeFlexibleSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null);
			btnToTimeDone = new UIBarButtonItem(UIBarButtonSystemItem.Done, pickerToTimeDone);
			btnToTimeItems = new UIBarButtonItem[] { btnToTimeCancel, btnToTimeFlexibleSpace, btnToTimeDone }; 
			myToTimeToolbar.SetItems(btnToTimeItems, true);

			dpJobToTime = new UIDatePicker (new RectangleF (0, 44, w - 10, 216));
			dpJobToTime.Mode = UIDatePickerMode.Time;
			dpJobToTime.TimeZone = NSTimeZone.LocalTimeZone;
			dpJobToTime.UserInteractionEnabled = true;
			dpJobToTime.BackgroundColor = UIColor.White;

			pToTimeView = new UIView (new RectangleF (0, 0, w - 10, 260));
			pToTimeView.AddSubview (myToTimeToolbar);
			pToTimeView.AddSubview (dpJobToTime);


			txtToTime.EditingDidBegin += delegate {
				txtToTime.InputView = pToTimeView;
			};
			#endregion To Time Picker

			#region ScrollView 
			scrollView = new UIScrollView () {
				Frame = new RectangleF (0, 0, float.Parse (View.Frame.Width.ToString ()), float.Parse ((View.Frame.Height - 44).ToString ())),
				ContentSize = new CoreGraphics.CGSize(View.Frame.Width, 800)
			};
					
			//Adding controls to Scroll view as subviews
			scrollView.AddSubview (lblHeading);
			scrollView.AddSubview(lblsubHeading);
			scrollView.AddSubview (lblTitleCaption);
			scrollView.AddSubview (txtTitle);
			scrollView.AddSubview (lblJobDescCaption);
			scrollView.AddSubview (txtJobDesc);
			scrollView.AddSubview (lblJobDateCaption);
			scrollView.AddSubview (txtJobDate);
			scrollView.AddSubview (lblJobFromTimeCaption);
			scrollView.AddSubview (txtFromTime);
			scrollView.AddSubview (lblJobToTimeCaption);
			scrollView.AddSubview (txtToTime);
			scrollView.AddSubview (lblBudgetCaption);
			scrollView.AddSubview (txtBudget);
			scrollView.AddSubview (swtchForBusiness);
			scrollView.AddSubview (lblForBusinessCaption);
			scrollView.AddSubview(lblByInviteCaption);
			scrollView.AddSubview(swtchByInvite);
//			scrollView.AddSubview (btnPostJob);

			View.AddSubview (scrollView);
			#endregion Scroll View

			//Post button click event
//			btnPostJob.TouchUpInside+= (object sender, EventArgs e) => {
//				Types.Job newJob = new Types.Job();
//				newJob.Title = txtTitle.Text;
//				newJob.JobDesc = txtJobDesc.Text;
//				newJob.JobStartDate = DateTime.Parse( txtJobDate.Text);
//				newJob.JobStartTime = txtFromTime.Text;
//				newJob.JobEndTime = txtToTime.Text;
//				newJob.Budget = float.Parse(txtBudget.Text);
//				newJob.ForHospital = swtchForBusiness.On;
//				newJob.ByInvite = swtchByInvite.On;
//				newJob.CreatorId = 1;
//				newJob.CreatorType = "Hospitalist";
//
//				DataTable dtNewJob = new DataTable();
//				dtNewJob = BL.PostJob(newJob);
//
//				if(dtNewJob.Rows.Count>0){
//					var av = new UIAlertView("Task Status", "Task has been posted",null,"OK");
//					av.Show();
//					ClearForm();
//
//					//Redirect to Tag Providers
//					var tagProviderView = (TagProviderViewController) this.Storyboard.InstantiateViewController("TagProviderViewController") ;
//					tagProviderView.JobId = Int32.Parse(dtNewJob.Rows[0][0].ToString());
//
//
//					this.NavigationController.PushViewController(tagProviderView, true);
//				}
//				else{
//					var av = new UIAlertView("Task Status", "Some thing went wrong", null, "OK");
//					av.Show();
//				}
//			};


			//Resigning Firstresponder
			txtTitle.ShouldReturn += ((txtField) => {
				txtField.ResignFirstResponder ();
				return true;
			});

			txtTitle.ShouldReturn += ((textField) => { 
				textField.ResignFirstResponder ();
				return true; 
			});

			txtJobDesc.ShouldReturn += ((textField) => { 
				textField.ResignFirstResponder ();
				return true; 
			});

		 	txtJobDate.ShouldReturn += ((textField) => { 
				textField.ResignFirstResponder ();
				return true; 
			});

			txtFromTime.ShouldReturn += ((textField) => { 
				textField.ResignFirstResponder ();
				return true; 
			});

			txtToTime.ShouldReturn += ((textField) => { 
				textField.ResignFirstResponder ();
				return true; 
			});

			txtBudget.ShouldReturn += ((textField) => { 
				textField.ResignFirstResponder ();
				return true; 
			});

			this.SetToolbarItems( new UIBarButtonItem[] {
				new UIBarButtonItem(UIBarButtonSystemItem.Save, (s,e) => {
					if(isFormValid()){
					Types.Job newJob = new Types.Job();
					newJob.Title = txtTitle.Text;
					newJob.JobDesc = txtJobDesc.Text;
					newJob.JobStartDate = DateTime.Parse( txtJobDate.Text);
					newJob.JobStartTime = txtFromTime.Text;
					newJob.JobEndTime = txtToTime.Text;
					newJob.Budget = float.Parse(txtBudget.Text);
					newJob.ForHospital = swtchForBusiness.On;
					newJob.ByInvite = swtchByInvite.On;
					newJob.CreatorId = 1;
					newJob.CreatorType = "Hospitalist";

					DataTable dtNewJob = new DataTable();
					dtNewJob = BL.PostJob(newJob);

					if(dtNewJob.Rows.Count>0){
						var av = new UIAlertView("Task Status", "Task has been posted",null,"OK");
						av.Show();
						ClearForm();

						//Redirect to Tag Providers
						var tagProviderView = (TagProviderViewController) this.Storyboard.InstantiateViewController("TagProviderViewController") ;
						tagProviderView.JobId = Int32.Parse(dtNewJob.Rows[0][0].ToString());


						this.NavigationController.PushViewController(tagProviderView, true);
					}
					else{
						var av = new UIAlertView("Task Status", "Some thing went wrong", null, "OK");
						av.Show();
						}
					}
				})
				, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) { Width = 50 }
				, new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (s,e) => {
					this.NavigationController.PopViewController(true);
				})
			}, false);

			this.NavigationController.ToolbarHidden = false;
		}

		bool isFormValid(){
			string msg = "";
			if (txtTitle.Text == "") {
				msg = "Title can not be empty";
			} else if (txtJobDesc.Text == "") {
				msg = "Job description can not be empty";
			} else if (txtJobDate.Text == "") {
				msg = "Job start date can not be empty";
			} else if (txtFromTime.Text == "") {
				msg = "Job start time can not be empty";
			} else if (txtToTime.Text == "") {
				msg = "Job end time can not be empty";
			} else if (txtBudget.Text == "") {
				msg="Budget can not be empty";
			}

			if (msg != "") {
				UIAlertView av = new UIAlertView ("Validation Error", msg, null, "OK");
				av.Show();
				return false;
			}

			return true;
		}

		#region Job Date, From Time & To Time Done and Cancel delegates
		private void pickerDateDone(object sender, EventArgs e)
		{
			pDateView.Hidden = true;
			txtJobDate.ResignFirstResponder ();
			DateTime dtDate = DateTime.Parse (dpJobDate.Date.ToString ());
			txtJobDate.Text = dtDate.ToShortDateString ();
		}

		private void pickerDateCancel(object sender, EventArgs e)
		{
			pDateView.Hidden = true;
		}

		private void pickerFromTimeDone(object sender, EventArgs e)
		{
			pFromTimeView.Hidden = true;
			txtFromTime.ResignFirstResponder ();
			DateTime dtDate = DateTime.Parse ( dpJobFromTime.Date.ToString ());
			txtFromTime.Text = dtDate.ToShortTimeString ();
		}

		private void pickerFromTimeCancel(object sender, EventArgs e)
		{
			pFromTimeView.Hidden = true;
		}

		private void pickerToTimeDone(object sender, EventArgs e)
		{
			pToTimeView.Hidden = true;
			txtToTime.ResignFirstResponder();
			DateTime dtDate = DateTime.Parse (dpJobToTime.Date.ToString ());
			txtToTime.Text = dtDate.ToShortTimeString ();
		}

		private void pickerToTimeCancel(object sender, EventArgs e)
		{
			pToTimeView.Hidden = true;
		}
		#endregion Job Date, From Time & To Time Done and Cancel delegates

		#region ClearForm
		void ClearForm(){
			txtTitle.Text = "";
			txtJobDesc.Text = "";
			txtJobDate.Text = "";
			txtFromTime.Text = "";
			txtToTime.Text = "";
			txtBudget.Text = "";
			swtchForBusiness.On = true;
		}

		#endregion ClearForm
	}
}
