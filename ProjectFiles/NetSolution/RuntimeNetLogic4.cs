#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.DataLogger;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.OPCUAServer;
using FTOptix.OPCUAClient;
using FTOptix.Modbus;
using FTOptix.CODESYS;
using FTOptix.System;
using FTOptix.Retentivity;
using FTOptix.CommunicationDriver;
using FTOptix.SerialPort;
using FTOptix.UI;
using FTOptix.Core;
using FTOptix.WebUI;
#endregion

public class RuntimeNetLogic4 : BaseNetLogic
{
    const string FILL_COLOR_VAR = "Ellipse1FillColor";

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void ChangeColor(){
        // Bad practise: se Ellipse cambiasse posizione o nome, il codice non funzionerebbe più
        // var ellipse = Owner.Get<Ellipse>("Ellipse1");
        // ellipse.FillColor = Colors.Blue;

        var colorToChange = LogicObject.GetVariable(FILL_COLOR_VAR);
        colorToChange.Value = Colors.Blue;
    }
}
