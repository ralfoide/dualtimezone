//*******************************************************************
/*

	Solution:	DualTimeZone
	Project:	DualTimeZoneApp
	File:		RMainForm.cs

	Copyright 2005, Raphael MOLL.

	This file is part of DualTimeZone.

	DualTimeZone is free software; you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation; either version 2 of the License, or
	(at your option) any later version.

	DualTimeZone is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with DualTimeZone; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

*/
//*******************************************************************



using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Alfray.LibUtils.Misc;
using Alfray.DualTimeZone.DualTimeZoneLib;

//*********************************
namespace Alfray.DualTimeZone.DualTimeZoneApp
{
	//**************************************
	/// <summary>
	/// Summary description for RMainForm.
	/// </summary>
	public class RMainForm : System.Windows.Forms.Form, RILog
	{
		//-------------------------------------------
		//----------- Public Constants --------------
		//-------------------------------------------


		//-------------------------------------------
		//----------- Public Properties -------------
		//-------------------------------------------

		public string[] ProgramArguments {
			get {
				return mProgArgs;
			}
			set {
				mProgArgs = value;
			}
		}


		public RAboutForm AboutForm {
			get {
				return mAboutForm;
			}
			set {
				mAboutForm = value;
			}
		}


		//-------------------------------------------
		//----------- Public Methods ----------------
		//-------------------------------------------

		
		//****************
		public RMainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// Add any constructor code after InitializeComponent call
			//

			init();

		}

		//*****************
		public void Start() {
			// load all settings
			loadSettings();

			// apply defaults
			reloadPrefs();

			// setup initial icon and start update timer
			updateIcon();
			mTimer.Enabled = true;
		}

		#region RILog Members

		//***********************
		public void Log(object o)
		{
			Log(o.ToString());
		}

		//***********************
		public void Log(string s)
		{
			System.Diagnostics.Debug.WriteLine(s);
		}

		#endregion


		//***********************
		public void ReloadPrefs()
		{
			reloadPrefs();
		}



		//-------------------------------------------
		//----------- Private Methods ---------------
		//-------------------------------------------


		//*****************
		private void init()
		{
		}


		//**********************
		private void terminate()
		{
			// about form
			if (mAboutForm != null)
				mAboutForm.Close();

			// save settings
			saveSettings();

			// remove the taskbar icon
			mNotifyIcon.Icon = null;

			// this is final
			Application.Exit();
		}


		//*************************
		/// <summary>
		/// Loads settings specific to this window.
		/// Done only once when the window is created.
		/// </summary>
		//*************************
		private void loadSettings()
		{
			// load settings
			RMainModule.Pref.Load();

			// tell Windows not to change this position automatically
			this.StartPosition = FormStartPosition.Manual;

			// load position of this window
			Rectangle r;
			if (RMainModule.Pref.GetRect(RPrefConstants.kMainForm, out r)) {
				// RM 20050307 No longer change the size of the window.
				// This is because the window cannot be resized manually,
				// instead it adapts to the size of the inner video preview.
				this.Location = r.Location;
			}

			// Restore hide/show, unless overriden by --show on the command line arguments
			bool force_show = false;
			foreach(string s in ProgramArguments)
				if (s == "--show") {
					force_show = true;
					break;
				}

			makeVisible(force_show ||
						Convert.ToBoolean(RMainModule.Pref[RPrefConstants.kFormVisible]));

			// Restore time offset
			mTimeOffset = Convert.ToInt32(RMainModule.Pref[RPrefConstants.kTimeOffset]);
			mTextDualTimeZoneShift.Text = mTimeOffset.ToString();
		}


		//*************************
		private void saveSettings()
		{
			// save time offset
			RMainModule.Pref[RPrefConstants.kTimeOffset] = mTimeOffset.ToString();
	
			// save position & size of this window
			RMainModule.Pref.SetRect(RPrefConstants.kMainForm, this.Bounds);

			// save settings
			RMainModule.Pref.Save();
		}


		//************************
		/// <summary>
		/// (Re)Loads app-wide preferences.
		/// Done anytime the user applies changes to the preference window
		/// or once at startup.
		/// </summary>
		//************************
		private void reloadPrefs()
		{
			Log("Prefs reloaded");
		}


		//***********************
		private void updateIcon()
		{
			int h = (DateTime.Now.Hour + mTimeOffset + 24) % 24;

			if (h != mCurrentHour) {
				mCurrentHour = h;
				mNotifyIcon.Icon = mIconRef.GetHourIcon(mCurrentHour);

				Log(String.Format("Current Hour: {0}", h));
			}

			setTimerInterval();
		}


		//*****************************
		private void setTimerInterval()
		{
			int m = DateTime.Now.Minute;
			if (m >= 59) {
				int s = DateTime.Now.Second;
				if (s < 57)
					mTimer.Interval = 1000 * (57 - s); // seconds -> milliseconds
				else
					mTimer.Interval = 1000; // every other second
			} else {
				mTimer.Interval = 1000 * 60 * (59 - m); // minutes -> milliseconds
			}

			Log(String.Format("Timer: {0} ms, {1:#.##} min => {2}",
				mTimer.Interval, (double) mTimer.Interval / 60000.0,
				DateTime.Now.AddMilliseconds(mTimer.Interval).ToLongTimeString()));
		}

		//************************************
		private void makeVisible(bool visible)
		{
			if (visible) {
				this.Show();
				this.BringToFront();
				Log("Show Form");
			} else {
				this.Hide();
				Log("Hide Form");
			}

			RMainModule.Pref[RPrefConstants.kFormVisible] = visible.ToString();
		}


		//********************
		private void about() {
			if (mAboutForm != null) {
				mAboutForm.Show();
				mAboutForm.BringToFront();
			} else {
				mAboutForm = new RAboutForm(this);
				mAboutForm.Show();
			}
		}


		//********************************
		private void openUrl(string url) {
			System.Diagnostics.Process.Start(url);
		}

		//-------------------------------------------
		//----------- Private Callbacks -------------
		//-------------------------------------------


		//******************************************************************
		private void RMainForm_Closing(object sender,
			System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			makeVisible(false);
		}

		//******************************************************************
		private void mButtonApply_Click(object sender, System.EventArgs e)
		{
			try {
				mTimeOffset = Convert.ToInt32(mTextDualTimeZoneShift.Text);
				saveSettings();
				updateIcon();
			} catch(Exception ex) {
				MessageBox.Show(this, "Invalid Time Offset. " + ex.ToString(),
					"Invalid Time Offset");
			}
		}

		//******************************************************************
		private void mButtonQuit_Click(object sender, System.EventArgs e)
		{
			terminate();
		}

		//******************************************************************
		private void mTimer_Tick(object sender, System.EventArgs e)
		{
			updateIcon();
		}

		//******************************************************************
		private void mNotifyIcon_DoubleClick(object sender, System.EventArgs e)
		{
			makeVisible(true);
		}

		//******************************************************************
		private void mMenuSettings_Click(object sender, System.EventArgs e)
		{
			makeVisible(true);
		}

		//******************************************************************
		private void mMenuExit_Click(object sender, System.EventArgs e)
		{
			mButtonQuit_Click(sender, e);
		}

		//******************************************************************
		private void RMainForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			if (mFirstPaint) {
				makeVisible(Convert.ToBoolean(RMainModule.Pref[RPrefConstants.kFormVisible]));
				mFirstPaint = false;
			}
		}

		//******************************************************************
		private void mButtonAbout_Click(object sender, System.EventArgs e) {
			about();
		}

		//******************************************************************
		private void menuItem2_Click(object sender, System.EventArgs e) {
			about();	
		}

		//******************************************************************
		private void mLinkLabelAlfray_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			openUrl("http://www.alfray.com");
		}

		//-------------------------------------------
		//----------- Private WinForms --------------
		//-------------------------------------------

		//***********************************
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Windows Form Designer generated code

		//********************************
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RMainForm));
			this.mNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.mIconContextMenu = new System.Windows.Forms.ContextMenu();
			this.mMenuSettings = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mMenuExit = new System.Windows.Forms.MenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.mTextDualTimeZoneShift = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.mButtonApply = new System.Windows.Forms.Button();
			this.mTimer = new System.Windows.Forms.Timer(this.components);
			this.mButtonQuit = new System.Windows.Forms.Button();
			this.mButtonAbout = new System.Windows.Forms.Button();
			this.mLinkLabelAlfray = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// mNotifyIcon
			// 
			this.mNotifyIcon.ContextMenu = this.mIconContextMenu;
			this.mNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("mNotifyIcon.Icon")));
			this.mNotifyIcon.Text = resources.GetString("mNotifyIcon.Text");
			this.mNotifyIcon.Visible = ((bool)(resources.GetObject("mNotifyIcon.Visible")));
			this.mNotifyIcon.DoubleClick += new System.EventHandler(this.mNotifyIcon_DoubleClick);
			// 
			// mIconContextMenu
			// 
			this.mIconContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							 this.mMenuSettings,
																							 this.menuItem2,
																							 this.menuItem1,
																							 this.mMenuExit});
			this.mIconContextMenu.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mIconContextMenu.RightToLeft")));
			// 
			// mMenuSettings
			// 
			this.mMenuSettings.Enabled = ((bool)(resources.GetObject("mMenuSettings.Enabled")));
			this.mMenuSettings.Index = 0;
			this.mMenuSettings.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mMenuSettings.Shortcut")));
			this.mMenuSettings.ShowShortcut = ((bool)(resources.GetObject("mMenuSettings.ShowShortcut")));
			this.mMenuSettings.Text = resources.GetString("mMenuSettings.Text");
			this.mMenuSettings.Visible = ((bool)(resources.GetObject("mMenuSettings.Visible")));
			this.mMenuSettings.Click += new System.EventHandler(this.mMenuSettings_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Enabled = ((bool)(resources.GetObject("menuItem2.Enabled")));
			this.menuItem2.Index = 1;
			this.menuItem2.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem2.Shortcut")));
			this.menuItem2.ShowShortcut = ((bool)(resources.GetObject("menuItem2.ShowShortcut")));
			this.menuItem2.Text = resources.GetString("menuItem2.Text");
			this.menuItem2.Visible = ((bool)(resources.GetObject("menuItem2.Visible")));
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Enabled = ((bool)(resources.GetObject("menuItem1.Enabled")));
			this.menuItem1.Index = 2;
			this.menuItem1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem1.Shortcut")));
			this.menuItem1.ShowShortcut = ((bool)(resources.GetObject("menuItem1.ShowShortcut")));
			this.menuItem1.Text = resources.GetString("menuItem1.Text");
			this.menuItem1.Visible = ((bool)(resources.GetObject("menuItem1.Visible")));
			// 
			// mMenuExit
			// 
			this.mMenuExit.Enabled = ((bool)(resources.GetObject("mMenuExit.Enabled")));
			this.mMenuExit.Index = 3;
			this.mMenuExit.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mMenuExit.Shortcut")));
			this.mMenuExit.ShowShortcut = ((bool)(resources.GetObject("mMenuExit.ShowShortcut")));
			this.mMenuExit.Text = resources.GetString("mMenuExit.Text");
			this.mMenuExit.Visible = ((bool)(resources.GetObject("mMenuExit.Visible")));
			this.mMenuExit.Click += new System.EventHandler(this.mMenuExit_Click);
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// mTextDualTimeZoneShift
			// 
			this.mTextDualTimeZoneShift.AccessibleDescription = resources.GetString("mTextDualTimeZoneShift.AccessibleDescription");
			this.mTextDualTimeZoneShift.AccessibleName = resources.GetString("mTextDualTimeZoneShift.AccessibleName");
			this.mTextDualTimeZoneShift.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("mTextDualTimeZoneShift.Anchor")));
			this.mTextDualTimeZoneShift.AutoSize = ((bool)(resources.GetObject("mTextDualTimeZoneShift.AutoSize")));
			this.mTextDualTimeZoneShift.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mTextDualTimeZoneShift.BackgroundImage")));
			this.mTextDualTimeZoneShift.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("mTextDualTimeZoneShift.Dock")));
			this.mTextDualTimeZoneShift.Enabled = ((bool)(resources.GetObject("mTextDualTimeZoneShift.Enabled")));
			this.mTextDualTimeZoneShift.Font = ((System.Drawing.Font)(resources.GetObject("mTextDualTimeZoneShift.Font")));
			this.mTextDualTimeZoneShift.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("mTextDualTimeZoneShift.ImeMode")));
			this.mTextDualTimeZoneShift.Location = ((System.Drawing.Point)(resources.GetObject("mTextDualTimeZoneShift.Location")));
			this.mTextDualTimeZoneShift.MaxLength = ((int)(resources.GetObject("mTextDualTimeZoneShift.MaxLength")));
			this.mTextDualTimeZoneShift.Multiline = ((bool)(resources.GetObject("mTextDualTimeZoneShift.Multiline")));
			this.mTextDualTimeZoneShift.Name = "mTextDualTimeZoneShift";
			this.mTextDualTimeZoneShift.PasswordChar = ((char)(resources.GetObject("mTextDualTimeZoneShift.PasswordChar")));
			this.mTextDualTimeZoneShift.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mTextDualTimeZoneShift.RightToLeft")));
			this.mTextDualTimeZoneShift.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("mTextDualTimeZoneShift.ScrollBars")));
			this.mTextDualTimeZoneShift.Size = ((System.Drawing.Size)(resources.GetObject("mTextDualTimeZoneShift.Size")));
			this.mTextDualTimeZoneShift.TabIndex = ((int)(resources.GetObject("mTextDualTimeZoneShift.TabIndex")));
			this.mTextDualTimeZoneShift.Text = resources.GetString("mTextDualTimeZoneShift.Text");
			this.mTextDualTimeZoneShift.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("mTextDualTimeZoneShift.TextAlign")));
			this.mTextDualTimeZoneShift.Visible = ((bool)(resources.GetObject("mTextDualTimeZoneShift.Visible")));
			this.mTextDualTimeZoneShift.WordWrap = ((bool)(resources.GetObject("mTextDualTimeZoneShift.WordWrap")));
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// mButtonApply
			// 
			this.mButtonApply.AccessibleDescription = resources.GetString("mButtonApply.AccessibleDescription");
			this.mButtonApply.AccessibleName = resources.GetString("mButtonApply.AccessibleName");
			this.mButtonApply.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("mButtonApply.Anchor")));
			this.mButtonApply.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mButtonApply.BackgroundImage")));
			this.mButtonApply.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("mButtonApply.Dock")));
			this.mButtonApply.Enabled = ((bool)(resources.GetObject("mButtonApply.Enabled")));
			this.mButtonApply.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("mButtonApply.FlatStyle")));
			this.mButtonApply.Font = ((System.Drawing.Font)(resources.GetObject("mButtonApply.Font")));
			this.mButtonApply.Image = ((System.Drawing.Image)(resources.GetObject("mButtonApply.Image")));
			this.mButtonApply.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mButtonApply.ImageAlign")));
			this.mButtonApply.ImageIndex = ((int)(resources.GetObject("mButtonApply.ImageIndex")));
			this.mButtonApply.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("mButtonApply.ImeMode")));
			this.mButtonApply.Location = ((System.Drawing.Point)(resources.GetObject("mButtonApply.Location")));
			this.mButtonApply.Name = "mButtonApply";
			this.mButtonApply.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mButtonApply.RightToLeft")));
			this.mButtonApply.Size = ((System.Drawing.Size)(resources.GetObject("mButtonApply.Size")));
			this.mButtonApply.TabIndex = ((int)(resources.GetObject("mButtonApply.TabIndex")));
			this.mButtonApply.Text = resources.GetString("mButtonApply.Text");
			this.mButtonApply.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mButtonApply.TextAlign")));
			this.mButtonApply.Visible = ((bool)(resources.GetObject("mButtonApply.Visible")));
			this.mButtonApply.Click += new System.EventHandler(this.mButtonApply_Click);
			// 
			// mTimer
			// 
			this.mTimer.Interval = 2000;
			this.mTimer.Tick += new System.EventHandler(this.mTimer_Tick);
			// 
			// mButtonQuit
			// 
			this.mButtonQuit.AccessibleDescription = resources.GetString("mButtonQuit.AccessibleDescription");
			this.mButtonQuit.AccessibleName = resources.GetString("mButtonQuit.AccessibleName");
			this.mButtonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("mButtonQuit.Anchor")));
			this.mButtonQuit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mButtonQuit.BackgroundImage")));
			this.mButtonQuit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("mButtonQuit.Dock")));
			this.mButtonQuit.Enabled = ((bool)(resources.GetObject("mButtonQuit.Enabled")));
			this.mButtonQuit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("mButtonQuit.FlatStyle")));
			this.mButtonQuit.Font = ((System.Drawing.Font)(resources.GetObject("mButtonQuit.Font")));
			this.mButtonQuit.Image = ((System.Drawing.Image)(resources.GetObject("mButtonQuit.Image")));
			this.mButtonQuit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mButtonQuit.ImageAlign")));
			this.mButtonQuit.ImageIndex = ((int)(resources.GetObject("mButtonQuit.ImageIndex")));
			this.mButtonQuit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("mButtonQuit.ImeMode")));
			this.mButtonQuit.Location = ((System.Drawing.Point)(resources.GetObject("mButtonQuit.Location")));
			this.mButtonQuit.Name = "mButtonQuit";
			this.mButtonQuit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mButtonQuit.RightToLeft")));
			this.mButtonQuit.Size = ((System.Drawing.Size)(resources.GetObject("mButtonQuit.Size")));
			this.mButtonQuit.TabIndex = ((int)(resources.GetObject("mButtonQuit.TabIndex")));
			this.mButtonQuit.Text = resources.GetString("mButtonQuit.Text");
			this.mButtonQuit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mButtonQuit.TextAlign")));
			this.mButtonQuit.Visible = ((bool)(resources.GetObject("mButtonQuit.Visible")));
			this.mButtonQuit.Click += new System.EventHandler(this.mButtonQuit_Click);
			// 
			// mButtonAbout
			// 
			this.mButtonAbout.AccessibleDescription = resources.GetString("mButtonAbout.AccessibleDescription");
			this.mButtonAbout.AccessibleName = resources.GetString("mButtonAbout.AccessibleName");
			this.mButtonAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("mButtonAbout.Anchor")));
			this.mButtonAbout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mButtonAbout.BackgroundImage")));
			this.mButtonAbout.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("mButtonAbout.Dock")));
			this.mButtonAbout.Enabled = ((bool)(resources.GetObject("mButtonAbout.Enabled")));
			this.mButtonAbout.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("mButtonAbout.FlatStyle")));
			this.mButtonAbout.Font = ((System.Drawing.Font)(resources.GetObject("mButtonAbout.Font")));
			this.mButtonAbout.Image = ((System.Drawing.Image)(resources.GetObject("mButtonAbout.Image")));
			this.mButtonAbout.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mButtonAbout.ImageAlign")));
			this.mButtonAbout.ImageIndex = ((int)(resources.GetObject("mButtonAbout.ImageIndex")));
			this.mButtonAbout.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("mButtonAbout.ImeMode")));
			this.mButtonAbout.Location = ((System.Drawing.Point)(resources.GetObject("mButtonAbout.Location")));
			this.mButtonAbout.Name = "mButtonAbout";
			this.mButtonAbout.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mButtonAbout.RightToLeft")));
			this.mButtonAbout.Size = ((System.Drawing.Size)(resources.GetObject("mButtonAbout.Size")));
			this.mButtonAbout.TabIndex = ((int)(resources.GetObject("mButtonAbout.TabIndex")));
			this.mButtonAbout.Text = resources.GetString("mButtonAbout.Text");
			this.mButtonAbout.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mButtonAbout.TextAlign")));
			this.mButtonAbout.Visible = ((bool)(resources.GetObject("mButtonAbout.Visible")));
			this.mButtonAbout.Click += new System.EventHandler(this.mButtonAbout_Click);
			// 
			// mLinkLabelAlfray
			// 
			this.mLinkLabelAlfray.AccessibleDescription = resources.GetString("mLinkLabelAlfray.AccessibleDescription");
			this.mLinkLabelAlfray.AccessibleName = resources.GetString("mLinkLabelAlfray.AccessibleName");
			this.mLinkLabelAlfray.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("mLinkLabelAlfray.Anchor")));
			this.mLinkLabelAlfray.AutoSize = ((bool)(resources.GetObject("mLinkLabelAlfray.AutoSize")));
			this.mLinkLabelAlfray.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("mLinkLabelAlfray.Dock")));
			this.mLinkLabelAlfray.Enabled = ((bool)(resources.GetObject("mLinkLabelAlfray.Enabled")));
			this.mLinkLabelAlfray.Font = ((System.Drawing.Font)(resources.GetObject("mLinkLabelAlfray.Font")));
			this.mLinkLabelAlfray.Image = ((System.Drawing.Image)(resources.GetObject("mLinkLabelAlfray.Image")));
			this.mLinkLabelAlfray.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mLinkLabelAlfray.ImageAlign")));
			this.mLinkLabelAlfray.ImageIndex = ((int)(resources.GetObject("mLinkLabelAlfray.ImageIndex")));
			this.mLinkLabelAlfray.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("mLinkLabelAlfray.ImeMode")));
			this.mLinkLabelAlfray.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("mLinkLabelAlfray.LinkArea")));
			this.mLinkLabelAlfray.Location = ((System.Drawing.Point)(resources.GetObject("mLinkLabelAlfray.Location")));
			this.mLinkLabelAlfray.Name = "mLinkLabelAlfray";
			this.mLinkLabelAlfray.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mLinkLabelAlfray.RightToLeft")));
			this.mLinkLabelAlfray.Size = ((System.Drawing.Size)(resources.GetObject("mLinkLabelAlfray.Size")));
			this.mLinkLabelAlfray.TabIndex = ((int)(resources.GetObject("mLinkLabelAlfray.TabIndex")));
			this.mLinkLabelAlfray.TabStop = true;
			this.mLinkLabelAlfray.Text = resources.GetString("mLinkLabelAlfray.Text");
			this.mLinkLabelAlfray.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mLinkLabelAlfray.TextAlign")));
			this.mLinkLabelAlfray.Visible = ((bool)(resources.GetObject("mLinkLabelAlfray.Visible")));
			this.mLinkLabelAlfray.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mLinkLabelAlfray_LinkClicked);
			// 
			// RMainForm
			// 
			this.AcceptButton = this.mButtonApply;
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.mLinkLabelAlfray);
			this.Controls.Add(this.mButtonAbout);
			this.Controls.Add(this.mButtonQuit);
			this.Controls.Add(this.mButtonApply);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.mTextDualTimeZoneShift);
			this.Controls.Add(this.label1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "RMainForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.RMainForm_Closing);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.RMainForm_Paint);
			this.ResumeLayout(false);

		}

		#endregion

		private System.ComponentModel.IContainer components;


		//-------------------------------------------
		//----------- Private Attributes ------------
		//-------------------------------------------

		private int mTimeOffset = 0;
		private int mCurrentHour = -1;
		private bool mFirstPaint = true;
		private string[] mProgArgs = new string[] { };
		private DualTimeZoneLib.Icons.RIconRef mIconRef = new DualTimeZoneLib.Icons.RIconRef();

		private RAboutForm mAboutForm = null;

		// forms
		private System.Windows.Forms.NotifyIcon mNotifyIcon;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox mTextDualTimeZoneShift;
		private System.Windows.Forms.Button mButtonApply;
		private System.Windows.Forms.ContextMenu mIconContextMenu;
		private System.Windows.Forms.MenuItem mMenuSettings;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mMenuExit;
		private System.Windows.Forms.Button mButtonQuit;
		private System.Windows.Forms.Button mButtonAbout;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.LinkLabel mLinkLabelAlfray;
		private System.Windows.Forms.Timer mTimer;




	} // class RMainForm
} // namespace Alfray.DualTimeZone.DualTimeZoneApp


//---------------------------------------------------------------
//	[C# Template RM 20040516]
//	$Log: RMainForm.cs,v $
//	Revision 1.4  2005-05-23 02:13:57  ralf
//	Added pref window skeleton.
//	Added load/save window settings for pref & debug windows.
//
//	Revision 1.3  2005/04/28 21:31:14  ralf
//	Using new LibUtils project
//	
//	Revision 1.2  2005/03/20 19:48:39  ralf
//	Added GPL headers.
//	
//	Revision 1.1  2005/02/18 23:21:52  ralf
//	Creating both an App and a Class Lib
//	
//	Revision 1.1.1.1  2005/02/18 22:54:53  ralf
//	A skeleton application template, with NUnit testing
//	
//---------------------------------------------------------------

