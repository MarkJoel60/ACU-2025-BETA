// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Database.ResultSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>The base type for classes that define BQL statements, such as
/// <see cref="T:PX.Data.PXSelect`1">PXSelect&lt;&gt;</see> class and its
/// variants and the <see cref="T:PX.Data.PXProcessing`1">PXProcessing&lt;&gt;</see> class and
/// its successors.</summary>
/// <typeparam name="Table">The DAC type of the data records that the class retrieves
/// from the database.</typeparam>
/// <example><para>The code below defines a data view, extends its Where conditional expression, and executes the data view.</para>
/// <code title="Example" lang="CS">
/// // Definition of a data view
/// PXSelectBase&lt;ARDocumentResult&gt; sel = new PXSelectReadOnly2&lt;ARDocumentResult,
///     LeftJoin&lt;ARInvoice, On&lt;ARInvoice.docType, Equal&lt;ARDocumentResult.docType&gt;,
///         And&lt;ARInvoice.refNbr, Equal&lt;ARDocumentResult.refNbr&gt;&gt;&gt;,
///     Where&lt;ARRegister.customerID, Equal&lt;Current&lt;ARDocumentFilter.customerID&gt;&gt;&gt;&gt;
///     (this);
/// ARDocumentFilter header = Filter.Current;
/// // Appending a condition if BranchID is specified in the filter
/// if (header.BranchID != null)
/// {
///     sel.WhereAnd&lt;Where&lt;ARRegister.branchID,
///         Equal&lt;Current&lt;ARDocumentFilter.branchID&gt;&gt;&gt;&gt;();
/// }
/// // Appending a condition if DocType is specified in the filter
/// if (header.DocType != null)
/// {
///     sel.WhereAnd&lt;Where&lt;ARRegister.docType,
///         Equal&lt;Current&lt;ARDocumentFilter.docType&gt;&gt;&gt;&gt;();
/// }
/// // Execution of the data view and iteration through the result set
/// foreach (PXResult&lt;ARDocumentResult, ARInvoice&gt; reg in sel.Select())
/// {
///     ARDocumentResult res = reg;
///     ARInvoice invoice = reg;
///     ...
/// }</code>
/// </example>
public abstract class PXSelectBase<Table> : PXSelectBase where Table : class, IBqlTable, new()
{
  /// <summary>Clears the dialog information saved by the graph on last
  /// invocation of the <tt>Ask()</tt> method.</summary>
  public void ClearDialog() => this.View.ClearDialog();

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  /// <remarks>
  /// <para>This method and its overloads provide the interface for the
  /// corresponding methods of the <tt>PXView</tt> class.
  /// </para>
  /// <para>The method can be used to display the panel configured by the
  /// <tt>PXSmartPanel</tt> control. In this case, the <tt>key</tt>
  /// parameter is set to the <tt>Key</tt> property of the control,
  /// <tt>refreshRequired</tt> is typically set to <tt>true</tt>, and other
  /// parameters are set to <tt>null</tt>. The more common way to display a
  /// panel is to call the AskExt(key)
  /// method.</para>
  /// <para>Note that the method is executed asynchronously. When the method
  /// invocation is reached for the first time, execution of the enclosing
  /// method stops, and a request is send to the client to display the
  /// dialog. When the user clicks one of the buttons, the webpage sends a
  /// request to the server, and the system starts execution of the method
  /// that invoked <tt>Ask()</tt> one more time. This time the
  /// <tt>Ask()</tt> method returns the value that indicates the user's
  /// choice, and code execution continues.</para>
  /// </remarks>
  /// <example><para>The code below defines an event handler that asks for confirmation to continue deletion of a data record.</para>
  /// <code title="Example" lang="CS">
  /// public PXSelect&lt;INComponent&gt; Components;
  /// protected void INComponent_RowDeleting(
  ///     PXCache sender, PXRowDeletingEventArgs e)
  /// {
  ///     if (Components.Ask("Deleting Revenue Component",
  ///                        "Are you sure?",
  ///                        MessageButtons.YesNo) != WebDialogResult.Yes)
  ///         e.Cancel = true;
  /// }</code>
  /// </example>
  public WebDialogResult Ask(
    string key,
    string header,
    string message,
    MessageButtons buttons,
    bool refreshRequired)
  {
    return this.View.Ask(key, (object) null, header, message, buttons, MessageIcon.None, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(string key, string header, string message, MessageButtons buttons)
  {
    return this.View.Ask(key, (object) null, header, message, buttons, MessageIcon.None);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string header,
    string message,
    MessageButtons buttons,
    bool refreshRequired)
  {
    return this.View.Ask((string) null, (object) null, header, message, buttons, MessageIcon.None, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(string header, string message, MessageButtons buttons)
  {
    return this.View.Ask((string) null, (object) null, header, message, buttons, MessageIcon.None);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(string message, MessageButtons buttons)
  {
    return this.View.Ask(message, buttons);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string key,
    Table row,
    string header,
    string message,
    MessageButtons buttons,
    bool refreshRequired)
  {
    return this.View.Ask(key, (object) row, header, message, buttons, MessageIcon.None, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string key,
    Table row,
    string header,
    string message,
    MessageButtons buttons)
  {
    return this.View.Ask(key, (object) row, header, message, buttons, MessageIcon.None);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    Table row,
    string header,
    string message,
    MessageButtons buttons,
    bool refreshRequired)
  {
    return this.Ask((string) null, row, header, message, buttons, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(Table row, string header, string message, MessageButtons buttons)
  {
    return this.Ask((string) null, row, header, message, buttons);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string key,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool refreshRequired)
  {
    return this.View.Ask(key, (object) null, header, message, buttons, icon, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string key,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon)
  {
    return this.View.Ask(key, (object) null, header, message, buttons, icon);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool refreshRequired)
  {
    return this.View.Ask((string) null, (object) null, header, message, buttons, icon, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon)
  {
    return this.View.Ask((string) null, (object) null, header, message, buttons, icon);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string key,
    Table row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool refreshRequired)
  {
    return this.View.Ask(key, (object) row, header, message, buttons, icon, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string key,
    Table row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon)
  {
    return this.View.Ask(key, (object) row, header, message, buttons, icon);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    Table row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool refreshRequired)
  {
    return this.View.Ask((object) row, header, message, buttons, icon, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    Table row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon)
  {
    return this.View.Ask((object) row, header, message, buttons, icon);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control. The method requests repainting of the
  /// panel.</summary>
  /// <param name="key">The identifier of the panel to display.</param>
  public WebDialogResult AskExt(string key) => this.View.AskExt(key);

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control.</summary>
  /// <param name="key">The identifier of the panel to display.</param>
  /// <param name="refreshRequired">The value that indicates whether the
  /// dialog should be repainted or displayed as it was cached. If
  /// <tt>true</tt>, the dialog is repainted.</param>
  public WebDialogResult AskExt(string key, bool refreshRequired)
  {
    return this.View.AskExt(key, refreshRequired);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control. As a key, the method uses the name of
  /// the variable that holds the BQL statement. The method requests
  /// repainting of the panel.</summary>
  public WebDialogResult AskExt() => this.View.AskExt();

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control. As a key, the method uses the name of
  /// the variable that holds the BQL statement. The method requests
  /// repainting of the panel.</summary>
  public WebDialogResult AskExt(MessageButtons buttons) => this.View.AskExt(buttons);

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control. As a key, the method uses the name of
  /// the variable that holds the BQL statement.</summary>
  /// <param name="refreshRequired">The value that indicates whether the
  /// dialog should be repainted or displayed as it was cached. If
  /// <tt>true</tt>, the dialog is repainted.</param>
  public WebDialogResult AskExt(bool refreshRequired) => this.View.AskExt(refreshRequired);

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control.</summary>
  /// <param name="key">The identifier of the panel to display.</param>
  /// <param name="initializeHandler">The delegate of the method that is
  /// called before the dialog is displayed.</param>
  public WebDialogResult AskExt(string key, PXView.InitializePanel initializeHandler)
  {
    return this.View.AskExt(key, initializeHandler);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control.</summary>
  /// <param name="key">The identifier of the panel to display.</param>
  /// <param name="initializeHandler">The delegate of the method that is
  /// called before the dialog is displayed.</param>
  /// <param name="refreshRequired">The value that indicates whether the
  /// dialog should be repainted or displayed as it was cached. If
  /// <tt>true</tt>, the dialog is repainted.</param>
  public WebDialogResult AskExt(
    string key,
    PXView.InitializePanel initializeHandler,
    bool refreshRequired)
  {
    return this.View.AskExt(key, initializeHandler, refreshRequired);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control.</summary>
  /// <param name="initializeHandler">The delegate of the method that is
  /// called before the dialog is displayed.</param>
  public WebDialogResult AskExt(PXView.InitializePanel initializeHandler)
  {
    return this.View.AskExt(initializeHandler);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control.</summary>
  /// <param name="initializeHandler">The delegate of the method that is
  /// called before the dialog is displayed.</param>
  /// <param name="refreshRequired">The value that indicates whether the
  /// dialog should be repainted or displayed as it was cached. If
  /// <tt>true</tt>, the dialog is repainted.</param>
  public WebDialogResult AskExt(PXView.InitializePanel initializeHandler, bool refreshRequired)
  {
    return this.View.AskExt(initializeHandler, refreshRequired);
  }

  /// <summary>Gets or sets the <tt>Current</tt> property of the cache that
  /// corresponds to the DAC specified in the type parameter.</summary>
  public virtual Table Current
  {
    get => (Table) this.View.Cache.Current;
    set => this.View.Cache.Current = (object) value;
  }

  /// <summary>Inserts a new data record into the cache by invoking the
  /// <see cref="M:PX.Data.PXCache`1.Insert">Insert()</see>
  /// method on the cache. Returns the inserted data record or
  /// <tt>null</tt>-if the insertion fails.</summary>
  public virtual Table Insert() => (Table) this.View.Cache.Insert();

  /// <summary>Inserts the provided data record into the cache by invoking
  /// the <see cref="M:PX.Data.PXCache`1.Insert(System.Object)">Insert(object)</see>
  /// method on the cache. Returns the inserted data record or
  /// <tt>null</tt>-if the insertion fails.</summary>
  /// <param name="item">The data record to insert.</param>
  public virtual Table Insert(Table item) => (Table) this.View.Cache.Insert((object) item);

  /// <summary>Initializes a data record of the derived DAC from the provided data record of the base DAC and inserts the new data record into the cache. Returns the inserted
  /// data record.</summary>
  /// <param name="item">The instance of the base DAC.</param>
  /// <remarks>The method relies on the <see cref="M:PX.Data.PXCache`1.Extend``1(``0)">Extend&lt;Parent&gt;(Parent)</see>
  /// method called on the cache.</remarks>
  /// <example><para>Suppose that the B DAC derives from the A DAC, as follows.</para>
  ///   <code title="Example" lang="CS">
  /// [Serializable]
  /// public class A : PXBqlTable, IBqlTable { ... }
  /// [Serializable]
  /// public class B : A { ... }</code>
  ///   <code title="Example2" description="The following data views can be declared in a graph." lang="CS">
  /// PXSelect&lt;A&gt; BaseRecords;
  /// PXSelect&lt;B&gt; Records;</code>
  ///   <code title="Example3" description="The code above will result in initialization of two caches, of PXCache&lt;A&gt; and PXCache&lt;B&gt; types. The following code initializes a data record of derived type and inserts it into the cache." lang="CS">
  /// A baseRec = BaseRecords.Insert();
  /// B rec = Records.Extend&lt;B&gt;(baseRec);</code>
  /// </example>
  public virtual Table Extend<Parent>(Parent item) where Parent : class, IBqlTable, new()
  {
    return (Table) this.View.Cache.Extend<Parent>(item);
  }

  /// <summary>Updates the data record in the cache by invoking the
  /// <see cref="M:PX.Data.PXCache`1.Update(System.Object)">Update(object)</see>
  /// method on the cache. </summary>
  /// <param name="item">The data record to be updated.</param>
  /// <returns>Returns the updated data record.</returns>
  public virtual Table Update(Table item) => (Table) this.View.Cache.Update((object) item);

  public Table UpdateCurrent()
  {
    return (object) this.Current == null ? default (Table) : this.Update(this.Current);
  }

  /// <summary>Deletes the data record by invoking the
  /// <see cref="M:PX.Data.PXCache`1.Delete(System.Object)">Delete(object)</see>
  /// method on the cache. Returns the data record marked as
  /// deleted.</summary>
  /// <param name="item">The data record to delete.</param>
  public virtual Table Delete(Table item) => (Table) this.View.Cache.Delete((object) item);

  public Table DeleteCurrent()
  {
    return (object) this.Current == null ? default (Table) : this.Delete(this.Current);
  }

  /// <summary>Searches the cache for the data record that has the same key
  /// fields as the provided data record, by invoking the
  /// <see cref="M:PX.Data.PXCache`1.Locate(System.Object)">Locate(object)</see>
  /// method on the cache. Returns the data record if it is found in the
  /// cache or null otherwise.</summary>
  /// <param name="item">The data record that is searched in the cache by
  /// the values of its key fields.</param>
  public virtual Table Locate(Table item) => (Table) this.View.Cache.Locate((object) item);

  internal PXResultset<Table> this[params object[] arguments] => this.Select(arguments);

  /// <summary>Retrieves the top data record of the data set that
  /// corresponds to the BQL statement.</summary>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public virtual Table SelectSingle(params object[] arguments) => (Table) this.Select(arguments);

  /// <summary>Executes the BQL statement and retrieves all matching data
  /// records.</summary>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public virtual PXResultset<Table> Select(params object[] arguments)
  {
    return this.selectBound((object[]) null, arguments);
  }

  public Table[] SelectMain(params object[] arguments)
  {
    return this.Select(arguments).RowCast<Table>().ToArray<Table>();
  }

  public TJoinedTable[] Select<TJoinedTable>(params object[] arguments) where TJoinedTable : IBqlTable
  {
    return Enumerable.ToArray<TJoinedTable>(this.Select(arguments).RowCast<TJoinedTable>());
  }

  /// <summary>Executes the view delegate query that is combined with the view context (that is
  /// the user defined filters, sorting settings, and range of records) and retrieves all matching data records.</summary>
  /// <param name="parameters">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// query. If no parameters are passed to the method,
  /// the <see cref="P:PX.Data.PXView.Parameters" /> array is used instead.</param>
  /// <returns>The method returns the data records that are retrieved by the query execution.
  /// <see cref="T:PX.Data.PXDelegateResult" /> contains information about whether the result is
  /// truncated by a specific number of records, filtered, or sorted.</returns>
  public PXDelegateResult SelectDelegateResult(params object[] parameters)
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultFiltered = true;
    pxDelegateResult.IsResultTruncated = true;
    pxDelegateResult.IsResultSorted = true;
    int startRow = PXView.StartRow;
    int totalRows = 0;
    pxDelegateResult.AddRange((IEnumerable<object>) this.View.Select(PXView.Currents, parameters ?? PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow, PXView.MaximumRows, ref totalRows));
    return pxDelegateResult;
  }

  /// <summary>
  /// Executes the BQL statement combined with the view context (namely,
  /// the user defined filters, sorting settings, and records range) and retrieves all matching data records.
  /// </summary>
  /// <param name="parameters">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement. If no parameters are passed to the method,
  /// the <tt>PXView.Parameters</tt> array is used instead.</param>
  /// <returns>Data records retrieved by executing the BQL statement.</returns>
  public PXResultset<Table> SelectWithViewContext(params object[] parameters)
  {
    int startRow = PXView.StartRow;
    int totalRows = 0;
    List<object> objectList = this.View.Select(PXView.Currents, parameters ?? PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow, PXView.MaximumRows, ref totalRows);
    PXView.StartRow = 0;
    PXResultset<Table> pxResultset = new PXResultset<Table>();
    foreach (object obj in objectList)
    {
      if (!(obj is PXResult<Table>))
      {
        if (obj is Table i0)
          pxResultset.Add(new PXResult<Table>(i0));
      }
      else
        pxResultset.Add((PXResult<Table>) obj);
    }
    return pxResultset;
  }

  /// <summary>
  /// Executes the BQL statement combined with the view context (namely,
  /// the user defined filters, sorting settings, and records range) and returns the <tt>IEnumerable</tt>
  /// interface that gives access to all matching data records.
  /// </summary>
  /// <param name="parameters">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement. If no parameters are passed to the method,
  /// the <tt>PXView.Parameters</tt> array is used instead.</param>
  /// <returns>The <tt>IEnumerable</tt> interface to data records
  /// retrieved by executing the BQL statement.</returns>
  public IEnumerable<PXResult<Table>> SelectWithViewContextLazy(params object[] parameters)
  {
    PXSelectBase<Table> pxSelectBase = this;
    int maxRows = System.Math.Max(PXView.MaximumRows * 2, 100);
    if (PXView.MaximumRows == 0)
      maxRows = 0;
    int returnedRows = 0;
    while (true)
    {
      int startRow = PXView.StartRow;
      int totalRows = 0;
      List<object> list = pxSelectBase.View.Select(PXView.Currents, parameters ?? PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow, maxRows, ref totalRows);
      PXView.StartRow = 0;
      foreach (object obj in list.Skip<object>(returnedRows))
      {
        if (!(obj is PXResult<Table>))
        {
          if (obj is Table i0)
          {
            ++returnedRows;
            yield return new PXResult<Table>(i0);
          }
        }
        else
        {
          ++returnedRows;
          yield return (PXResult<Table>) obj;
        }
      }
      if (PXView.MaximumRows != 0 && list.Count >= maxRows)
      {
        maxRows *= 3;
        list = (List<object>) null;
      }
      else
        break;
    }
  }

  /// <exclude />
  internal virtual PXResultset<Table> selectBound(object[] currents, params object[] arguments)
  {
    PXResultset<Table> pxResultset = new PXResultset<Table>();
    if (!WebConfig.EnableSignleRowOptimizations || (object) this.View.BqlDelegate != null)
    {
      foreach (object obj in this.View.SelectMultiBound(currents, arguments))
      {
        if (!(obj is PXResult<Table>))
        {
          if (obj is Table i0)
            pxResultset.Add(new PXResult<Table>(i0));
        }
        else
          pxResultset.Add((PXResult<Table>) obj);
      }
    }
    else
      pxResultset.SetDelayedQuery(new PXDelayedQuery()
      {
        View = this.View,
        arguments = arguments,
        currents = currents,
        restrictedFields = new RestrictedFieldsSet(this.View.RestrictedFields)
      });
    return pxResultset;
  }

  /// <summary>Searches for a data record by the value of specified field in
  /// the data set that corresponds to the BQL statement. The method extends
  /// the BQL statement with filtering and ordering by the specified field
  /// and retrieves the top data record.</summary>
  /// <param name="field0">The value of <tt>Field0</tt> by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  /// <example>
  /// The code below finds the data record with the given reference number
  /// among the possible results of the data view.
  /// <code>
  /// // Defining the data view in a graph
  /// public PXSelect&lt;ARInvoice,
  ///     Where&lt;ARInvoice.docType, Equal&lt;Optional&lt;ARInvoice.docType&gt;&gt;&gt;&gt; Document;
  /// ...
  /// // Search a data record with the given value of the RefNbr field
  /// Document.Search&lt;ARInvoice.refNbr&gt;(ardoc.RefNbr, ardoc.DocType);
  /// 
  /// // The Current property is now pointing to the data record found
  /// // by Search&lt;&gt;(...)
  /// Document.Current.InstallmentCntr = Convert.ToInt16(installments.Count);
  /// ...</code>
  /// </example>
  public virtual PXResultset<Table> Search<Field0>(object field0, params object[] arguments) where Field0 : IBqlField
  {
    return this.SearchWindowed<Asc<Field0>>(new object[1]
    {
      field0
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0">The value by which the data set is filtered and sorted.</param>
  /// <param name="field1">The value by which the data set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public virtual PXResultset<Table> Search<Field0, Field1>(
    object field0,
    object field1,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1>>>(new object[2]
    {
      field0,
      field1
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field1">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field2">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public virtual PXResultset<Table> Search<Field0, Field1, Field2>(
    object field0,
    object field1,
    object field2,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2>>>>(new object[3]
    {
      field0,
      field1,
      field2
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field1">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field2">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field3">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public virtual PXResultset<Table> Search<Field0, Field1, Field2, Field3>(
    object field0,
    object field1,
    object field2,
    object field3,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3>>>>>(new object[4]
    {
      field0,
      field1,
      field2,
      field3
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field1">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field2">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field3">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field4">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public virtual PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4>>>>>>(new object[5]
    {
      field0,
      field1,
      field2,
      field3,
      field4
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field1">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field2">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field3">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field4">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field5">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public virtual PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5>>>>>>>(new object[6]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field1">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field2">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field3">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field4">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field5">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field6">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public virtual PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6>>>>>>>>(new object[7]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field1">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field2">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field3">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field4">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field5">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field6">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field7">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public virtual PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6, Field7>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    object field7,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6, Asc<Field7>>>>>>>>>(new object[8]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6,
      field7
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field1">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field2">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field3">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field4">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field5">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field6">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field7">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field8">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public virtual PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    object field7,
    object field8,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6, Asc<Field7, Asc<Field8>>>>>>>>>>(new object[9]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6,
      field7,
      field8
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="field0">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field1">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field2">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field3">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field4">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field5">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field6">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field7">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field8">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="field9">The value by which the data set is filtered and
  /// sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public virtual PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9>(
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    object field7,
    object field8,
    object field9,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
    where Field9 : IBqlField
  {
    return this.SearchWindowed<Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6, Asc<Field7, Asc<Field8, Asc<Field9>>>>>>>>>>>(new object[10]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6,
      field7,
      field8,
      field9
    }, 0, 1, arguments);
  }

  /// <summary>Searches the data set that corresponds to the BQL statement
  /// for all data records whose fields have the specified values. The
  /// fields are specified in the type parameter. The method extends the BQL
  /// statement with filtering and ordering by the fields and retrieves all
  /// data records from the resulting data set.</summary>
  /// <param name="searchValues">The values of fields referenced in
  /// <tt>Sort</tt> by which the data set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  /// <remarks>Though ordering may seem superfluous here, it is needed for
  /// better performance of the selection from the database.</remarks>
  /// <example><para>The code below searches the data view for all data records whose TranClass field has the G value.</para>
  /// <code title="Example" lang="CS">
  /// // Data view definition in a graph
  /// public PXSelect&lt;GLTran,
  ///     Where&lt;GLTran.module, Equal&lt;Current&lt;Batch.module&gt;&gt;,
  ///         And&lt;GLTran.batchNbr, Equal&lt;Current&lt;Batch.batchNbr&gt;&gt;&gt;&gt;&gt; Trans;
  /// ...
  /// // Code in some method
  /// foreach(GLTran tran in
  ///             Trans.SearchAll&lt;Asc&lt;GLTran.tranClass&gt;&gt;(new object [] {"G"}))
  ///     ...</code>
  /// </example>
  public virtual PXResultset<Table> SearchAll<Sort>(
    object[] searchValues,
    params object[] arguments)
    where Sort : IBqlSortColumn
  {
    return this.SearchWindowed<Sort>(searchValues, 0, 0, arguments);
  }

  /// <summary>Retrieves the specified number of contiguous data records
  /// starting from the given position in the filtered data set. The fields
  /// are specified in the type parameter. The method extends the BQL
  /// statement with filtering and ordering by the fields and requests the
  /// limited numer of data records.</summary>
  /// <param name="searchValues">The values of fields referenced in
  /// <tt>Sort</tt> by which the data set is filtered and sorted.</param>
  /// <param name="startRow">The 0-based index of the first data record to
  /// retrieve.</param>
  /// <param name="totalRows">The number of data records to
  /// retrieve.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  /// <example><para>The code below retrieves the first five data records whose TranClass field has the G value from the data view.</para>
  /// <code title="Example" lang="CS">
  /// // Data view definition in a graph
  /// public PXSelect&lt;GLTran,
  ///     Where&lt;GLTran.module, Equal&lt;Current&lt;Batch.module&gt;&gt;,
  ///         And&lt;GLTran.batchNbr, Equal&lt;Current&lt;Batch.batchNbr&gt;&gt;&gt;&gt;&gt; Trans;
  /// ...
  /// // Code in some method
  /// PXResultset&lt;GLTran&gt; res =
  ///     Trans.SearchWindowed&lt;Asc&lt;GLTran.tranClass&gt;&gt;(new object [] {"G"}, 0, 5);</code>
  /// </example>
  public virtual PXResultset<Table> SearchWindowed<Sort>(
    object[] searchValues,
    int startRow,
    int totalRows,
    params object[] arguments)
    where Sort : IBqlSortColumn
  {
    PXResultset<Table> pxResultset = new PXResultset<Table>();
    int totalRows1 = 0;
    if (totalRows == 0)
      totalRows1 = -2;
    string[] columns;
    bool[] descendings;
    PXSelectBase<Table>.generateSort<Sort>(out columns, out descendings);
    PXFilterRow[] filters = (PXFilterRow[]) null;
    if (totalRows == 0)
    {
      filters = new PXFilterRow[columns.Length < searchValues.Length ? columns.Length : searchValues.Length];
      for (int index = 0; index < filters.Length; ++index)
        filters[index] = new PXFilterRow(columns[index], PXCondition.EQ, searchValues[index]);
      searchValues = (object[]) null;
    }
    foreach (object obj in this.View.Select((object[]) null, arguments, searchValues, columns, descendings, filters, ref startRow, totalRows, ref totalRows1))
    {
      if (!(obj is PXResult<Table>))
      {
        if (obj is Table i0)
          pxResultset.Add(new PXResult<Table>(i0));
      }
      else
        pxResultset.Add((PXResult<Table>) obj);
    }
    return pxResultset;
  }

  /// <summary>Retrieves the specified number of data records starting from
  /// the given position.</summary>
  /// <param name="startRow">The 0-based index of the first data record to
  /// retrieve.</param>
  /// <param name="totalRows">The number of data records to
  /// retrieve.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  /// <example><para>The code below retrieves the first data record from the data set that corresponds to the BQL statement.</para>
  /// <code title="Example" lang="CS">
  /// // Initializing the data view
  /// PXSelectBase&lt;FinPeriod&gt; select = new PXSelect&lt;FinPeriod,
  ///     Where&lt;FinPeriod.finYear, Equal&lt;Required&lt;FinPeriod.finYear&gt;&gt;&gt;,
  ///     OrderBy&lt;Asc&lt;FinPeriod.periodNbr&gt;&gt;&gt;(sender.Graph);
  /// // Executing the data view
  /// FinPeriod fp = select.SelectWindowed(0, 1, DateTime.Now.Year);</code>
  /// </example>
  public virtual PXResultset<Table> SelectWindowed(
    int startRow,
    int totalRows,
    params object[] arguments)
  {
    PXResultset<Table> pxResultset = new PXResultset<Table>();
    int totalRows1 = 0;
    foreach (object obj in this.View.Select((object[]) null, arguments, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, totalRows, ref totalRows1))
    {
      if (!(obj is PXResult<Table>))
      {
        if (obj is Table i0)
          pxResultset.Add(new PXResult<Table>(i0));
      }
      else
        pxResultset.Add((PXResult<Table>) obj);
    }
    return pxResultset;
  }

  /// <summary>Appends a filtering expression to the BQL statement via the
  /// logical <tt>And</tt>. The additional filtering expression is provided in the
  /// type parameter.</summary>
  /// <typeparam name="TWhere">The additional filtering expression.</typeparam>
  /// <example><para>The code below appends additional comparison to the BQL statement when the corresponding field in the filter is set to a value.</para>
  ///   <code title="Example" lang="CS">
  /// // Initializing the data view
  /// PXSelectBase&lt;APDocumentResult&gt; sel = new PXSelect&lt;APDocumentResult,
  ///     Where&lt;APRegister.vendorID, Equal&lt;Current&lt;APDocumentFilter.vendorID&gt;&gt;&gt;,
  ///     OrderBy&lt;Desc&lt;APDocumentResult.docDate&gt;&gt;&gt;&gt;(this);
  /// // Checking whether a filter object has a value in the BranchID field
  /// if (Filter.Current.BranchID != null)
  /// {
  ///     // Extending the Where clause with additional condition
  ///     sel.WhereAnd&lt;Where&lt;APRegister.branchID,
  ///                      Equal&lt;Current&lt;APDocumentFilter.branchID&gt;&gt;&gt;&gt;();
  /// }</code>
  /// </example>
  public void WhereAnd<TWhere>() where TWhere : IBqlWhere, new() => this.View.WhereAnd<TWhere>();

  /// <summary>Appends a filtering expression to the BQL statement via the logical <tt>And</tt>. The additional filtering expression is specified in the parameter.</summary>
  /// <param name="where">The additional filtering expression.</param>
  public void WhereAnd(System.Type where) => this.View.WhereAnd(where);

  /// <summary>Appends a filtering expression to the BQL statement via the logical <tt>Or</tt>. The additional filtering expression is provided in the type parameter.</summary>
  /// <typeparam name="TWhere">The additional filtering expression.</typeparam>
  public void WhereOr<TWhere>() where TWhere : IBqlWhere, new() => this.View.WhereOr<TWhere>();

  /// <summary>Appends a filtering expression to the BQL statement via the logical <tt>Or</tt>. The additional filtering expression is specified in the parameter.</summary>
  /// <param name="where">The additional filtering expression.</param>
  public void WhereOr(System.Type where) => this.View.WhereOr(where);

  /// <summary>Replaces the <tt>OrderBy</tt> clause if the BQL statement has
  /// one, otherwise the new <tt>OrderBy</tt> clause is simply attached to
  /// the BQL statement.</summary>
  /// <example>
  /// The code below initializes a data view as a local variable and adds
  /// different ordering expression depending on the value of a variable.
  /// <code title="Example">
  /// // Initialization of a data view
  /// PXSelectBase&lt;INLotSerialStatus&gt; cmd =
  ///     new PXSelect&lt;INLotSerialStatus, ...&gt;(this);
  /// 
  /// // Adding a different ordering expression depending on
  /// // a variable's value
  /// switch (lotSerIssueMethod)
  /// {
  ///     case INLotSerIssueMethod.FIFO:
  ///         cmd.OrderByNew&lt;
  ///             OrderBy&lt;Asc&lt;INLocation.pickPriority,
  ///                     Asc&lt;INLotSerialStatus.receiptDate,
  ///                     Asc&lt;INLotSerialStatus.lotSerialNbr&gt;&gt;&gt;&gt;&gt;();
  ///         break;
  ///     case INLotSerIssueMethod.LIFO:
  ///         cmd.OrderByNew&lt;
  ///             OrderBy&lt;Asc&lt;INLocation.pickPriority,
  ///                     Desc&lt;INLotSerialStatus.receiptDate,
  ///                     Asc&lt;INLotSerialStatus.lotSerialNbr&gt;&gt;&gt;&gt;&gt;();
  ///         break;
  ///     ...
  /// }</code>
  /// </example>
  public virtual void OrderByNew<newOrderBy>() where newOrderBy : IBqlOrderBy, new()
  {
    this.View.OrderByNew<newOrderBy>();
  }

  /// <summary>Appends a joining clause to the BQL statement.</summary>
  /// <example><para>The code below appends the LeftJoin clause to the BQL statement.</para>
  /// <code title="Example" lang="CS">
  /// PXSelectBase&lt;GLTran&gt; select = new PXSelect&lt;GLTran&gt;(this);
  /// select.Join&lt;LeftJoin&lt;AP.APTran,
  ///     On&lt;AP.APTran.refNbr, Equal&lt;GLTran.refNbr&gt;,
  ///     And&lt;AP.APTran.lineNbr, Equal&lt;GLTran.tranLineNbr&gt;&gt;&gt;&gt;&gt;();</code>
  /// </example>
  public virtual void Join<join>() where join : IBqlJoin, new() => this.View.Join<join>();

  /// <summary>Adds logical <tt>Not</tt> to the whole <tt>Where</tt> clause of the BQL statement, reversing the condition to the opposite.</summary>
  public void WhereNot() => this.View.WhereNot();

  /// <summary>Replaces the filtering expression in the BQL statement. The
  /// new filtering expression is provided in the type parameter.</summary>
  /// <typeparam name="newWhere">The new filtering expression.</typeparam>
  /// <example><para>The code below replaces the Where clause in a data view</para>
  ///   <code title="Example" lang="CS">
  /// // Defining the data view in a graph
  /// public PXSelect&lt;ARInvoice,
  ///     Where&lt;ARInvoice.docType, Equal&lt;Current&lt;ARInvoice.docType&gt;&gt;,
  ///         And2&lt;Where&lt;ARInvoice.origModule, Equal&lt;BatchModule.moduleAR&gt;,
  ///                  Or&lt;ARInvoice.released, Equal&lt;True&gt;&gt;&gt;&gt;&gt;&gt; Document;
  /// ...
  /// // Replacing the Where clause
  /// Document.WhereNew&lt;
  ///     Where&lt;ARInvoice.docType, Equal&lt;Required&lt;ARInvoice.docType&gt;&gt;&gt;&gt;();
  /// // Getting an ARInvoice data record
  /// ARInvoice ardoc = (ARInvoice)resultsetRecord;
  /// // Executing the modified data view
  /// Document.Select(ardoc.DocType);</code>
  /// </example>
  public void WhereNew<newWhere>() where newWhere : IBqlWhere, new()
  {
    this.View.WhereNew<newWhere>();
  }

  internal virtual PXResultset<Table> this[int argument]
  {
    get => this[new object[1]{ (object) argument }];
  }

  internal virtual PXResultset<Table> this[string arguments]
  {
    get => this[PXSelectBase.ParseString(arguments)];
  }

  /// <summary>Returns the type of the DAC provided as the type parameter of
  /// <tt>PXSelectBase&lt;&gt;</tt> class. For BQL statements that are
  /// derived from <tt>PXSelectBase&lt;&gt;</tt>, it is the first mentioned
  /// DAC.</summary>
  public virtual System.Type GetItemType() => typeof (Table);

  protected static void generateSort<Sort>(out string[] columns, out bool[] descendings) where Sort : IBqlSortColumn
  {
    List<string> stringList = new List<string>();
    List<bool> boolList = new List<bool>();
    System.Type[] genericArguments;
    for (System.Type type = typeof (Sort); type.IsGenericType; type = genericArguments[1])
    {
      genericArguments = type.GetGenericArguments();
      stringList.Add(char.ToUpper(genericArguments[0].Name[0]).ToString() + genericArguments[0].Name.Substring(1));
      System.Type genericTypeDefinition = type.GetGenericTypeDefinition();
      boolList.Add(genericTypeDefinition == typeof (Desc<>) || genericTypeDefinition == typeof (Desc<,>));
      if (genericArguments.Length == 1)
        break;
    }
    columns = stringList.ToArray();
    descendings = boolList.ToArray();
  }

  protected static Resultset selectBound<Resultset>(
    BqlCommand command,
    bool readOnly,
    PXGraph graph,
    int startRow,
    int totalRows,
    object[] currents,
    params object[] pars)
    where Resultset : PXResultset<Table>, new()
  {
    Resultset resultset = new Resultset();
    int totalRows1 = 0;
    PXView view = graph.TypedViews.GetView(command, readOnly);
    if (!WebConfig.EnableSignleRowOptimizations || (object) view.BqlDelegate != null)
    {
      foreach (object obj in view.Select(currents, pars, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, totalRows, ref totalRows1))
      {
        if (!(obj is PXResult<Table>))
        {
          if (obj is Table i0)
            resultset.Add(new PXResult<Table>(i0));
        }
        else
          resultset.Add((PXResult<Table>) obj);
      }
    }
    else
      resultset.SetDelayedQuery(new PXDelayedQuery()
      {
        View = view,
        arguments = pars,
        currents = currents,
        startRow = startRow,
        maxRows = totalRows,
        restrictedFields = new RestrictedFieldsSet(view.RestrictedFields)
      });
    return resultset;
  }

  internal static IPXAsyncResultset<PXResult<Table>> selectBoundAsync(
    BqlCommand command,
    bool readOnly,
    PXGraph graph,
    int startRow,
    int totalRows,
    object[] currents,
    CancellationToken cancellationToken,
    params object[] pars)
  {
    int totalRows1 = 0;
    PXView view = graph.TypedViews.GetView(command, readOnly);
    if (!WebConfig.EnableSignleRowOptimizations || (object) view.BqlDelegate != null)
      return (IPXAsyncResultset<PXResult<Table>>) new PXAsyncResultset<Table>(AsyncEnumerable.ToAsyncEnumerable<PXResult<Table>>(view.Select(currents, pars, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, totalRows, ref totalRows1).Select<object, PXResult<Table>>((Func<object, PXResult<Table>>) (item =>
      {
        switch (item)
        {
          case PXResult<Table> pxResult2:
            return pxResult2;
          case Table i0_2:
            return new PXResult<Table>(i0_2);
          default:
            return (PXResult<Table>) null;
        }
      })).Where<PXResult<Table>>((Func<PXResult<Table>, bool>) (item => item != null))), cancellationToken);
    return (IPXAsyncResultset<PXResult<Table>>) new PXAsyncQueryableResultset<Table>(new PXDelayedQuery()
    {
      View = view,
      arguments = pars,
      currents = currents,
      startRow = startRow,
      maxRows = totalRows,
      restrictedFields = new RestrictedFieldsSet(view.RestrictedFields)
    }, cancellationToken);
  }

  protected static Resultset select<Resultset>(
    BqlCommand command,
    bool readOnly,
    PXGraph graph,
    int startRow,
    int totalRows,
    params object[] pars)
    where Resultset : PXResultset<Table>, new()
  {
    return PXSelectBase<Table>.selectBound<Resultset>(command, readOnly, graph, startRow, totalRows, (object[]) null, pars);
  }

  protected static Resultset search<Resultset, Sort>(
    BqlCommand command,
    bool readOnly,
    PXGraph graph,
    object[] searchValues,
    int startRow,
    int totalRows,
    params object[] pars)
    where Resultset : PXResultset<Table>, new()
    where Sort : IBqlSortColumn
  {
    Resultset resultset = new Resultset();
    int totalRows1 = 0;
    if (totalRows == 0)
      totalRows1 = -2;
    string[] columns;
    bool[] descendings;
    PXSelectBase<Table>.generateSort<Sort>(out columns, out descendings);
    foreach (object obj in graph.TypedViews.GetView(command, readOnly).Select((object[]) null, pars, searchValues, columns, descendings, (PXFilterRow[]) null, ref startRow, totalRows, ref totalRows1))
    {
      if (!(obj is PXResult<Table>))
      {
        if (obj is Table i0)
          resultset.Add(new PXResult<Table>(i0));
      }
      else
        resultset.Add((PXResult<Table>) obj);
    }
    return resultset;
  }

  protected static void Clear(BqlCommand command, bool readOnly, PXGraph graph)
  {
    graph.TypedViews.GetView(command, readOnly).Clear();
  }

  protected static void StoreCached(
    BqlCommand command,
    bool readOnly,
    PXGraph graph,
    PXCommandKey queryKey,
    List<object> records)
  {
    graph.TypedViews.GetView(command, readOnly).StoreCached(queryKey, records);
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// </summary>
  protected static void StoreResult(
    BqlCommand command,
    bool readOnly,
    PXGraph graph,
    List<object> selectResult,
    PXQueryParameters queryParameters)
  {
    graph.TypedViews.GetView(command, readOnly).StoreResult(selectResult, queryParameters);
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// </summary>
  protected static void StoreResult(
    BqlCommand command,
    bool readOnly,
    PXGraph graph,
    List<object> selectResult,
    PXQueryParameters queryParameters,
    bool clearView)
  {
    PXView view = graph.TypedViews.GetView(command, readOnly);
    if (clearView)
      view.Clear();
    view.StoreResult(selectResult, queryParameters);
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// Query parameters determined automatically from the provided selectResult
  /// </summary>
  protected static void StoreResult(
    BqlCommand command,
    bool readOnly,
    PXGraph graph,
    IBqlTable selectResult)
  {
    graph.TypedViews.GetView(command, readOnly).StoreResult(selectResult);
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// Query parameters determined automatically from the provided selectResult
  /// </summary>
  protected static void StoreResult(
    BqlCommand command,
    bool readOnly,
    PXGraph graph,
    PXResult selectResult)
  {
    graph.TypedViews.GetView(command, readOnly).StoreResult(selectResult);
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// </summary>
  public void StoreResult(List<object> selectResult, PXQueryParameters queryParameters)
  {
    this.View.StoreResult(selectResult, queryParameters);
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// Query parameters determined automatically from the provided selectResult
  /// </summary>
  public void StoreResult(IBqlTable selectResult) => this.View.StoreResult(selectResult);

  /// <summary>
  /// Intercepts PXView.Select calls for tail view, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// </summary>
  public void StoreTailResult(PXResult result, object[] currents, params object[] queryParameters)
  {
    this.StoreTailResult(new List<object>()
    {
      (object) result
    }, currents, queryParameters);
  }

  /// <summary>
  /// Intercepts PXView.Select calls for tail view, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// </summary>
  public void StoreTailResult(
    List<object> result,
    object[] currents,
    params object[] queryParameters)
  {
    BqlCommand tail = this.View.BqlSelect is IBqlJoinedSelect bqlSelect ? bqlSelect.GetTail() : (BqlCommand) null;
    if (tail == null)
      return;
    PXView view = this.View.Graph.TypedViews.GetView(tail, true);
    view?.StoreResult(result, PXQueryParameters.ExplicitParameters(view.PrepareParameters(currents, queryParameters)));
  }

  /// <exclude />
  public virtual void StoreCached(PXCommandKey queryKey, List<object> records)
  {
    this.View.StoreCached(queryKey, records);
  }

  /// <summary>Sets the value of the specified field in the given data
  /// record. The method relies on the <see cref="M:PX.Data.PXCache`1.SetValueExt(System.Object,System.String,System.Object)">SetValueExt(object,string, object)</see>
  /// method of the cache.</summary>
  /// <param name="row">The data record whose field value is set.</param>
  /// <param name="value">The value to set to the field.</param>
  public virtual void SetValueExt<Field>(Table row, object value) where Field : IBqlField
  {
    this.SetValueExt((object) row, typeof (Field).Name, value);
  }

  /// <summary>Gets the value of the specified field for the given data
  /// record. The method relies on the <see cref="M:PX.Data.PXCache.GetValueExt``1(System.Object)">GetValueExt&lt;Field&gt;(object)</see>
  /// method of the cache, but unlike the cache's method always
  /// returns a value, not a <tt>PXFieldState</tt> object.</summary>
  /// <param name="row">The data record whose field value is
  /// returned.</param>
  public virtual object GetValueExt<Field>(Table row) where Field : IBqlField
  {
    return this.GetValueExt((object) row, typeof (Field).Name);
  }
}
