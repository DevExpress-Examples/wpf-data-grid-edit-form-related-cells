﻿using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.EditForm;
using System;
using System.Linq;
using System.Windows;

namespace SynchronizeEditValuesInEditForm_CodeBehind {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        bool locker = false;

        void EditFormCellValueChanging(object sender, CellValueChangedEventArgs e) {
            if(locker || !(e is CellValueChangedInEditFormEventArgs)) {
                return;
            }

            var editorData = ((CellValueChangedInEditFormEventArgs)e).EditorData;
            if(editorData.FieldName != nameof(DataItem.Price)) {
                return;
            }

            var editFormData = editorData.RowData;
            if(editFormData == null || editFormData.EditFormCellData == null) {
                return;
            }

            locker = true;

            var positionValueData = editFormData.EditFormCellData.Where(d => d is EditFormCellData).Cast<EditFormCellData>()
                .FirstOrDefault(d => d.FieldName == nameof(DataItem.PositionValue));
            var amountData = (EditFormCellData)editFormData.EditFormCellData
                .FirstOrDefault(d => d is EditFormCellData && ((EditFormCellData)d).FieldName == nameof(DataItem.Amount));

            try {
                positionValueData.Value = Convert.ToInt32(amountData.Value) * Convert.ToInt32(e.Value);
            } catch(FormatException ex) { } finally {
                locker = false;
            }
        }
    }
}