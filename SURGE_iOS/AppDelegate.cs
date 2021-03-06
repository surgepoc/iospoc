﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace SURGE_iOS
{

	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		public static UIStoryboard storyBoard = UIStoryboard.FromName("MainStoryBoard", null);

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			var navigationController = storyBoard.InstantiateInitialViewController () as NavigationViewController;
			navigationController.NavigationBar.TintColor = UIColor.White;
			navigationController.NavigationBar.BarTintColor = UIColor.FromRGB (0, 44, 84);
			navigationController.Toolbar.Hidden = false;
			navigationController.NavigationBar.BarStyle = UIBarStyle.Black;
			navigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
			{
				ForegroundColor = UIColor.White
			};

			window.RootViewController = navigationController;

			// make the window visible
			window.MakeKeyAndVisible ();
//			app.SetStatusBarStyle (UIStatusBarStyle.LightContent, true);

			return true;
		}


	}
}

