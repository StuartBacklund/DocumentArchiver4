﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using DocumentArchiver.Helpers;

namespace DocumentArchiver.Converters
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.IsNotNull())
            {
                if ((value as string).Contains(@"\"))
                {
                    Uri uri = new Uri("pack://application:,,,/Images/diskdrive.png");
                    BitmapImage source = new BitmapImage(uri);
                    return source;
                }
                else
                {
                    Uri uri = new Uri("pack://application:,,,/Images/folder.png");
                    BitmapImage source = new BitmapImage(uri);
                    return source;
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}