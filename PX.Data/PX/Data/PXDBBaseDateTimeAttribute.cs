// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBBaseDateTimeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <summary>Base class for PXDB***DateTime attributes</summary>
public abstract class PXDBBaseDateTimeAttribute : PXDBDateAttribute
{
  protected virtual System.DateTime GetDate()
  {
    System.DateTime? serverDateTime = PXTransactionScope.GetServerDateTime(true);
    return !serverDateTime.HasValue ? PXTimeZoneInfo.Now : PXTimeZoneInfo.ConvertTimeFromUtc(serverDateTime.Value, this.GetTimeZone());
  }

  /// <summary>
  /// Initializes a new instance of the <tt>PXDBCreatedDateTime</tt> attribute.
  /// </summary>
  public PXDBBaseDateTimeAttribute()
  {
    this.UseSmallDateTime = false;
    this.PreserveTime = true;
    base.UseTimeZone = true;
  }

  /// <summary>Gets or sets the value that indicates (if set to <tt>true</tt>) that the attribute should convert the time to UTC, using the local time zone.</summary>
  /// <value>
  /// By default, the value is <tt>true</tt>.</value>
  public override bool UseTimeZone
  {
    get => base.UseTimeZone;
    set
    {
    }
  }
}
