using System;
using QAction_1;
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

            object[] interfaceRow = (object[])protocol.GetRow(Parameter.Iftable.tablePid, rowKey);

            RowState rowState = (RowState)Convert.ToInt32(interfaceRow[Parameter.Iftable.Idx.iftablerowstate]);

            uint currentSpeed = Convert.ToUInt32(interfaceRow[Parameter.Iftable.Idx.iftablespeed]);

            Boolean speedOverflow = IsSpeedOverflowing(currentSpeed);

            protocol.Log($"QA{protocol.QActionID}|22222|{rowState}", LogType.Error, LogLevel.NoLogging);

            switch (rowState)
            {
                case RowState.New:
                case RowState.Recreated:
                case RowState.Updated:
                    {

                        protocol.Log($"QA{protocol.QActionID}|yooooo|{rowState}, {speedOverflow}, {currentSpeed}", LogType.Error, LogLevel.NoLogging);

                        if (speedOverflow)
                        {
                            SetSpeedFromExtensionTable(protocol, rowKey);

                        }
                        else
                        {
                            protocol.SetParameterIndexByKey(Parameter.Iftable.tablePid, rowKey, Parameter.Iftable.Idx.iftablecalculatedspeed + 1, currentSpeed);
                        }

                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }

    private static Boolean IsSpeedOverflowing(uint speed)
    {
        return speed == uint.MaxValue;
    }

    private static void SetSpeedFromExtensionTable(SLProtocol protocol, string primaryKey)
    {
        object[] extensionRow = (object[])protocol.GetRow(Parameter.Ifxtable.tablePid, primaryKey);

        uint speed = Convert.ToUInt32(extensionRow[Parameter.Ifxtable.Idx.ifxifhighspeed]);

        protocol.SetParameterIndexByKey(Parameter.Iftable.tablePid, primaryKey, Parameter.Iftable.Idx.iftablecalculatedspeed + 1, speed);
    }
}