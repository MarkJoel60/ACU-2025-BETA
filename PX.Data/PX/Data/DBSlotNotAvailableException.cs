// Decompiled with JetBrains decompiler
// Type: PX.Data.DBSlotNotAvailableException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// This exception is thrown when code requires database slot but it is unavailable.
/// </summary>
[Serializable]
public class DBSlotNotAvailableException : Exception
{
  public DBSlotNotAvailableException()
    : base("Database slot is not available.")
  {
  }

  public DBSlotNotAvailableException(string message)
    : base(message)
  {
  }
}
