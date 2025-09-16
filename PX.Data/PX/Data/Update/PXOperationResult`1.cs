// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXOperationResult`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Update;

public class PXOperationResult<T>
{
  public T Result;
  public Exception Error;
  public object Tag;

  public bool Success => this.Error == null;

  public PXOperationResult(T result, object tag = null)
  {
    this.Result = result;
    this.Tag = tag;
  }

  public PXOperationResult(Exception error, object tag = null)
  {
    this.Error = error;
    this.Tag = tag;
  }
}
