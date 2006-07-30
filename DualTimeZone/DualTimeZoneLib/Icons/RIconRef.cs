//*******************************************************************
/* 

 		Project:	DualTimeZoneLib
 		File:		RIconRef.cs

*/ 
//*******************************************************************

using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Collections;

//*************************************
namespace Alfray.DualTimeZone.DualTimeZoneLib.Icons
{
	//***************************************************
	/// <summary>
	/// Summary description for RIconRef.
	/// </summary>
	public class RIconRef
	{
		//-------------------------------------------
		//----------- Public Constants --------------
		//-------------------------------------------


		//-------------------------------------------
		//----------- Public Properties -------------
		//-------------------------------------------


		//-------------------------------------------
		//----------- Public Methods ----------------
		//-------------------------------------------

		//***************
		public RIconRef()
		{
			mIcons = new Icon[24];
		}


		//*******************************
		public Icon GetHourIcon(int hour)
		{
			System.Diagnostics.Debug.Assert(hour >= 0 && hour < 24);

			if (mIcons[hour] != null)
				return mIcons[hour] as Icon;

			// Get it from a file if it so exists
			string filename = String.Format("H{0:d2}.ico", hour);
			Assembly assembly = Assembly.GetAssembly(this.GetType());
			Stream stream = assembly.GetManifestResourceStream(this.GetType(), filename);
			if (stream != null) {
				Icon ic = new Icon(stream);
				mIcons[hour] = ic;
				return ic;
			}

			return null;
		}
		

		//-------------------------------------------
		//----------- Private Methods ---------------
		//-------------------------------------------


		//-------------------------------------------
		//----------- Private Attributes ------------
		//-------------------------------------------

		private Icon[] mIcons = null;
		

	} // class RIconRef
} // namespace Alfray.DualTimeZone.DualTimeZoneLib.Icons


//---------------------------------------------------------------
//	[C# Template RM 20040516]
//	$Log: RLibMain.cs,v $
//	Revision 1.1  2005-02-18 23:21:52  ralf
//
//---------------------------------------------------------------
