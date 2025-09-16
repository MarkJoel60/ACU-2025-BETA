// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSpecialButtonType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Defines possible special types of a button. The enumeration
/// is used to set <tt>PXButton</tt> attribute properties.</summary>
public enum PXSpecialButtonType
{
  /// <summary>The button does not have a special type.</summary>
  Default,
  /// <summary>The button has the <b>Save</b> button type. In particular, a graph searches buttons of this type when the graph's <tt>Actions.PressSave()</tt> method is
  /// invoked.</summary>
  Save,
  /// <summary>The button has the <b>Save &amp; Close</b> button type.</summary>
  SaveNotClose,
  /// <summary>The button has the <b>Cancel</b> button type. In particular,
  /// a graph searches buttons of this type when the graph's
  /// <tt>Actions.PressCancel()</tt> method is invoked.</summary>
  Cancel,
  /// <summary>The button has the <b>Refresh</b> button type.</summary>
  Refresh,
  /// <summary>The button has the <b>CopyPaste</b> button type.</summary>
  CopyPaste,
  /// <summary>The button has the <b>Report</b> button type.</summary>
  Report,
  /// <summary>The button has the <strong>Go to First Record </strong>button type.</summary>
  First,
  /// <summary>The button has the <strong>Go to Next Record</strong> button type.</summary>
  Next,
  /// <summary>The button has the <b>Go to Previous Record </b>button type.</summary>
  Prev,
  /// <summary>The button has the <strong>Go to Last Record</strong> button type.</summary>
  Last,
  /// <summary>The button has the <b>Add New Record </b>button type.</summary>
  Insert,
  /// <summary>The button has the <b>Delete</b> button type.</summary>
  Delete,
  /// <summary>The button has the <b>Approve</b> button type.</summary>
  Approve,
  /// <summary>The button has the <b>Approve All</b> button type.</summary>
  ApproveAll,
  /// <summary>The button has the <b>Process</b> button type.</summary>
  Process,
  /// <summary>The button has the <b>Process All</b> button type.</summary>
  ProcessAll,
  /// <summary>The button has the <b>Schedule</b> button type.</summary>
  Schedule,
  /// <summary>The button has the <b>Switch Between Grid and Form </b>button type.</summary>
  EditDetail,
  /// <summary>The button has the <b>ActionsFolder</b>button type.</summary>
  ActionsFolder,
  /// <summary>The button has the <b>InquiriesFolder</b>button type.</summary>
  InquiriesFolder,
  /// <summary>The button has the <b>ReportsFolder</b>button type.</summary>
  ReportsFolder,
  /// <summary>The button has the <b>ToolbarFolder</b>button type.</summary>
  ToolbarFolder,
  /// <summary>The button has the <b>SidePanelFolder</b> button type.</summary>
  SidePanelFolder,
  /// <summary>The button has the <b>ApiCallback</b> button type.</summary>
  ApiCallback,
  /// <summary>The button has the <b>Archive</b> button type.</summary>
  Archive,
  Extract,
}
