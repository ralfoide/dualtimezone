//*******************************************************************
/*

	Solution:	DualTimeZone
	Project:	DualTimeZoneApp
	File:		RPrefConstants.cs

	Copyright 2003, 2004, Raphael MOLL.

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

//*************************************
namespace Alfray.DualTimeZone.DualTimeZoneApp
{
	//***************************************************
	/// <summary>
	/// Summary description for RPrefConstants.
	/// </summary>
	public class RPrefConstants
	{
		//-------------------------------------------
		//----------- Public Constants --------------
		//-------------------------------------------

		// UI

		public const string kMainForm = "forms-main";
		public const string kPrefForm = "forms-pref";
		public const string kDebugForm = "forms-debug";

		public const string kFormVisible = "forms-main-vis";
		public const string kTimeOffset = "time-offset";

	} // class RPrefConstants
} // namespace Alfray.DualTimeZone.DualTimeZoneApp


//---------------------------------------------------------------
//	[C# Template RM 20040516]
//	$Log: RPrefConstants.cs,v $
//	Revision 1.1  2005-05-23 02:13:57  ralf
//	Added pref window skeleton.
//	Added load/save window settings for pref & debug windows.
//
//---------------------------------------------------------------
