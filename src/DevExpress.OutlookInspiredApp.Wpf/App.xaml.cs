using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using DevExpress.Images;
using DevExpress.Internal;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoLauncher;

namespace DevExpress.DevAV {
    public partial class App : Application {
        static IDisposable singleInstanceApplicationGuard;
        static App()
        {
            ApplicationThemeHelper.UseLegacyDefaultTheme = true;
        }

        protected override void OnStartup(StartupEventArgs e) {
            ExceptionHelper.Initialize();
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
            DevAVDataDirectoryHelper.LocalPrefix = "WpfOutlookInspiredApp";
            ImagesAssemblyLoader.Load();
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(200));
            base.OnStartup(e);
            ViewLocator.Default = new ViewLocator(typeof(App).Assembly);
            bool exit;
            singleInstanceApplicationGuard = DevAVDataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfOutlookInspiredApp", out exit);
            if(exit) {
                Shutdown();
                return;
            }
            Theme.TouchlineDark.ShowInThemeSelector = false;
            //ApplicationThemeHelper.ApplicationThemeName = Theme.Office2016ColorfulSE.Name;
            SetCultureInfo();
        }
        static void SetCultureInfo() {
            CultureInfo demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            demoCI.NumberFormat.CurrencySymbol = "$";
            demoCI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentCulture = demoCI;
            CultureInfo demoUI = (CultureInfo)Thread.CurrentThread.CurrentUICulture.Clone();
            demoUI.NumberFormat.CurrencySymbol = "$";
            demoUI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentUICulture = demoUI;
        }
        static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args) {
            string partialName = DevExpress.Utils.AssemblyHelper.GetPartialName(args.Name).ToLower();
            if(partialName == "entityframework" || partialName == "system.data.sqlite" || partialName == "system.data.sqlite.ef6") {
                string path = Path.Combine(Path.GetDirectoryName(typeof(App).Assembly.Location), partialName + ".dll");
                return Assembly.LoadFrom(path);
            }
            return null;
        }
    }
}