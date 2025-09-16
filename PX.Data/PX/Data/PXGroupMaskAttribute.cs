// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGroupMaskAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Set the BQL JOIN or\and WHERE for the DAC which restricts this DAC by related DAC with group restrictions (e.g. SOOrder + Customer) in some modules (e.g. OData v4)
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PXGroupMaskAttribute : Attribute
{
  public PXGroupMaskAttribute(System.Type groupRestriction)
  {
    this.JoinRestriction = groupRestriction;
  }

  public virtual System.Type JoinRestriction { get; set; }

  public virtual System.Type WhereRestriction { get; set; }
}
