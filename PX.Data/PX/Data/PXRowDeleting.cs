// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowDeleting
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The delegate for the <tt>RowDeleting</tt> event.</summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="e">Required. The instance of the <see cref="T:PX.Data.PXRowDeletingEventArgs">PXRowDeletingEventArgs</see> type
/// that holds data for the <tt>RowDeleting</tt> event.</param>
/// <remarks>
///   <para>The <tt>RowDeleting</tt> event is generated for a data record that is being deleted
///   from the <tt>PXCache</tt> object after its status has been set to
/// <tt>Deleted</tt> or <tt>InsertedDeleted</tt>, but the data record can still be reverted
/// to the previous state by canceling the deletion. The status of
/// the data record is set to <tt>Deleted</tt> or <tt>InsertedDeleted</tt> as a result of either of the following actions:</para>
///   <list type="bullet">
///     <item><description>Deletion initiated in the UI or through the Web Service API.</description></item>
///     <item><description>Invocation of the following methods of the <tt>PXCache</tt> class:
///         <ul><li><tt>Delete(object)</tt></li><li><tt>Delete(IDictionary, IDictionary)</tt></li></ul></description></item>
///   </list>
///   <para>When a data record is deleted that has already been stored
///     in the database (and, hence, exists in
///     both the database and the <tt>PXCache</tt> object), the status of the data record is set to <tt>Deleted</tt>.
///     For a data record that has not yet been stored in the
///     database but was only inserted in the <tt>PXCache</tt> object, the status of the data record is set to <tt>InsertedDeleted</tt>.
///   </para>
///   <para>The <tt>RowDeleting</tt> event handler is used to evaluate the data record that is marked as
///   <tt>Deleted</tt> or <tt>InsertedDeleted</tt> and cancel the deletion if
/// it is required by the business logic.</para>
///   <para>The following execution order is used for the <tt>RowDeleting</tt> event handlers:</para>
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
/// protected virtual void DACName_RowDeleting(PXCache sender,
///                                            PXRowDeletingEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXRowDeleting(PXCache sender, PXRowDeletingEventArgs e);
