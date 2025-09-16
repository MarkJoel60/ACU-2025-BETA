// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXSYConnectionInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Data;

#nullable disable
namespace PX.Data.Update;

public class PXSYConnectionInfo
{
  public IDbConnection Connection { get; set; }

  public PXCredentials Credentials { get; set; }

  public PXSYConnectionInfo(IDbConnection connection) => this.Connection = connection;

  public PXSYConnectionInfo(IDbConnection connection, PXCredentials credentials)
  {
    this.Connection = connection;
    this.Credentials = credentials;
  }

  public PXSYConnectionInfo(IDbConnection connection, string login, string password)
  {
    this.Connection = connection;
    this.Credentials = new PXCredentials(login, password);
  }
}
