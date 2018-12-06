using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Design.Mvvm;
using DevExpress.DevAV;
using DevExpress.Entity.Model;
using DevExpress.Entity.ProjectModel;
using DevExpress.Xpf.Core.Design.Wizards.Mvvm;
using DevExpress.Xpf.Core.Design.Wizards.Mvvm.EntityFramework;
using DevExpress.Xpf.Core.Design.Wizards.Utils;
using NUnit.Framework;

namespace DevExpress.OutlookInspiredApp.Wpf.Generator {
    [TestFixture]
    public class Generator {
        static void Main() {
            new Generator().Generate();
        }
        [Test]
        public void Generate() {
            DevExpress.Entity.Model.DescendantBuilding.Internal.SqlExpressDescendantBuilderConfig.UseUserInstance = true;
            DevExpress.Xpf.Core.Design.Wizards.Mvvm.EntityFramework.DescendantBuilderFactory.DoNotUseLocalDbForTests = true;
            StandaloneTemplatesCodeGen codeGen = new StandaloneTemplatesCodeGen(typeof(SqlProviderServices), GetType().Assembly, typeof(DevAVDb).Assembly);
            codeGen.CollectionViewModelHooks = new T4TemplateHook[] {
                new T4TemplateHook(
                    @", query => query.OrderBy(x => x.Category)",
                    TemplatesCodeGen.STR_CollectionViewModelHook_GenerateIncludes,
                    IT4TemplateExtensions.GetViewModelByNameFilter("ProductCollectionViewModel"), false),
                new T4TemplateHook(
                    @", query => query.OrderBy(x => x.Name)",
                    TemplatesCodeGen.STR_CollectionViewModelHook_GenerateIncludes,
                    IT4TemplateExtensions.GetViewModelByNameFilter("CustomerCollectionViewModel"), false),
                new T4TemplateHook(
                    @", query => query.Include(x => x.Store).Include(x => x.Customer).ActualOrders().OrderBy(x => x.InvoiceNumber)",
                    TemplatesCodeGen.STR_CollectionViewModelHook_GenerateIncludes,
                    IT4TemplateExtensions.GetViewModelByNameFilter("OrderCollectionViewModel"), false),
                new T4TemplateHook(
                    @", query => QueriesHelper.GetQuoteInfo(query)",
                    TemplatesCodeGen.STR_CollectionViewModelHook_GenerateIncludes,
                    IT4TemplateExtensions.GetViewModelByNameFilter("QuoteCollectionViewModel"), false),
                new T4TemplateHook(
                    @" QuoteInfo,",
                    TemplatesCodeGen.STR_CollectionViewModelHook_GenerateProjectionType,
                    IT4TemplateExtensions.GetViewModelByNameFilter("QuoteCollectionViewModel"), false),
            };
            string baseFolder = Path.GetDirectoryName(GetType().Assembly.Location);
            codeGen.TargetFolder = Path.GetFullPath(Path.Combine(baseFolder, @"..\.."));
            codeGen.DefaultNamespace = "DevExpress.DevAV";
            IDataModel dataModel = codeGen.CreateDataModel(typeof(DevAVDb));
            Assert.IsNotNull(dataModel);
            codeGen.GenerateDocumentsView(dataModel, UIType.Standard, DocumentsGenerationOptions.CollectionViewModels, "Customer", "Order", "Quote");
            codeGen.GenerateDocumentsView(dataModel, UIType.Standard, DocumentsGenerationOptions.AllViewModels, "Employee", "Product");
        }
    }
}
