using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using SURGE.Common;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace SURGE_iOS
{
	partial class AdminJobsViewController : UIViewController
	{
		#region Declare Controls
		UILabel lblHeading, lblSubTitle;
		UIButton btnNewJob, btnHome;
		UITableView tblJobs;
		UIScrollView scrollView;

		#endregion Declare Controls

		float h,w;
		DataTable dtJobs;

		#region Constructor
		public AdminJobsViewController (IntPtr handle) : base (handle)
		{
			this.Title = "Task List";
		}
		#endregion Constructor

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			h = 30;
			w = float.Parse (View.Frame.Width.ToString ());
			dtJobs = BL.GetAllJobsForAdmins (0, 0, 0);


			#region Instantiate Controls
			lblHeading = new UILabel(){Text = "Task List - " + ((dtJobs.Rows.Count>0)?dtJobs.Rows.Count.ToString():"0")
					, Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 20, w - 10, h) };
			lblSubTitle = new UILabel(){Text = "Manage Tasks here", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 40, w - 10, h) };

			tblJobs = new UITableView(){ RowHeight=30, Frame = new RectangleF (0, 75, w - 10, 320)};

			btnNewJob = UIButton.FromType(UIButtonType.RoundedRect);
			btnNewJob.Font = UIFont.FromName ("Helvetica", 14f);
			btnNewJob.Frame = new RectangleF (10, 405, 65, h);
			btnNewJob.SetTitle ("New Task", UIControlState.Normal);

			btnHome = UIButton.FromType(UIButtonType.RoundedRect);
			btnHome.Font = UIFont.FromName ("Helvetica", 14f);
			btnHome.Frame = new RectangleF (10, 430, 45, h);
			btnHome.SetTitle ("Home", UIControlState.Normal);

			scrollView = new UIScrollView () {
				Frame = new RectangleF (0, 0, float.Parse (View.Frame.Width.ToString ()), float.Parse ((View.Frame.Height - 44).ToString ())),
				ContentSize = new CoreGraphics.CGSize(View.Frame.Width, 800)
			};

			scrollView.AddSubview(lblHeading);
			scrollView.AddSubview(lblSubTitle);
			scrollView.AddSubview(tblJobs);
			scrollView.AddSubview(btnNewJob);
			scrollView.AddSubview(btnHome);

			View.AddSubview(scrollView);

			tblJobs.Source = new ProviderTableSource(this,dtJobs);

			#endregion Instantiate Controls
		
			btnNewJob.TouchUpInside+= (object sender, EventArgs e) => {

				PostJobViewController postJobView = (PostJobViewController) this.Storyboard.InstantiateViewController("PostJobViewController");

				this.NavigationController.PushViewController(postJobView, true);

			};

			btnHome.TouchUpInside+= (object sender, EventArgs e) => {
				HomeViewController homeView = (HomeViewController) this.Storyboard.InstantiateViewController("HomeViewController");

				this.NavigationController.PushViewController(homeView, true);
			};

		}	

		class ProviderTableSource: UITableViewSource
		{
			DataTable dtJobs;
			UIViewController parentView;
			string cellIdentifier = "TableCell";
			Dictionary<string, List<string>> indexedTableItems;
			string[] keys;

			public ProviderTableSource(UIViewController _parent, DataTable _dtJobs)
			{
				this.dtJobs = _dtJobs;
				parentView = _parent;

				indexedTableItems = new Dictionary<string, List<string>>();
				foreach (DataRow t in dtJobs.Rows){
					if (indexedTableItems.ContainsKey (t["JobStatus"].ToString())) {
						indexedTableItems[t["JobStatus"].ToString()].Add(t["JobStatus"].ToString());
					} else {
						indexedTableItems.Add (t["JobStatus"].ToString(), new List<string>() {t["JobStatus"].ToString()});
					}
				}
				keys =  new string[indexedTableItems.Keys.Count];
				indexedTableItems.Keys.CopyTo(keys,0);

			}

			public override nint NumberOfSections (UITableView tableView)
			{
				return keys.Length;
			}

			#region implemented abstract members of UITableViewSource
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
				string taskStatus = keys [indexPath.Section];

				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);

				DataTable dtNew = dtJobs.Copy ();
				dtNew.Clear ();
				foreach (DataRow dr in dtJobs.Rows) {
					if (dr ["jobStatus"].ToString () == keys [indexPath.Section]) {
						DataRow newRow = dtNew.NewRow ();
						newRow.ItemArray = dr.ItemArray;
						dtNew.Rows.Add (newRow);
					}
				}


				if (taskStatus == "New") {
					cell.ImageView.Image = UIImage.FromBundle ("icons/tag.png");
				} else if (taskStatus == "Awarded") {
					cell.ImageView.Image = UIImage.FromBundle ("icons/award.png");
				} else if (taskStatus == "Inprogress") {
					cell.ImageView.Image = UIImage.FromBundle ("icons/view.png");
				} else if (taskStatus == "Submitted") {
					cell.ImageView.Image = UIImage.FromBundle ("icons/approve.png");
				} else if (taskStatus == "Completed") {
					cell.ImageView.Image = UIImage.FromBundle ("icons/rate.png");
				}

				cell.TextLabel.Text = dtNew.Rows [indexPath.Row] ["Title"].ToString ();
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

				return cell;
			}

			public override void WillDisplayHeaderView (UITableView tableView, UIView headerView, nint section)
			{
				UITableViewHeaderFooterView header = (UITableViewHeaderFooterView)headerView;
				header.TextLabel.TextColor = UIColor.FromRGB (0, 44, 84);
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return indexedTableItems [keys [section]].Count;
			}

			public override string TitleForHeader (UITableView tableView, nint section)
			{
				return keys [section] + " - " + indexedTableItems [keys [section]].Count.ToString ();
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				DataTable dtNew = dtJobs.Copy ();
				dtNew.Clear ();
				foreach (DataRow dr in dtJobs.Rows) {
					if (dr ["jobStatus"].ToString () == keys [indexPath.Section]) {
						DataRow newRow = dtNew.NewRow ();
						newRow.ItemArray = dr.ItemArray;
						dtNew.Rows.Add (newRow);
					}
				}

				if (dtNew.Rows [indexPath.Row] ["jobStatus"].ToString () == "New") {
					ReviewProviderViewController reviewProvider = (ReviewProviderViewController)parentView.Storyboard.InstantiateViewController ("ReviewProviderViewController");
					reviewProvider.JobId = Int32.Parse (dtNew.Rows [indexPath.Row] ["ID"].ToString ());
					parentView.NavigationController.PushViewController (reviewProvider, true);
				} else {
					SurgeDetailsViewController surgeDetails = (SurgeDetailsViewController)parentView.Storyboard.InstantiateViewController ("SurgeDetailsViewController");
					surgeDetails.JobId = Int32.Parse (dtNew.Rows [indexPath.Row] ["ID"].ToString ());
					parentView.NavigationController.PushViewController (surgeDetails, true);
				}

				tableView.DeselectRow (indexPath, true);
			}

			#endregion
		}
	}
}
