// Decompiled with JetBrains decompiler
// Type: PX.Async.ILongOperationManagerCustomInfoExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable enable
namespace PX.Async;

[PXInternalUseOnly]
public static class ILongOperationManagerCustomInfoExtensions
{
  /// <summary>
  /// <para>From the custom information dictionary of the current long-running operation,
  /// returns the data object stored under the default key. </para>
  /// </summary>
  public static object? GetCustomInfo(this ILongOperationManager manager)
  {
    return manager.GetCustomInfo((string) null);
  }

  /// <summary>
  /// <para>From the custom information dictionary of the long-running operation specified by the <paramref name="key" />,
  /// returns the data object that is stored under the default key.</para>
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  public static object? GetCustomInfoFor(this ILongOperationManager manager, object? key)
  {
    return manager.GetCustomInfoFor(key, (string) null, out object[] _);
  }

  /// <summary>
  /// <para>From the custom information dictionary of the long-running operation specified
  /// by the <paramref name="key" /> parameter, returns the data object that is stored under the key that
  /// is defined by the <paramref name="customInfoKey" /> parameter.
  /// If the <see cref="T:PX.Data.PXLongOperation" /> static class does not contain a long-running operation with the
  /// key specified in the <paramref name="key" /> parameter, the method returns null. Otherwise, the method does the following:</para>
  /// 	<list type="bullet">
  /// 		<item><description>If the <paramref name="customInfoKey" /> parameter is null or empty:
  /// 		Returns the data object that is stored under the default key</description></item>
  /// 		<item><description>Otherwise: Returns the data object that is stored under the key specified
  /// 		in the <paramref name="customInfoKey" /> parameter</description></item>
  /// 	</list>
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  /// <param name="customInfoKey">The key to access the data object in the custom information dictionary of
  /// the long-running operation specified by the first parameter.</param>
  public static object? GetCustomInfoFor(
    this ILongOperationManager manager,
    object? key,
    string? customInfoKey)
  {
    return manager.GetCustomInfoFor(key, customInfoKey, out object[] _);
  }

  /// <summary>
  /// <para>From the custom information dictionary of the long-running operation specified by the <paramref name="key" /> parameter,
  /// returns the data object that is stored under the default key.
  /// In the <paramref name="processingList" /> parameter, this method returns the list of the records processed
  /// by the delegate of the long-running operation.</para>
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  /// <param name="processingList">The out parameter that is used to return the array of the data records that must
  /// be processed by the delegate of the long-running operation.</param>
  public static object? GetCustomInfoFor(
    this ILongOperationManager manager,
    object? key,
    out object[]? processingList)
  {
    return manager.GetCustomInfoFor(key, (string) null, out processingList);
  }

  /// <summary>
  /// In the custom information dictionary of the current long-running operation, stores the specified data object under the default key.
  /// </summary>
  /// <param name="info">The data object to be stored in the dictionary.</param>
  public static void SetCustomInfo(this ILongOperationManager manager, object? info)
  {
    manager.SetCustomInfo(info, (string) null);
  }
}
