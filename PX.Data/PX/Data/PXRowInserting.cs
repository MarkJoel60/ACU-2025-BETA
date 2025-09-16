// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowInserting
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>RowInserting</tt> event.
/// </summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="e">Required. The instance of the <see cref="T:PX.Data.PXRowInsertingEventArgs">PXRowInsertingEventArgs</see> type
/// that holds data for the <tt>RowInserting</tt> event.</param>
/// <remarks>
///   <para>The <tt>RowInserting</tt> event is trigged before the new data record is inserted into the <tt>PXCache</tt> object
///   as a result of one of the following actions:</para>
///   <list type="bullet">
///     <item><description>Insertion initiated in the UI or through the Web Service API</description></item>
///     <item><description>Invocation of either of the following methods of the <tt>PXCache</tt> class:
///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li></ul></description></item>
///   </list>
///   <para>The <tt>RowInserting</tt> event handler is used to perform the following actions:
///   </para>
///   <list type="bullet">
///     <item><description>Evaluation of the data record that is being inserted</description></item>
///     <item><description>Cancellation of the insertion by throwing an exception</description></item>
///     <item><description>Assignment of the default values to the fields of the data record that is being inserted</description></item>
///   </list>
///   <para>The following execution order is used for the <tt>RowInserting</tt> event handlers:</para>
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
/// protected virtual void DACName_RowInserting(PXCache sender,
///                                             PXRowInsertingEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXRowInserting(PXCache sender, PXRowInsertingEventArgs e);
