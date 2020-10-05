using Android.App;
using Android.Widget;
using Android.OS;
using Android.Webkit;

namespace WebViewTest
{
    [Activity(Label = "WebViewTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //create our object instance
        WebView myWebView;

        protected override void OnCreate(Bundle bundle)
        {
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            base.OnCreate(bundle);

            myWebView = FindViewById<WebView>(Resource.Id.webView1);
            myWebView.SetWebViewClient(new WebViewClient());
            myWebView.Settings.JavaScriptEnabled = true;
            //myWebView.LoadUrl("http://rhysma.github.io");
            myWebView.LoadUrl("file:///android_asset/starter.htm");
        }

        public override void OnBackPressed()
        {
            myWebView.GoBack();
        }

    }

}

