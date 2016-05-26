﻿//
// Unit tests for NSTimer
//
// Authors:
//	Rolf Bjarne Kvinge <rolf@xamarin.com>
//
// Copyright 2014 Xamarin Inc. All rights reserved.
//

using System;
using System.Reflection;
#if XAMCORE_2_0
using Foundation;
using ObjCRuntime;
#else
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
#endif
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MonoTouchFixtures.Foundation {

	[TestFixture]
	[Preserve (AllMembers = true)]
	public class TimerTest {
		[Test]
		public void Bug17793 ()
		{
			var evt = new CountdownEvent (2);

			NSTimer timer = null;

			using (timer = NSTimer.CreateRepeatingTimer (0.1f, delegate {
				// This is the real test - not a timer test, but properly preserving VFP registers.
				// When bug #17793 manifests, this method is only called once instead of repeatedly.
				Func (0, 0, 0, 0, 0, 0, 0);
				if (evt.Signal ())
					timer.Invalidate ();
			})) {

				var thread = new Thread (() => {
					NSRunLoop.Current.AddTimer (timer, NSRunLoopMode.Default);
					NSRunLoop.Current.RunUntil (NSRunLoopMode.Default, NSDate.Now.AddSeconds (5));
				})
				{
					IsBackground = true,
				};
				thread.Start ();

				Assert.IsTrue (evt.Wait (TimeSpan.FromSeconds (5)), "Not signalled twice in 5s");
				thread.Join ();
			}
		}

		void Func (double m00, double m01, double m02, double m03, double m10, double m11, double m12)
		{
		}

#if XAMCORE_2_0
		[Test]
		public void CreateTimer_NewSignature ()
		{
			bool result = false;
			var evt = new ManualResetEvent (false);

			NSTimer timer = null;

			using (timer = NSTimer.CreateTimer (0.1f, (NSTimer t) => {
				result = t != null && t.Handle == timer.Handle;
				evt.Set ();
			})) {

				var thread = new Thread (() => {
					NSRunLoop.Current.AddTimer (timer, NSRunLoopMode.Default);
					NSRunLoop.Current.RunUntil (NSRunLoopMode.Default, NSDate.Now.AddSeconds (5));
				})
				{
					IsBackground = true,
				};
				thread.Start ();

				Assert.IsTrue (evt.WaitOne (TimeSpan.FromSeconds (5)), "WaitOne");
				Assert.IsTrue (result, "result");
				thread.Join ();
			}
		}
#endif
	}
}
