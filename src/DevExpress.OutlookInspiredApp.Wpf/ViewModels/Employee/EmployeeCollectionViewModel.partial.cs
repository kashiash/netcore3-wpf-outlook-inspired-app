using System;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;

namespace DevExpress.DevAV.ViewModels {
    partial class EmployeeCollectionViewModel : ISupportFiltering<Employee> {
        internal static NavigatorMapViewModel<Employee> CreateEmployeeMapViewModel(Employee employee, Action<Address> applyDestination) {
            return NavigatorMapViewModel<Employee>.Create(
                employee,
                AddressHelper.DevAVHomeOffice.ToString(),
                new GeoPoint(AddressHelper.DevAVHomeOffice.Latitude, AddressHelper.DevAVHomeOffice.Longitude),
                employee.Address.ToString(),
                new GeoPoint(employee.Address.Latitude, employee.Address.Longitude),
                applyDestination);
        }

        EmployeeContactsViewModel entityPanelViewModel;
        EmployeeQuickLetterViewModel quickLetter;
        ViewSettingsViewModel viewSettings;

        public EmployeeContactsViewModel EntityPanelViewModel {
            get {
                if(entityPanelViewModel == null)
                    entityPanelViewModel = EmployeeContactsViewModel.Create();
                return entityPanelViewModel;
            }
        }
        public ViewSettingsViewModel ViewSettings {
            get {
                if(viewSettings == null)
                    viewSettings = ViewSettingsViewModel.Create(CollectionViewKind.CardView, this);
                return viewSettings;
            }
        }
        public EmployeeQuickLetterViewModel QuickLetter {
            get {
                if(quickLetter == null)
                    quickLetter = EmployeeQuickLetterViewModel.Create().SetParentViewModel(this);
                return quickLetter;
            }
        }
        public virtual Employee TableViewSelectedEntity { get; set; }
        public virtual Employee CardViewSelectedEntity { get; set; }

        public void ShowMap() {
            var mapViewModel = CreateEmployeeMapViewModel(SelectedEntity, destination => {
                SelectedEntity.Address = destination;
                Save(SelectedEntity);
            });
            this.GetRequiredService<IDocumentManagerService>().CreateDocument("EmployeeMapView", mapViewModel, null, this).Show();
        }
        public bool CanShowMap() {
            return SelectedEntity != null;
        }

        public void PrintEmployeeProfile() {
        }
        public bool CanPrintEmployeeProfile() {
            return SelectedEntity != null;
        }
        public void PrintSummaryReport() {
        }
        public void PrintDirectory() {
        }
        public void PrintTaskList() {
            //ShowReport(ReportInfoFactory.TaskListReport(CreateUnitOfWork().Tasks.ToList()), "TaskList");
        }

        void ShowReport(IReportInfo reportInfo, string reportId) {
            ExportService.ShowReport(reportInfo);
            PrintService.ShowReport(reportInfo);
        }
        void SetDefaultReport(IReportInfo reportInfo) {
            if(this.IsInDesignMode()) return;
            ExportService.SetDefaultReport(reportInfo);
            PrintService.SetDefaultReport(reportInfo);
        }
        IReportService PrintService { get { return this.GetRequiredService<IReportService>("PrintService"); } }
        IReportService ExportService { get { return this.GetRequiredService<IReportService>("ExportService"); } }

        protected override void OnSelectedEntityChanged() {
            base.OnSelectedEntityChanged();
            QuickLetter.Entity = SelectedEntity;
            QuickLetter.RaisePropertyChanged(x => x.Entity);
            if(SelectedEntity != null)
                EntityPanelViewModel.Entity = SelectedEntity;
            TableViewSelectedEntity = SelectedEntity;
            CardViewSelectedEntity = SelectedEntity;
        }
        public override void UpdateSelectedEntity() {
            base.UpdateSelectedEntity();
            QuickLetter.RaisePropertyChanged(x => x.Entity);
            EntityPanelViewModel.RaisePropertyChanged(x => x.Entity);
        }
        protected override void OnEntitiesAssigned(Func<Employee> getSelectedEntityCallback) {
            base.OnEntitiesAssigned(getSelectedEntityCallback);
            if(Entities.Any() && SelectedEntity == null)
                SelectedEntity = Entities.OrderBy(x => x.FullName).FirstOrDefault();
        }
        public virtual void OnTableViewSelectedEntityChanged() {
            if(viewSettings.ViewKind == CollectionViewKind.ListView)
                SelectedEntity = TableViewSelectedEntity;
        }
        public virtual void OnCardViewSelectedEntityChanged() {
            if(viewSettings.ViewKind == CollectionViewKind.CardView)
                SelectedEntity = CardViewSelectedEntity;
        }

        public void ShowScheduler(string title) {
            MessageBoxService.Show(@"This demo does not include integration with our WPF Scheduler Suite but you can easily introduce Outlook-inspired scheduling and task management capabilities to your apps with DevExpress Tools.",
                    title, MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK);
        }
        public void CreateCustomFilter() {
            Messenger.Default.Send(new CreateCustomFilterMessage<Employee>());
        }
        public bool CanAddTask() {
            return SelectedEntity != null;
        }

        #region ISupportFiltering
        Expression<Func<Employee, bool>> ISupportFiltering<Employee>.FilterExpression {
            get { return FilterExpression; }
            set { FilterExpression = value; }
        }
        #endregion
    }
}