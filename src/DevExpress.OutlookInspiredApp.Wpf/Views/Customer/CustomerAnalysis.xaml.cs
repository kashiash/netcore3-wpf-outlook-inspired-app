using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.DevAV.ViewModels;
using DevExpress.Spreadsheet;

namespace DevExpress.DevAV.Views.Customer {
    public partial class CustomerAnalysis : UserControl {
        public CustomerAnalysis() {
            InitializeComponent();
            SubscribeEvents();
        }

        void SubscribeEvents() {
            Loaded += OnLoaded;
            spreadsheetControl.DocumentLoaded += OnDocumentLoaded;
        }
        
        void OnLoaded(object sender, RoutedEventArgs e) {
            ((Mvvm.ISplashScreenService)splashScreenService).ShowSplashScreen(string.Empty);
            LoadTemplate();
        }
        void LoadTemplate() {
            using(var stream = AnalysisTemplatesHelper.GetAnalysisTemplate(AnalysisTemplate.CustomerSales))
                spreadsheetControl.LoadDocument(stream, DocumentFormat.Xlsm);
        }

        void OnDocumentLoaded(object sender, EventArgs e) {            
            LoadData();
            ((Mvvm.ISplashScreenService)splashScreenService).HideSplashScreen();
        }
        void LoadData() {
        }
    }
}
