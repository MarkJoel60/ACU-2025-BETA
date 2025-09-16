// Decompiled with JetBrains decompiler
// Type: PX.Data.IPXSqlLimits
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Data;

#nullable disable
namespace PX.Data;

public interface IPXSqlLimits
{
  bool VerifyRowCountLimit(ref long topCount);

  void LimitExceeded(IDbCommand cmd);

  void BeforeExecuteReader(IDbCommand command);

  void ThrowSqlTimeout(IDbCommand cmd);

  TimeSpan? GetSqlTimeLimit(IDbCommand cmd);
}
