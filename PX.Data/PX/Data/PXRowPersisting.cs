// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowPersisting
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>RowPersisting</tt> event.
/// </summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="e">Required. The instance of the <see cref="T:PX.Data.PXRowPersistingEventArgs">PXRowPersistingEventArgs</see> type
/// that holds data for the <tt>RowPersisting</tt> event.</param>
/// <remarks>
///   <para>The <tt>RowPersisting</tt> event is generated in the process of committing changes to the database
///   for every data record whose status is <tt>Inserted</tt>, <tt>Updated</tt>, or <tt>Deleted</tt> before
///   the corresponding changes for the data record are committed to the database. The committing of changes to a database
///   is initiated by invoking the <tt>Actions.PressSave()</tt> method of the business logic controller (BLC).
///   While processing this method, the Acumatica data access layer commits first every inserted
/// data record, then every updated data record, and finally every deleted data record.</para>
///   <para>Avoid executing additional BQL statements
///     in a <tt>RowPersisting</tt> event handler. When the <tt>RowPersisting</tt>
///     event is raised, the associated transaction scope is busy saving the changes,
///     and any other operation performed within this transaction scope may cause
///     performance degradation and deadlocks.</para>
///   <para>The <tt>RowPersisting</tt> event handler is used to do the following:</para>
///   <list type="bullet">
///     <item><description>Validate the data record before it has been committed to the database</description></item>
///     <item><description>Cancel the committing of the data record by throwing an exception</description></item>
///   </list>
///   <para>The following execution order is used for the <tt>RowPersisting</tt> event handlers:</para>
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
/// <code>
/// protected virtual void DACName_RowPersisting(PXCache sender,
///                                              PXRowPersistingEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXRowPersisting(PXCache sender, PXRowPersistingEventArgs e);
