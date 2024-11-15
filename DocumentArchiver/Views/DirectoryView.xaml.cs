﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DocumentArchiver.Commands;
using DocumentArchiver.Helpers;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.Prism.PubSubEvents;

namespace DocumentArchiver.Views
{
    /// <summary>
    /// Interaction logic for DirectoryView.xaml
    /// </summary>
    public partial class DirectoryView : UserControl
    {
        private object dummyNode = null;
        public string SelectedImagePath { get; set; }

        private DirectoryInfo workingDirectory { get; set; }

        private IEventAggregator eventAggregator;

        public string DocumentName
        {
            get { return (string)GetValue(DocumentNameProperty); }
            set { SetValue(DocumentNameProperty, value); }
        }

        public static readonly DependencyProperty DocumentNameProperty =
            DependencyProperty.Register("DocumentName", typeof(string), typeof(DirectoryView));

        public string SelectedInputPath
        {
            get { return (string)GetValue(SelectedInputPathProperty); }
            set { SetValue(SelectedInputPathProperty, value); }
        }

        public static readonly DependencyProperty SelectedInputPathProperty =
            DependencyProperty.Register("SelectedInputPath", typeof(string), typeof(DirectoryView), new UIPropertyMetadata(string.Empty));

        public char[] AlphabetList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public DirectoryView()
        {
            InitializeComponent();
        }

        private void ucLoaded(object sender, RoutedEventArgs e)
        {
            if (this.eventAggregator == null)
            {
                this.eventAggregator = SimpleIoc.Default.GetInstance<IEventAggregator>();
            }

            LoadedDirectoryInformation();
        }

        private void LoadedDirectoryInformation()
        {
            foreach (string s in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                foldersItem.Items.Add(item);
            }
        }

        private void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void foldersItem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = (TreeView)sender;
            TreeViewItem temp = ((TreeViewItem)tree.SelectedItem);

            if (temp.IsNull())
                return;
            SelectedImagePath = string.Empty;
            string temp1 = string.Empty;
            string temp2 = string.Empty;
            while (true)
            {
                temp1 = temp.Header.ToString();
                if (temp1.Contains(@"\"))
                {
                    temp2 = string.Empty;
                }
                SelectedImagePath = string.Concat(temp1, temp2, SelectedImagePath);
                if (temp.Parent.GetType().Equals(typeof(TreeView)))
                {
                    break;
                }
                temp = ((TreeViewItem)temp.Parent);
                temp2 = @"\";
            }

            workingDirectory = new DirectoryInfo(SelectedImagePath);
            SelectedInputPath = workingDirectory.FullName.ToString();
            eventAggregator.GetEvent<ListDirectoryContentsEvent>().Publish(SelectedInputPath);
        }
    }
}