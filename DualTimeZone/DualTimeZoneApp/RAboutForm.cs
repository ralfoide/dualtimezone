//*******************************************************************
/*

	Solution:	DualTimeZone
	Project:	DualTimeZoneApp
	File:		RAboutForm.cs

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
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

//*********************************
namespace Alfray.DualTimeZone.DualTimeZoneApp
{
	//**************************************
	/// <summary>
	/// Summary description for RAboutForm.
	/// </summary>
	public class RAboutForm : System.Windows.Forms.Form {
		//-------------------------------------------
		//----------- Public Constants --------------
		//-------------------------------------------


		//-------------------------------------------
		//----------- Public Properties -------------
		//-------------------------------------------

		
		//-------------------------------------------
		//----------- Public Methods ----------------
		//-------------------------------------------


		public RAboutForm(RMainForm owner)
		{
			mMainForm = owner;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// Initialize fields
			//
			init();
		}

		
		//-------------------------------------------
		//----------- Private Methods ---------------
		//-------------------------------------------


		//*******************
		private void init() {
			string name = this.ProductName;
			string version = this.ProductVersion;

			AssemblyCopyrightAttribute copyr = Attribute.GetCustomAttribute(
				Assembly.GetAssembly(this.GetType()),
				typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute;

			mLabelVersion.Text = version;
			mLabelAppName.Text = name;
			mLabelCopyright.Text =
				mLabelCopyright.Text.Replace("[copyright]", copyr.Copyright);
		}

		
		//-------------------------------------------
		//----------- Private Callbacks -------------
		//-------------------------------------------


		//************************************************
		private void mLinkLabel_LinkClicked(object sender,
			System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
		
			System.Diagnostics.Process.Start("http://www.alfray.com");
		}
		
		
		//************************************************
		private void mButtonClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		//************************************************
		private void RAboutForm_Closed(object sender, System.EventArgs e) {
			mMainForm.AboutForm = null;
		}

		//-------------------------------------------
		//----------- Private WinForms --------------
		//-------------------------------------------




		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RAboutForm));
			this.mLabelAppName = new System.Windows.Forms.Label();
			this.mButtonClose = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.mLinkLabel = new System.Windows.Forms.LinkLabel();
			this.mLabelCopyright = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.mLabelVersion = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// mLabelAppName
			// 
			this.mLabelAppName.AccessibleDescription = resources.GetString("mLabelAppName.AccessibleDescription");
			this.mLabelAppName.AccessibleName = resources.GetString("mLabelAppName.AccessibleName");
			this.mLabelAppName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("mLabelAppName.Anchor")));
			this.mLabelAppName.AutoSize = ((bool)(resources.GetObject("mLabelAppName.AutoSize")));
			this.mLabelAppName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mLabelAppName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("mLabelAppName.Dock")));
			this.mLabelAppName.Enabled = ((bool)(resources.GetObject("mLabelAppName.Enabled")));
			this.mLabelAppName.Font = ((System.Drawing.Font)(resources.GetObject("mLabelAppName.Font")));
			this.mLabelAppName.Image = ((System.Drawing.Image)(resources.GetObject("mLabelAppName.Image")));
			this.mLabelAppName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mLabelAppName.ImageAlign")));
			this.mLabelAppName.ImageIndex = ((int)(resources.GetObject("mLabelAppName.ImageIndex")));
			this.mLabelAppName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("mLabelAppName.ImeMode")));
			this.mLabelAppName.Location = ((System.Drawing.Point)(resources.GetObject("mLabelAppName.Location")));
			this.mLabelAppName.Name = "mLabelAppName";
			this.mLabelAppName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mLabelAppName.RightToLeft")));
			this.mLabelAppName.Size = ((System.Drawing.Size)(resources.GetObject("mLabelAppName.Size")));
			this.mLabelAppName.TabIndex = ((int)(resources.GetObject("mLabelAppName.TabIndex")));
			this.mLabelAppName.Text = resources.GetString("mLabelAppName.Text");
			this.mLabelAppName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mLabelAppName.TextAlign")));
			this.mLabelAppName.Visible = ((bool)(resources.GetObject("mLabelAppName.Visible")));
			// 
			// mButtonClose
			// 
			this.mButtonClose.AccessibleDescription = resources.GetString("mButtonClose.AccessibleDescription");
			this.mButtonClose.AccessibleName = resources.GetString("mButtonClose.AccessibleName");
			this.mButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("mButtonClose.Anchor")));
			this.mButtonClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mButtonClose.BackgroundImage")));
			this.mButtonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.mButtonClose.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("mButtonClose.Dock")));
			this.mButtonClose.Enabled = ((bool)(resources.GetObject("mButtonClose.Enabled")));
			this.mButtonClose.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("mButtonClose.FlatStyle")));
			this.mButtonClose.Font = ((System.Drawing.Font)(resources.GetObject("mButtonClose.Font")));
			this.mButtonClose.Image = ((System.Drawing.Image)(resources.GetObject("mButtonClose.Image")));
			this.mButtonClose.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mButtonClose.ImageAlign")));
			this.mButtonClose.ImageIndex = ((int)(resources.GetObject("mButtonClose.ImageIndex")));
			this.mButtonClose.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("mButtonClose.ImeMode")));
			this.mButtonClose.Location = ((System.Drawing.Point)(resources.GetObject("mButtonClose.Location")));
			this.mButtonClose.Name = "mButtonClose";
			this.mButtonClose.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mButtonClose.RightToLeft")));
			this.mButtonClose.Size = ((System.Drawing.Size)(resources.GetObject("mButtonClose.Size")));
			this.mButtonClose.TabIndex = ((int)(resources.GetObject("mButtonClose.TabIndex")));
			this.mButtonClose.Text = resources.GetString("mButtonClose.Text");
			this.mButtonClose.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mButtonClose.TextAlign")));
			this.mButtonClose.Visible = ((bool)(resources.GetObject("mButtonClose.Visible")));
			this.mButtonClose.Click += new System.EventHandler(this.mButtonClose_Click);
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
			// mLinkLabel
			// 
			this.mLinkLabel.AccessibleDescription = resources.GetString("mLinkLabel.AccessibleDescription");
			this.mLinkLabel.AccessibleName = resources.GetString("mLinkLabel.AccessibleName");
			this.mLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("mLinkLabel.Anchor")));
			this.mLinkLabel.AutoSize = ((bool)(resources.GetObject("mLinkLabel.AutoSize")));
			this.mLinkLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("mLinkLabel.Dock")));
			this.mLinkLabel.Enabled = ((bool)(resources.GetObject("mLinkLabel.Enabled")));
			this.mLinkLabel.Font = ((System.Drawing.Font)(resources.GetObject("mLinkLabel.Font")));
			this.mLinkLabel.Image = ((System.Drawing.Image)(resources.GetObject("mLinkLabel.Image")));
			this.mLinkLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mLinkLabel.ImageAlign")));
			this.mLinkLabel.ImageIndex = ((int)(resources.GetObject("mLinkLabel.ImageIndex")));
			this.mLinkLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("mLinkLabel.ImeMode")));
			this.mLinkLabel.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("mLinkLabel.LinkArea")));
			this.mLinkLabel.Location = ((System.Drawing.Point)(resources.GetObject("mLinkLabel.Location")));
			this.mLinkLabel.Name = "mLinkLabel";
			this.mLinkLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mLinkLabel.RightToLeft")));
			this.mLinkLabel.Size = ((System.Drawing.Size)(resources.GetObject("mLinkLabel.Size")));
			this.mLinkLabel.TabIndex = ((int)(resources.GetObject("mLinkLabel.TabIndex")));
			this.mLinkLabel.TabStop = true;
			this.mLinkLabel.Text = resources.GetString("mLinkLabel.Text");
			this.mLinkLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mLinkLabel.TextAlign")));
			this.mLinkLabel.Visible = ((bool)(resources.GetObject("mLinkLabel.Visible")));
			this.mLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mLinkLabel_LinkClicked);
			// 
			// mLabelCopyright
			// 
			this.mLabelCopyright.AccessibleDescription = resources.GetString("mLabelCopyright.AccessibleDescription");
			this.mLabelCopyright.AccessibleName = resources.GetString("mLabelCopyright.AccessibleName");
			this.mLabelCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("mLabelCopyright.Anchor")));
			this.mLabelCopyright.AutoSize = ((bool)(resources.GetObject("mLabelCopyright.AutoSize")));
			this.mLabelCopyright.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("mLabelCopyright.Dock")));
			this.mLabelCopyright.Enabled = ((bool)(resources.GetObject("mLabelCopyright.Enabled")));
			this.mLabelCopyright.Font = ((System.Drawing.Font)(resources.GetObject("mLabelCopyright.Font")));
			this.mLabelCopyright.Image = ((System.Drawing.Image)(resources.GetObject("mLabelCopyright.Image")));
			this.mLabelCopyright.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mLabelCopyright.ImageAlign")));
			this.mLabelCopyright.ImageIndex = ((int)(resources.GetObject("mLabelCopyright.ImageIndex")));
			this.mLabelCopyright.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("mLabelCopyright.ImeMode")));
			this.mLabelCopyright.Location = ((System.Drawing.Point)(resources.GetObject("mLabelCopyright.Location")));
			this.mLabelCopyright.Name = "mLabelCopyright";
			this.mLabelCopyright.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mLabelCopyright.RightToLeft")));
			this.mLabelCopyright.Size = ((System.Drawing.Size)(resources.GetObject("mLabelCopyright.Size")));
			this.mLabelCopyright.TabIndex = ((int)(resources.GetObject("mLabelCopyright.TabIndex")));
			this.mLabelCopyright.Text = resources.GetString("mLabelCopyright.Text");
			this.mLabelCopyright.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mLabelCopyright.TextAlign")));
			this.mLabelCopyright.Visible = ((bool)(resources.GetObject("mLabelCopyright.Visible")));
			// 
			// label4
			// 
			this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
			this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label4.Anchor")));
			this.label4.AutoSize = ((bool)(resources.GetObject("label4.AutoSize")));
			this.label4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label4.Dock")));
			this.label4.Enabled = ((bool)(resources.GetObject("label4.Enabled")));
			this.label4.Font = ((System.Drawing.Font)(resources.GetObject("label4.Font")));
			this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
			this.label4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.ImageAlign")));
			this.label4.ImageIndex = ((int)(resources.GetObject("label4.ImageIndex")));
			this.label4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label4.ImeMode")));
			this.label4.Location = ((System.Drawing.Point)(resources.GetObject("label4.Location")));
			this.label4.Name = "label4";
			this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label4.RightToLeft")));
			this.label4.Size = ((System.Drawing.Size)(resources.GetObject("label4.Size")));
			this.label4.TabIndex = ((int)(resources.GetObject("label4.TabIndex")));
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.TextAlign")));
			this.label4.Visible = ((bool)(resources.GetObject("label4.Visible")));
			// 
			// mLabelVersion
			// 
			this.mLabelVersion.AccessibleDescription = resources.GetString("mLabelVersion.AccessibleDescription");
			this.mLabelVersion.AccessibleName = resources.GetString("mLabelVersion.AccessibleName");
			this.mLabelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("mLabelVersion.Anchor")));
			this.mLabelVersion.AutoSize = ((bool)(resources.GetObject("mLabelVersion.AutoSize")));
			this.mLabelVersion.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("mLabelVersion.Dock")));
			this.mLabelVersion.Enabled = ((bool)(resources.GetObject("mLabelVersion.Enabled")));
			this.mLabelVersion.Font = ((System.Drawing.Font)(resources.GetObject("mLabelVersion.Font")));
			this.mLabelVersion.Image = ((System.Drawing.Image)(resources.GetObject("mLabelVersion.Image")));
			this.mLabelVersion.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mLabelVersion.ImageAlign")));
			this.mLabelVersion.ImageIndex = ((int)(resources.GetObject("mLabelVersion.ImageIndex")));
			this.mLabelVersion.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("mLabelVersion.ImeMode")));
			this.mLabelVersion.Location = ((System.Drawing.Point)(resources.GetObject("mLabelVersion.Location")));
			this.mLabelVersion.Name = "mLabelVersion";
			this.mLabelVersion.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mLabelVersion.RightToLeft")));
			this.mLabelVersion.Size = ((System.Drawing.Size)(resources.GetObject("mLabelVersion.Size")));
			this.mLabelVersion.TabIndex = ((int)(resources.GetObject("mLabelVersion.TabIndex")));
			this.mLabelVersion.Text = resources.GetString("mLabelVersion.Text");
			this.mLabelVersion.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("mLabelVersion.TextAlign")));
			this.mLabelVersion.Visible = ((bool)(resources.GetObject("mLabelVersion.Visible")));
			// 
			// label6
			// 
			this.label6.AccessibleDescription = resources.GetString("label6.AccessibleDescription");
			this.label6.AccessibleName = resources.GetString("label6.AccessibleName");
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label6.Anchor")));
			this.label6.AutoSize = ((bool)(resources.GetObject("label6.AutoSize")));
			this.label6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label6.Dock")));
			this.label6.Enabled = ((bool)(resources.GetObject("label6.Enabled")));
			this.label6.Font = ((System.Drawing.Font)(resources.GetObject("label6.Font")));
			this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
			this.label6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.ImageAlign")));
			this.label6.ImageIndex = ((int)(resources.GetObject("label6.ImageIndex")));
			this.label6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label6.ImeMode")));
			this.label6.Location = ((System.Drawing.Point)(resources.GetObject("label6.Location")));
			this.label6.Name = "label6";
			this.label6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label6.RightToLeft")));
			this.label6.Size = ((System.Drawing.Size)(resources.GetObject("label6.Size")));
			this.label6.TabIndex = ((int)(resources.GetObject("label6.TabIndex")));
			this.label6.Text = resources.GetString("label6.Text");
			this.label6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.TextAlign")));
			this.label6.Visible = ((bool)(resources.GetObject("label6.Visible")));
			// 
			// label7
			// 
			this.label7.AccessibleDescription = resources.GetString("label7.AccessibleDescription");
			this.label7.AccessibleName = resources.GetString("label7.AccessibleName");
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label7.Anchor")));
			this.label7.AutoSize = ((bool)(resources.GetObject("label7.AutoSize")));
			this.label7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label7.Dock")));
			this.label7.Enabled = ((bool)(resources.GetObject("label7.Enabled")));
			this.label7.Font = ((System.Drawing.Font)(resources.GetObject("label7.Font")));
			this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
			this.label7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.ImageAlign")));
			this.label7.ImageIndex = ((int)(resources.GetObject("label7.ImageIndex")));
			this.label7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label7.ImeMode")));
			this.label7.Location = ((System.Drawing.Point)(resources.GetObject("label7.Location")));
			this.label7.Name = "label7";
			this.label7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label7.RightToLeft")));
			this.label7.Size = ((System.Drawing.Size)(resources.GetObject("label7.Size")));
			this.label7.TabIndex = ((int)(resources.GetObject("label7.TabIndex")));
			this.label7.Text = resources.GetString("label7.Text");
			this.label7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.TextAlign")));
			this.label7.Visible = ((bool)(resources.GetObject("label7.Visible")));
			// 
			// RAboutForm
			// 
			this.AcceptButton = this.mButtonClose;
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.mLinkLabel);
			this.Controls.Add(this.mButtonClose);
			this.Controls.Add(this.mLabelAppName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.mLabelCopyright);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.mLabelVersion);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
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
			this.Name = "RAboutForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closed += new System.EventHandler(this.RAboutForm_Closed);
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.Button mButtonClose;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel mLinkLabel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label mLabelAppName;
		private System.Windows.Forms.Label mLabelCopyright;
		private System.Windows.Forms.Label mLabelVersion;

		// forms
		private System.ComponentModel.Container components = null;


		//-------------------------------------------
		//----------- Private Attributes ------------
		//-------------------------------------------

		RMainForm mMainForm = null;


	}
}
