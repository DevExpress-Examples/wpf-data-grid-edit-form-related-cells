﻿using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SynchronizeEditValuesInEditForm_CodeBehind {
    public class DataItem {
        public int Amount { get; set; }

        public int Price { get; set; }

        public bool CanEdit { get; set; } = true;

        public int PositionValue { get => Price * Amount; }

        public DataItem(Random random) {
            Amount = random.Next(1, 10);
            Price = random.Next(100, 1000);
        }
    }

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            grid.ItemsSource = GetData(10).ToList();
        }

        static IEnumerable<DataItem> GetData(int amount) {
            var random = new Random();
            return Enumerable.Range(0, amount).Select(i => new DataItem(random));
        }

        void OnEditFormCellValueChanging(object sender, CellValueChangedEventArgs e) {
            CellValueChangedInEditFormEventArgs editFormArgs = e as CellValueChangedInEditFormEventArgs;
            if(editFormArgs == null) {
                return;
            }

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
    }
}
