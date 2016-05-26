#if !XAMCORE_2_0
using MonoMac.Foundation;
#else
using Foundation;
#endif

namespace Test
{
	[BaseType (typeof (NSObject))]
	[Protocol, Model]
	public interface NSTextInputClient
	{
		[Export ("selectedRange")]
		NSRange SelectedRange { get; }
	}
	
	[BaseType (typeof (NSObject))]
	public interface NSText
	{
		[Export ("selectedRange")]
		NSRange SelectedRange { get; set; }
	}

	[BaseType (typeof (NSText))]
	public interface NSTextView : NSTextInputClient
	{
	}
}