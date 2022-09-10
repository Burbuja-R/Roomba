using CommunityToolkit.Mvvm.ComponentModel;
using ELROOMBA.Contracts.Services;
using ELROOMBA.Views;
using Microsoft.UI.Xaml.Navigation;

namespace ELROOMBA.ViewModels;

public class ShellViewModel : ObservableRecipient
{
    private bool _isBackEnabled;
    private object? _selected;
    private string? _statetext;
    private int? _statecode;

    public INavigationService NavigationService
    {
        get;
    }

    public INavigationViewService NavigationViewService
    {
        get;
    }

    public bool IsBackEnabled
    {
        get => _isBackEnabled;
        set => SetProperty(ref _isBackEnabled, value);
    }

    public int? Statecode
    {
        get => _statecode;
        set => SetProperty(ref _statecode, value);
    }

    public string? StateText
    {
        get => _statetext;
        set => SetProperty(ref _statetext, value);
    }

    public object? Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }

    }

    public void SetData()
    {
        if (_statecode != null) { _statetext = $"Hemos detectado nuevos {Statecode} Errores"; }
        if(_statecode == 1) { _statetext = $"Hemos detectado un nuevo Error"; }
        if (_statecode == null) { _statetext = "No hemos detectado problemas"; }
    }
}
