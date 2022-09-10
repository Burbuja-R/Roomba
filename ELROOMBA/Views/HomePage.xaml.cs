using System.Collections.ObjectModel;
using ELROOMBA.DataModel;
using ELROOMBA.ViewModels;
using Microsoft.UI.Xaml.Controls;
namespace ELROOMBA.Views;

public sealed partial class HomePage : Page
{
    private readonly List<ButtonInfo>? ButtonsInfo;

    public HomeViewModel ViewModel
    {
        get;
    }
    public HomePage()
    {
        ViewModel = App.GetService<HomeViewModel>();
        InitializeComponent();
        ButtonsInfo = ButtonManager.GetButtons();
    }

}
