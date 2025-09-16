// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowSelected
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>RowSelected</tt> event.
/// </summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="e">Required. The instance of the <see cref="T:PX.Data.PXRowSelectedEventArgs">PXRowSelectedEventArgs</see> type that holds data for the <tt>RowSelected</tt> event.</param>
/// <remarks>
///   <para>The <tt>RowSelected</tt> event is generated in the following cases:</para>
///   <list type="bullet">
///     <item><description>To display a data record in the UI</description></item>
///     <item><description>To execute the following methods of the <tt>PXCache</tt> class:
///         <ul><li><tt>Locate(IDictionary)</tt></li><li><tt>Insert()</tt></li><li><tt>Insert()</tt></li><li><tt>Insert(IDictionary)</tt></li><li><tt>Update(object)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li><li><tt>Delete(IDictionary, IDictionary)</tt></li></ul></description></item>
///   </list>
///   <para>Avoid executing BQL statements in a <tt>RowSelected</tt> event handler,
///     because this execution may cause performance degradation caused by multiple invocations
///     of the <tt>RowSelected</tt> event for a single data record.</para>
///   <para>The <tt>RowSelected</tt> event handler is used to do the following:</para>
///   <list type="bullet">
///     <item><description>Implement the UI presentation logic</description></item>
///     <item><description>Set up the processing operation on a processing screen (which is a type of UI screen
///     that allows the execution of a long-running operation on multiple data records at once)</description></item>
///   </list>
///   <para>The following execution order is used for the <tt>RowSelected</tt> event handlers:</para>
///   <list type="number">
///     <item><description>Attribute event handlers are executed.</description></item>
///     <item><description>Graph event handlers are executed.</description></item>
///   </list>
/// </remarks>
/// <example>
/// <para>
/// According to the naming convention for graph event handlers in Acumatica Framework,
/// the classic event handler has the following signature.
/// </para>
/// <code lang="CS">
/// protected virtual void DACName_RowSelected(PXCache sender,
///                                            PXRowSelectedEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXRowSelected(PXCache sender, PXRowSelectedEventArgs e);
