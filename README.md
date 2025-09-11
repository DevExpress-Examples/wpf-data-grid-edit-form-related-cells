<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/393277465/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1037808)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Data Grid for WPF - How to Process Related Cells in the Edit Form

This example illustrates how to process related cells in the Edit Form. This Edit Form contains information about goods. A user can change the `Price` value if the `CanEdit` checkbox is checked. `PositionValue` is the result of `Price` and `Amount` multiplication. Handle the [RowEditStarting](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditStarting) event to initialize values in editors when a user starts to edit the row. The [CellValueChanging](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridViewBase.CellValueChanging) event handler disables the `Price` editor depending on the `CanEdit` value and calculates `PositionValue`. 

```cs
void OnEditFormCellValueChanging(object sender, CellValueChangedEventArgs e) {
    CellValueChangedInEditFormEventArgs editFormArgs = e as CellValueChangedInEditFormEventArgs;
    //...
    if(e.Cell.Property == nameof(DataItem.CanEdit)) {
        var priceData = editFormArgs.CellEditors.FirstOrDefault(x => x.FieldName == nameof(DataItem.Price));
        priceData.ReadOnly = !bool.Parse(e.Cell.Value.ToString());
        return;     
    }
    if(e.Cell.Property == nameof(DataItem.Price)) {
        var positionValueData = editFormArgs.CellEditors.First(d => d.FieldName == nameof(DataItem.PositionValue));
        var amountData = editFormArgs.CellEditors.First(d => d.FieldName == nameof(DataItem.Amount));

         int price = 0;

         int.TryParse((string)e.Value, out price);
         positionValueData.Value = (int)amountData.Value * price;
    }
}

private void OnRowEditStarting(object sender, RowEditStartingEventArgs e) {
    var priceData = e.CellEditors.FirstOrDefault(x => x.FieldName == nameof(DataItem.Price));
    var canEditData = e.CellEditors.FirstOrDefault(x => x.FieldName == nameof(DataItem.CanEdit));
    priceData.ReadOnly = !(bool)canEditData.Value;
}

```

Alternatively, you can create commands in a View Model and bind them to the [RowEditStartingCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditStartingCommand) and [CellValueChangingCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridViewBase.CellValueChangingCommand) properties.

In the [TreeListView](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView), use the following events and properties: 
- [NodeEditStarting](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditStarting)
- [TreeListView.CellValueChanging](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.CellValueChanging)
- [NodeEditStartingCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditStartingCommand)
- [TreeListView.CellValueChangingCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.CellValueChangingCommand)

<!-- default file list -->

## Files to Look At

### Code-Behind
- [MainViewModel.xaml.cs](./CS/SynchronizeEditValuesInEditForm_CodeBehind/MainWindow.xaml.cs#L34-L55) ([MainViewModel.xaml.vb](./VB/SynchronizeEditValuesInEditForm_CodeBehind/MainWindow.xaml.vb#L42-L61))
- [MainWindow.xaml](./CS/SynchronizeEditValuesInEditForm_CodeBehind/MainWindow.xaml#L19) ([MainWindow.xaml](./VB/SynchronizeEditValuesInEditForm_CodeBehind/MainWindow.xaml#L19))

### MVVM Pattern
- [MainViewModel.cs](./CS/SynchronizeEditValuesInEditForm_MVVM/MainViewModel.cs#L38-L60) ([MainViewModel.vb](./VB/SynchronizeEditValuesInEditForm_MVVM/MainViewModel.vb#L45-L65))
- [MainWindow.xaml](./CS/SynchronizeEditValuesInEditForm_MVVM/MainWindow.xaml#L22) ([MainWindow.xaml](./VB/SynchronizeEditValuesInEditForm_MVVM/MainWindow.xaml#L22))

<!-- default file list end -->

## Documentation

- [Edit Form](https://docs.devexpress.com/WPF/403491/controls-and-libraries/data-grid/data-editing-and-validation/modify-cell-values/edit-form)
- [RowEditStarting](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditStarting) / [NodeEditStarting](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditStarting)
- [RowEditStartingCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditStartingCommand) / [NodeEditStartingCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditStartingCommand)
- [CellValueChanging](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridViewBase.CellValueChanging) / [TreeListView.CellValueChanging](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.CellValueChanging)
- [CellValueChangingCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridViewBase.CellValueChangingCommand) / [TreeListView.CellValueChangingCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.CellValueChangingCommand)

## More Examples
- [Data Grid for WPF - How to Specify Edit Form Settings](https://github.com/DevExpress-Examples/wpf-data-grid-specify-edit-form-settings)
- [Data Grid for WPF - How to Pause Data Updates in the Edit Form](https://github.com/DevExpress-Examples/wpf-data-grid-edit-form-pause-updates)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-edit-form-related-cells&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-edit-form-related-cells&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
