// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.SuppressPushNotificationsScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.PushNotifications;

[PXInternalUseOnly]
public class SuppressPushNotificationsScope : IDisposable
{
  private const string ContextKey = "SuppressPushNotificationsScope.Context";
  private SuppressPushNotificationsScope _previousScope;
  private HashSet<Guid> _giSetToSkip;
  private HashSet<string> _entityScreenSetToSkip;
  private bool _skipAll;

  internal static SuppressPushNotificationsScope Context
  {
    get
    {
      return PXContext.GetSlot<SuppressPushNotificationsScope>("SuppressPushNotificationsScope.Context");
    }
    private set
    {
      PXContext.SetSlot<SuppressPushNotificationsScope>("SuppressPushNotificationsScope.Context", value);
    }
  }

  public SuppressPushNotificationsScope(
    IEnumerable<Guid> giNotificationsSetToSkip = null,
    IEnumerable<string> screenNotificationsSetToSkip = null)
  {
    this._giSetToSkip = giNotificationsSetToSkip != null ? new HashSet<Guid>(giNotificationsSetToSkip) : (HashSet<Guid>) null;
    this._entityScreenSetToSkip = screenNotificationsSetToSkip != null ? new HashSet<string>(screenNotificationsSetToSkip) : (HashSet<string>) null;
    if (this._giSetToSkip == null && giNotificationsSetToSkip == null)
      this._skipAll = true;
    this._previousScope = SuppressPushNotificationsScope.Context;
    HashSet<Guid> giSetToSkip = this._giSetToSkip;
    if (giSetToSkip != null)
      EnumerableExtensions.AddRange<Guid>((ISet<Guid>) giSetToSkip, (IEnumerable<Guid>) this._previousScope?._giSetToSkip ?? Enumerable.Empty<Guid>());
    HashSet<string> entityScreenSetToSkip = this._entityScreenSetToSkip;
    if (entityScreenSetToSkip != null)
      EnumerableExtensions.AddRange<string>((ISet<string>) entityScreenSetToSkip, (IEnumerable<string>) this._previousScope?._entityScreenSetToSkip ?? Enumerable.Empty<string>());
    int num;
    if (!this._skipAll)
    {
      SuppressPushNotificationsScope previousScope = this._previousScope;
      num = previousScope != null ? (previousScope._skipAll ? 1 : 0) : 0;
    }
    else
      num = 1;
    this._skipAll = num != 0;
    SuppressPushNotificationsScope.Context = this;
  }

  internal static bool ShouldSuppressAll()
  {
    SuppressPushNotificationsScope context = SuppressPushNotificationsScope.Context;
    return context != null && context._skipAll;
  }

  internal static bool ShouldSuppressGI(Guid designId)
  {
    SuppressPushNotificationsScope context = SuppressPushNotificationsScope.Context;
    if (context == null)
      return false;
    if (context._skipAll)
      return true;
    HashSet<Guid> giSetToSkip = context._giSetToSkip;
    // ISSUE: explicit non-virtual call
    return giSetToSkip != null && __nonvirtual (giSetToSkip.Contains(designId));
  }

  internal static bool ShouldSuppressEntityScreen(string screenId)
  {
    SuppressPushNotificationsScope context = SuppressPushNotificationsScope.Context;
    if (context == null)
      return false;
    if (context._skipAll)
      return true;
    HashSet<string> entityScreenSetToSkip = context._entityScreenSetToSkip;
    // ISSUE: explicit non-virtual call
    return entityScreenSetToSkip != null && __nonvirtual (entityScreenSetToSkip.Contains(screenId));
  }

  internal static List<Guid> GetGiSetToSkip()
  {
    SuppressPushNotificationsScope context = SuppressPushNotificationsScope.Context;
    if (context == null)
      return (List<Guid>) null;
    if (context._skipAll)
      return (List<Guid>) null;
    HashSet<Guid> giSetToSkip = context._giSetToSkip;
    return giSetToSkip == null ? (List<Guid>) null : giSetToSkip.ToList<Guid>();
  }

  internal static List<string> GetEntityScreenSetToSkip()
  {
    SuppressPushNotificationsScope context = SuppressPushNotificationsScope.Context;
    if (context == null)
      return (List<string>) null;
    if (context._skipAll)
      return (List<string>) null;
    HashSet<string> entityScreenSetToSkip = context._entityScreenSetToSkip;
    return entityScreenSetToSkip == null ? (List<string>) null : entityScreenSetToSkip.ToList<string>();
  }

  public void Dispose() => SuppressPushNotificationsScope.Context = this._previousScope;
}
