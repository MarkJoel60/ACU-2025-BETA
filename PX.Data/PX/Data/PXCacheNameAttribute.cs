// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheNameAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>Sets the user-friendly name and type of the data access class (DAC).</summary>
/// <remarks>
///   <para>The attribute is added to the DAC declaration. The name can be obtained at run time through the
/// <see cref="M:PX.Data.PXUIFieldAttribute.GetItemName(PX.Data.PXCache)">GetItemName(PXCache)</see> static method of the <tt>PXUIField</tt> attribute.</para>
///   <para>The CacheGlobal flag can be obtained at run time through the <see cref="M:PX.Data.PXCacheNameAttribute.UseGlobalCache(System.Type)" /> static method of the current attribute.</para>
///   <para>The attribute is used on the Generic Inquiry (SM208000) form to filter the list of DACs during the selection. By default, the user is suggested to
/// use DACs with the non-empty attribute.</para>
/// </remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXCacheName("Currency Info", PXDacType.Config)]
/// public partial class CurrencyInfo : PXBqlTable, PX.Data.IBqlTable
/// {
///     ...
/// }</code>
/// </example>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXCacheName("Inventory Item", PXDacType.Catalogue, CacheGlobal = true)]
/// public partial class InventoryItem : PXBqlTable, PX.Data.IBqlTable
/// {
///     ...
/// }</code>
/// </example>
public class PXCacheNameAttribute : PXNameAttribute
{
  private static ConcurrentDictionary<System.Type, bool> GlobalCacheUses;

  /// <summary>
  /// Gets the <see cref="T:PX.Data.PXDacType" /> value specified as the type of DAC in the constructor.
  /// By default, the property is set to <see cref="F:PX.Data.PXDacType.Unknown" />.
  /// </summary>
  public PXDacType DacType { get; private set; }

  /// <summary>
  /// If the flag set corresponding type rows should cache.
  /// By default, the data records does not cache.
  /// The flag can be obtained at run time through the <see cref="M:PX.Data.PXCacheNameAttribute.UseGlobalCache(System.Type)" /> static method of the current attribute.
  /// </summary>
  public bool CacheGlobal { get; set; }

  /// <summary>
  /// Initializes a new instance that assigns the specified name to the DAC.
  /// </summary>
  /// <param name="name">The name to assign to the DAC.</param>
  public PXCacheNameAttribute(string name)
    : this(name, PXDacType.Unknown)
  {
  }

  /// <summary>
  /// Initializes a new instance that assigns the specified name and type to the DAC.
  /// </summary>
  /// <param name="name">The name to assign to the DAC.</param>
  /// <param name="dacType">The type to assign to the DAC. This parameter is optional. By default, the property is set to <see cref="F:PX.Data.PXDacType.Unknown" /></param>
  public PXCacheNameAttribute(string name, PXDacType dacType)
    : base(name)
  {
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentNullException(nameof (PXCacheNameAttribute));
    this.DacType = dacType;
  }

  /// <summary>Gets the localized DAC display name.</summary>
  /// <param name="dacType">The DAC type</param>
  /// <returns>The localized DAC display name.</returns>
  public static string GetName(System.Type dacType)
  {
    return ExceptionExtensions.CheckIfNull<System.Type>(dacType, nameof (dacType), (string) null).GetCustomAttributes<PXCacheNameAttribute>(true).FirstOrDefault<PXCacheNameAttribute>()?.GetName();
  }

  public virtual string GetName(object row) => this.GetName();

  /// <summary>Returns the flag of the data access class (DAC).
  /// The flag is set using the <see cref="P:PX.Data.PXCacheNameAttribute.CacheGlobal" /> property of the <see cref="T:PX.Data.PXCacheNameAttribute" /> attribute.</summary>
  /// <param name="entityType">data access class (DAC)</param>
  public static bool UseGlobalCache(System.Type entityType)
  {
    if (PXCacheNameAttribute.GlobalCacheUses == null)
    {
      PXCacheNameAttribute.GlobalCacheUses = new ConcurrentDictionary<System.Type, bool>();
    }
    else
    {
      bool flag;
      if (PXCacheNameAttribute.GlobalCacheUses.TryGetValue(entityType, out flag))
        return flag;
    }
    PXCacheNameAttribute cacheNameAttribute = entityType.GetCustomAttributes<PXCacheNameAttribute>(false).FirstOrDefault<PXCacheNameAttribute>();
    bool flag1 = cacheNameAttribute != null && cacheNameAttribute.CacheGlobal;
    PXCacheNameAttribute.GlobalCacheUses.TryAdd(entityType, flag1);
    return flag1;
  }
}
