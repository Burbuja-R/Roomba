using System.Xml;

namespace ELROOMBA.DataModel;

public class ButtonInfo
{
    public string? UniqueID
    {
        get;
        set;
    }
    public string? Title
    {
        get;
        set;
    }
    public string? Description
    {
        get;
        set;
    }
    public string? ImagePath
    {
        get;
        set;
    }
    public bool IsNew
    {
        get;
        set;
    }
    public bool IsUpdate
    {
        get;
        set;
    }
}

public class ButtonManager
{
    public static List<ButtonInfo> GetButtons()
    {
        var Button = new List<ButtonInfo>();

        Button.Add(new ButtonInfo
        {
            UniqueID = "#1",
            Title = "SystemTweaks",
            Description = "Ajusta Windows para obtener el maximo rendimiento",
            ImagePath = "ms-appx:///Assets/Ajustes.png",
            IsNew = true,
            IsUpdate = false,
        });

        Button.Add(new ButtonInfo
        {
            UniqueID = "#2",
            Title = "Mouse/KeyboardDataQueueSize",
            Description = "Ajusta la memoria dedicada al raton, para menor input lag",
            ImagePath = "ms-appx:///Assets/Ajustes.png",
            IsNew = true,
            IsUpdate = false,
        });

        Button.Add(new ButtonInfo
        {
            UniqueID = "#3",
            Title = "Win32PrioritySeparation",
            Description = "Ajusta la memoria dedicada de los programas al sistema",
            ImagePath = "ms-appx:///Assets/Ajustes.png",
            IsNew = true,
            IsUpdate = false,
        });

        Button.Add(new ButtonInfo
        {
            UniqueID = "#4",
            Title = "Delete Mitigation",
            Description = "Esta opcion Elimina Los procesos asociados a las mitigaciones,Haciendo el sistema mas seguro",
            ImagePath = "ms-appx:///Assets/Ajustes.png",
            IsNew = true,
            IsUpdate = false,
        });

        Button.Add(new ButtonInfo
        {
            UniqueID = "#5",
            Title = "Disable Windows Updates",
            Description = "Fuerza a Windows a nunca actualizarse, esto es conveniente si esta en una version antigua a la actual, y no quieres que se actualize",
            ImagePath = "ms-appx:///Assets/Ajustes.png",
            IsNew = true,
            IsUpdate = false,
        });

        Button.Add(new ButtonInfo
        {
            UniqueID = "#6",
            Title = "Disable Spectre and Meltdown",
            Description = "Son vulnerabilidades en el CPU, Por eso es recomendable desactivarlas",
            ImagePath = "ms-appx:///Assets/Ajustes.png",
            IsNew = true,
            IsUpdate = false,
        });

        Button.Add(new ButtonInfo
        {
            UniqueID = "#7",
            Title = "Display Real monitor RefreshRate",
            Description = "Hace que el monitor Muestre Siempre la tasa de Hz más alta que este admita",
            ImagePath = "ms-appx:///Assets/Ajustes.png",
            IsNew = true,
            IsUpdate = false,
        });

        Button.Add(new ButtonInfo
        {
            UniqueID = "#8",
            Title = "DMA Remmaping",
            Description = "Deshabilita ciertas configuraciones de seguridad de los drivers instalados por microsoft, Para menor Latencia",
            ImagePath = "ms-appx:///Assets/Ajustes.png",
            IsNew = true,
            IsUpdate = false,
        });

        Button.Add(new ButtonInfo
        {
            UniqueID = "#9",
            Title = "Disable FullScreenOptimization",
            Description = "Deshabilita todas las optimizaciones a pantalla completa de windows",
            ImagePath = "ms-appx:///Assets/Ajustes.png",
            IsNew = true,
            IsUpdate = false,
        });

        return Button;
    }
}