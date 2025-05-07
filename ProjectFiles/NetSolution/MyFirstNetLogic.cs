#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.HMIProject;
using FTOptix.CoreBase;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.OPCUAServer;
using FTOptix.OPCUAClient;
using FTOptix.Modbus;
using FTOptix.CODESYS;
using FTOptix.System;
using FTOptix.Retentivity;
using FTOptix.NetLogic;
using FTOptix.CommunicationDriver;
using FTOptix.SerialPort;
using FTOptix.UI;
using FTOptix.Core;
using FTOptix.Alarm;
using FTOptix.WebUI;
#endregion

public class MyFirstNetLogic : BaseNetLogic
{
    [ExportMethod]
    public void GenerateMyVars()
    {
        // Recuperare cartella /Model
        var modelFolder = Project.Current.Get<Folder>("Model");
        // Controllo che Model non abbia già MyFolder
        if (modelFolder.Children.Get<Folder>("MyFolder") != null)
        {
            Log.Warning("GenerateMyVars", "MyFolder already exists");
            return;
        }
        // Creare cartella MyFolder
        var myFolder = InformationModel.Make<Folder>("MyFolder");
        // Creo 10 var in MyFolder
        for (int i = 0; i < 10; i++)
        {
            var v = InformationModel.MakeVariable("MyVar" + i, OpcUa.DataTypes.Boolean);
            myFolder.Add(v);
        }
        // Aggiungo MyFolder in /Model in modo da ottenre /Model/MyFolder
        modelFolder.Add(myFolder);
        Log.Info("GenerateMyVars", "MyFolder created");
    }

    [ExportMethod]
    public void Create(){
        var myVar = Project.Current.GetVariable("Model/MyVar");
        if (myVar == null)
        {
            var modelFolder = Project.Current.Get<Folder>("Model");
            myVar = InformationModel.MakeVariable("MyVar", OpcUa.DataTypes.Boolean);
            modelFolder.Add(myVar);
        }
    }

    [ExportMethod]
    public void TestError(){
        Log.Error("TestError", "This is an error message");
    }

}
