// Decompiled with JetBrains decompiler
// Type: PX.Data.PXEntryStatus
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The enumeration that specifies a data record status. A data record status changes as a result of manipulations with the data record: insertion, update, or
/// removal.</summary>
/// <remarks>
///   <para>The table below shows data record status update depending on different <tt>PXCache</tt> methods.</para>
///   <table>
///     <tbody>
///       <tr>
///         <td>
///           <para align="center">
///             <strong>Original Status</strong>
///           </para>
///         </td>
///         <td>
///           <para align="center">
///             <strong>Previous status</strong>
///           </para>
///         </td>
///         <td>
///           <para align="center">
///             <strong>PXCache Method Invoked</strong>
///           </para>
///         </td>
///         <td>
///           <para align="center">
///             <strong>Status After</strong>
///           </para>
///         </td>
///       </tr>
///       <tr>
///         <td>N/A</td>
///         <td>N/A</td>
///         <td>Insert() / Insert(object)</td>
///         <td>Inserted</td>
///       </tr>
///       <tr>
///         <td>N/A</td>
///         <td>Inserted</td>
///         <td>Update(object)</td>
///         <td>Inserted</td>
///       </tr>
///       <tr>
///         <td>N/A</td>
///         <td>Inserted</td>
///         <td>Delete(object)</td>
///         <td>InsertedDeleted</td>
///       </tr>
///       <tr>
///         <td>Inserted</td>
///         <td>InsertedDeleted</td>
///         <td>Insert(object) / Update(object)</td>
///         <td>Inserted</td>
///       </tr>
///       <tr>
///         <td>N/A</td>
///         <td>Notchanged</td>
///         <td>Update(object)</td>
///         <td>Updated</td>
///       </tr>
///       <tr>
///         <td>N/A</td>
///         <td>Notchanged</td>
///         <td>Delete(object)</td>
///         <td>Deleted</td>
///       </tr>
///       <tr>
///         <td>Notchanged</td>
///         <td>Deleted</td>
///         <td>Insert(object) / Update(object)</td>
///         <td>Updated</td>
///       </tr>
///       <tr>
///         <td>N/A</td>
///         <td>Updated</td>
///         <td>Delete(object)</td>
///         <td>Deleted</td>
///       </tr>
///       <tr>
///         <td>Updated</td>
///         <td>Deleted</td>
///         <td>Insert(object) / Update(object)</td>
///         <td>Updated</td>
///       </tr>
///     </tbody>
///   </table>
/// </remarks>
public enum PXEntryStatus
{
  /// <summary>The data record has not been modified since it was placed in
  /// the <tt>PXCache</tt> object or since the last time the <tt>Save</tt>
  /// action was invoked (triggering execution of BLC's
  /// <tt>Actions.PressSave()</tt>).</summary>
  Notchanged,
  /// <summary>The data record has been modified, and the <tt>Save</tt>
  /// action has not been invoked. After the changes are saved to the
  /// database, the data record status changes to
  /// <tt>Notchanged</tt>.</summary>
  Updated,
  /// <summary>The data record is new and has been added to the
  /// <tt>PXCache</tt> object, and the <tt>Save</tt> action has not been
  /// invoked. After the changes are saved to the database, the data record
  /// status changes to <tt>Notchanged</tt>.</summary>
  Inserted,
  /// <summary>The data record is not new and has been marked as
  /// <tt>Deleted</tt> within the <tt>PXCache</tt> object. After the changes
  /// are saved, the data record is deleted from the database and removed
  /// from the <tt>PXCache</tt> object.</summary>
  Deleted,
  /// <summary>The data record is new and has been added to the
  /// <tt>PXCache</tt> object and then marked as <tt>Deleted</tt> within the
  /// <tt>PXCache</tt> object. After the changes are saved, the data record
  /// is removed from the <tt>PXCache</tt> object.</summary>
  InsertedDeleted,
  /// <summary>An <tt>Unchanged</tt> data record can be marked as
  /// <tt>Held</tt> within the <tt>PXCache</tt> object to avoid being
  /// collected during memory cleanup. <tt>Updated</tt>, <tt>Inserted</tt>,
  /// <tt>Deleted</tt>, <tt>InsertedDeleted</tt>, or <tt>Held</tt> data
  /// records are never collected during memory cleanup. Any
  /// <tt>Notchanged</tt> data record can be removed from the
  /// <tt>PXCache</tt> object during memory cleanup.</summary>
  Held,
  /// <summary>This flag is passed to the <tt>PXCache&lt;&gt;.SetStatus</tt>
  /// method to indicate that the data record must be saved. The final status assigned
  /// to the data record (whether <tt>Inserted</tt> or <tt>Updated</tt>) depends
  /// on the initial status of the data record.</summary>
  Modified,
}
