// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public class PXParameter : ICloneable
{
  public string Name;
  public object Value;
  public System.Type Table;
  public string DataField;
  public bool Required;

  public object Clone()
  {
    return (object) new PXParameter()
    {
      Name = this.Name,
      Value = this.Value,
      DataField = this.DataField,
      Table = this.Table,
      Required = this.Required
    };
  }
}
