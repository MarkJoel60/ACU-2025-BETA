// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowInserted
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>RowInserted</tt> event.
/// </summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="e">Required. The instance of the <see cref="T:PX.Data.PXRowInsertedEventArgs" /> type that holds data for the <tt>RowInserted</tt> event.</param>
/// <remarks>
///   <para>The <tt>RowInserted</tt> event is generated after a new data record has been successfully inserted
///   into the <tt>PXCache</tt> as a result of one of the following actions:</para>
///   <list type="bullet">
///     <item><description>Insertion initiated in the UI or through the Web Service API</description></item>
///     <item><description>Invocation of any of the following <tt>PXCache</tt> class methods:
///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li></ul></description></item>
///   </list>
///   <para>The <tt>RowInserted</tt> event handler is used to implement the business logic for
///   the following actions:</para>
///   <list type="bullet">
///     <item><description>Insertion of the detail data records in a one-to-many relationship</description></item>
///     <item><description>Update of the master data record in a many-to-one relationship</description></item>
///     <item><description>Insertion or update of the related data record in a one-to-one relationship</description></item>
///   </list>
///   <para>The following execution order is used for the <tt>RowInserted</tt> event handlers:</para>
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
/// protected virtual void DACName_RowInserted(PXCache sender,
///                                            PXRowInsertedEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXRowInserted(PXCache sender, PXRowInsertedEventArgs e);
