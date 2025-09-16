// Decompiled with JetBrains decompiler
// Type: PX.Data.Now
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;

#nullable enable
namespace PX.Data;

/// <summary>Current UTC time.</summary>
/// <example><para>The code below shows a data view that select EPActivity data records with the ReminderDate field less or equal than the current date, and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;EPActivity,
///     Where&lt;EPActivity.reminderDate, LessEqual&lt;Now&gt;&gt;&gt; records;</code>
/// <code title="" description="" lang="SQL">
/// SELECT * FROM EPActivity
/// WHERE EPActivity.ReminderDate &lt;= [current date and time]</code>
/// </example>
public sealed class Now : BqlType<IBqlDateTime, System.DateTime>.Constant<
#nullable disable
Now>
{
  /// <exclude />
  public Now()
    : base(Now.GetUTCNowForDB())
  {
  }

  private static System.DateTime GetUTCNowForDB()
  {
    return PXTimeZoneInfo.ConvertTimeToUtc(PXTimeZoneInfo.Now, LocaleInfo.GetTimeZone());
  }
}
