﻿using System;
using NUnit.Framework;

#if !XAMCORE_2_0
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using MonoMac.Foundation;
#else
using AppKit;
using ObjCRuntime;
using Foundation;
#endif

namespace Xamarin.Mac.Tests
{
	public class NSVisualEffectViewTests
	{
		NSVisualEffectView view;

		[SetUp]
		public void SetUp ()
		{
			Asserts.EnsureYosemite ();

			view = new NSVisualEffectView ();
		}

		[Test]
		public void NSVisualEffectViewShouldChangeMaterial ()
		{
			var material = view.Material;
			view.Material = NSVisualEffectMaterial.Titlebar;

			Assert.IsFalse (view.Material == material, "NSVisualEffectViewShouldChangeMaterial - Failed to set the Material property");
		}

		[Test]
		public void NSVisualEffectViewShouldChangeBlendingMode ()
		{
			var blendingMode = view.BlendingMode;
			view.BlendingMode = NSVisualEffectBlendingMode.WithinWindow;

			Assert.IsFalse (view.BlendingMode == blendingMode, "NSVisualEffectViewShouldChangeBlendingMode - Failed to set the BlendingMode property");
		}

		[Test]
		public void NSVisualEffectViewShouldChangeState ()
		{
			var state = view.State;
			view.State = NSVisualEffectState.Inactive;

			Assert.IsFalse (view.State == state, "NSVisualEffectViewShouldChangeState - Failed to set the State property");
		}

		[Test]
		public void NSVisualEffectViewShouldChangeMaskImage ()
		{
			var image = view.MaskImage;
			view.MaskImage = new NSImage ();

			Assert.IsFalse (view.MaskImage == image, "NSVisualEffectViewShouldChangeMaskImage - Failed to set the MaskImage property");
		}
	}
}