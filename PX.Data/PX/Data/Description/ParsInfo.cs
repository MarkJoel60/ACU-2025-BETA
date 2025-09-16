// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.ParsInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Description;

/// <summary>
/// Describes single parameter defined in Searches or Parameters section
/// of any data container control.
/// </summary>
public class ParsInfo
{
  public readonly ParType Type;
  public readonly string Name;
  public readonly string Field;
  public readonly string DefValue;
  public bool ForeignKey;
  public bool Invisible;

  public ParsInfo(ParType type, string name, string field, string defValue)
  {
    this.Type = type;
    this.Name = name;
    this.Field = field;
    this.DefValue = defValue;
  }
}
