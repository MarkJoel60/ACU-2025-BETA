// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBqlTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.ComponentModel;

#nullable disable
namespace PX.Data;

/// <summary>Represents a database table. The data access class (DAC) corresponding to the table must be derived from this class.</summary>
/// <example><para>A DAC representing the &lt;tt&gt;Product&lt;/tt&gt; database table is defined in the following way.</para>
///   <code title="Example" lang="CS">
/// using System;
/// using PX.Data;
/// public class Product : PXBqlTable, IBqlTable
/// {
///     //Declarations of fields
/// }</code>
/// </example>
public abstract class PXBqlTable : IBqlTableSystemDataStorage
{
  private PXBqlTableSystemData bqlTableSystemData;

  [PXInternalUseOnly]
  [EditorBrowsable(EditorBrowsableState.Never)]
  ref PXBqlTableSystemData IBqlTableSystemDataStorage.GetBqlTableSystemData()
  {
    return ref this.bqlTableSystemData;
  }
}
