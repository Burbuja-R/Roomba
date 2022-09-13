using System.CodeDom;
using System.Management;
using System.Numerics;
using CommunityToolkit.Mvvm.ComponentModel;
using ELROOMBA.Contracts.Services;
using ELROOMBA.Views;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace ELROOMBA.ViewModels;

public class ShellViewModel : ObservableRecipient
{
    #region --> Variables

    private bool _isBackEnabled;
    private object? _selected;
    private string? _statetext;
    private int? _statecode;
    private string? _stateexclamationtext;
    private int? _stateexclamationcode;
    private string? _stateerrortext;
    private int? _stateerrorcode;

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
    public int? StateErrorCode
    {
        get => _stateerrorcode;
        set => SetProperty(ref _stateerrorcode, value);
    }
    public string? StateErrorText
    {
        get => _stateerrortext;
        set => SetProperty(ref _stateerrortext, value);
    }
    public int? StateExclamationCode
    {
        get => _stateexclamationcode;
        set => SetProperty(ref _stateexclamationcode, value);
    }
    public string? StateExclamationText
    {
        get => _stateexclamationtext;
        set => SetProperty(ref _stateexclamationtext, value);
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

    #endregion
    public INavigationService NavigationService
    {
        get;
    }

    public INavigationViewService NavigationViewService
    {
        get;
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

    public async Task RestorePoint()
    {
        /*Se asegura de crear un punto de restauración, En caso de existir se asegura que sea reciente,
         en caso de no existir, Se creará uno de manera automatica*/
        await Task.Run(() =>
        {
            var LocalMachine = Registry.LocalMachine;
            var SystemRestore = LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore");
            string? RestorePointValue = SystemRestore.GetValue("RPSessionInterval").ToString();
            if (RestorePointValue.Contains("1"))
            {
                dynamic? restPoint = Interaction.GetObject("winmgmts:\\\\.\\root\\default:Systemrestore");
                if (restPoint != null)
                {
                    if (restPoint.CreateRestorePoint("ELRoomba", 0, 100) == 0) { _statecode = ++_statecode; _statetext = $"Hemos detectado un punto de restauracion Creado anteriormente"; }
                    else { _statecode = ++_statecode; _statetext = $"Hemos encontrado una error {new Exception()}"; }
                }
            }
            if (RestorePointValue.Contains("0"))
            {
                var osDrive = Path.GetPathRoot(Environment.SystemDirectory);
                var scope = new ManagementScope("\\\\localhost\\root\\default");
                var path = new ManagementPath("SystemRestore");
                var options = new ObjectGetOptions();
                var process = new ManagementClass(scope, path, options);
                var inParams = process.GetMethodParameters("Enable");
                inParams["WaitTillEnabled"] = true;
                inParams["Drive"] = osDrive;
                var outParams = process.InvokeMethod("Enable", inParams, null);
                dynamic? restPoint = Interaction.GetObject("winmgmts:\\\\.\\root\\default:Systemrestore");
                if (restPoint != null)
                {
                    if (restPoint.CreateRestorePoint("ELRoomba", 0, 100) == 0) { _statecode = ++_statecode; _statetext = $"Hemos creado el punto de restauración correctamente {restPoint}{DateTime.Now:h:mm:ss tt}"; }
                    else { _statecode = ++_statecode; _statetext = $"Hemos encontrado una error {new Exception()}"; }
                }
            }
        });
        await Task.CompletedTask;
    }


    public void SetDataAsync()
    {
        Statecode = ++Statecode;
        if (_statecode != null) { _statetext = $"Hemos detectado nuevos {Statecode} Errores"; }
        if (_statecode == 1) { _statetext = $"Hemos detectado un nuevo Error"; }
        if (_statecode == null) { _statetext = "No hemos detectado problemas"; }
        
    }
}
