using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogHandler : MonoBehaviour
{
    private StreamWriter _writer;
    void Awake()
    {
        _writer = File.AppendText("log.txt");
        _writer.Write("\n\n=============== Game started ================\n\n");
        DontDestroyOnLoad(gameObject);
        Application.logMessageReceived += HandleLog;
    }

    private void HandleLog(string condition, string stackTrace, LogType type)
    {
        var logEntry = string.Format("\n{0} {1:f8}" , Time.frameCount, condition);
        _writer.Write(logEntry);
    }

    void OnDestroy()
    {
        _writer.Close();
        Application.logMessageReceived -= HandleLog;
    }
}
