using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogHandler : MonoBehaviour
{
    [SerializeField]
    private int _maxLogCount = 300;
    private int _logCount = 0;

    private StreamWriter _writer;
    void Awake()
    {
        _writer = File.AppendText("log.txt");
        _writer.Write("\n\n=============== Game started ================\n\n");
        DontDestroyOnLoad(gameObject);
        Application.logMessageReceived += HandleLog;
        _logCount = 0;
    }

    private void HandleLog(string condition, string stackTrace, LogType type)
    {
        _logCount++;

        if (_logCount <= _maxLogCount)
        {
            var logEntry = string.Format("\n{0:f8}", condition);
            _writer.Write(logEntry);
        }
    }

    void OnDestroy()
    {
        _writer.Close();
        Application.logMessageReceived -= HandleLog;
    }
}
