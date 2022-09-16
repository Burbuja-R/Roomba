using System.CodeDom;
using System.Management;
using System.Numerics;
using CommunityToolkit.Mvvm.ComponentModel;
using ELROOMBA.Contracts.Services;
using ELROOMBA.Views;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace ELROOMBA.ViewModels;

public class ShellViewModel : ObservableRecipient
{
    #region --> Variables

    private bool _isBackEnabled;
    private object? _selected;
    private string? _NotificationTitle;
    private string? _NotificationMessage;


    public bool IsBackEnabled
    {
        get => _isBackEnabled; set => SetProperty(ref _isBackEnabled, value);
    }
    public object? Selected
    {
        get => _selected; set => SetProperty(ref _selected, value);
    }
    public string? NotificationTitle
    {
        get => _NotificationTitle; set => SetProperty(ref _NotificationTitle, value);
    }
    public string? NotificationMessage
    {
        get => _NotificationMessage; set => SetProperty(ref _NotificationMessage, value);
    }
    public bool IsNotificationOpen { get; set; } = false;
    public bool CanCloseNotification { get; set; } = false;
    public bool NotificationIconsVisible { get; set; } = false;





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
            if (RestorePointValue != null)
            {
                if (RestorePointValue.Contains('1'))
                {
                    dynamic? restPoint = Interaction.GetObject("winmgmts:\\\\.\\root\\default:Systemrestore");
                    if (restPoint != null)
                    {
                        IsNotificationOpen = true;
                        NotificationIconsVisible = true;
                        CanCloseNotification = true;
                        NotificationTitle = $"ELRoomba";
                        NotificationMessage = $"Hemos detectado un punto de restauracion ya hecho previamente, Puede cerrar esta pestaña";

                    }
                }
                else
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
                        if (restPoint.CreateRestorePoint("ELRoomba", 0, 100) == 0)
                        {
                            IsNotificationOpen = true;
                            NotificationIconsVisible = true;
                            CanCloseNotification = true;
                            NotificationTitle = $"ELRoomba";
                            NotificationMessage = $"Se ha creado un Punto de restauración a la Hora {DateTime.Now.ToString("HH:mm")} ";
                        }
                    }
                }
            }
            else
            {
            }

        });
        await Task.CompletedTask;
    }


    public async void SetDataAsync()
    {
        for (var i = 0; i < 5; i++)
        {
            await RestorePoint();
        }
    }
}
