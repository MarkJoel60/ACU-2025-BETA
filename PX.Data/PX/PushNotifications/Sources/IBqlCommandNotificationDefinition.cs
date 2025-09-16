// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.Sources.IBqlCommandNotificationDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.PushNotifications.Sources;

/// <summary>An interface that contains the methods that must be implemented for a built-in definition.</summary>
public interface IBqlCommandNotificationDefinition
{
  /// <summary>Returns a tuple of a <tt>BqlCommand</tt> object, which defines the data query, and a <tt>PXDataValue</tt> array, which defines the parameters that should be
  /// passed to the query.</summary>
  /// <remarks>The data query that the method defines should adhere to <see href="https://help.acumatica.com/Help?ScreenId=ShowWiki&amp;pageid=99dc32c1-00ac-498a-9412-6c7cf766eaa8">Recommendations for the Data Queries</see> in
  /// the Integration Development Guide.</remarks>
  /// <returns>A tuple of a <see cref="T:PX.Data.BqlCommand" /> object, which defines the data query, and a <see cref="T:PX.Data.PXDataValue" /> array, which defines the parameters that should be passed to the query.</returns>
  /// <example>
  /// The following example shows the GetSourceSelect() method implementation.
  ///   <code title="Example" lang="CS">
  /// using PX.Data;
  /// using PX.PushNotifications.Sources;
  /// using PX.PushNotifications.UI.DAC;
  /// 
  /// public class TestInCodeDefinition : IInCodeNotificationDefinition
  /// {
  ///     public Tuple&lt;BqlCommand, PXDataValue[]&gt; GetSourceSelect()
  ///     {
  ///         return Tuple.Create(PXSelectJoin&lt;PushNotificationsHook,
  ///             LeftJoin&lt;PushNotificationsSource,
  ///                 On&lt;PushNotificationsHook.hookId,
  ///                     Equal&lt;PushNotificationsSource.hookId&gt;&gt;&gt;&gt;.GetCommand(), new PXDataValue[0]);
  /// 
  ///     }
  /// }</code>
  /// </example>
  Tuple<BqlCommand, PXDataValue[]> GetSourceSelect();

  /// <summary>Returns an array of <tt>IBqlField</tt>-derived types, which contains the fields that should be returned from the query.</summary>
  /// <remarks>
  ///   <para></para>
  /// </remarks>
  /// <returns>An array of <tt>IBqlField</tt>-derived types, which contains the fields that should be returned from the query. If you need to return all fields, the method must return
  /// <tt>null</tt>.</returns>
  /// <example><para>The following code shows an example of the implementation of the GetRestictedFields() method.</para>
  ///   <code title="Example" lang="CS">
  /// using PX.Data;
  /// using PX.PushNotifications.Sources;
  /// using PX.PushNotifications.UI.DAC;
  /// 
  /// public class TestInCodeDefinition : IInCodeNotificationDefinition
  /// {
  ///     ...
  ///     public Type[] GetRestrictedFields()
  ///     {
  ///         return new []
  ///         {
  ///             typeof(PushNotificationsHook.address),
  ///             typeof(PushNotificationsHook.type),
  ///             typeof(PushNotificationsSource.designID),
  ///             typeof(PushNotificationsSource.inCodeClass),
  ///             typeof(PushNotificationsSource.lineNbr)
  ///         };
  ///     }
  /// }</code>
  /// </example>
  System.Type[] GetRestrictedFields();
}
