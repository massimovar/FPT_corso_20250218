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

public class RuntimeNetLogic1 : BaseNetLogic
{
    private IUAVariable variableToIncrement;
    private PeriodicTask myPeriodicTask;
    private int incrementValue;
    private LongRunningTask myLongRunningTask;
    private IUAVariable varToSync;

    public override void Start()
    {
        Log.Info("App started");
        varToSync = Project.Current.GetVariable("Model/Variable6");
        varToSync.VariableChange += VarToSync_VariableChange;
        SynchVariables();
    }

    private void VarToSync_VariableChange(object sender, VariableChangeEventArgs e)
    {
        Log.Info("Variable changed from" + e.OldValue + " to " + e.NewValue);
    }

    public override void Stop()
    {
        Log.Info("App stopped");
    }

    [ExportMethod]
    public void TestMe()
    {
        Log.Error("TestMe", "TestMe method called");
    }

    [ExportMethod]
    public void IncrementVariable(int increment)
    {
        incrementValue = increment;

        if (myPeriodicTask != null)
        {
            Log.Error("IncrementVariable", "PeriodicTask already running, please stop it first.");
            return;
        }

        // recuperare la variabile da incrementare
        variableToIncrement = Project.Current.GetVariable("Model/Variable5");
        // creare un PeriodicTask per incrementare la variabile ogni tot secondi
        myPeriodicTask = new PeriodicTask(IncrementMyVariable, 1000, LogicObject);
        // avviare il PeriodicTask
        myPeriodicTask.Start();
    }

    [ExportMethod]
    public void DelayedTaskExample()
    {
        var myDelayedTask = new DelayedTask(WriteSomethingIntoLogs, 5000, LogicObject);
        myDelayedTask.Start();
        // non dimenticare il Dispose.
    }

    [ExportMethod]
    public void LongRunningTaskExample()
    {
        myLongRunningTask = new LongRunningTask(WriteSomethingIntoLogs, LogicObject);
        myLongRunningTask.Start();
    }

    [ExportMethod]
    public void DisposeLongRunningTaskExample()
    {
        myLongRunningTask.Dispose();
    }

    private void WriteSomethingIntoLogs()
    {
        for (int i = 0; i < 10; i++)
        {
            Log.Info("I'm a long running task");
        }
    }

    private void IncrementMyVariable()
    {
        variableToIncrement.Value = variableToIncrement.Value + incrementValue;
    }

    [ExportMethod]
    public void StopIncrementVariable()
    {
        // fermare il PeriodicTask
        myPeriodicTask?.Dispose();
    }

    private void SynchVariables()
    {
        // Quando una tag viene messa in uso in FTOptix?
        // Quando associata ad un oggetto UI
        // Quando utilizzata in un DataLogger, Ricetta, Allarme
        // Quando sulla tag è stato associato un evento al cambiamento di valore
        // Quando la metto in uso via script:

        var synchronizer = new RemoteVariableSynchronizer(TimeSpan.FromMilliseconds(5000));
        synchronizer.Add(varToSync);

        // Come forzare la lettura / scrittura di una variabile?
        varToSync.RemoteRead(); // forzo la lettura della variabile. 
        varToSync.RemoteRead(1000); // forzo la lettura della variabile con timeout di 1 secondo. 
        // rispetto al timeout: viene generato un errore da gestire con try catch
        var v = varToSync.Value; // leggo l'ultimo valore ricavato dal campo
        varToSync.RemoteWrite(1000); // forzo la scrittura
    }   
}
