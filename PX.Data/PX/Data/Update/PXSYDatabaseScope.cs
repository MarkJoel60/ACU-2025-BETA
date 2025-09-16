// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXSYDatabaseScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog.Context;
using System;
using System.Data;

#nullable disable
namespace PX.Data.Update;

internal class PXSYDatabaseScope : IDisposable
{
  protected IDbConnection Connection { get; private set; }

  protected IDbCommand Command { get; private set; }

  protected IDataReader Reader { get; private set; }

  protected PXImpersonationScope Scope { get; private set; }

  public PXSYDatabaseScope(PXSYConnectionInfo info)
  {
    this.Connection = info != null ? info.Connection : throw new ArgumentNullException();
    if (info.Credentials != null)
      this.Scope = new PXImpersonationScope(info.Credentials);
    this.Connection.Open();
  }

  public IDbCommand GetCommand(string cmdText)
  {
    if (this.Reader != null)
      this.Reader.Dispose();
    if (this.Command != null)
      this.Command.Dispose();
    this.Command = this.Connection.CreateCommand();
    this.Command.CommandText = cmdText;
    this.Command.CommandTimeout = 300;
    return this.Command;
  }

  public void ExecuteCommand(string cmdText)
  {
    try
    {
      this.GetCommand(cmdText);
      this.Command.ExecuteNonQuery();
      PXTrace.WriteVerbose("Execute Non Query: " + cmdText);
    }
    catch (Exception ex)
    {
      using (LogContext.PushProperty("DBCommand", (object) new
      {
        cmdText = cmdText
      }, false))
        PXTrace.WriteError(ex);
      throw;
    }
  }

  public T ExecuteScalar<T>(string cmdText)
  {
    try
    {
      this.GetCommand(cmdText);
      T obj = (T) this.Command.ExecuteScalar();
      PXTrace.WriteVerbose("Execute Scalar: " + cmdText);
      return obj;
    }
    catch (Exception ex)
    {
      using (LogContext.PushProperty("DBCommand", (object) new
      {
        cmdText = cmdText
      }, false))
        PXTrace.WriteError(ex);
      throw;
    }
  }

  public IDataReader GetReader(string cmdText) => this.GetReader(cmdText, CommandBehavior.Default);

  public IDataReader GetReader(string cmdText, CommandBehavior mode)
  {
    try
    {
      this.GetCommand(cmdText);
      IDataReader reader = this.Command.ExecuteReader(mode);
      PXTrace.WriteVerbose("Get reader: " + cmdText);
      return reader;
    }
    catch (Exception ex)
    {
      using (LogContext.PushProperty("DBCommand", (object) new
      {
        cmdText = cmdText
      }, false))
        PXTrace.WriteError(ex);
      throw;
    }
  }

  public virtual void Dispose()
  {
    if (this.Connection != null)
      this.Connection.Dispose();
    if (this.Command != null)
      this.Command.Dispose();
    if (this.Reader != null)
      this.Reader.Dispose();
    if (this.Scope == null)
      return;
    this.Scope.Dispose();
  }

  public static void WithReader(
    string commandText,
    PXSYConnectionInfo connection,
    System.Action<IDataReader> processRecord)
  {
    using (PXSYDatabaseScope pxsyDatabaseScope = new PXSYDatabaseScope(connection))
    {
      IDataReader reader = pxsyDatabaseScope.GetReader(commandText);
      while (reader.Read())
        processRecord(reader);
    }
  }
}
