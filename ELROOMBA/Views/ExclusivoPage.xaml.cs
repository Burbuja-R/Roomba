using ELROOMBA.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace ELROOMBA.Views;

public sealed partial class ExclusivoPage : Page
{
    public ExclusivoViewModel ViewModel
    {
        get;
    }

    public ExclusivoPage()
    {
        ViewModel = App.GetService<ExclusivoViewModel>();
        InitializeComponent();
    }
}
