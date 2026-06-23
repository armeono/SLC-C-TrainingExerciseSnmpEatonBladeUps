using System;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: QActionNkjkjame.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocol protocol)
    {
        try
        {
            var rowKey = protocol.RowKey();





            protocol.Log($"QA{protocol.QActionID}|shabadaj|yoooooooooo", LogType.Error, LogLevel.NoLogging);
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}