// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowUpdating
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>RowUpdating</tt> event.
/// </summary>
/// <param name="sender">
///   <para>Required. The cache object that generated the event.</para>
/// </param>
/// <param name="e">
///   <para>Required. The instance of the <see cref="T:PX.Data.PXRowUpdatingEventArgs">PXRowUpdatingEventArgs</see> type
///   that holds data for the <tt>RowUpdating</tt> event.</para>
/// </param>
/// <remarks>
///   <para>The <tt>RowUpdating</tt> event is generated before the data record is actually updated
///   in the <tt>PXCache</tt> object during an update initiated in either of the following cases:</para>
///   <list type="bullet">
///     <item><description>In the UI or via Web Service API.</description></item>
///     <item><description>When the following <tt>PXCache</tt> class methods were invoked:<ul><li><tt>Update(object)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li></ul></description></item>
///   </list>
///   <para>The <tt>RowUpdating</tt> event handler is used to evaluate a data record that is being updated
///   and to cancel the update operation if the data record does not
/// fit the business logic requirements.</para>
///   <para>The following execution order is used for the <tt>RowUpdating</tt> event handlers:</para>
///   <list type="number">
///     <item><description>Graph event handlers are executed.</description></item>
///     <item><description>If <tt>e.Cancel</tt> is <tt>false</tt>, attribute event handlers are executed.</description></item>
///   </list>
/// </remarks>
/// <example>
/// <para>
/// According to the naming convention for graph event handlers in Acumatica Framework,
/// the classic event handler has the following signature.
/// </para>
/// <code lang="CS">
/// protected virtual void DACName_RowUpdating(
///     PXCache sender,
///     PXRowUpdatingEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXRowUpdating(PXCache sender, PXRowUpdatingEventArgs e);
