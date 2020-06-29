using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.SqliteClient;
using System.IO;
using UnityEngine.Networking;

public class CQuestDBManager
{
    private static CQuestDBManager instance = null;

    public static CQuestDBManager GetInstance()
    {
        if (instance == null)
        {
            instance = new CQuestDBManager();
        }

        return instance;
    }

    private const string DB_NAME = "MarTruck.db";

    public void DBCreate()
    {
        string filePath = Application.dataPath + "/" + DB_NAME;

        if (!File.Exists(filePath))
        {
            File.Copy(Application.streamingAssetsPath + "/" + DB_NAME, filePath);
        }
    }

    public string GetDBFilePath()
    {
        string filePath = "URI=file:" + Application.dataPath + "/" + DB_NAME;

        return filePath;
    }

    public bool DBConnectionCheck()
    {
        try
        {
            IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
            dbConnection.Open();

            if (dbConnection.State == ConnectionState.Open)
            {
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        return false;
    }

    public List<SQuest> ReadAllQuest()
    {
        List<SQuest> questList = new List<SQuest>();

        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "Select * from Quest";

        IDataReader dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            SQuest item = new SQuest();
            item.Name = dataReader.GetString(1);
            item.Description = dataReader.GetString(2);

            questList.Add(item);
        }

        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;

        return questList;
    }
}
