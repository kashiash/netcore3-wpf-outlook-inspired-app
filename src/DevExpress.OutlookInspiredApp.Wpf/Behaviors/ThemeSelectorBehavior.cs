using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;

namespace DevExpress.DevAV.Common.View {
    public class ThemeSelectorBehavior : RibbonGalleryItemThemeSelectorBehavior {
        protected override ICollectionView CreateCollectionView() {
            ICollectionView view = CollectionViewSource.GetDefaultView(
                Theme.Themes.Where(t => t.ShowInThemeSelector
            && !t.Name.EndsWith(ThemeManager.TouchDelimiterAndDefinition, StringComparison.InvariantCultureIgnoreCase)
            && !t.Name.StartsWith("Office2013", StringComparison.InvariantCultureIgnoreCase)
            && !(t.Name.StartsWith("Office2016", StringComparison.InvariantCultureIgnoreCase) && !t.Name.EndsWith("SE", StringComparison.InvariantCulture)))
            .Select(t => new ThemeViewModel(t)).ToArray());
            view.GroupDescriptions.Add(new PropertyGroupDescription("Theme.Category"));
            return view;
        }
    }
}
