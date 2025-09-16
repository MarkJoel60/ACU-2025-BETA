// Decompiled with JetBrains decompiler
// Type: PX.Caching.CachingKeys
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

#nullable disable
namespace PX.Caching;

[PXInternalUseOnly]
public static class CachingKeys
{
  public static Func<object> AppVersion(IAppInstanceInfo appInstanceInfo)
  {
    return (Func<object>) (() => (object) appInstanceInfo.Version);
  }

  public static Func<object> IsPortal(IAppInstanceInfo appInstanceInfo)
  {
    return (Func<object>) (() => (object) appInstanceInfo.IsPortal);
  }

  public static Func<object> InstallationID(IAppInstanceInfo appInstanceInfo)
  {
    return (Func<object>) (() => (object) appInstanceInfo.InstallationId);
  }

  public static Func<object> Username(IPXIdentityAccessor identityAccessor)
  {
    return (Func<object>) (() =>
    {
      IPXIdentity identity = identityAccessor.Identity;
      return identity == null ? (object) null : (object) identity.Username;
    });
  }

  public static Func<object> Tenant(IPXIdentityAccessor identityAccessor)
  {
    return (Func<object>) (() =>
    {
      IPXIdentity identity = identityAccessor.Identity;
      return identity == null ? (object) null : (object) identity.TenantId;
    });
  }

  public static Func<object> CurrentCulture(IPXIdentityAccessor identityAccessor)
  {
    return (Func<object>) (() =>
    {
      IPXIdentity identity = identityAccessor.Identity;
      return identity == null ? (object) null : (object) identity.Culture.Name;
    });
  }

  public static Func<object> Branch(IPXIdentityAccessor identityAccessor)
  {
    return (Func<object>) (() => (object) (int?) identityAccessor.Identity?.BranchId);
  }

  public static Func<object> BusinessDate(IPXIdentityAccessor identityAccessor)
  {
    return (Func<object>) (() => (object) identityAccessor.Identity?.BusinessDate);
  }

  public static Func<object> TimeZone(IPXIdentityAccessor identityAccessor)
  {
    return (Func<object>) (() =>
    {
      IPXIdentity identity = identityAccessor.Identity;
      return identity == null ? (object) null : (object) identity.TimeZone.Id;
    });
  }

  public static Func<object> CacheVersion<TCacheName>(ICacheVersion<TCacheName> cacheVersion)
  {
    return (Func<object>) (() => (object) cacheVersion.Current);
  }

  public static Func<object> UITypeKey()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return CachingKeys.\u003C\u003EO.\u003C0\u003E__UIType ?? (CachingKeys.\u003C\u003EO.\u003C0\u003E__UIType = new Func<object>(CachingKeys.UIType));
  }

  public static string UIType() => PXContext.GetSlot<string>("ForceUI");

  public static string ToString(params Func<object>[] keys)
  {
    return CachingKeys.ToString(((IEnumerable<Func<object>>) keys).Select<Func<object>, object>((Func<Func<object>, object>) (k => k())));
  }

  public static string ToString(params object[] keys)
  {
    return CachingKeys.ToString((IEnumerable<object>) keys);
  }

  internal static string ToString(IEnumerable<object> keys)
  {
    StringBuilder sbld = new StringBuilder();
    bool flag = true;
    foreach (object key in keys)
    {
      if (!flag)
        sbld.Append('$');
      flag = false;
      if (key != null)
        CachingKeys.ToString(sbld, key);
    }
    return sbld.ToString();
  }

  private static void ToString(StringBuilder sbld, object item)
  {
    switch (item)
    {
      case null:
        return;
      case IEnumerable enumerable:
        if (item.ToString() == item.GetType().ToString())
        {
          IEnumerator enumerator = enumerable.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              object current = enumerator.Current;
              CachingKeys.ToString(sbld, current);
            }
            return;
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        }
        else
          break;
    }
    switch (item)
    {
      case ITuple tuple:
        for (int index = 0; index < tuple.Length; ++index)
          CachingKeys.ToString(sbld, tuple[index]);
        break;
      case bool flag:
        sbld.Append(flag ? '1' : '0');
        break;
      case DateTime dateTime:
        sbld.Append(dateTime.ToString("yyyyMMddHHmmssfff", (IFormatProvider) CultureInfo.InvariantCulture));
        break;
      default:
        sbld.Append(item.ToInvariantString());
        break;
    }
  }
}
