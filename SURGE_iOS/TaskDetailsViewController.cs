using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SURGE_iOS
{
	partial class TaskDetailsViewController : UIViewController
	{
		#region Declare Controls
		UILabel lblHeading, lblTitleCaption, lblJobDescCaption, lblJobDateCaption, lblJobFromTimeCaption, lblJobToTimeCaption, lblBudgetCaption, lblForBusinessCaption;
		UILabel lblTitle, lblJobDesc, lblJobDate, lblFromTime, lblToTime, lblBudget;
		UISwitch swtchForBusiness;
		UIScrollView scrollView;
		UIButton btnTaskList;
		#endregion Declare Controls

		float h,w;

		#region Constructor
		public TaskDetailsViewController (IntPtr handle) : base (handle)
		{
		}
	}
}
