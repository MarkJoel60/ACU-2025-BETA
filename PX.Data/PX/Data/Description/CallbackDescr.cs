// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.CallbackDescr
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Description;

/// <summary>
/// Represents information about autocallback for the field.
/// </summary>
public class CallbackDescr
{
  public readonly CallbackTarget Target;
  public readonly string dsCommandName;

  public CallbackDescr(CallbackTarget target, string dsCommandName)
  {
    this.Target = target;
    this.dsCommandName = dsCommandName;
  }
}
