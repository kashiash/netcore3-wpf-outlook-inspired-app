using DevExpress.Xpf.Core;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System.Diagnostics;

namespace DevExpress.DevAV.ViewModels {
    public class LinksViewModel {
        public static LinksViewModel Create() {
            return ViewModelSource.Create(() => new LinksViewModel());
        }
        protected LinksViewModel() { }

        public void GettingStarted() {
            Process.Start(new ProcessStartInfo("cmd", $"/c start https://go.devexpress.com/Demo_GetStarted_WPF.aspx"));
        }
        public void GetFreeSupport() {
            Process.Start(new ProcessStartInfo("cmd", $"/c start Https://go.devexpress.com/Demo_2013_GetSupport.aspx"));
        }
        public void BuyNow() {
            Process.Start(new ProcessStartInfo("cmd", $"/c start https://go.devexpress.com/Demo_Subscriptions_Buy.aspx"));
        }
        public void UniversalSubscription() {
            Process.Start(new ProcessStartInfo("cmd", $"/c start https://go.devexpress.com/Demo_UniversalSubscription.aspx"));
        }
        public void About() {
        }
    }
}