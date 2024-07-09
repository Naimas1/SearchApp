using EO.Base;
using EO.Internal;
using EO.WebBrowser.DOM;
using static System.Net.Mime.MediaTypeNames;

using System.Xml.Linq;

< Window x: Class = "ImageSearchApp.MainWindow"
        xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"
        Title = "Image Search App" Height = "500" Width = "800" >
    < Grid >
        < StackPanel >
            < TextBox Name = "SearchTextBox" Width = "400" Height = "30" Margin = "10" />
            < StackPanel Orientation = "Horizontal" Margin = "10" >
                < CheckBox Name = "BingCheckBox" Content = "Bing" IsChecked = "True" Margin = "5" />
                < CheckBox Name = "GoogleCheckBox" Content = "Google" IsChecked = "True" Margin = "5" />
            </ StackPanel >
            < Button Content = "Search" Width = "100" Height = "30" Margin = "10" Click = "SearchButton_Click" />
            < ListBox Name = "ResultsListBox" Width = "750" Height = "350" Margin = "10" ScrollViewer.VerticalScrollBarVisibility = "Auto" >
                < ListBox.ItemTemplate >
                    < DataTemplate >
                        < StackPanel Orientation = "Horizontal" >
                            < Image Source = "{Binding ThumbnailUrl}" Width = "100" Height = "100" Margin = "5" />
                            < TextBlock Text = "{Binding Name}" VerticalAlignment = "Center" Margin = "5" />
                        </ StackPanel >
                    </ DataTemplate >
                </ ListBox.ItemTemplate >
            </ ListBox >
        </ StackPanel >
    </ Grid >
</ Window >
