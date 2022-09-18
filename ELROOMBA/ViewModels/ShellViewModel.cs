using System.Management;
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
    private string? _NotificationTitle;
    private string? _NotificationMessage;
    private string? _NotificationsIconSource;
    private bool _IsBusy = false;

    public bool IsBusy
    {
        get => _IsBusy; set => SetProperty(ref _IsBusy, value);
    }
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
    public string? NotificationsIconSource
    {
        get => _NotificationsIconSource; set => SetProperty(ref _NotificationsIconSource, value);
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
            var RestorePointValue = SystemRestore.GetValue("RPSessionInterval").ToString();
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
                        _NotificationsIconSource = $"ms-appx:///Assets/Exclamacion.png";
                        _NotificationTitle = $"ELRoomba";
                        _NotificationMessage = $"Hemos detectado un punto de restauracion ya hecho previamente, Puede cerrar esta pestaña";

                    }
                }
                if (RestorePointValue.Contains('0'))
                {
                    try
                    {
                            ManagementScope oScope = new ManagementScope("\\\\localhost\\root\\default");
                            ManagementPath oPath = new ManagementPath("SystemRestore");
                            ObjectGetOptions oGetOp = new ObjectGetOptions();
                            ManagementClass oProcess = new ManagementClass(oScope, oPath, oGetOp);

                            ManagementBaseObject oInParams =
                                 oProcess.GetMethodParameters("CreateRestorePoint");
                            oInParams["Description"] = "si";
                            oInParams["RestorePointType"] = 12;
                            oInParams["EventType"] = 100;
                             
                            ManagementBaseObject oOutParams = oProcess.InvokeMethod("CreateRestorePoint", oInParams, null);

                            IsNotificationOpen = true;
                            NotificationIconsVisible = true;
                            CanCloseNotification = true;
                            _NotificationsIconSource = $"ms-appx:///Assets/Exclamacion.png";
                            _NotificationTitle = $"ELRoomba";
                            _NotificationMessage = $"Funciona";

                    }
                    catch (Exception ex)
                    {
                        IsNotificationOpen = true;
                        NotificationIconsVisible = true;
                        CanCloseNotification = true;
                        _NotificationsIconSource = $"ms-appx:///Assets/Exclamacion.png";
                        _NotificationTitle = $"ELRoomba";
                        _NotificationMessage = $"no Funciona" + ex.Message;
                    }
                   

                }
            }
            else
            {
                IsNotificationOpen = true;
                NotificationIconsVisible = true;
                CanCloseNotification = false;
                _NotificationTitle = $"ELRoomba";
                _NotificationsIconSource = $"ms-appx:///Assets/Error.png";
                _NotificationMessage = $"Ha ocurrido un error al crear el punto de restauración, porfavor haga uno manualmente ";
            }
            return Task.CompletedTask;
        });
    }

    public async void SetDataAsync()
    {
        await RestorePoint();
    }
}
