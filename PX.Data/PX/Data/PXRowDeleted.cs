// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowDeleted
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The delegate for the <tt>RowDeleted</tt> event.</summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="e">Required. The instance of the <see cref="T:PX.Data.PXRowDeletedEventArgs">PXRowDeletedEventArgs</see>
/// type that holds data for the <tt>RowDeleted</tt> event.</param>
/// <remarks>
///   <para>The <tt>RowDeleted</tt> event is generated for a data record that is being deleted from the <tt>PXCache</tt>
///   object (that is, a data record whose status has been successfully set to <tt>Deleted</tt> or <tt>InsertedDeleted</tt>)
///   as a result of the following actions:</para>
///   <list type="bullet">
///     <item><description>Deletion initiated in the UI or through the Web Service API</description></item>
///     <item><description>Invocation of the following methods of the <tt>PXCache</tt> class:
///         <ul><li><tt>Delete(object)</tt></li><li><tt>Delete(IDictionary, IDictionary)</tt></li></ul></description></item>
///   </list>
///   <para>When a data record is deleted that has already been stored in the database
///     (and, hence, exists in both the database and the <tt>PXCache</tt> object),
///     the status of the data record is set to <tt>Deleted</tt>. For a data record that has not yet been stored in the database
///     but has only been inserted in the <tt>PXCache</tt> object, the status of the data record is set to <tt>InsertedDeleted</tt>.</para>
///   <para>The <tt>RowDeleted</tt> event handler is used to implement the business logic of the following actions:</para>
///   <list type="bullet">
///     <item><description>Deletion of the detail data records in a one-to-many relationship</description></item>
///     <item><description>Update of the master data record in a many-to-one relationship</description></item>
///     <item><description>Deletion or update of the related data record in a one-to-one relationship</description></item>
///   </list>
///   <para>The following execution order is used for the <tt>RowDeleted</tt> event handlers:</para>
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
/// <code>
/// protected virtual void DACName_RowDeleted(PXCache sender,
///                                           PXRowDeletedEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXRowDeleted(PXCache sender, PXRowDeletedEventArgs e);
