using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DealCIM
{
    class CimExtensionViewModel : ViewModelBase
    {
        public CimExtensionViewModel()
        {
            AddCommand = new RelayCommand(() => Contents.Add(Content));
            DeleteCommand = new RelayCommand(() => Contents.Remove(SelectedContent));
            SaveCommand = new RelayCommand(()=>
            {
                CimExtensionService.GetInstance().Save();
                MessageBox.Show("保存完成");
            });
        }

        #region peroperties
        private string _content = string.Empty;
        public string Content
        {
            get => _content;
            set => Set(this.Content, ref _content, value);
        }

        private string _selectedContent = null;
        public string SelectedContent
        {
            get => _selectedContent;
            set => Set(SelectedContent, ref _selectedContent, value);
        }

        public ObservableCollection<string> Contents => CimExtensionService.GetInstance().GetContents();
        #endregion

        #region command
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        #endregion
    }
}
