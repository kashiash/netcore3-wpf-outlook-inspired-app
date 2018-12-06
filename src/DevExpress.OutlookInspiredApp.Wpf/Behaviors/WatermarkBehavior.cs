using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Pdf;
using DevExpress.Xpf.PdfViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace DevExpress.DevAV.Common.View {
    public class WatermarkBehavior : Behavior<PdfViewerControl> {
        readonly static PdfStringFormat watermarkFormat;
        public static readonly DependencyProperty DocumentSourceProperty =
            DependencyProperty.Register("DocumentSource", typeof(Stream), typeof(WatermarkBehavior), new PropertyMetadata(null, (d, e) => ((WatermarkBehavior)d).UpdateDocumnt()));
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(WatermarkBehavior), new PropertyMetadata(string.Empty, (d, e) => ((WatermarkBehavior)d).UpdateDocumnt()));

        public Stream DocumentSource { get { return (Stream)GetValue(DocumentSourceProperty); } set { SetValue(DocumentSourceProperty, value); } }
        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }

        static WatermarkBehavior() {
            watermarkFormat = new PdfStringFormat();
            watermarkFormat.FormatFlags = PdfStringFormatFlags.NoWrap | PdfStringFormatFlags.NoClip;
            watermarkFormat.Alignment = PdfStringAlignment.Center;
            watermarkFormat.LineAlignment = PdfStringAlignment.Center;
        }

        protected override void OnAttached() {
            base.OnAttached();
            UpdateDocumnt();
        }
        void UpdateDocumnt() {
            if(AssociatedObject == null || string.IsNullOrEmpty(Text) || DocumentSource == null) {
                AssociatedObject.DocumentSource = null;
                return;
            }
        }
    }
}
