// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowUpdated
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>RowUpdated</tt> event.
/// </summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="e">Required. The instance of the <see cref="T:PX.Data.PXRowUpdatedEventArgs">PXRowUpdatedEventArgs</see> type that holds data for the <tt>RowUpdated</tt> event.</param>
/// <remarks>
///   <para>The <tt>RowUpdated</tt> event is generated after the data record has been successfully updated
///   in the <tt>PXCache</tt> object in one of the following cases:</para>
///   <list type="bullet">
///     <item><description>The update is initiated in the UI or through the Web Service API.</description></item>
///     <item><description>One of the following methods of the <tt>PXCache</tt> class is invoked:
///         <ul><li><tt>Update(object)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li></ul></description></item>
///   </list>
///         The updating of a data record is executed only when there is a data record
///         with the same values of the data access class (DAC) key fields, either in the
///         <tt>PXCache</tt> object or in the database. Otherwise, the process of inserting the data record is started.
///   <para>The <tt>RowUpdated</tt> event handler is used to implement the business logic of the following actions:</para>
///   <list type="bullet">
///     <item><description>Update of the master data record in a many-to-one relationship</description></item>
///     <item><description>Insertion or update of the detail data records in a one-to-many relationship</description></item>
///     <item><description>Update of the related data record in a one-to-one relationship</description></item>
///   </list>
///   <para>The following execution order is used for the <tt>RowUpdated</tt> event handlers:</para>
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
/// protected virtual void DACName_RowUpdated(PXCache sender,
///                                           PXRowUpdatedEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXRowUpdated(PXCache sender, PXRowUpdatedEventArgs e);
