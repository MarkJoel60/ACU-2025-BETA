// Decompiled with JetBrains decompiler
// Type: PX.Data.PXConnectionScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.PushNotifications;
using System;
using System.Data;
using System.Data.Common;

#nullable disable
namespace PX.Data;

/// <summary>
/// Objects of this class are connection scopes for executing BQL statements.
/// </summary>
/// <remarks>When a long database operation is being performed,
/// you must create another connection scope to perform other operations with the database
/// without waiting for the first operation to finish. For example, a separate
/// connection scope is needed in the <tt>RowSelecting</tt> event handler to execute
/// additional BQL statements.</remarks>
/// <example>The <tt>PXConnectionScope</tt> class inherits <tt>IDisposable</tt>, so
/// you can create a <tt>PXConnectionScope</tt> object in the <tt>using</tt> statement as follows.
/// <code lang="CS">
/// using (new PXConnectionScope())
/// {
///     ...
/// }</code></example>
public sealed class PXConnectionScope : IDisposable
{
  internal DbConnection _Connection;
  private bool _Disposed;
  private readonly PXConnectionScope _Previous;
  private readonly bool _isTableChangingScopedBefore;

  internal DbConnection Connection => this._Connection;

  /// <exclude />
  public PXConnectionScope()
  {
    this._Previous = PXContext.GetSlot<PXConnectionScope>();
    this._isTableChangingScopedBefore = TableChangingScope.IsScoped;
    TableChangingScope.IsScoped = false;
    PXContext.SetSlot<PXConnectionScope>(this);
  }

  /// <exclude />
  public void Dispose()
  {
    if (this._Disposed)
      return;
    PXContext.SetSlot<PXConnectionScope>(this._Previous);
    TableChangingScope.IsScoped = this._isTableChangingScopedBefore;
    if (this._Connection != null)
      this._Connection.Dispose();
    this._Disposed = true;
  }

  internal static DbConnection GetConnection()
  {
    PXConnectionScope slot = PXContext.GetSlot<PXConnectionScope>();
    return slot == null ? (DbConnection) null : slot._Connection ?? (slot._Connection = PXDatabase.CreateConnection());
  }

  internal static bool IsScoped(IDbConnection connection)
  {
    for (PXConnectionScope pxConnectionScope = PXContext.GetSlot<PXConnectionScope>(); pxConnectionScope != null; pxConnectionScope = pxConnectionScope._Previous)
    {
      if (pxConnectionScope._Connection == connection)
        return true;
    }
    return false;
  }

  internal static bool IsTopMost() => PXContext.GetSlot<PXConnectionScope>()?._Previous == null;
}
