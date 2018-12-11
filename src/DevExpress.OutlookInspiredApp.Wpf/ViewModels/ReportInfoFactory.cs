using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DevExpress.DevAV.Common.ViewModel;
//using DevExpress.DevAV.Reports;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraReports;

namespace DevExpress.DevAV.ViewModels {
    public static class ReportInfoFactory {
        #region Employee
        public static IReportInfo EmployeeTaskList(IEnumerable<EmployeeTask> tasks) {
            return GetReportInfo(SortByDateViewModel.Create(), p => null /*ReportFactory.EmployeeTaskList(tasks, p.SortDirection == SortByDatePrintMode.SortByDueDate)*/);
        }

        public static IReportInfo EmployeeProfile(Employee employee) {
            return GetReportInfo(EmployeeEvaluationsPrintModeViewModel.Create(), p => null /*employee == null ? null : ReportFactory.EmployeeProfile(employee, p.EmployeeEvaluationsPrintMode != EmployeeEvaluationsPrintMode.ExcludeEvaluations)*/);
        }

        public static IReportInfo EmployeeSummary(IEnumerable<Employee> employees) {
            return GetReportInfo(SortDirectionViewModel.Create(), p => null /*ReportFactory.EmployeeSummary(employees, p.SortDirection == SortOrderPrintMode.AscendingOrder)*/);
        }

        public static IReportInfo EmployeeDirectory(IEnumerable<Employee> employees) {
            return GetReportInfo(SortDirectionViewModel.Create(), p => null /*ReportFactory.EmployeeDirectory(employees, p.SortDirection == SortOrderPrintMode.AscendingOrder)*/);
        }
        #endregion

        #region Customer
        public static IReportInfo CusomerProfile(Customer customer) {
            return GetReportInfo(CustomerContactsPrintModeViewModel.Create(), p => null /*customer == null ? null : ReportFactory.CustomerProfile(customer, p.CustomerContactsPrintMode != CustomerContactsPrintMode.ExcludeContacts)*/);
        }
        public static IReportInfo CustomerContactsDirectory(Customer customer) {
            return GetReportInfo(SortDirectionViewModel.Create(),
                p => null /*ReportFactory.CustomerContactsDirectory(customer.Employees, p.SortDirection == SortOrderPrintMode.AscendingOrder)*/);
        }
        public static IReportInfo CustomerSalesDetail(IEnumerable<Order> orders) {
            return GetReportInfo(SortByAndDateRangeViewModel.Create(), p => null /*ReportFactory.CustomerSalesDetail(orders, orders.SelectMany(x => x.OrderItems).ToArray(), p.SortDirection == SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate)*/);
        }
        public static IReportInfo CustomerSalesDetailReport(IEnumerable<CustomerSaleDetailOrderInfo> orders) {
            var orderItems = orders.SelectMany(x => x.OrderItems).ToArray();
            return GetReportInfo(SortByAndDateRangeViewModel.Create(),
                p => null /*ReportFactory.CustomerSalesDetailReport(orders, orderItems, p.SortDirection == SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate)*/);
        }
        public static IReportInfo CustomerSalesSummary(IEnumerable<OrderItem> sales) {
            return GetReportInfo(SortByAndDateRangeViewModel.Create(), p => null /*ReportFactory.CustomerSalesSummary(sales, p.SortDirection == SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate)*/);
        }
        public static IReportInfo CustomerSalesSummaryReport(IEnumerable<CustomerSaleDetailOrderItemInfo> sales) {
            return GetReportInfo(SortByAndDateRangeViewModel.Create(), p => null /*ReportFactory.CustomerSalesSummaryReport(sales, p.SortDirection == SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate)*/);
        }
        public static IReportInfo CustomerLocationsDirectory(IEnumerable<Customer> customers) {
            return GetReportInfo(SortDirectionViewModel.Create(), p => null /*ReportFactory.CustomerLocationsDirectory(customers, p.SortDirection == SortOrderPrintMode.AscendingOrder)*/);
        }
        #endregion

        #region Order
        public static IReportInfo SalesInvoice(Order order) {
            return GetReportInfo(InvoiceOptionsViewModel.Create(), p => null /*ReportFactory.SalesInvoice(order, p.IsOptionSelected("ShowHeader"), p.IsOptionSelected("ShowFooter"),
                p.IsOptionSelected("ShowStatus"), p.IsOptionSelected("ShowComments"))*/);
        }
        public static IReportInfo SalesOrdersSummary(IEnumerable<OrderItem> sales) {
            return GetReportInfo(SortByAndDateRangeViewModel.Create(), p => null /*ReportFactory.SalesOrdersSummary(sales, p.SortDirection == SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate)*/);
        }
        public static IReportInfo SalesOrdersSummaryReport(IEnumerable<SaleSummaryInfo> sales) {
            return GetReportInfo(SortByAndDateRangeViewModel.Create(), p => null /*ReportFactory.SalesOrdersSummaryReport(sales, p.SortDirection == SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate)*/);
        }
        public static IReportInfo SalesAnalysis(IEnumerable<OrderItem> sales) {
            return GetReportInfo(SelectYearsViewModel.Create(), p => null /*ReportFactory.SalesAnalysis(sales, p.Years)*/);
        }
        public static IReportInfo SalesAnalysisReport(IEnumerable<SaleAnalisysInfo> sales) {
            return GetReportInfo(SelectYearsViewModel.Create(), p => null /*ReportFactory.SalesAnalysisReport(sales, p.Years)*/);
        }

        public static IReportInfo SalesRevenueReport(IEnumerable<OrderItem> sales) {
            return GetReportInfo(SortByAndDateRangeViewModel.Create(), p => null/*ReportFactory.SalesRevenueReport(sales, p.SortDirection == SortByPrintMode.SortByOrderDate)*/);
        }
        public static IReportInfo SalesRevenueAnalysisReport(IEnumerable<OrderItem> sales) {
            return GetReportInfo(SortByAndDateRangeViewModel.Create(), p => null/*ReportFactory.SalesRevenueAnalysisReport(sales, p.SortDirection == SortByPrintMode.SortByOrderDate)*/);
        }
        #endregion

        #region Task
        public static IReportInfo TaskListReport(IEnumerable<EmployeeTask> tasks) {
            return GetReportInfo(SortByDateViewModel.Create(), p => null/*ReportFactory.TaskListReport(tasks, p.SortDirection == SortByDatePrintMode.SortByDueDate)*/);
        }

        public static IReportInfo TaskDetailReport(EmployeeTask task) {
            return null;// new ParameterlessReportInfo(ReportFactory.TaskDetailReport(task));
        }
        #endregion

        #region Product
        public static IReportInfo ProductProfile(Product product) {
            return GetReportInfo(ProductImagesPrintModeViewModel.Create(), p => null/*ReportFactory.ProductProfile(product, p.ProductImagesPrintMode == ProductImagesPrintMode.DisplayProductImages)*/);
        }
        public static IReportInfo ProductOrders(IEnumerable<OrderItem> sales, IList<State> states) {
            return GetReportInfo(SortByAndDateRangeViewModel.Create(), p => null /*ReportFactory.ProductOrders(sales, states, p.SortDirection == SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate)*/);
        }
        public static IReportInfo ProductSalesSummary(IEnumerable<OrderItem> sales) {
            return GetReportInfo(SelectYearsViewModel.Create(), p => null /*ReportFactory.ProductSalesSummary(sales, p.Years)*/);
        }
        public static IReportInfo ProductTopSalesPerson(IEnumerable<OrderItem> sales) {
            return GetReportInfo(SortDirectionViewModel.Create(), p => null/*ReportFactory.ProductTopSalesPerson(sales, p.SortDirection == SortOrderPrintMode.AscendingOrder)*/);
        }
        #endregion

        static IReportInfo GetReportInfo<TParametersViewModel>(TParametersViewModel parametersViewModel, Func<TParametersViewModel, IReport> reportFactory) {
            return new ReportInfo<TParametersViewModel>(parametersViewModel, reportFactory);
        }
    }

    public class SortDirectionViewModel {
        public static SortDirectionViewModel Create() {
            return ViewModelSource.Create(() => new SortDirectionViewModel());
        }
        protected SortDirectionViewModel() { }
        public virtual SortOrderPrintMode SortDirection { get; set; }
    }

    public class SortDirectionAndDateRangeViewModel : SortDirectionViewModel {
        public new static SortDirectionAndDateRangeViewModel Create() {
            return ViewModelSource.Create(() => new SortDirectionAndDateRangeViewModel());
        }
        protected SortDirectionAndDateRangeViewModel() {
            FromDate = new DateTime(2011, 1, 1);
            ToDate = new DateTime(2013, 1, 1);
        }
        public virtual DateTime ToDate { get; set; }
        public virtual DateTime FromDate { get; set; }
    }

    public class SortByDateViewModel {
        public static SortByDateViewModel Create() {
            return ViewModelSource.Create(() => new SortByDateViewModel());
        }
        protected SortByDateViewModel() { }
        public virtual SortByDatePrintMode SortDirection { get; set; }
    }

    public class SortByViewModel {
        public static SortByViewModel Create() {
            return ViewModelSource.Create(() => new SortByViewModel());
        }
        protected SortByViewModel() { }
        public virtual SortByPrintMode SortDirection { get; set; }
    }

    public class SortByAndDateRangeViewModel : SortByViewModel {
        public new static SortByAndDateRangeViewModel Create() {
            return ViewModelSource.Create(() => new SortByAndDateRangeViewModel());
        }
        protected SortByAndDateRangeViewModel() {
            int releasePeriod = AssemblyInfo.VersionId % 10;
            int releaseYear = (int)(AssemblyInfo.VersionId / 10) + 2000;
            ToDate = new DateTime(releaseYear+1, releasePeriod == 1 ? 1 : 6, 1);
            FromDate = new DateTime(ToDate.Year - 3, 1, 1);
        }
        public virtual DateTime ToDate { get; set; }
        public virtual DateTime FromDate { get; set; }
    }

    public class EmployeeEvaluationsPrintModeViewModel {
        public static EmployeeEvaluationsPrintModeViewModel Create() {
            return ViewModelSource.Create(() => new EmployeeEvaluationsPrintModeViewModel());
        }
        protected EmployeeEvaluationsPrintModeViewModel() { }
        public virtual EmployeeEvaluationsPrintMode EmployeeEvaluationsPrintMode { get; set; }
    }

    public class CustomerContactsPrintModeViewModel {
        public static CustomerContactsPrintModeViewModel Create() {
            return ViewModelSource.Create(() => new CustomerContactsPrintModeViewModel());
        }
        protected CustomerContactsPrintModeViewModel() { }
        public virtual CustomerContactsPrintMode CustomerContactsPrintMode { get; set; }
    }

    public class SelectYearsViewModel {
        public static SelectYearsViewModel Create() {
            return ViewModelSource.Create(() => new SelectYearsViewModel());
        }
        protected SelectYearsViewModel() {
            AvailableYears = new List<string>() { GetYear(-3), GetYear(-2), GetYear(-1), GetYear() };
            Years = string.Format("{0},{1}", GetYear(-2), GetYear(-1));
        }
        string GetYear(int offset = 0) {
            int releaseYear = (int)(AssemblyInfo.VersionId / 10) + 2000;
            return (releaseYear + offset).ToString();
        }

        public List<string> AvailableYears { get; set; }
        public virtual string Years { get; set; }
    }

    public class InvoiceOptionsViewModel {
        public static InvoiceOptionsViewModel Create() {
            return ViewModelSource.Create(() => new InvoiceOptionsViewModel());
        }
        protected InvoiceOptionsViewModel() {
            AvailableOptions = new List<Tuple<string, string>>() {
                new Tuple<string, string>("ShowHeader", "Show Header"),
                new Tuple<string, string>("ShowFooter", "Show Footer"),
                new Tuple<string, string>("ShowStatus", "Show Status"),
                new Tuple<string, string>("ShowComments", "Show Comments")
            };
            SelectedOptions = new List<object>() { AvailableOptions[0] };
        }
        public List<Tuple<string, string>> AvailableOptions { get; set; }
        public virtual List<object> SelectedOptions { get; set; }

        public bool IsOptionSelected(string id) {
            if(SelectedOptions == null)
                return false;
            return SelectedOptions.Cast<Tuple<string, string>>().Any(x => object.Equals(x.Item1, id));
        }
    }

    public class ProductImagesPrintModeViewModel {
        public static ProductImagesPrintModeViewModel Create() {
            return ViewModelSource.Create(() => new ProductImagesPrintModeViewModel());
        }
        protected ProductImagesPrintModeViewModel() { }
        public virtual ProductImagesPrintMode ProductImagesPrintMode { get; set; }
    }

    public enum EmployeeEvaluationsPrintMode {
        [Image("/Resources/PrintExcludeEvaluations.svg")]
        ExcludeEvaluations,
        [Image("/Resources/PrintIncludeEvaluations.svg")]
        IncludeEvaluations,
    }
    public enum CustomerContactsPrintMode {
        [Image("/Resources/PrintIncludeEvaluations.svg")]
        IncludeContacts,
        [Image("/Resources/PrintExcludeEvaluations.svg")]
        ExcludeContacts,
    }
    public enum ProductImagesPrintMode {
        [Image("/Resources/ShowProduct.svg")]
        DisplayProductImages,
        [Image("/Resources/HideProduct.svg")]
        HideProductImages,
    }
    public enum SortOrderPrintMode {
        [Image("/Resources/SortAsc.svg")]
        AscendingOrder,
        [Image("/Resources/SortDesc.svg")]
        DescencingOrder
    }
    public enum SortByDatePrintMode {
        [Display(Name = "Sort by Due Date"), Image("/Resources/ShowDueDate.svg")]
        SortByDueDate,
        [Display(Name = "Sort by Start Date"), Image("/Resources/ShowStartDate.svg")]
        SortByStartDate
    }
    public enum SortByPrintMode {
        [Display(Name = "Sort by Order Date"), Image("/Resources/SortByOrderDate.svg")]
        SortByOrderDate,
        [Display(Name = "Sort by Invoice #"), Image("/Resources/SortByInvoice.svg")]
        SortByInvoice
    }
}