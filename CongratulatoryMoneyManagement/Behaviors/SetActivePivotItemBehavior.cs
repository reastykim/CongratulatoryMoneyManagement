using CongratulatoryMoneyManagement.Views;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CongratulatoryMoneyManagement.Behaviors
{
    public class SetActivePivotItemBehavior : Behavior<Pivot>
    {
        #region Properties

        public int ContentDepth { get; set; } = 1;

        #endregion

        #region Overrides of Behavior

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
            base.OnDetaching();
        }

        #endregion

        #region EventHandlers

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivotItem = e.RemovedItems.FirstOrDefault() as PivotItem;
            var content = GetContentItem(pivotItem);
            if (content?.DataContext is IPivotItemActivate pivotItemDeactivate)
            {
                pivotItemDeactivate.OnPivotItemDeactived();
                Debug.WriteLine($"OnPivotItemDeactived, [{content.GetType()}]");
            }

            pivotItem = e.AddedItems.FirstOrDefault() as PivotItem;
            content = GetContentItem(pivotItem);
            if (content?.DataContext is IPivotItemActivate pivotItemActivate)
            {
                pivotItemActivate.OnPivotItemActived();
                Debug.WriteLine($"OnPivotItemActived, [{content.GetType()}]");
            }
        }

        private FrameworkElement GetContentItem(PivotItem pivotItem)
        {
            dynamic dControl = pivotItem;
            for (int depth = 1; depth <= ContentDepth; depth++)
            {
                dControl = dControl?.Content;
            }

            return dControl as FrameworkElement;
        }

        #endregion
    }
}
