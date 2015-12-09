using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DataIntegrationTool.MainProgram.Dialogs.ExceptionDialog
{
    /// <summary>
    /// A WPF window for viewing Exceptions and inner Exceptions, including all their properties.
    /// </summary>
    public partial class ExceptionDialogView
    {
        private readonly Exception _exception;

        // Font sizes based on the "normal" size.
        private readonly double _small;
        private readonly double _med;
        private readonly double _large;

        // This is used to dynamically calculate the mainGrid.MaxWidth when the Window is resized,
        // since I can't quite get the behavior I want without it.  See CalcMaxTreeWidth().
        private double _chromeWidth;

        /// <summary>
        /// The _exception and header message cannot be null.  If owner is specified, this window
        /// uses its Style and will appear centered on the Owner.  You can override this before
        /// calling ShowDialog().
        /// </summary>
        public ExceptionDialogView(string headerMessage, Exception e)
        {
            InitializeComponent();

            _small = treeView1.FontSize;
            _med = _small * 1.1;
            _large = _small * 1.2;

            BuildTree(e, headerMessage);
            _exception = e;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // The grid column used for the tree started with Width="Auto" so it is now exactly
            // wide enough to fit the longest _exception (up to the MaxWidth set in XAML).
            // Changing the width to a fixed pixel value prevents it from changing if the user
            // resizes the window.

            treeCol.Width = new GridLength(treeCol.ActualWidth, GridUnitType.Pixel);
            _chromeWidth = ActualWidth - mainGrid.ActualWidth;
            CalcMaxTreeWidth();
        }        

        // Builds the tree in the left pane.
        // Each TreeViewItem.Tag will contain a list of Inlines
        // to display in the right-hand pane When it is selected.
        private void BuildTree(Exception e, string summaryMessage)
        {
            // The first node in the tree contains the summary message and all the
            // nested _exception messages.
            
            var inlines = new List<Inline>();
            var firstItem = new TreeViewItem {Header = "All Messages"};
            treeView1.Items.Add(firstItem);

            var inline = new Bold(new Run(summaryMessage)) {FontSize = _large};
            inlines.Add(inline);

            // Now add top-level nodes for each _exception while building
            // the contents of the first node.
            while (e != null)
            {
                inlines.Add(new LineBreak());
                inlines.Add(new LineBreak());
                AddLines(inlines, e.Message);

                AddException(e);
                e = e.InnerException;
            }

            firstItem.Tag = inlines;
            firstItem.IsSelected = true;
        }

        private void AddProperty(List<Inline> inlines, string propName, object propVal)
        {
            inlines.Add(new LineBreak());
            inlines.Add(new LineBreak());
            var inline = new Bold(new Run(propName + ":")) {FontSize = _med};
            inlines.Add(inline);
            inlines.Add(new LineBreak());

            var propValString = propVal as string;
            if (propValString != null)
            {
                // Might have embedded newlines.
                AddLines(inlines, propValString);
            }
            else
            {
                inlines.Add(new Run(propVal.ToString()));
            }
        }

        // Adds the string to the list of Inlines, substituting
        // LineBreaks for an newline chars found.
        private void AddLines(List<Inline> inlines, string str)
        {
            var lines = str.Split('\n');

            inlines.Add(new Run(lines[0].Trim('\r')));

            foreach (var line in lines.Skip(1))
            {
                inlines.Add(new LineBreak());
                inlines.Add(new Run(line.Trim('\r')));
            }
        }

        // Adds the _exception as a new top-level node to the tree with child nodes
        // for all the _exception's properties.
        private void AddException(Exception e)
        {
            // Create a list of Inlines containing all the properties of the _exception object.
            // The three most important properties (message, type, and stack trace) go first.

            var exceptionItem = new TreeViewItem();
            var inlines = new List<Inline>();
            var properties = e.GetType().GetProperties();

            exceptionItem.Header = e.GetType();
            exceptionItem.Tag = inlines;
            treeView1.Items.Add(exceptionItem);

            Inline inline = new Bold(new Run(e.GetType().ToString()));
            inline.FontSize = _large;
            inlines.Add(inline);

            AddProperty(inlines, "Message", e.Message);
            AddProperty(inlines, "Stack Trace", e.StackTrace);

            foreach (var info in properties)
            {
                // Skip InnerException because it will get a whole
                // top-level node of its own.

                if (info.Name == "InnerException") continue;
                var value = info.GetValue(e, null);

                if (value == null) continue;
                if (value is string)
                {
                    if (string.IsNullOrEmpty(value as string)) continue;
                }
                else if (value is IDictionary)
                {
                    value = RenderDictionary(value as IDictionary);
                    if (string.IsNullOrEmpty((string) value)) continue;
                }
                else if (value is IEnumerable)
                {
                    value = RenderEnumerable(value as IEnumerable);
                    if (string.IsNullOrEmpty((string) value)) continue;
                }

                if (info.Name != "Message" &&
                    info.Name != "StackTrace")
                {
                    // Add the property to list for the exceptionItem.
                    AddProperty(inlines, info.Name, value);
                }

                // Create a TreeViewItem for the individual property.
                var propertyItem = new TreeViewItem();
                var propertyInlines = new List<Inline>();

                propertyItem.Header = info.Name;
                propertyItem.Tag = propertyInlines;
                exceptionItem.Items.Add(propertyItem);
                AddProperty(propertyInlines, info.Name, value);
            }
        }

        private static string RenderEnumerable(IEnumerable data)
        {
            var result = new StringBuilder();

            foreach (var obj in data)
            {
                result.AppendFormat("{0}\n", obj);
            }

            if (result.Length > 0) result.Length = result.Length - 1;
            return result.ToString();
        }

        private static string RenderDictionary(IDictionary data)
        {
            var result = new StringBuilder();

            foreach (var key in data.Keys.Cast<object>().Where(key => key != null && data[key] != null))
            {
                result.AppendLine(key + " = " + data[key]);
            }

            if (result.Length > 0) result.Length = result.Length - 1;
            return result.ToString();
        }

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ShowCurrentItem();
        }

        private void ShowCurrentItem()
        {
            var treeViewItem = treeView1.SelectedItem as TreeViewItem;
            if (treeViewItem == null) return;
            var inlines = treeViewItem.Tag as List<Inline>;
            var doc = new FlowDocument
            {
                FontSize = _small,
                FontFamily = treeView1.FontFamily,
                TextAlignment = TextAlignment.Left,
                Background = docViewer.Background
            };
        
            var para = new Paragraph();
            para.Inlines.AddRange(inlines);
            doc.Blocks.Add(para);
            docViewer.Document = doc;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            // Build a FlowDocument with Inlines from all top-level tree items.

            var inlines = new List<Inline>();
            var doc = new FlowDocument();
            var para = new Paragraph();

            doc.FontSize = _small;
            doc.FontFamily = treeView1.FontFamily;
            doc.TextAlignment = TextAlignment.Left;

            foreach (TreeViewItem treeItem in treeView1.Items)
            {
                if (inlines.Any())
                {
                    // Put a line of underscores between each _exception.

                    inlines.Add(new LineBreak());
                    inlines.Add(new Run("____________________________________________________"));
                    inlines.Add(new LineBreak());
                }

                inlines.AddRange(treeItem.Tag as List<Inline>);
            }

            para.Inlines.AddRange(inlines);
            doc.Blocks.Add(para);

            // Now place the doc contents on the clipboard in both
            // rich text and plain text format.

            var range = new TextRange(doc.ContentStart, doc.ContentEnd);
            var data = new DataObject();

            using (Stream stream = new MemoryStream())
            {
                range.Save(stream, DataFormats.Rtf);
                data.SetData(DataFormats.Rtf, Encoding.UTF8.GetString(((MemoryStream) stream).ToArray()));
            }

            data.SetData(DataFormats.StringFormat, range.Text);
            Clipboard.SetDataObject(data);

            // The Inlines that were being displayed are now in the temporary document we just built,
            // causing them to disappear from the viewer.  This puts them back.

            ShowCurrentItem();
        }

        private void ExpressionViewerWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                CalcMaxTreeWidth();
            }
        }

        private void CalcMaxTreeWidth()
        {
            // This prevents the GridSplitter from being dragged beyond the right edge of the window.
            // Another way would be to use star sizing for all Grid columns including the left 
            // Grid column (i.e. treeCol), but that causes the width of that column to change when the
            // window's width changes, which I don't like.

            mainGrid.MaxWidth = ActualWidth - _chromeWidth;
            treeCol.MaxWidth = mainGrid.MaxWidth - textCol.MinWidth;
        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            ShowCurrentItem();
            Process.Start("mailto:Steven.Southwick@quintiles.com?subject=Error Occurred in Data Integration Tool&body=" + _exception);
            e.Handled = true;
        }
    }
}
