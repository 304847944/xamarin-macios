//
// Unit tests for NSFileCoordinator
//
// Authors:
//	Sebastien Pouliot <sebastien@xamarin.com>
//
// Copyright 2012 Xamarin Inc. All rights reserved.
//

using System;
using System.IO;
#if XAMCORE_2_0
using Foundation;
using ObjCRuntime;
#else
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
#endif
using NUnit.Framework;

namespace MonoTouchFixtures.Foundation {
	
	[TestFixture]
	[Preserve (AllMembers = true)]
	public class FileCoordinatorTest {
		
		NSUrl GetUrl ()
		{
			return new NSUrl (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "FileCoordinatorTest.txt"));
		}
		
		bool fileop;
		
		void FileOp (NSUrl url)
		{
			fileop = true;
		}
		
		[Test]
		public void CoordinateRead ()
		{
			using (var url = GetUrl ())
			using (var fc = new NSFileCoordinator ()) {
				NSError err;
				fileop = false;
				fc.CoordinateRead (url, NSFileCoordinatorReadingOptions.WithoutChanges, out err, FileOp);
				Assert.True (fileop, "fileop/sync");
				Assert.Null (err, "NSError");
			}
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CoordinateRead_Null ()
		{
			using (var url = GetUrl ())
			using (var fc = new NSFileCoordinator ()) {
				NSError err;
				// NULL is not documented by Apple but it crash the app with:
				// NSFileCoordinator: A surprising server error was signaled. Details: Connection invalid
				fc.CoordinateRead (url, NSFileCoordinatorReadingOptions.WithoutChanges, out err, null);
			}
		}

		[Test]
		public void CoordinateWrite ()
		{
			using (var url = GetUrl ())
			using (var fc = new NSFileCoordinator ()) {
				NSError err;
				fileop = false;
				fc.CoordinateWrite (url, NSFileCoordinatorWritingOptions.ForDeleting, out err, FileOp);
				Assert.True (fileop, "fileop/sync");
				Assert.Null (err, "NSError");
			}
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CoordinateWrite_Null ()
		{
			using (var url = GetUrl ())
			using (var fc = new NSFileCoordinator ()) {
				NSError err;
				// NULL is not documented by Apple but it crash the app with:
				// NSFileCoordinator: A surprising server error was signaled. Details: Connection invalid
				fc.CoordinateWrite (url, NSFileCoordinatorWritingOptions.ForDeleting, out err, null);
			}
		}
		
		void FileOp (NSUrl url1, NSUrl url2)
		{
			fileop = true;
		}
		
		[Test]
		public void CoordinateReadWrite ()
		{
			using (var url = GetUrl ())
			using (var fc = new NSFileCoordinator ()) {
				NSError err;
				fileop = false;
				fc.CoordinateReadWrite (url, NSFileCoordinatorReadingOptions.WithoutChanges, url, NSFileCoordinatorWritingOptions.ForDeleting, out err, FileOp);
				Assert.True (fileop, "fileop/sync");
				Assert.Null (err, "NSError");
			}
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CoordinateReadWrite_Null ()
		{
			using (var url = GetUrl ())
			using (var fc = new NSFileCoordinator ()) {
				NSError err;
				// NULL is not documented by Apple but it crash the app with:
				// NSFileCoordinator: A surprising server error was signaled. Details: Connection invalid
				fc.CoordinateReadWrite (url, NSFileCoordinatorReadingOptions.WithoutChanges, url, NSFileCoordinatorWritingOptions.ForDeleting, out err, null);
			}
		}

		[Test]
		public void CoordinateWriteWrite ()
		{
			using (var url = GetUrl ())
			using (var fc = new NSFileCoordinator ()) {
				NSError err;
				fileop = false;
				fc.CoordinateWriteWrite (url, NSFileCoordinatorWritingOptions.ForMoving, url, NSFileCoordinatorWritingOptions.ForDeleting, out err, FileOp);
				Assert.True (fileop, "fileop/sync");
				Assert.Null (err, "NSError");
			}
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CoordinateWriteWrite_Null ()
		{
			using (var url = GetUrl ())
			using (var fc = new NSFileCoordinator ()) {
				NSError err;
				// NULL is not documented by Apple but it crash the app with:
				// NSFileCoordinator: A surprising server error was signaled. Details: Connection invalid
				fc.CoordinateWriteWrite (url, NSFileCoordinatorWritingOptions.ForMoving, url, NSFileCoordinatorWritingOptions.ForDeleting, out err, null);
			}
		}
		
		void Action ()
		{
			fileop = true;
		}
		
		[Test]
		public void CoordinateBatch_Action ()
		{
			using (var url = GetUrl ())
			using (var fc = new NSFileCoordinator ()) {
				NSError err;
				fileop = false;
				fc.CoordinateBatc (new NSUrl[] { url }, NSFileCoordinatorReadingOptions.WithoutChanges, new NSUrl[] { url }, NSFileCoordinatorWritingOptions.ForDeleting, out err, Action);
				Assert.True (fileop, "fileop/sync");
				Assert.Null (err, "NSError");
			}
		}
		
		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CoordinateBatch_Action_Null ()
		{
			using (var url = GetUrl ())
			using (var fc = new NSFileCoordinator ()) {
				NSError err;
				// NULL is not documented by Apple but it crash the app with:
				// NSFileCoordinator: A surprising server error was signaled. Details: Connection invalid
				fc.CoordinateBatc (new NSUrl[] { url }, NSFileCoordinatorReadingOptions.WithoutChanges, new NSUrl[] { url }, NSFileCoordinatorWritingOptions.ForDeleting, out err, null);
			}
		}
	}
}
