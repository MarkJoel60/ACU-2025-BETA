// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.IPXValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public abstract class IPXValue : IEquatable<IPXValue>
{
  public abstract object[] GetParameters(Func<string, IPXValue> paramHandler, bool tryInline = false);

  public abstract PXDataValue[] GetDataValueParameters(
    Func<string, IPXValue> paramHandler,
    bool tryInline = false);

  public abstract SQLExpression GetExpression(
    Func<string, SQLExpression> onParameter,
    bool tryInline = false);

  public override int GetHashCode() => base.GetHashCode();

  public abstract bool Equals(IPXValue other);
}
