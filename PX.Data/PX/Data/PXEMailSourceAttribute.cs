// Decompiled with JetBrains decompiler
// Type: PX.Data.PXEMailSourceAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <example><para>The code below shows the use of the attribute on the declaration of a DAC.</para>
/// <code title="Example" lang="CS">
/// [System.SerializableAttribute()]
/// [PXPrimaryGraph(typeof(ARStatementUpdate))]
/// [PXEMailSource]
/// public partial class ARStatement : PXBqlTable, PX.Data.IBqlTable
/// { ... }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class)]
[Obsolete("The Wiki Notifications functionality, which is related to PXEMailSourceAttribute, no longer exists.")]
public class PXEMailSourceAttribute : Attribute
{
  private readonly System.Type[] _types;

  public PXEMailSourceAttribute()
  {
  }

  public PXEMailSourceAttribute(params System.Type[] types) => this._types = types;

  /// <summary>Get.</summary>
  public System.Type[] Types => this._types ?? new System.Type[0];
}
