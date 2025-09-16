// Decompiled with JetBrains decompiler
// Type: PX.Data.Events
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data;

/// <summary>
/// A class that provides the types that are used as parameters in generic event handlers.
/// Unlike a handler with a parameter from the <see cref="T:PX.Data.AbstractEvents" /> class, a handler with a parameter from this class <b>gets automatically subscribed</b> to the corresponding event.
/// </summary>
public class Events
{
  /// <summary>
  /// A class for the <tt>CacheAttached</tt> event. The class is used in generic event handlers.
  /// </summary>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <remarks>
  ///   <para>The <tt>CacheAttached</tt> handler is used to override data access class (DAC) field attributes declared directly within the DAC. By declaring a
  /// <tt>CacheAttached</tt> handler and attaching <span>appropriate attributes</span> to the handler within a graph, the developer forces the framework to
  /// completely override DAC field attributes within this graph.</para>
  ///   <para>The execution order for the <tt>CacheAttached</tt> event handlers is the following:</para>
  ///   <list type="number">
  ///     <item><description>Attribute event handlers are executed.</description></item>
  ///     <item><description>Graph event handlers are executed.</description></item>
  ///   </list>
  ///   <para></para>
  /// </remarks>
  /// <example>
  /// <para>
  /// The generic event handler has the following signature.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.CacheAttached&lt;DACType.fieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The code below overrides DAC field attributes within a graph.
  /// </para>
  /// <code lang="CS">
  /// public class DimensionMaint : PXGraph&lt;DimensionMaint, Dimension&gt;
  /// {
  ///     ...
  /// 
  ///     [PXDBString(15, IsUnicode = true, IsKey = true)]
  ///     [PXDefault(typeof(Dimension.dimensionID))]
  ///     [PXUIField(DisplayName = "Dimension ID", Visibility =
  ///     PXUIVisibility.Invisible, Visible = false)]
  ///     [PXSelector(typeof(Dimension.dimensionID), DirtyRead = true)]
  ///     protected virtual void _(Events.CacheAttached&lt;Segment.dimensionID&gt; e)
  ///     {
  /// 
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class CacheAttached<TField> : IAutoSubscriptionEventMarker where TField : 
  #nullable disable
  class, IBqlField
  {
    public PXCache Cache { get; }

    public System.Type BqlField => typeof (TField);

    internal CacheAttached(PXCache cache) => this.Cache = cache;
  }

  public abstract class Event<TClassicEventArgs, TGenericEventArgs> : 
    IAutoSubscriptionEventMarker,
    IGenericEventWith<
    #nullable enable
    TClassicEventArgs>
    where TClassicEventArgs : EventArgs
    where TGenericEventArgs : Events.Event<TClassicEventArgs, TGenericEventArgs>, new()
  {
    protected readonly PXCache _cache;
    protected readonly TClassicEventArgs _args;

    public PXCache Cache
    {
      get => this._cache;
      init => this._cache = value;
    }

    public TClassicEventArgs Args
    {
      get => this._args;
      init => this._args = value;
    }

    public delegate void EventDelegate(TGenericEventArgs args)
      where TClassicEventArgs : 
      #nullable disable
      EventArgs
      where TGenericEventArgs : Events.Event<TClassicEventArgs, TGenericEventArgs>, new();

    internal class Adapter : 
      IGenericEventAdapter<
      #nullable enable
      TClassicEventArgs, Events.Event<
      #nullable disable
      TClassicEventArgs, TGenericEventArgs>.EventDelegate>
    {
      public 
      #nullable enable
      Events.Event<
      #nullable disable
      TClassicEventArgs, TGenericEventArgs>.EventDelegate GenericHandler { get; }

      public Adapter(
        #nullable enable
        Events.Event<
        #nullable disable
        TClassicEventArgs, TGenericEventArgs>.EventDelegate genericHandler)
      {
        this.GenericHandler = genericHandler;
      }

      public void Execute(
      #nullable enable
      PXCache cache, TClassicEventArgs args)
      {
        Events.Event<TClassicEventArgs, TGenericEventArgs>.EventDelegate genericHandler = this.GenericHandler;
        TGenericEventArgs args1 = new TGenericEventArgs();
        args1.Cache = cache;
        args1.Args = args;
        genericHandler(args1);
      }
    }

    public delegate void ClassicDelegate(PXCache cache, TClassicEventArgs args)
      where TClassicEventArgs : 
      #nullable disable
      EventArgs
      where TGenericEventArgs : Events.Event<TClassicEventArgs, TGenericEventArgs>, new();

    internal delegate void EventInterceptorDelegate(
      #nullable enable
      TGenericEventArgs args,
      Events.Event<
      #nullable disable
      TClassicEventArgs, TGenericEventArgs>.ClassicDelegate classicHandler)
      where TClassicEventArgs : EventArgs
      where TGenericEventArgs : Events.Event<TClassicEventArgs, TGenericEventArgs>, new();

    internal class AdapterInterceptor
    {
      private readonly 
      #nullable enable
      Events.Event<
      #nullable disable
      TClassicEventArgs, TGenericEventArgs>.EventInterceptorDelegate _genericInterceptor;

      public AdapterInterceptor(
        #nullable enable
        Events.Event<
        #nullable disable
        TClassicEventArgs, TGenericEventArgs>.EventInterceptorDelegate genericInterceptor)
      {
        this._genericInterceptor = genericInterceptor;
      }

      public void Execute(
        #nullable enable
        PXCache cache,
        TClassicEventArgs args,
        Events.Event<
        #nullable disable
        TClassicEventArgs, TGenericEventArgs>.ClassicDelegate classicHandler)
      {
        Events.Event<TClassicEventArgs, TGenericEventArgs>.EventInterceptorDelegate genericInterceptor = this._genericInterceptor;
        TGenericEventArgs args1 = new TGenericEventArgs();
        args1.Cache = cache;
        args1.Args = args;
        Events.Event<TClassicEventArgs, TGenericEventArgs>.ClassicDelegate classicHandler1 = classicHandler;
        genericInterceptor(args1, classicHandler1);
      }
    }
  }

  /// <summary>
  /// A class for the <tt>CommandPreparing</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.ICommandPreparing`2" />.
  /// </summary>
  /// <remarks>
  ///   <para>The <tt>CommandPreparing</tt> event is generated each time the Acumatica data access layer prepares a database-specific SQL statement for a <tt>SELECT</tt>,
  /// <tt>INSERT</tt>, <tt>UPDATE</tt>, or <tt>DELETE</tt> operation. This event is raised for every data access class (DAC) field placed in the <tt>PXCache</tt>
  /// object. By using the <tt>CommandPreparing</tt> event subscriber, you can alter the property values of the <tt>PXCommandPreparingEventArgs.FieldDescription</tt>
  /// object that is used in the generation of an SQL statement.</para>
  ///   <para>The <tt>CommandPreparing</tt> event handler is used in the following cases:</para>
  ///   <list type="bullet">
  ///     <item><description>To exclude a DAC field from a <tt>SELECT</tt>, <tt>INSERT</tt>, or <tt>UPDATE</tt> operation</description></item>
  ///     <item><description>To replace a DAC field from a <tt>SELECT</tt> operation with a custom SQL statement</description></item>
  ///     <item><description>To transform a DAC field value submitted to the server for an <tt>INSERT</tt>, <tt>UPDATE</tt>, or <tt>DELETE</tt> operation</description></item>
  ///   </list>
  ///   <para>The following execution order is used for the <tt>CommandPreparing</tt> event handlers:</para>
  ///   <list type="number">
  ///     <item><description>Graph event handlers are executed.</description></item>
  ///     <item><description>If <tt>e.Cancel</tt> is <tt>false</tt>, attribute event handlers are executed.</description></item>
  ///   </list>
  /// </remarks>
  public abstract class CommandPreparingBase<TGenericEventArgs> : 
    Events.Event<
    #nullable enable
    PXCommandPreparingEventArgs, TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXCommandPreparingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Row" />
    public object Row => this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Operation" />
    public PXDBOperation Operation => this._args.Operation;

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Table" />
    public System.Type Table => this._args.Table;

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Value" />
    public object? Value
    {
      get => this._args.Value;
      set => this._args.Value = value;
    }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.DataType" />
    public PXDbType DataType
    {
      get => this._args.DataType;
      set => this._args.DataType = value;
    }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.DataLength" />
    public int? DataLength
    {
      get => this._args.DataLength;
      set => this._args.DataLength = value;
    }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.DataValue" />
    public object? DataValue
    {
      get => this._args.DataValue;
      set => this._args.DataValue = value;
    }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.IsRestriction" />
    public bool IsRestriction
    {
      get => this._args.IsRestriction;
      set => this._args.IsRestriction = value;
    }

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <inheritdoc />
  public abstract class CommandPreparingBase<TGenericEventArgs, TTable> : 
    Events.CommandPreparingBase<TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXCommandPreparingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Row" />
    public TTable Row => (TTable) base.Row;
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  public class CommandPreparing<TTable, TField> : 
    Events.CommandPreparingBase<Events.CommandPreparing<TTable, TField>, TTable>,
    AbstractEvents.ICommandPreparing<TTable, TField>,
    IGenericEventWith<PXCommandPreparingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField
  {
  }

  /// <inheritdoc />
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler has the following signature.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.CommandPreparing&lt;FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code excludes a DAC field from the UPDATE operation.
  /// </para>
  /// <code>
  /// public class APReleaseProcess : PXGraph&lt;APReleaseProcess&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.CommandPreparing&lt;APRegister.FinPeriodID&gt; e)
  ///     {
  ///         if ((e.Operation &amp; PXDBOperation.Command) == PXDBOperation.Update)
  ///         {
  ///             e.Args.ExcludeFromInsertUpdate();
  ///         }
  ///     }
  /// }
  /// </code>
  /// </example>
  public class CommandPreparing<TField> : 
    Events.CommandPreparingBase<Events.CommandPreparing<TField>, object>,
    AbstractEvents.ICommandPreparing<object, TField>,
    IGenericEventWith<PXCommandPreparingEventArgs>,
    ICancelEventArgs
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Row" />
    public new object Row => base.Row;

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Operation" />
    public new PXDBOperation Operation => base.Operation;

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Table" />
    public new System.Type Table => base.Table;

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.Value" />
    public new object? Value
    {
      get => base.Value;
      set => base.Value = value;
    }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.DataType" />
    public new PXDbType DataType
    {
      get => base.DataType;
      set => base.DataType = value;
    }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.DataLength" />
    public new int? DataLength
    {
      get => base.DataLength;
      set => base.DataLength = value;
    }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.DataValue" />
    public new object? DataValue
    {
      get => base.DataValue;
      set => base.DataValue = value;
    }

    /// <inheritdoc cref="P:PX.Data.PXCommandPreparingEventArgs.IsRestriction" />
    public new bool IsRestriction
    {
      get => base.IsRestriction;
      set => base.IsRestriction = value;
    }

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public new bool Cancel
    {
      get => base.Cancel;
      set => base.Cancel = value;
    }
  }

  /// <summary>
  /// A class for the <tt>ExceptionHandling</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IExceptionHandling`3" />.
  /// </summary>
  /// <remarks>
  ///   <para>The <tt>ExceptionHandling</tt> event is generated in the following cases:</para>
  ///   <list type="bullet">
  ///     <item><description>The <tt>PXSetPropertyException</tt> exception is thrown while the system is processing
  ///     a data access class (DAC) field value received from the UI or
  ///     through the Web Service API when a data record
  ///     is being inserted or updated in the <tt>PXCache</tt> object.</description></item>
  ///     <item><description>The <tt>PXSetPropertyException</tt> exception is thrown while the system is processing
  ///     DAC key field values when the deletion of a data record from the <tt>PXCache</tt> object
  ///     is initiated in the UI or through the Web Service API.</description></item>
  ///     <item><description>The <tt>PXSetPropertyException</tt> exception is thrown when
  ///     the system is assigning the default value to a field
  ///     or updating the value when the assignment or update is initiated by any
  ///     of the following methods of the <tt>PXCache</tt> class:
  ///     <list type="bullet">
  ///     <item><description><tt>Insert(IDictionary)</tt></description></item>
  ///     <item><description><tt>SetDefaultExt(object, string)</tt></description></item>
  ///     <item><description><tt>SetDefaultExt&lt;Field&gt;(object)</tt></description></item>
  ///     <item><description><tt>Update(IDictionary, IDictionary)</tt></description></item>
  ///     <item><description><tt>SetValueExt(object, string, object)</tt></description></item>
  ///     <item><description><tt>SetValueExt&lt;Field&gt;(object, object</tt></description></item>
  ///     </list>
  ///     </description></item>
  ///     <item><description>The <tt>PXSetPropertyException</tt> exception is thrown while the system is converting
  ///     the external DAC key field presentation to the internal field value initiated by any of the following methods
  ///     of the <tt>PXCache</tt> class:
  ///     <list type="bullet">
  ///     <item><description><tt>Locate(IDictionary)</tt></description></item>
  ///     <item><description><tt>Update(IDictionary, IDictionary)</tt></description></item>
  ///     <item><description><tt>Delete(IDictionary, IDictionary)</tt></description></item>
  ///     </list>
  ///     </description></item>
  ///     <item><description>The <tt>PXCommandPreparingException</tt>, <tt>PXRowPersistingException</tt>,
  ///     or <tt>PXRowPersistedException</tt> exception is thrown when an inserted, updated,
  ///     or deleted data record is saved in the database.</description></item>
  ///   </list>
  ///   <para>The <tt>ExceptionHandling</tt> event handler is used to do the following:</para>
  ///   <list type="bullet">
  ///     <item><description>Catch and handle the exceptions mentioned above
  ///     (the platform rethrows all unhandled exceptions)</description></item>
  ///     <item><description>Implement non-standard handling of the exceptions mentioned above</description></item>
  ///   </list>
  ///   <para>The following execution order is used for the <tt>ExceptionHandling</tt> event handlers:</para>
  ///   <list type="number">
  ///     <item><description>Graph event handlers are executed.</description></item>
  ///     <item><description>If <tt>e.Cancel</tt> is <tt>false</tt>, attribute event handlers are executed.</description></item>
  ///   </list>
  /// </remarks>
  public abstract class ExceptionHandlingBase<TGenericEventArgs> : 
    Events.Event<PXExceptionHandlingEventArgs, TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXExceptionHandlingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXExceptionHandlingEventArgs.Row" />
    public object Row => this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXExceptionHandlingEventArgs.Exception" />
    public Exception Exception => this._args.Exception;

    /// <inheritdoc cref="P:PX.Data.PXExceptionHandlingEventArgs.NewValue" />
    public object? NewValue
    {
      get => this._args.NewValue;
      set => this._args.NewValue = value;
    }

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <inheritdoc />
  public abstract class ExceptionHandlingBase<TGenericEventArgs, TTable, TValue> : 
    Events.ExceptionHandlingBase<TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXExceptionHandlingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXExceptionHandlingEventArgs.Row" />
    public TTable Row => (TTable) base.Row;

    /// <inheritdoc cref="P:PX.Data.PXExceptionHandlingEventArgs.NewValue" />
    public TValue? NewValue
    {
      get => (TValue) base.NewValue;
      set => this.NewValue = (object) value;
    }
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <typeparam name="TValue">The type of the field of the DAC that raised the event.</typeparam>
  public class ExceptionHandling<TTable, TField, TValue> : 
    Events.ExceptionHandlingBase<Events.ExceptionHandling<TTable, TField, TValue>, TTable, TValue>,
    AbstractEvents.IExceptionHandling<TTable, TField, TValue>,
    IGenericEventWith<PXExceptionHandlingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler has the following signature.
  /// </para>
  /// <code lang="CS">
  /// protected virtual void _(Events.ExceptionHandling&lt;DACType, FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class ExceptionHandling<TTable, TField> : 
    Events.ExceptionHandlingBase<Events.ExceptionHandling<TTable, TField>, TTable, object?>,
    AbstractEvents.IExceptionHandling<TTable, TField, object?>,
    IGenericEventWith<PXExceptionHandlingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXExceptionHandlingEventArgs.Row" />
    public new TTable Row => base.Row;
  }

  /// <inheritdoc />
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler signature is shown in the following example.
  /// </para>
  /// <code lang="CS">
  /// protected virtual void _(Events.ExceptionHandling&lt;FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code handles
  /// an exception on a DAC field and sets the field value.
  /// </para>
  /// <code lang="CS">
  /// public class APVendorBalanceEnq : PXGraph&lt;APVendorBalanceEnq&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.ExceptionHandling&lt;APHistoryFilter.AccountID&gt; e)
  ///     {
  ///         APHistoryFilter header = e.Row;
  ///         if (header != null)
  ///         {
  ///             e.Cancel = true;
  ///             header.AccountID = null;
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code alters an exception on a DAC field by setting its description.
  /// </para>
  /// <code lang="CS">
  /// public class CustomerMaint :
  ///     BusinessAccountGraphBase&lt;Customer, Customer,
  ///                              Where&lt;BAccount.type,
  ///                                    Equal&lt;BAccountType.customerType&gt;,
  ///                                    Or&lt;BAccount.type,
  ///                                       Equal&lt;BAccountType.combinedType&gt;&gt;&gt;&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.ExceptionHandling&lt;Customer.CustomerClassID&gt; e)
  ///     {
  ///         PXSetPropertyException ex = e.Exception;
  ///         if (ex != null)
  ///         {
  ///             ex.SetMessage(ex.Message + System.Environment.NewLine +
  ///                           System.Environment.NewLine +
  ///                           "Stack Trace:" + System.Environment.NewLine +
  ///                           ex.StackTrace);
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class ExceptionHandling<TField> : 
    Events.ExceptionHandlingBase<Events.ExceptionHandling<TField>, object, object?>,
    AbstractEvents.IExceptionHandling<object, TField, object?>,
    IGenericEventWith<PXExceptionHandlingEventArgs>,
    ICancelEventArgs
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXExceptionHandlingEventArgs.Row" />
    public new object Row => base.Row;
  }

  /// <summary>
  /// A class for the <tt>FieldDefaulting</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IFieldDefaulting`3" />.
  /// </summary>
  /// <remarks>
  ///   <para>The <tt>FieldDefaulting</tt> event is generated in either of the following cases:</para>
  ///   <list type="bullet">
  ///     <item><description>A new record is inserted into the <tt>PXCache</tt> object by user action in the user interface or via the Web API.</description></item>
  ///     <item><description>Any of the following methods of the <tt>PXCache</tt> class initiates the assigning the default value to a field:
  ///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li><li><tt>SetDefaultExt(object, string)</tt></li><li><tt>SetDefaultExt(object)</tt></li></ul></description></item>
  ///   </list>
  ///   <para>The <tt>FieldDefaulting</tt> event handler is used to generate and assign the default value to a data access class (DAC) field.</para>
  ///   <para>The following execution order is used for the <tt>FieldDefaulting</tt> event handlers:</para>
  ///   <list type="number">
  ///     <item><description>Graph event handlers are executed.</description></item>
  ///     <item><description>If <tt>e.Cancel</tt> is <tt>false</tt>, attribute event handlers are executed.</description></item>
  ///   </list>
  /// </remarks>
  public abstract class FieldDefaultingBase<TGenericEventArgs> : 
    Events.Event<PXFieldDefaultingEventArgs, TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXFieldDefaultingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldDefaultingEventArgs.Row" />
    public object Row => this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXFieldDefaultingEventArgs.NewValue" />
    public object? NewValue
    {
      get => this._args.NewValue;
      set => this._args.NewValue = value;
    }

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <inheritdoc />
  public abstract class FieldDefaultingBase<TGenericEventArgs, TTable, TValue> : 
    Events.FieldDefaultingBase<TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXFieldDefaultingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldDefaultingEventArgs.Row" />
    public TTable Row => (TTable) base.Row;

    /// <inheritdoc cref="P:PX.Data.PXFieldDefaultingEventArgs.NewValue" />
    public TValue? NewValue
    {
      get => (TValue) base.NewValue;
      set => this.NewValue = (object) value;
    }
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <typeparam name="TValue">The type of the field of the DAC that raised the event.</typeparam>
  public class FieldDefaulting<TTable, TField, TValue> : 
    Events.FieldDefaultingBase<Events.FieldDefaulting<TTable, TField, TValue>, TTable, TValue>,
    AbstractEvents.IFieldDefaulting<TTable, TField, TValue>,
    IGenericEventWith<PXFieldDefaultingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler has the following signature.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.FieldDefaulting&lt;DACType, FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class FieldDefaulting<TTable, TField> : 
    Events.FieldDefaultingBase<Events.FieldDefaulting<TTable, TField>, TTable, object?>,
    AbstractEvents.IFieldDefaulting<TTable, TField, object?>,
    IGenericEventWith<PXFieldDefaultingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldDefaultingEventArgs.Row" />
    public new TTable Row => base.Row;
  }

  /// <inheritdoc />
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler signature is shown below.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.FieldDefaulting&lt;FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The example below shows how to assign a default value for a DAC field.
  /// </para>
  /// <code>
  /// public class POOrderEntry : PXGraph&lt;POOrderEntry, POOrder&gt;,
  ///                             PXImportAttribute.IPXPrepareItems
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.FieldDefaulting&lt;POOrder.ExpectedDate&gt; e)
  ///     {
  ///         POOrder row = e.Row;
  ///         Location vendorLocation = this.location.Current;
  ///         if (row != null &amp;&amp; row.OrderDate.HasValue)
  ///         {
  ///             int offset = (vendorLocation != null ?
  ///                          (int)(vendorLocation.VLeadTime ?? 0) : 0);
  ///             e.NewValue = row.OrderDate.Value.AddDays(offset);
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class FieldDefaulting<TField> : 
    Events.FieldDefaultingBase<Events.FieldDefaulting<TField>, object, object?>,
    AbstractEvents.IFieldDefaulting<object, TField, object?>,
    IGenericEventWith<PXFieldDefaultingEventArgs>,
    ICancelEventArgs
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldDefaultingEventArgs.Row" />
    public new object Row => base.Row;
  }

  /// <summary>
  /// A class for the <tt>FieldSelecting</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IFieldSelecting`2" />.
  /// </summary>
  /// <remarks>
  ///   <para>The <tt>FieldSelecting</tt> event is generated in the following cases:</para>
  ///   <list type="bullet">
  ///     <item><description>When the external representation (that is, with the way the value should be displayed in the UI) of
  ///     a data access class field (DAC) value is requested
  ///     from the UI or through the Web Service API.</description></item>
  ///     <item><description>When any of the following methods of the <tt>PXCache</tt> class initiates the assigning the default value to a field:
  ///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li></ul></description></item>
  ///     <item><description>While a field is updated in the <tt>PXCache</tt> object, and this action is initiated
  ///     by any of the following methods of the <tt>PXCache</tt> class:
  ///         <ul><li><tt>Update(object)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li></ul></description></item>
  ///     <item><description>While a DAC field value is requested through any of the following methods of the <tt>PXCache</tt> class:
  ///         <ul><li><tt>GetValueInt(object, string)</tt></li><li><tt>GetValueIntField(object)</tt></li><li><tt>GetValueExt(object, string)</tt></li><li><tt>GetValueExt&lt;Field&gt;(object)</tt></li><li><tt>GetValuePending(object, string)</tt></li><li><tt>ToDictionary(object)</tt></li><li><tt>GetStateExt(object, string)</tt></li><li><tt>GetStateExt&lt;Field&gt;(object)</tt></li></ul></description></item>
  ///   </list>
  ///   <para>The <tt>FieldSelecting</tt> event handler is used to perform the following actions:</para>
  ///   <list type="bullet">
  ///     <item><description>Conversion of the internal presentation of a DAC field (the data field value of a DAC instance)
  ///     to the external presentation (the value displayed in the UI)</description></item>
  ///     <item><description>Conversion of the values of multiple DAC fields to a single external presentation</description></item>
  ///     <item><description>Provision of additional information to set up a DAC field input control or cell presentation</description></item>
  ///   </list>
  ///   <para>The following execution order is used for the <tt>FieldSelecting</tt> event handlers:</para>
  ///   <list type="number">
  ///     <item><description>Graph event handlers are executed.</description></item>
  ///     <item><description>If <tt>e.Cancel</tt> is <tt>false</tt>, attribute event handlers are executed.</description></item>
  ///   </list>
  /// </remarks>
  public abstract class FieldSelectingBase<TGenericEventArgs> : 
    Events.Event<PXFieldSelectingEventArgs, TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXFieldSelectingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldSelectingEventArgs.Row" />
    public object? Row => this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXFieldSelectingEventArgs.IsAltered" />
    public bool IsAltered
    {
      get => this._args.IsAltered;
      set => this._args.IsAltered = value;
    }

    /// <inheritdoc cref="P:PX.Data.PXFieldSelectingEventArgs.ReturnValue" />
    public object? ReturnValue
    {
      get => this._args.ReturnValue;
      set => this._args.ReturnValue = value;
    }

    /// <inheritdoc cref="P:PX.Data.PXFieldSelectingEventArgs.ReturnState" />
    public object? ReturnState
    {
      get => this._args.ReturnState;
      set => this._args.ReturnState = value;
    }

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <inheritdoc />
  public abstract class FieldSelectingBase<TGenericEventArgs, TTable> : 
    Events.FieldSelectingBase<TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXFieldSelectingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldSelectingEventArgs.Row" />
    public TTable? Row => (TTable) base.Row;
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler has the following signature.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.FieldSelecting&lt;DACType, FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class FieldSelecting<TTable, TField> : 
    Events.FieldSelectingBase<Events.FieldSelecting<TTable, TField>, TTable>,
    AbstractEvents.IFieldSelecting<TTable, TField>,
    IGenericEventWith<PXFieldSelectingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldSelectingEventArgs.Row" />
    public new TTable? Row => base.Row;
  }

  /// <inheritdoc />
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler signature is shown in the following example.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.FieldSelecting&lt;FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code calculates the external value of a DAC field.
  /// </para>
  /// <code>
  /// [TableAndChartDashboardType]
  /// public class RevalueAPAccounts : PXGraph&lt;RevalueAPAccounts&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.FieldSelecting&lt;RevalueFilter.TotalRevalued&gt; e)
  ///     {
  ///         if (e.Row == null) return;
  /// 
  ///         decimal val = 0m;
  ///         foreach (RevaluedAPHistory res in APAccountList.Cache.Updated)
  ///             if ((bool)res.Selected)
  ///                 val += (decimal)res.FinPtdRevalued;
  ///         e.Args.ReturnValue = val;
  ///         e.Args.Cancel = true;
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class FieldSelecting<TField> : 
    Events.FieldSelectingBase<Events.FieldSelecting<TField>, object>,
    AbstractEvents.IFieldSelecting<object, TField>,
    IGenericEventWith<PXFieldSelectingEventArgs>,
    ICancelEventArgs
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldSelectingEventArgs.Row" />
    public new object? Row => base.Row;
  }

  /// <summary>
  /// A class for the <tt>FieldUpdated</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IFieldUpdated`3" />.
  /// </summary>
  /// <remarks>
  ///   <para>In the following cases, the <tt>FieldUpdated</tt> event is generated after a data access class (DAC) field is actually updated:</para>
  ///   <list type="bullet">
  ///     <item><description>For each DAC field value that is received
  ///     from the UI or through the Web Service API when a data record is
  ///     inserted or updated in the <tt>PXCache</tt> object.</description></item>
  ///     <item><description>For each DAC key field value in the process of deleting a data record
  ///     from the <tt>PXCache</tt> object when the deletion is initiated from the UI or
  ///     through the Web Service API.</description></item>
  ///     <item><description>When any of the following methods of the <tt>PXCache</tt> class
  ///     initiates the assigning of a default value to a field:
  ///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li><li><tt>SetDefaultExt(object, string)</tt></li><li><tt>SetDefaultExt&lt;Event&gt;(object)</tt></li></ul></description></item>
  ///     <item><description>When the field update is initiated
  ///     by any of the following methods of the <tt>PXCache</tt> class:
  ///         <ul><li><tt>Update(object)</tt></li><li><tt>SetValueExt(object, string, object)</tt></li><li><tt>SetValueExt&lt;Field&gt;(object, object)</tt></li></ul></description></item>
  ///     <item><description>During the validation of the DAC key field value
  ///     that is initiated by any of the following <tt>PXCache</tt> class methods:
  ///         <ul><li><tt>Locate(IDictionary)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li><li><tt>Delete(IDictionary, IDictionary)</tt></li></ul></description></item>
  ///   </list>
  ///   <para>The <tt>FieldUpdated</tt> event handler is used to implement the business logic
  ///   related to the changes to the value of the DAC field in the following cases:</para>
  ///   <list type="bullet">
  ///     <item><description>To update the related fields of a data record containing a modified field or assigning default values to these fields</description></item>
  ///     <item><description>To update any of the following:
  ///         <ul><li>The detail data records in a one-to-many relationship</li>
  ///         <li>The related data records in a one-to-one relationship</li>
  ///         <li>The master data records in a many-to-one relationship</li></ul></description></item>
  ///   </list>
  ///   <para>The following execution order is used for the <tt>FieldUpdated</tt> event handlers:</para>
  ///   <list type="number">
  ///     <item><description>Attribute event handlers are executed.</description></item>
  ///     <item><description>Graph event handlers are executed.</description></item>
  ///   </list>
  /// </remarks>
  public abstract class FieldUpdatedBase<TGenericEventArgs> : 
    Events.Event<PXFieldUpdatedEventArgs, TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXFieldUpdatedEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatedEventArgs.Row" />
    public object Row => this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatedEventArgs.ExternalCall" />
    public bool ExternalCall => this._args.ExternalCall;

    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatedEventArgs.OldValue" />
    public object? OldValue => this._args.OldValue;
  }

  /// <inheritdoc />
  public abstract class FieldUpdatedBase<TGenericEventArgs, TTable, TValue> : 
    Events.FieldUpdatedBase<TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXFieldUpdatedEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatedEventArgs.Row" />
    public TTable Row => (TTable) base.Row;

    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatedEventArgs.OldValue" />
    public TValue? OldValue => (TValue) base.OldValue;

    private protected TValue? GetCurrentValue<TField>() where TField : class, IBqlField
    {
      if (typeof (AbstractEvents.IFakeBqlField).IsAssignableFrom(typeof (TField)))
        throw new NotSupportedException();
      return (TValue) this.Cache.GetValue<TField>(base.Row);
    }
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <typeparam name="TValue">The type of the field of the DAC that raised the event.</typeparam>
  public class FieldUpdated<TTable, TField, TValue> : 
    Events.FieldUpdatedBase<Events.FieldUpdated<TTable, TField, TValue>, TTable, TValue>,
    AbstractEvents.IFieldUpdated<TTable, TField, TValue>,
    IGenericEventWith<PXFieldUpdatedEventArgs>
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
    public TValue? NewValue => this.GetCurrentValue<TField>();
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler has the following signature.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.FieldUpdated&lt;DACType, FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class FieldUpdated<TTable, TField> : 
    Events.FieldUpdatedBase<Events.FieldUpdated<TTable, TField>, TTable, object?>,
    AbstractEvents.IFieldUpdated<TTable, TField, object?>,
    IGenericEventWith<PXFieldUpdatedEventArgs>
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatedEventArgs.Row" />
    public new TTable Row => base.Row;

    public object? NewValue => this.GetCurrentValue<TField>();
  }

  /// <inheritdoc />
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler signature is shown below.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.FieldUpdated&lt;FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code updates the related field values
  /// of the current data record, assigns them the default values, or performs both actions.
  /// </para>
  /// <code>
  /// public class APInvoiceEntry : APDataEntryGraph&lt;APInvoiceEntry,
  ///                               APInvoice&gt;,
  ///                               PXImportAttribute.IPXPrepareItems
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.FieldUpdated&lt;APTran.UOM&gt; e)
  ///     {
  ///         APTran tran = e.Row;
  ///         e.Cache.SetDefaultExt&lt;APTran.unitCost&gt;(tran);
  ///         e.Cache.SetDefaultExt&lt;APTran.curyUnitCost&gt;(tran);
  ///         e.Cache.SetValue&lt;APTran.unitCost&gt;(tran, null);
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code updates the related data records.
  /// </para>
  /// <code lang="CS">
  /// public class ARCashSaleEntry : ARDataEntryGraph&lt;ARCashSaleEntry,
  ///                                                 ARCashSale&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.FieldUpdated&lt;ARCashSale.ProjectID&gt; e)
  ///     {
  ///         ARCashSale row = e.Row;
  /// 
  ///         foreach (ARTran tran in Transactions.Select())
  ///             Transactions.Cache.SetDefaultExt&lt;ARTran.projectID&gt;(tran);
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class FieldUpdated<TField> : 
    Events.FieldUpdatedBase<Events.FieldUpdated<TField>, object, object?>,
    AbstractEvents.IFieldUpdated<object, TField, object?>,
    IGenericEventWith<PXFieldUpdatedEventArgs>
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatedEventArgs.Row" />
    public new object Row => base.Row;

    public object? NewValue => this.GetCurrentValue<TField>();
  }

  /// <summary>
  /// A class for the <tt>FieldUpdating</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IFieldUpdating`2" />.
  /// </summary>
  /// <remarks>
  ///   <para>In the following cases, the <tt>FieldUpdating</tt> event is generated for a data access class (DAC) field
  ///   before the field is updated:</para>
  ///   <list type="bullet">
  ///     <item><description>For each DAC field value that is received
  ///     from the UI or through the Web Service
  ///     API when a data record is being inserted or updated.</description></item>
  ///     <item><description>For each DAC key field value in the process
  ///     of deleting a data record when the deletion is initiated
  ///     from the UI or through the Web Service API.</description></item>
  ///     <item><description>When any of the following methods of the <tt>PXCache</tt> class
  ///     initiates the assigning of the default value to a field:
  ///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li><li><tt>SetDefaultExt(object, string)</tt></li><li><tt>SetDefaultExt&lt;Field&gt;(object)</tt></li></ul></description></item>
  ///     <item><description>When any of the following methods of the <tt>PXCache</tt> class
  ///     initiates the updating of a field:
  ///         <ul><li><tt>Update(IDictionary, IDictionary)</tt></li><li><tt>SetValueExt(object, string, object)</tt></li><li><tt>SetValueExt&lt;Field&gt;(object, object)</tt></li><li><tt>SetValuePending(object, string, object)</tt></li><li><tt>SetValuePending&lt;Field&gt;(object, object)</tt></li></ul></description></item>
  ///     <item><description>When the conversion of the external DAC key field presentation
  ///     to the internal field value is initiated by the following <tt>PXCache</tt> class methods:
  ///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li></ul></description></item>
  ///   </list>
  ///   <para>The <tt>FieldUpdating</tt> event handler is used when either or both of the following occur:</para>
  ///   <list type="bullet">
  ///     <item><description>The external presentation of a DAC field (the value displayed in the UI) differs from the value stored in the DAC.</description></item>
  ///     <item><description>The storage of values is spread among multiple DAC fields (database columns).</description></item>
  ///   </list>
  ///   <para>In both cases, the application should implement both the <tt>FieldUpdating</tt> and
  ///   <tt>FieldSelecting</tt> events.</para>
  ///   <para>The following execution order is used for the <tt>FieldUpdating</tt> event handlers:</para>
  ///   <list type="number">
  ///     <item><description>Graph event handlers are executed.</description></item>
  ///     <item><description>If <tt>e.Cancel</tt> is <tt>false</tt>, attribute event handlers are executed.</description></item>
  ///   </list>
  /// </remarks>
  public abstract class FieldUpdatingBase<TGenericEventArgs> : 
    Events.Event<PXFieldUpdatingEventArgs, TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXFieldUpdatingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatingEventArgs.Row" />
    public object Row => this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatingEventArgs.NewValue" />
    public object? NewValue
    {
      get => this._args.NewValue;
      set => this._args.NewValue = value;
    }

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <inheritdoc />
  public abstract class FieldUpdatingBase<TGenericEventArgs, TTable, TValue> : 
    Events.FieldUpdatingBase<TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXFieldUpdatingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatingEventArgs.Row" />
    public TTable Row => (TTable) base.Row;

    private protected TValue? GetCurrentValue<TField>() where TField : class, IBqlField
    {
      if (typeof (AbstractEvents.IFakeBqlField).IsAssignableFrom(typeof (TField)))
        throw new NotSupportedException();
      return (TValue) this.Cache.GetValue<TField>(base.Row);
    }
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <typeparam name="TValue">The type of the field of the DAC that raised the event.</typeparam>
  public class FieldUpdating<TTable, TField, TValue> : 
    Events.FieldUpdatingBase<Events.FieldUpdating<TTable, TField, TValue>, TTable, TValue>,
    AbstractEvents.IFieldUpdating<TTable, TField>,
    IGenericEventWith<PXFieldUpdatingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue?>>
  {
    public TValue? OldValue => this.GetCurrentValue<TField>();
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler has the following signature.
  /// </para>
  /// <code title="Example1b" lang="CS">
  /// protected virtual void _(Events.FieldUpdating&lt;DACType, FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class FieldUpdating<TTable, TField> : 
    Events.FieldUpdatingBase<Events.FieldUpdating<TTable, TField>, TTable, object>,
    AbstractEvents.IFieldUpdating<TTable, TField>,
    IGenericEventWith<PXFieldUpdatingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatingEventArgs.Row" />
    public new TTable Row => base.Row;

    public object? OldValue => this.GetCurrentValue<TField>();
  }

  /// <inheritdoc />
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler signature is shown in the following example.
  /// </para>
  /// <code lang="CS">
  /// protected virtual void _(Events.FieldUpdating&lt;FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code spreads the external presentation of a field
  /// among multiple DAC fields.
  /// </para>
  /// <code lang="CS">
  /// protected void _(Events.FieldUpdating&lt;Batch.ManualStatus&gt; e)
  /// {
  ///     Batch batch = e.Row;
  ///     if (batch != null &amp;&amp; e.NewValue != null)
  ///     {
  ///         switch ((string)e.NewValue)
  ///         {
  ///             case "H":
  ///                 batch.Hold = true;
  ///                 batch.Released = false;
  ///                 batch.Posted = false;
  ///                 break;
  ///             case "B":
  ///                 batch.Hold = false;
  ///                 batch.Released = false;
  ///                 batch.Posted = false;
  ///                 break;
  ///             case "U":
  ///                 batch.Hold = false;
  ///                 batch.Released = true;
  ///                 batch.Posted = false;
  ///                 break;
  ///             case "P":
  ///                 batch.Hold = false;
  ///                 batch.Released = true;
  ///                 batch.Posted = true;
  ///                 break;
  ///         }
  ///    }
  /// }
  /// 
  /// protected void _(Events.FieldSelecting&lt;Batch.ManualStatus&gt; e)
  /// {
  ///     Batch batch = e.Row;
  ///     if (batch != null)
  ///     {
  ///         if (batch.Hold == true)
  ///         {
  ///             e.ReturnValue = "H";
  ///         }
  ///         else if (batch.Released != true)
  ///         {
  ///             e.ReturnValue = "B";
  ///         }
  ///         else if (batch.Posted != true)
  ///         {
  ///             e.ReturnValue = "U";
  ///         }
  ///         else
  ///         {
  ///             e.ReturnValue = "P";
  ///         }
  ///     }
  /// }
  /// </code>
  /// </example>
  public class FieldUpdating<TField> : 
    Events.FieldUpdatingBase<Events.FieldUpdating<TField>, object, object>,
    AbstractEvents.IFieldUpdating<object, TField>,
    IGenericEventWith<PXFieldUpdatingEventArgs>,
    ICancelEventArgs
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldUpdatingEventArgs.Row" />
    public new object Row => base.Row;

    public object? OldValue => this.GetCurrentValue<TField>();
  }

  /// <summary>
  /// A class for the <tt>FieldVerifying</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IFieldVerifying`3" />.
  /// </summary>
  /// <remarks>
  ///   <para>The system generates the <tt>FieldVerifying</tt> event for each data access class (DAC) field
  ///   of a data record that is inserted or updated in the
  /// <tt>PXCache</tt> object in the following processes:</para>
  ///   <list type="bullet">
  ///     <item><description>Insertion or update that is initiated in the UI
  ///     or through the Web Service API.</description></item>
  ///     <item><description>Assignment of the default value to the DAC field that is initiated by any of the following methods of the <tt>PXCache</tt> class:
  ///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li><li><tt>SetDefaultExt(object, string)</tt></li><li><tt>SetDefaultExt&lt;Event&gt;(object)</tt></li></ul></description></item>
  ///     <item><description>A DAC field update that is initiated by any of the following methods of the <tt>PXCache</tt> class:
  ///         <ul><li><tt>Update(object)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li><li><tt>SetValueExt(object, string, object)</tt></li><li><tt>SetValueExt&lt;Field&gt;(object, object)</tt></li></ul></description></item>
  ///     <item><description>Validation of a DAC key field value when the validation is initiated by any of the following methods of the <tt>PXCache</tt> class:
  ///         <ul><li><tt>Locate(IDictionary)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li></ul></description></item>
  ///   </list>
  ///   <para>The <tt>FieldVerifying</tt> event handler is used to perform the following actions:</para>
  ///   <list type="bullet">
  ///     <item><description>Implementation of the business logic associated with the validation of the DAC field value before the value is assigned to the DAC field.</description></item>
  ///     <item><description>Cancellation of the assigning of a value by throwing an exception of the <tt>PXSetPropertyException</tt>
  ///     type if the value does not meet the requirements.</description></item>
  ///     <item><description>Conversion of the external presentation of a DAC field value to the internal presentation
  ///     and implementation of the associated business logic. The internal presentation is the value stored in a DAC instance.</description></item>
  ///   </list>
  ///   <para>The following execution order is used for the <tt>FieldVerifying</tt> event handlers:</para>
  ///   <list type="number">
  ///     <item><description>Graph event handlers are executed.</description></item>
  ///     <item><description>If <tt>e.Cancel</tt> is <tt>false</tt>, attribute event handlers are executed.</description></item>
  ///   </list>
  /// </remarks>
  public abstract class FieldVerifyingBase<TGenericEventArgs> : 
    Events.Event<PXFieldVerifyingEventArgs, TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXFieldVerifyingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldVerifyingEventArgs.Row" />
    public object Row => this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXFieldVerifyingEventArgs.ExternalCall" />
    public bool ExternalCall => this._args.ExternalCall;

    /// <inheritdoc cref="P:PX.Data.PXFieldVerifyingEventArgs.NewValue" />
    public object? NewValue
    {
      get => this._args.NewValue;
      set => this._args.NewValue = value;
    }

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <inheritdoc />
  public abstract class FieldVerifyingBase<TGenericEventArgs, TTable, TValue> : 
    Events.FieldVerifyingBase<TGenericEventArgs>
    where TGenericEventArgs : Events.Event<PXFieldVerifyingEventArgs, TGenericEventArgs>, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldVerifyingEventArgs.Row" />
    public TTable Row => (TTable) base.Row;

    /// <inheritdoc cref="P:PX.Data.PXFieldVerifyingEventArgs.NewValue" />
    public TValue? NewValue
    {
      get => (TValue) base.NewValue;
      set => this.NewValue = (object) value;
    }

    private protected TValue? GetCurrentValue<TField>() where TField : class, IBqlField
    {
      if (typeof (AbstractEvents.IFakeBqlField).IsAssignableFrom(typeof (TField)))
        throw new NotSupportedException();
      return (TValue) this.Cache.GetValue<TField>(base.Row);
    }
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <typeparam name="TValue">The type of the field of the DAC that raised the event.</typeparam>
  public class FieldVerifying<TTable, TField, TValue> : 
    Events.FieldVerifyingBase<Events.FieldVerifying<TTable, TField, TValue>, TTable, TValue>,
    AbstractEvents.IFieldVerifying<TTable, TField, TValue>,
    IGenericEventWith<PXFieldVerifyingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField, IImplement<IBqlCorrespondsTo<TValue>>
  {
    public TValue? OldValue => this.GetCurrentValue<TField>();
  }

  /// <inheritdoc />
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler has the following signature.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.FieldVerifying&lt;DACType, FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class FieldVerifying<TTable, TField> : 
    Events.FieldVerifyingBase<Events.FieldVerifying<TTable, TField>, TTable, object?>,
    AbstractEvents.IFieldVerifying<TTable, TField, object?>,
    IGenericEventWith<PXFieldVerifyingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldVerifyingEventArgs.Row" />
    public new TTable Row => base.Row;

    public object? OldValue => this.GetCurrentValue<TField>();
  }

  /// <inheritdoc />
  /// <typeparam name="TField">The field of the DAC that raised the event.</typeparam>
  /// <example>
  /// <para>
  /// The generic event handler signature is shown in the following example.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.FieldVerifying&lt;FieldType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code validates the new value of a DAC field.
  /// </para>
  /// <code>
  /// public class APPaymentEntry : APDataEntryGraph&lt;APPaymentEntry, APPayment&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.FieldVerifying&lt;APPayment.AdjDate&gt; e)
  ///     {
  ///         if ((bool)e.Row.VoidAppl == false &amp;&amp;
  ///             vendor.Current != null &amp;&amp; (bool)vendor.Current.Vendor1099)
  ///         {
  ///             string Year1099 = ((DateTime)e.NewValue).Year.ToString();
  ///             AP1099Year year = PXSelect&lt;
  ///                 AP1099Year,
  ///                 Where&lt;AP1099Year.finYear,
  ///                       Equal&lt;Required&lt;AP1099Year.finYear&gt;&gt;&gt;&gt;.
  ///                 Select(this, Year1099);
  ///             if (year != null &amp;&amp; year.Status != "N")
  ///                 throw new PXSetPropertyException(
  ///                     Messages.AP1099_PaymentDate_NotIn_OpenYear);
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code validates
  /// the external presentation of a DAC field value and converts it
  /// to the internal presentation if the validation succeeds.
  /// </para>
  /// <code>
  /// [TableAndChartDashboardType]
  /// public class CAReconEnq : PXGraph&lt;CAReconEnq&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.FieldVerifying&lt;CashAccountFilter.CashAccountID&gt; e)
  ///     {
  ///         CashAccountFilter createReconFilter = e.Row;
  ///         if (!e.NewValue is string) return;
  ///         CashAccount acct =
  ///             PXSelect&lt;CashAccount,
  ///                      Where&lt;CashAccount.accountCD,
  ///                            Equal&lt;Required&lt;CashAccount.accountCD&gt;&gt;&gt;&gt;.
  ///                 Select(this, (string)e.NewValue);
  ///         if (acct != null &amp;&amp; acct.Reconcile != true)
  ///             throw new PXSetPropertyException(Messages.CashAccounNotReconcile);
  ///         e.NewValue = acct.AccountID;
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class FieldVerifying<TField> : 
    Events.FieldVerifyingBase<Events.FieldVerifying<TField>, object, object?>,
    AbstractEvents.IFieldVerifying<object, TField, object?>,
    IGenericEventWith<PXFieldVerifyingEventArgs>,
    ICancelEventArgs
    where TField : class, IBqlField
  {
    /// <inheritdoc cref="P:PX.Data.PXFieldVerifyingEventArgs.Row" />
    public new object Row => base.Row;

    public object? OldValue => this.GetCurrentValue<TField>();
  }

  /// <summary>
  /// A class for the <tt>RowDeleted</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IRowDeleted`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
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
  /// The generic event handler has the following signature.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.RowDeleted&lt;DACType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code deletes detail data records in a one-to-many relationship.
  /// </para>
  /// <code>
  /// public class CashTransferEntry : PXGraph&lt;CashTransferEntry, CATransfer&gt;
  /// {
  ///     ...
  /// 
  ///     public virtual void _(Events.RowDeleted&lt;CATransfer&gt; e)
  ///     {
  ///         foreach (CATran item in TransferTran.Select())
  ///             TransferTran.Delete(item);
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code updates the master data record in a many-to-one relationship.
  /// </para>
  /// <code>
  /// public class INSiteMaint : PXGraph&lt;INSiteMaint, INSite&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowDeleted&lt;INLocation&gt; e)
  ///     {
  ///         INLocation l = e.Row;
  ///         if (site.Current == null || l == null ||
  ///             site.Cache.GetStatus(site.Current) == PXEntryStatus.Deleted)
  ///             return;
  /// 
  ///         INSite s = site.Current;
  ///         if (s.DropShipLocationID == l.LocationID)
  ///             s.DropShipLocationID = null;
  ///         if (s.ReceiptLocationID == l.LocationID)
  ///             s.ReceiptLocationID = null;
  ///         if (s.ShipLocationID == l.LocationID)
  ///             s.ShipLocationID = null;
  ///         if (s.ReturnLocationID == l.LocationID)
  ///             s.ReturnLocationID = null;
  ///         site.Update(s);
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class RowDeleted<TTable> : 
    Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TTable>>,
    AbstractEvents.IRowDeleted<TTable>,
    IGenericEventWith<PXRowDeletedEventArgs>
    where TTable : class, IBqlTable, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXRowDeletedEventArgs.Row" />
    public TTable Row => (TTable) this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXRowDeletedEventArgs.ExternalCall" />
    public bool ExternalCall => this._args.ExternalCall;
  }

  /// <summary>
  /// A class for the <tt>RowDeleting</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IRowDeleting`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
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
  /// The generic event handler has the following signature.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.RowDeleting&lt;DACType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code evaluates the data record
  /// that is being deleted and cancels the deletion by throwing an exception.
  /// </para>
  /// <code>
  /// public class VendorMaint : BusinessAccountGraphBase&lt;
  ///     VendorR, VendorR,
  ///      Where&lt;BAccount.type,
  ///            Equal&lt;BAccountType.vendorType&gt;,
  ///            Or&lt;BAccount.type,
  ///               Equal&lt;BAccountType.combinedType&gt;&gt;&gt;&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowDeleting&lt;Vendor&gt; e)
  ///     {
  ///         Vendor row = e.Row;
  /// 
  ///         TX.Tax tax = PXSelect&lt;
  ///             TX.Tax,
  ///             Where&lt;TX.Tax.taxVendorID,
  ///                   Equal&lt;Current&lt;Vendor.bAccountID&gt;&gt;&gt;&gt;.
  ///             Select(this);
  ///         if (tax != null)
  ///             throw new PXException(Messages.TaxVendorDeleteErr);
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class RowDeleting<TTable> : 
    Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<TTable>>,
    AbstractEvents.IRowDeleting<TTable>,
    IGenericEventWith<PXRowDeletingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXRowDeletingEventArgs.Row" />
    public TTable Row => (TTable) this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXRowDeletingEventArgs.ExternalCall" />
    public bool ExternalCall => this._args.ExternalCall;

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <summary>
  /// A class for the <tt>RowInserted</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IRowInserted`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
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
  /// The generic event handler has the following signature.
  /// </para>
  /// <code lang="CS">
  /// protected virtual void _(Events.RowInserted&lt;DACType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code inserts the detail data records in a one-to-many relationship.
  /// </para>
  /// <code lang="CS">
  /// public class VendorClassMaint : PXGraph&lt;VendorClassMaint&gt;
  /// {
  ///     ...
  /// 
  ///     public virtual void _(Events.RowInserted&lt;VendorClass&gt; e)
  ///     {
  ///         VendorClass row = e.Row;
  ///         if (row == null || row.VendorClassID == null) return;
  /// 
  ///         foreach (APNotification n in PXSelect&lt;
  ///             APNotification,
  ///             Where&lt;APNotification.sourceCD,
  ///                   Equal&lt;APNotificationSource.vendor&gt;&gt;&gt;.
  ///             Select(this))
  ///         {
  ///             NotificationSource source = new NotificationSource();
  ///             source.SetupID = n.SetupID;
  ///             NotificationSources.Insert(source);
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code updates the master data record in a many-to-one relationship.
  /// </para>
  /// <code lang="CS">
  /// public class InventoryItemMaint : PXGraph&lt;InventoryItemMaint&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowInserted&lt;POVendorInventory&gt; e)
  ///     {
  ///         POVendorInventory current = e.Row;
  ///         if (current.IsDefault == true &amp;&amp; current.VendorID != null &amp;&amp;
  ///             current.VendorLocationID != null &amp;&amp; current.SubItemID != null &amp;&amp;
  ///             this.Item.Current.PreferredVendorLocationID !=
  ///             current.VendorLocationID)
  ///         {
  ///             InventoryItem upd = Item.Current;
  ///             upd.PreferredVendorID = current.IsDefault == true ?
  ///                                                          current.VendorID :
  ///                                                          null;
  ///             upd = this.Item.Update(upd);
  ///             upd.PreferredVendorLocationID = current.IsDefault ==
  ///                 true ? current.VendorLocationID : null;
  ///             Item.Update(upd);
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class RowInserted<TTable> : 
    Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TTable>>,
    AbstractEvents.IRowInserted<TTable>,
    IGenericEventWith<PXRowInsertedEventArgs>
    where TTable : class, IBqlTable, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXRowInsertedEventArgs.Row" />
    public TTable Row => (TTable) this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXRowInsertedEventArgs.ExternalCall" />
    public bool ExternalCall => this._args.ExternalCall;
  }

  /// <summary>
  /// A class for the <tt>RowInserting</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IRowInserting`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
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
  /// The generic event handler has the following signature.
  /// </para>
  /// <code lang="CS">
  /// protected virtual void _(Events.RowInserting&lt;DACType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code evaluates the data record
  /// that is being inserted and cancel the insertion.
  /// </para>
  /// <code lang="CS">
  /// public class CashAccountMaint : PXGraph&lt;CashAccountMaint&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowInserting&lt;PaymentMethodAccount&gt; e)
  ///     {
  ///         PaymentMethodAccount row = e.Row;
  ///         if (row.PaymentMethodID != null)
  ///             foreach (PaymentMethodAccount it in Details.Select())
  ///                 if (!object.ReferenceEquals(row, it) &amp;&amp;
  ///                     it.PaymentMethodID == row.PaymentMethodID)
  ///                     throw new PXException(
  ///                         Messages.DuplicatedPaymentMethodForCashAccount,
  ///                         row.PaymentMethodID);
  ///         if (row.APIsDefault == true &amp;&amp;
  ///             String.IsNullOrEmpty(row.PaymentMethodID))
  ///             throw new PXException(ErrorMessages.FieldIsEmpty,
  ///                                   typeof(PaymentMethodAccount.
  ///                                       paymentMethodID).Name);
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code assigns the default field values to the data record that is being inserted.
  /// </para>
  /// <code lang="CS">
  /// public class MyCaseDetailsMaint : PXGraph&lt;MyCaseDetailsMaint&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowInserting&lt;EPActivity&gt; e)
  ///     {
  ///         EPActivity row = e.Row;
  ///         if (Case.Current != null)
  ///         {
  ///             row.StartDate = PXTimeZoneInfo.Now;
  ///             row.RefNoteID = Case.Current.NoteID;
  ///             row.ClassID = CRActivityClass.Activity;
  ///             row.IsExternal = true;
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class RowInserting<TTable> : 
    Events.Event<PXRowInsertingEventArgs, Events.RowInserting<TTable>>,
    AbstractEvents.IRowInserting<TTable>,
    IGenericEventWith<PXRowInsertingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXRowInsertingEventArgs.Row" />
    public TTable Row => (TTable) this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXRowInsertingEventArgs.ExternalCall" />
    public bool ExternalCall => this._args.ExternalCall;

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <summary>
  /// A class for the <tt>RowPersisted</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IRowPersisted`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <remarks>
  ///   <para>The <tt>RowPersisted</tt> event is generated in the process of committing changes to the database
  ///   for every data record whose status is <tt>Inserted</tt>, <tt>Updated</tt>,
  /// or <tt>Deleted</tt>. The <tt>RowPersisted</tt> event is generated in the following cases:</para>
  ///   <list type="bullet">
  ///     <item><description>When the data record has been committed to the database and the status of the transaction scope
  ///     (indicated in the <tt>e.TranStatus</tt> field) is <tt>Open</tt></description></item>
  ///     <item><description>When the status of the transaction scope has been changed to <tt>Completed</tt>, indicating successful committing,
  ///     or <tt>Aborted</tt>, indicating that a database error has occurred and changes to the database have been dropped</description></item>
  ///   </list>
  ///   <para>The <tt>Actions.PressSave()</tt> method of the business logic controller (graph)
  ///   initiates the committing of changes to a database. While processing this method,
  /// the Acumatica data access layer commits first every inserted data record, then every updated data record,
  /// and finally every deleted data record.</para>
  ///   <para>Avoid executing additional BQL statements in a
  ///     <tt>RowPersisted</tt> event handler when the status of the transaction scope is <tt>Open</tt>.
  ///     When the <tt>RowPersisted</tt> event is raised with this status, the associated transaction scope is busy saving the changes,
  ///     and any other operation performed within this transaction scope may cause performance degradation and deadlocks.</para>
  ///   <para>The <tt>RowPersisted</tt> event handler is used to perform the following actions:</para>
  ///   <list type="bullet">
  ///     <item><description>Retrieval of data generated by the database.</description></item>
  ///     <item><description>Restoring of data access class (DAC) field values if the status of the transaction scope
  ///     is <tt>Aborted</tt> indicating that changes have not been saved. Note that in this case
  ///     the DAC fields do not revert to any previous state automatically but are left
  ///     by the Acumatica data access layer in the state they were in before
  ///     the committing was initiated.</description></item>
  ///     <item><description>Validation of the data record while committing it to the database.</description></item>
  ///   </list>
  ///   <para>The following execution order is used for the <tt>RowPersisted</tt> event handlers:</para>
  ///   <list type="number">
  ///     <item><description>Attribute event handlers are executed.</description></item>
  ///     <item><description>Graph event handlers are executed.</description></item>
  ///   </list>
  /// </remarks>
  /// <example>
  /// <para>
  /// The generic event handler has the following signature.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.RowPersisted&lt;DACType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class RowPersisted<TTable> : 
    Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TTable>>,
    AbstractEvents.IRowPersisted<TTable>,
    IGenericEventWith<PXRowPersistedEventArgs>
    where TTable : class, IBqlTable, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXRowPersistedEventArgs.Row" />
    public TTable Row => (TTable) this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXRowPersistedEventArgs.Operation" />
    public PXDBOperation Operation => this._args.Operation;

    /// <inheritdoc cref="P:PX.Data.PXRowPersistedEventArgs.TranStatus" />
    public PXTranStatus TranStatus => this._args.TranStatus;

    /// <inheritdoc cref="P:PX.Data.PXRowPersistedEventArgs.Exception" />
    public Exception Exception => this._args.Exception;
  }

  /// <summary>
  /// A class for the <tt>RowPersisting</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IRowPersisting`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
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
  /// The generic event handler has the following signature.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.RowPersisting&lt;DACType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code validates the data record before it is committed to the database.
  /// </para>
  /// <code>
  /// public class CCProcessingCenterMaint : PXGraph&lt;CCProcessingCenterMaint,
  ///                                                CCProcessingCenter&gt;,
  ///                                        IProcessingCenterSettingsStorage
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowPersisting&lt;CCProcessingCenter&gt; e)
  ///     {
  ///         if ((e.Operation &amp; PXDBOperation.Command) != PXDBOperation.Delete &amp;&amp;
  ///              e.Row != null &amp;&amp;
  ///              (bool)e.Row.IsActive &amp;&amp;
  ///              string.IsNullOrEmpty(e.Row.ProcessingTypeName))
  ///         {
  ///             throw new PXRowPersistingException(
  ///                 typeof(CCProcessingCenter.processingTypeName).Name,
  ///                 null,
  ///                 ErrorMessages.FieldIsEmpty,
  ///                 typeof(CCProcessingCenter.processingTypeName).Name);
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code shows a message box, as well as the warning and error indications
  /// near the input control, for one field or multiple fields.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.RowPersisting&lt;APInvoice&gt; e)
  /// {
  ///     APInvoice doc = e.Row;
  ///     if (doc.PaySel == true &amp;&amp; doc.PayDate == null)
  ///     {
  ///         e.Cache.RaiseExceptionHandling&lt;APInvoice.payDate&gt;(
  ///             doc, null,
  ///             new PXSetPropertyException(ErrorMessages.FieldIsEmpty,
  ///                                        typeof(APInvoice.payDate).Name));
  ///     }
  ///     if (doc.PaySel == true &amp;&amp; doc.PayDate != null &amp;&amp;
  ///         ((DateTime)doc.DocDate).CompareTo((DateTime)doc.PayDate) &gt; 0)
  ///     {
  ///         e.Cache.RaiseExceptionHandling&lt;APInvoice.payDate&gt;(
  ///             e.Row, doc.PayDate,
  ///             new PXSetPropertyException(Messages.ApplDate_Less_DocDate,
  ///                                        PXErrorLevel.RowError,
  ///                                        typeof(APInvoice.payDate).Name));
  ///     }
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code cancels the operation of committing a data record.
  /// </para>
  /// <code>
  /// public class CampaignMemberMassProcess : PXGraph&lt;CampaignMemberMassProcess&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowPersisting&lt;Contact&gt; e)
  ///     {
  ///         e.Cancel = true;
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class RowPersisting<TTable> : 
    Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TTable>>,
    AbstractEvents.IRowPersisting<TTable>,
    IGenericEventWith<PXRowPersistingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXRowPersistingEventArgs.Row" />
    public TTable Row => (TTable) this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXRowPersistingEventArgs.Operation" />
    public PXDBOperation Operation => this._args.Operation;

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <summary>
  /// A class for the <tt>RowSelected</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IRowSelected`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
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
  /// The generic event handler has the following signature.
  /// </para>
  /// <code lang="CS">
  /// protected virtual void _(Events.RowSelected&lt;DACType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code sets the UI properties for input controls at run time.
  /// </para>
  /// <code lang="CS">
  /// public class VendorMaint :
  ///      BusinessAccountGraphBase&lt;VendorR, VendorR,
  ///          Where&lt;BAccount.type, Equal&lt;BAccountType.vendorType&gt;,
  ///              Or&lt;BAccount.type, Equal&lt;BAccountType.combinedType&gt;&gt;&gt;&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowSelected&lt;Vendor&gt; e)
  ///     {
  ///         Vendor row = e.Row;
  ///         if (row == null) return;
  /// 
  ///         bool isNotInserted = !(e.Cache.GetStatus(row) ==
  ///                                PXEntryStatus.Inserted);
  ///         PXUIFieldAttribute.SetVisible&lt;VendorBalanceSummary.depositsBalance&gt;(
  ///             VendorBalance.Cache, null, isNotInserted);
  ///         PXUIFieldAttribute.SetVisible&lt;VendorBalanceSummary.balance&gt;(
  ///             VendorBalance.Cache, null, isNotInserted);
  ///         PXUIFieldAttribute.SetEnabled&lt;Vendor.taxReportFinPeriod&gt;(
  ///             e.Cache, null,
  ///             row.TaxPeriodType != PX.Objects.TX.VendorTaxPeriodType.FiscalPeriod);
  ///         PXUIFieldAttribute.SetEnabled&lt;Vendor.taxReportPrecision&gt;(
  ///             e.Cache, null, row.TaxUseVendorCurPrecision != true);
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code sets the UI properties for actions.
  /// </para>
  /// <code lang="CS">
  /// public class APAccess : PX.SM.BaseAccess
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowSelected&lt;RelationGroup&gt; e)
  ///     {
  ///         RelationGroup group = e.Row;
  ///         if (group != null)
  ///         {
  ///             if (String.IsNullOrEmpty(group.GroupName))
  ///             {
  ///                 Save.SetEnabled(false);
  ///                 Vendor.Cache.AllowInsert = false;
  ///             }
  ///             else
  ///             {
  ///                 Save.SetEnabled(true);
  ///                 Vendor.Cache.AllowInsert = true;
  ///             }
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code sets up the processing operation on a processing screen.
  /// </para>
  /// <code lang="CS">
  /// [TableAndChartDashboardType]
  /// public class APIntegrityCheck : PXGraph&lt;APIntegrityCheck&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowSelected&lt;APIntegrityCheckFilter&gt; e)
  ///     {
  ///         APIntegrityCheckFilter filter = Filter.Current;
  /// 
  ///         APVendorList.SetProcessDelegate&lt;APReleaseProcess&gt;(
  ///             delegate(APReleaseProcess re, Vendor vend)
  ///             {
  ///                 re.Clear(PXClearOption.PreserveTimeStamp);
  ///                 re.IntegrityCheckProc(vend, filter.FinPeriodID);
  ///             }
  ///         );
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class RowSelected<TTable> : 
    Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TTable>>,
    AbstractEvents.IRowSelected<TTable>,
    IGenericEventWith<PXRowSelectedEventArgs>
    where TTable : class, IBqlTable, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXRowSelectedEventArgs.Row" />
    public TTable? Row => (TTable) this._args.Row;
  }

  /// <summary>
  /// A class for the <tt>RowSelecting</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IRowSelecting`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <remarks>
  /// <para>We recommend that you not use RowSelecting event handlers in the application code.</para>
  /// <para>The RowSelecting event is generated for each retrieved data record
  ///   when the result of a BQL statement is processed. For a BQL statement that
  /// contains a JOIN clause, the RowSelecting event is raised for every joined data access class (DAC).</para>
  /// <para>The RowSelecting event handler can be used for the following purposes:</para>
  /// <list type="bullet">
  /// <item><description>To calculate DAC field values that are not bound to specific database columns</description></item>
  /// <item><description>To modify the logic that converts the database record to the DAC</description></item>
  /// </list>
  /// <para>The following execution order is used for the RowSelecting event handlers:</para>
  /// <list type="number">
  /// <item><description>If e.Cancel is false, attribute event handlers are executed.</description></item>
  /// <item><description>After the iteration through the rows of the initial connection scope is completed,
  /// graph event handlers are executed. However if optimized export is performed or the PXStreamingQueryScope is used,
  /// the RowSelecting graph event handlers are executed during iteration throw records.
  /// In this case, a new database connection is opened automatically for execution of additional data queries.</description></item>
  /// </list>
  /// </remarks>
  /// <example>
  /// <para>
  /// The generic event handler has the following signature.
  /// </para>
  /// <code>
  /// protected virtual void _(Events.RowSelecting&lt;DACType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The code below calculates a DAC field value that is not bound
  /// to a specific column in a database table.
  /// </para>
  /// <code title="Example2b">
  /// public class LocationMaint :
  ///     LocationMaintBase&lt;Location, Location,
  ///                       Where&lt;Location.bAccountID,
  ///                             Equal&lt;Optional&lt;Location.bAccountID&gt;&gt;&gt;&gt;
  /// {
  /// 
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowSelecting&lt;Location&gt; e)
  ///     {
  ///         Location record = e.Row;
  ///         if (record != null)
  ///             record.IsARAccountSameAsMain =
  ///                 !object.Equals(record.LocationID, record.CARAccountLocationID);
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The code below executes an additional BQL statement
  /// to calculate a DAC field value that is not bound to a specific column in a database table.
  /// </para>
  /// <code>
  /// public class SOInvoiceEntry : ARInvoiceEntry
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowSelecting&lt;ARInvoice&gt; e)
  ///     {
  ///         ARInvoice row = e.Row;
  ///         if (row != null &amp;&amp; !String.IsNullOrEmpty(row.DocType)
  ///                         &amp;&amp; !String.IsNullOrEmpty(row.RefNbr))
  ///         {
  ///             row.IsCCPayment = false;
  ///             if (PXSelectJoin&lt;
  ///                 CustomerPaymentMethodC,
  ///                 InnerJoin&lt;
  ///                     CA.PaymentMethod,
  ///                     On&lt;CA.PaymentMethod.paymentMethodID,
  ///                         Equal&lt;CustomerPaymentMethodC.paymentMethodID&gt;&gt;,
  ///                     InnerJoin&lt;
  ///                         SOInvoice,
  ///                         On&lt;SOInvoice.pMInstanceID,
  ///                             Equal&lt;CustomerPaymentMethodC.pMInstanceID&gt;&gt;&gt;&gt;,
  ///                 Where&lt;SOInvoice.docType,
  ///                     Equal&lt;Required&lt;SOInvoice.docType&gt;&gt;,
  ///                     And&lt;SOInvoice.refNbr,
  ///                         Equal&lt;Required&lt;SOInvoice.refNbr&gt;&gt;,
  ///                         And&lt;CA.PaymentMethod.paymentType,
  ///                             Equal&lt;CA.PaymentMethodType.creditCard&gt;,
  ///                         And&lt;CA.PaymentMethod.aRIsProcessingRequired,
  ///                             Equal&lt;True&gt;&gt;&gt;&gt;&gt;&gt;.
  ///                 Select(this, row.DocType, row.RefNbr).Count &gt; 0)
  ///             {
  ///                 row.IsCCPayment = true;
  ///             }
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class RowSelecting<TTable> : 
    Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<TTable>>,
    AbstractEvents.IRowSelecting<TTable>,
    IGenericEventWith<PXRowSelectingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXRowSelectingEventArgs.Row" />
    public TTable Row => (TTable) this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXRowSelectingEventArgs.Record" />
    public PXDataRecord Record => this._args.Record;

    /// <inheritdoc cref="P:PX.Data.PXRowSelectingEventArgs.Position" />
    public int Position
    {
      get => this._args.Position;
      set => this._args.Position = value;
    }

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }

  /// <summary>
  /// A class for the <tt>RowUpdated</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IRowUpdated`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
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
  /// The generic event handler has the following signature.
  /// </para>
  /// <code lang="CS">
  /// protected virtual void _(Events.RowUpdated&lt;DACType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code updates the detail data records in a one-to-many relationship.
  /// </para>
  /// <code lang="CS">
  /// public class DraftScheduleMaint : PXGraph&lt;DraftScheduleMaint, DRSchedule&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowUpdated&lt;DRSchedule&gt; e)
  ///     {
  ///         DRSchedule row = e.Row;
  ///         if (!e.Cache.ObjectsEqual&lt;DRSchedule.documentType, DRSchedule.refNbr,
  ///                                  DRSchedule.lineNbr, DRSchedule.bAccountID,
  ///                                  DRSchedule.finPeriodID,
  ///                                  DRSchedule.docDate&gt;(e.Row, e.OldRow))
  ///         {
  ///             foreach (DRScheduleDetail detail in Components.Select())
  ///             {
  ///                 detail.Module = row.Module;
  ///                 detail.DocumentType = row.DocumentType;
  ///                 detail.DocType = row.DocType;
  ///                 detail.RefNbr = row.RefNbr;
  ///                 detail.LineNbr = row.LineNbr;
  ///                 detail.BAccountID = row.BAccountID;
  ///                 detail.FinPeriodID = row.FinPeriodID;
  ///                 detail.DocDate = row.DocDate;
  ///                 Components.Update(detail);
  ///             }
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code updates the master data record in a many-to-one relationship.
  /// </para>
  /// <code lang="CS">
  /// public class ARInvoiceEntry : ARDataEntryGraph&lt;ARInvoiceEntry, ARInvoice&gt;,
  ///                               PXImportAttribute.IPXPrepareItems
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowUpdated&lt;ARTran&gt; e)
  ///     {
  ///         ARTran row = e.Row;
  ///         ARTran oldRow = e.OldRow;
  ///         if (Document.Current != null &amp;&amp;
  ///             IsExternalTax == true &amp;&amp;
  ///             !e.Cache.ObjectsEqual&lt;ARTran.accountID, ARTran.inventoryID,
  ///                                  ARTran.tranDesc,
  ///                                  ARTran.tranAmt, ARTran.tranDate,
  ///                                  ARTran.taxCategoryID&gt;(e.Row, e.OldRow))
  ///         {
  ///             ARInvoice copy = Document.Current;
  ///             copy.IsTaxValid = false;
  ///             Document.Update(copy);
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  public class RowUpdated<TTable> : 
    Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TTable>>,
    AbstractEvents.IRowUpdated<TTable>,
    IGenericEventWith<PXRowUpdatedEventArgs>
    where TTable : class, IBqlTable, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXRowUpdatedEventArgs.Row" />
    public TTable Row => (TTable) this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXRowUpdatedEventArgs.OldRow" />
    public TTable OldRow => (TTable) this._args.OldRow;

    /// <inheritdoc cref="P:PX.Data.PXRowUpdatedEventArgs.ExternalCall" />
    public bool ExternalCall => this._args.ExternalCall;
  }

  /// <summary>
  /// A class for the <tt>RowUpdating</tt> event. The class is used in generic event handlers.
  /// A handler with such a parameter <b>gets automatically subscribed</b> to the corresponding event, unlike <see cref="T:PX.Data.AbstractEvents.IRowUpdating`1" />.
  /// </summary>
  /// <typeparam name="TTable">The DAC that raised the event.</typeparam>
  /// <remarks>
  ///   <para>The <tt>RowUpdating</tt> event is generated before the data record is actually updated
  ///   in the <tt>PXCache</tt> object during an update initiated in either of the following cases:</para>
  ///   <list type="bullet">
  ///     <item><description>In the UI or via Web Service API.</description></item>
  ///     <item><description>When the following <tt>PXCache</tt> class methods were invoked:<ul><li><tt>Update(object)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li></ul></description></item>
  ///   </list>
  ///   <para>The <tt>RowUpdating</tt> event handler is used to evaluate a data record that is being updated
  ///   and to cancel the update operation if the data record does not
  /// fit the business logic requirements.</para>
  ///   <para>The following execution order is used for the <tt>RowUpdating</tt> event handlers:</para>
  ///   <list type="number">
  ///     <item><description>Graph event handlers are executed.</description></item>
  ///     <item><description>If <tt>e.Cancel</tt> is <tt>false</tt>, attribute event handlers are executed.</description></item>
  ///   </list>
  /// </remarks>
  /// <example>
  /// <para>
  /// The generic event handler has the following signature.
  /// </para>
  /// <code lang="CS">
  /// protected virtual void _(Events.RowUpdating&lt;DACType&gt; e)
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code evaluates the data record that is being updated,
  /// cancels the update operation, and shows a message box.
  /// </para>
  /// <code lang="CS">
  /// public class APPaymentEntry : APDataEntryGraph&lt;APPaymentEntry, APPayment&gt;
  /// {
  ///     ...
  /// 
  ///     protected virtual void _(Events.RowUpdating&lt;APAdjust&gt; e)
  ///     {
  ///         APAdjust adj = e.Row;
  ///         if (_IsVoidCheckInProgress == false &amp;&amp; adj.Voided == true)
  ///         {
  ///             throw new PXException(ErrorMessages.CantUpdateRecord);
  ///         }
  ///     }
  /// 
  ///     ...
  /// }
  /// </code>
  /// </example>
  /// <example>
  /// <para>
  /// The following code evaluates the data record that is being updated,
  /// cancels the update operation, and shows the warning or error indication
  /// near the input control for one field or multiple fields.
  /// </para>
  /// <code lang="CS">
  /// protected virtual void _(Events.RowUpdating&lt;INLotSerClass&gt; e)
  /// {
  ///     INLotSerClass row = e.NewRow;
  ///     if (row.LotSerTrackExpiration != true &amp;&amp;
  ///         row.LotSerIssueMethod == INLotSerIssueMethod.Expiration)
  ///     {
  ///         e.Cache.RaiseExceptionHandling&lt;INLotSerClass.lotSerIssueMethod&gt;(
  ///             row, null,
  ///             new PXSetPropertyException(
  ///                 Messages.LotSerTrackExpirationInvalid,
  ///                 typeof(INLotSerClass.lotSerIssueMethod).Name));
  ///         e.Cancel = true;
  ///     }
  /// }
  /// </code>
  /// </example>
  public class RowUpdating<TTable> : 
    Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<TTable>>,
    AbstractEvents.IRowUpdating<TTable>,
    IGenericEventWith<PXRowUpdatingEventArgs>,
    ICancelEventArgs
    where TTable : class, IBqlTable, new()
  {
    /// <inheritdoc cref="P:PX.Data.PXRowUpdatingEventArgs.Row" />
    public TTable Row => (TTable) this._args.Row;

    /// <inheritdoc cref="P:PX.Data.PXRowUpdatingEventArgs.NewRow" />
    public TTable NewRow => (TTable) this._args.NewRow;

    /// <inheritdoc cref="P:PX.Data.PXRowUpdatingEventArgs.ExternalCall" />
    public bool ExternalCall => this._args.ExternalCall;

    /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
    public bool Cancel
    {
      get => this._args.Cancel;
      set => this._args.Cancel = value;
    }
  }
}
