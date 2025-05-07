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

public class DesignTimeNetLogic1 : BaseNetLogic
{
    [ExportMethod]
    public void HelloWorld()
    {
        Log.Info("Hello World!");
        Log.Info("HelloWorld Method", "Hello World!");
    }

    [ExportMethod]
    public void VariablesGenerator()
    {
        var myFolderName = "MyFolder";
        var numberOfVariables = 10;
        // Ricavo la cartella Model
        var modelFolder = Project.Current.Get<Folder>("Model");

        // Genero una Folder dentro la cartella Model se non esiste
        var myFolder = modelFolder.Children.Get<Folder>(myFolderName);

        if (myFolder == null)
        {
            myFolder = InformationModel.Make<Folder>(myFolderName);
            modelFolder.Add(myFolder);
        }

        // Genero n variabili di tipo int32 dentro la cartella Model/Folder
        for (int i = 0; i < numberOfVariables; i++)
        {
            var v = InformationModel.MakeVariable("MyVariable" + i, OpcUa.DataTypes.Boolean);
            myFolder.Add(v);
        }
    }
}
