using System.Xml;

namespace ELROOMBA.DataModel;

public class ButtonManager
{
    public static List<ButtonInfo> GetButtons()
    {
        var Button = new List<ButtonInfo>();

        Button.Add(new ButtonInfo
        {
            UniqueID = "#1",
            CommonID = "SystemTweaks",
            Title = "SystemTweaks",
            Description = "Ajusta Windows para obtener el maximo rendimiento",
            ImagePath = "ms-appx:///Assets/System.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });
        Button.Add(new ButtonInfo
        {
            UniqueID = "#2",
            CommonID = "SystemTweaks",
            Title = "Mouse/KeyboardDataQueueSize",
            Description = "Ajusta la memoria dedicada al raton, para menor input lag",
            ImagePath = "ms-appx:///Assets/Mouse.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });
        Button.Add(new ButtonInfo
        {
            UniqueID = "#3",
            CommonID = "SystemTweaks",
            Title = "Win32PrioritySeparation",
            Description = "Ajusta la memoria dedicada de los programas al sistema",
            ImagePath = "ms-appx:///Assets/Windows.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });
        Button.Add(new ButtonInfo
        {
            UniqueID = "#4",
            CommonID = "SecurityTweaks",
            Title = "Delete Mitigation",
            Description = "Esta opcion Elimina Los procesos asociados a las mitigaciones,Haciendo el sistema mas seguro",
            ImagePath = "ms-appx:///Assets/Servicio.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });
        Button.Add(new ButtonInfo
        {
            UniqueID = "#5",
            CommonID = "SecurityTweaks",
            Title = "Disable Windows Updates",
            Description = "Fuerza a Windows a nunca actualizarse, esto es conveniente si esta en una version antigua a la actual, y no quieres que se actualize",
            ImagePath = "ms-appx:///Assets/Updates.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });
        Button.Add(new ButtonInfo
        {
            UniqueID = "#6",
            CommonID = "SecurityTweaks",
            Title = "Disable Spectre and Meltdown",
            Description = "Son vulnerabilidades en el CPU, Por eso es recomendable desactivarlas",
            ImagePath = "ms-appx:///Assets/Spectre.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });
        Button.Add(new ButtonInfo
        {
            UniqueID = "#7",
            CommonID = "SystemTweaks",
            Title = "Display Real monitor RefreshRate",
            Description = "Hace que el monitor Muestre Siempre la tasa de Hz más alta que este admita",
            ImagePath = "ms-appx:///Assets/Monitor.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });
        Button.Add(new ButtonInfo
        {
            UniqueID = "#8",
            CommonID = "SecurityTweaks",
            Title = "DMA Remmaping",
            Description = "Deshabilita ciertas configuraciones de seguridad de los drivers instalados por microsoft, Para menor Latencia",
            ImagePath = "ms-appx:///Assets/Driver.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });
        Button.Add(new ButtonInfo
        {
            UniqueID = "#9",
            CommonID = "SecurityTweaks",
            Title = "Disable FullScreenOptimization",
            Description = "Deshabilita todas las optimizaciones a pantalla completa de windows",
            ImagePath = "ms-appx:///Assets/FSO.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });
        Button.Add(new ButtonInfo
        {
            UniqueID = "#10",
            CommonID = "SecurityTweaks",
            Title = "Disable MCSS",
            Description = "Deshabilita El servicios MCSS, que es un Servicio capaz de conectarse a internet y registrar las teclas que pulsas",
            ImagePath = "ms-appx:///Assets/MCSS.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });
        Button.Add(new ButtonInfo
        {
            UniqueID = "#11",
            Title = "Disable Powerthrottling",
            CommonID = "EnergyTweaks",
            Description = "Es una caracteristica que permite ahorrar bateria al usar multiples ventanas u programas",
            ImagePath = "ms-appx:///Assets/PowerThrottling.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });
        Button.Add(new ButtonInfo
        {
            UniqueID = "#12",
            CommonID = "EnergyTweaks",
            Title = "Install RoombaPowerPlan",
            Description = "Instala un plan de energia totalmente personalizado por ELROOMBA para garantizar el Máximo rendimiento",
            ImagePath = "ms-appx:///Assets/Energia.png",
            IsNew = true,
            IsUpdate = false,
            IsExclusive = false,
        });

        return Button;
    }
}

public class ButtonInfo
{
    public string? UniqueID { get; set; }
    public string? CommonID { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
    public bool IsNew { get; set; }
    public bool IsUpdate { get; set; }
    public bool IsExclusive { get; set; }
}