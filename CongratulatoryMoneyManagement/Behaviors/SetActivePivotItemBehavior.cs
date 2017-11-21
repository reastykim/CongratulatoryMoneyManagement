using CongratulatoryMoneyManagement.Views;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
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
            var pivotItemContent = pivotItem?.Content as FrameworkElement;
            if (pivotItemContent?.DataContext is IPivotItemActivate pivotItemDeactivate)
            {
                pivotItemDeactivate.OnPivotItemDeactived();
            }

            pivotItem = e.AddedItems.FirstOrDefault() as PivotItem;
            pivotItemContent = pivotItem?.Content as FrameworkElement;
            if (pivotItemContent?.DataContext is IPivotItemActivate pivotItemActivate)
            {
                pivotItemActivate.OnPivotItemActived();
            }
        }

        #endregion
    }
}
