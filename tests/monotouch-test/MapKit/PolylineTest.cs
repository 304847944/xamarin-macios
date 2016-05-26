// Copyright 2011, 2013 Xamarin Inc. All rights reserved

#if !__TVOS__ && !__WATCHOS__

using System;
using System.Drawing;
#if XAMCORE_2_0
using Foundation;
using UIKit;
using CoreLocation;
using MapKit;
#else
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.MapKit;
using MonoTouch.UIKit;
#endif
using NUnit.Framework;

namespace MonoTouchFixtures.MapKit {
	
	[TestFixture]
	[Preserve (AllMembers = true)]
	public class PolylineTest {
		
		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void FromPoints_Null ()
		{
			MKPolyline.FromPoints (null);
		}
		
		void CheckEmpty (MKPolyline pl)
		{
			// MKAnnotation
			Assert.That (pl.Coordinate.Longitude, Is.NaN, "Coordinate.Longitude");
			if (TestRuntime.CheckSystemAndSDKVersion (7,0))
				Assert.That (pl.Coordinate.Latitude, Is.EqualTo (-90f), "Coordinate.Latitude");
			else
				Assert.That (pl.Coordinate.Latitude, Is.NaN, "Coordinate.Latitude");
			Assert.Null (pl.Title, "Title");
			Assert.Null (pl.Subtitle, "Subtitle");
			// MKOverlay
			Assert.True (Double.IsPositiveInfinity (pl.BoundingMapRect.Origin.X), "BoundingMapRect.Origin.X");
			Assert.True (Double.IsPositiveInfinity (pl.BoundingMapRect.Origin.Y), "BoundingMapRect.Origin.Y");
			if (TestRuntime.CheckSystemAndSDKVersion (7,0)) {
				Assert.That (pl.BoundingMapRect.Size.Height, Is.EqualTo (0.0f), "BoundingMapRect.Size.Height");
				Assert.That (pl.BoundingMapRect.Size.Width, Is.EqualTo (0.0f), "BoundingMapRect.Size.Width");
			} else {
				Assert.True (Double.IsNegativeInfinity (pl.BoundingMapRect.Size.Height), "BoundingMapRect.Size.Height");
				Assert.True (Double.IsNegativeInfinity (pl.BoundingMapRect.Size.Width), "BoundingMapRect.Size.Width");
			}
			Assert.False (pl.Intersects (pl.BoundingMapRect), "Intersect/Self");
			MKMapRect rect = new MKMapRect (0, 0, 0, 0);
			Assert.False (pl.Intersects (rect), "Intersect/Empty");
			
			ShapeTest.CheckShape (pl);
		}

		[Test]
		public void From_PointEmpty ()
		{
			MKPolyline pl = MKPolyline.FromPoints (new MKMapPoint [] { });
			CheckEmpty (pl);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void FromCoordinates_Null ()
		{
			MKPolyline.FromCoordinates (null);
		}
		
		[Test]
		public void FromCoordinates_Empty ()
		{
			MKPolyline pl = MKPolyline.FromCoordinates (new CLLocationCoordinate2D [] { });
			CheckEmpty (pl);
		}
		
#if false
		// Annotations that support dragging should implement this method to update the position of the annotation.
		// keyword is SHOULD - it's not working for MKPolyline
		// http://developer.apple.com/library/ios/#documentation/MapKit/Reference/MKAnnotation_Protocol/Reference/Reference.html#//apple_ref/occ/intf/MKAnnotation
		[Test]
		public void setCoordinate_Selector ()
		{
			MKPolyline pl = MKPolyline.FromPoints (new MKMapPoint [] { });
			try {
				pl.Coordinate = new CLLocationCoordinate2D (10, 20);
			}
			catch (MonoTouchException mte) {
				Assert.True (mte.Message.Contains ("unrecognized selector sent to instance"));
			}
			catch {
				Assert.Fail ("API could be working/implemented");
			}
		}
#endif
	}
}

#endif // !__TVOS__ && !__WATCHOS__
