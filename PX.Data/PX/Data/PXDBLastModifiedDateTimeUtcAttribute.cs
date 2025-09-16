// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBLastModifiedDateTimeUtcAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <summary>
///   <para>This attribute is obsolete. Use <see cref="T:PX.Data.PXDBLastModifiedDateTimeAttribute">PXDBLastModifiedDateTimeAttribute Class</see> instead.</para>
///   <para>Maps a DAC field to the database column and automatically sets the field value to the data record's last modification date and time in UTC.</para>
/// </summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field data type should be <tt>DateTime?</tt>.</remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXDBLastModifiedDateTimeUtc]
/// [PXUIField(DisplayName = "Last Modified Date", Enabled = false)]
/// public virtual DateTime? LastModifiedDateTime { get; set; }</code>
/// </example>
[Obsolete]
public class PXDBLastModifiedDateTimeUtcAttribute : PXDBLastModifiedDateTimeAttribute
{
  protected override System.DateTime GetDate() => PXTimeZoneInfo.Now;

  /// <summary>Initializes a new instance with default parameters.</summary>
  public PXDBLastModifiedDateTimeUtcAttribute() => this.UseTimeZone = true;
}
