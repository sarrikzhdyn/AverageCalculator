using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageViewerWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Изображения (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|Все файлы (*.*)|*.*",
                Title = "Выберите изображение"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Загрузка выбранного изображения
                    imgViewer.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    zoomSlider.Value = 1.0; // Сброс масштаба при загрузке нового изображения
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при открытии изображения: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Применение масштаба к изображению
            if (imgViewer.Source != null)
            {
                imgViewer.LayoutTransform = new ScaleTransform(zoomSlider.Value, zoomSlider.Value);
            }
        }
    }
}