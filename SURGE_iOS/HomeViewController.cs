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
	partial class HomeViewController : UIViewController
	{
		#region Declare Controls
		UILabel lblHeading, lblSubTitle;
		UITableView	tblUsers;
		UIScrollView scrollView;

		#endregion Declare Controls

		float h,w;

		#region Constructor
		public HomeViewController (IntPtr handle) : base (handle)
		{
			this.Title = "SURGE";
		}
		#endregion Constructor

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			h = 30;
			w = float.Parse (View.Frame.Width.ToString ());

			#region Instantiate Controls
			lblHeading = new UILabel(){Text = "SURGE POC", Font=UIFont.FromName("Helvetica", 16f), Frame = new RectangleF (10, 20, w - 10, h) };
			lblSubTitle = new UILabel(){Text = "Choose Roles below to navigate", Font=UIFont.FromName("Helvetica", 12f), Frame = new RectangleF (10, 40, w - 10, h) };

			tblUsers = new UITableView(){ RowHeight=30, Frame = new RectangleF (0, 75, w - 10, 400)};
			
			scrollView = new UIScrollView () {
				Frame = new RectangleF (0, 0, float.Parse (View.Frame.Width.ToString ()), float.Parse ((View.Frame.Height - 44).ToString ())),
				ContentSize = new CoreGraphics.CGSize(View.Frame.Width, 800)
			};

			scrollView.AddSubview(lblHeading);
			scrollView.AddSubview(lblSubTitle);
			scrollView.AddSubview(tblUsers);

			View.AddSubview(scrollView);

			tblUsers.Source = new ProviderTableSource(this);

			#endregion Instantiate Controls



			//Set Navigationcontroller tab bar
			this.SetToolbarItems( new UIBarButtonItem[] {
				new UIBarButtonItem("Copyright 2015 Envision Inc", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) => {
				})
			}, false);

			this.NavigationController.ToolbarHidden = false;
		}

		class ProviderTableSource: UITableViewSource
		{
			UIViewController parentView;
			DataTable dtUsers;
			string cellIdentifier = "TableCell";
			Dictionary<string, List<string>> indexedTableItems;
			string[] keys;

			public ProviderTableSource(UIViewController _parent)
			{
				this.parentView = _parent;
				this.dtUsers = BL.GetAllAppUsers();

				indexedTableItems = new Dictionary<string, List<string>>();
				foreach (DataRow t in dtUsers.Rows){
					if (indexedTableItems.ContainsKey (t["Role"].ToString())) {
						indexedTableItems[t["Role"].ToString()].Add(t["Role"].ToString());
					} else {
						indexedTableItems.Add (t["Role"].ToString(), new List<string>() {t["Role"].ToString()});
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

				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Value1, cellIdentifier);

				DataTable dtNew = dtUsers.Copy ();
				dtNew.Clear ();
				foreach (DataRow dr in dtUsers.Rows) {
					if (dr ["Role"].ToString() == keys[indexPath.Section]) {
						DataRow newRow = dtNew.NewRow ();
						newRow.ItemArray = dr.ItemArray;
						dtNew.Rows.Add (newRow);
					}
				}


				cell.ImageView.Image = UIImage.FromBundle (dtNew.Rows [indexPath.Row] ["ProfilePic"].ToString ());
				cell.TextLabel.Text = dtNew.Rows [indexPath.Row] ["Name"].ToString ();
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
				DataTable dtNew = dtUsers.Copy ();
				dtNew.Clear ();
				foreach (DataRow dr in dtUsers.Rows) {
					if (dr ["Role"].ToString () == keys [indexPath.Section]) {
						DataRow newRow = dtNew.NewRow ();
						newRow.ItemArray = dr.ItemArray;
						dtNew.Rows.Add (newRow);
					}
				}

				if (dtNew.Rows [indexPath.Row] ["Role"].ToString () == "Provider") {
					ProviderJobsViewController providerTaskView = (ProviderJobsViewController)parentView.Storyboard.InstantiateViewController ("ProviderJobsViewController");
					providerTaskView.ProviderId = Int32.Parse (dtNew.Rows [indexPath.Row] ["ID"].ToString ());
					parentView.NavigationController.PushViewController (providerTaskView, true);

				} else {
					AdminJobsViewController adminJobsView = (AdminJobsViewController)parentView.Storyboard.InstantiateViewController ("AdminJobsViewController");
//					adminJobsView.JobId = Int32.Parse (dtNew.Rows [indexPath.Row] ["ID"].ToString ());
					parentView.NavigationController.PushViewController (adminJobsView, true);
				}

				tableView.DeselectRow (indexPath, true);
			}

			#endregion
		}
	}
}
