// Decompiled with JetBrains decompiler
// Type: PX.Data.DatabaseInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class DatabaseInfo
{
  public string ServerName { get; set; }

  public string MachineName { get; set; }

  public string InstanceName { get; set; }

  public string DatabaseID { get; set; }

  public string DatabaseName { get; set; }

  public string FullServerName
  {
    get
    {
      string serverName = this.ServerName;
      if (serverName != null)
        return serverName;
      return this.InstanceName != null ? $"{this.MachineName}/{this.InstanceName}" : this.MachineName;
    }
  }

  public string FullDatabaseInfo => $"{this.FullServerName}|{this.DatabaseID}|{this.DatabaseName}";
}
