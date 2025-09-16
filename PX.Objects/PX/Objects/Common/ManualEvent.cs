// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.ManualEvent
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Objects.Common;

[Obsolete]
[PXInternalUseOnly]
public static class ManualEvent
{
  private static readonly object StaticTarget = new object();

  public static class FieldOf<TTable> where TTable : class, IBqlTable, new()
  {
    public static class Defaulting
    {
      public static void Subscribe<TFieldType>(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.Defaulting.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.Defaulting.Args<TFieldType>>, PXFieldDefaulting>.Subscribe(graph, handler, (Action<PXGraph, PXFieldDefaulting>) ((g, h) => g.FieldDefaulting.AddHandler(typeof (TTable), fieldName, h)), (Func<Action<ManualEvent.FieldOf<TTable>.Defaulting.Args<TFieldType>>, PXFieldDefaulting>) (h => ManualEvent.FieldOf<TTable>.Defaulting.Wrap<TFieldType>(h)));
      }

      public static void Unsubscribe<TFieldType>(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.Defaulting.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.Defaulting.Args<TFieldType>>, PXFieldDefaulting>.Unsubscribe(graph, handler, (Action<PXGraph, PXFieldDefaulting>) ((g, h) => g.FieldDefaulting.RemoveHandler(typeof (TTable), fieldName, h)));
      }

      private static PXFieldDefaulting Wrap<TFieldType>(
        Action<ManualEvent.FieldOf<TTable>.Defaulting.Args<TFieldType>> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXFieldDefaulting((object) new ManualEvent.FieldOf<TTable>.Defaulting.\u003C\u003Ec__DisplayClass3_0<TFieldType>()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args<TFieldType>
      {
        public PXCache Cache { get; }

        public PXFieldDefaultingEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public TFieldType NewValue
        {
          get => (TFieldType) this.EventArgs.NewValue;
          set => this.EventArgs.NewValue = (object) value;
        }

        public bool Cancel
        {
          get => ((CancelEventArgs) this.EventArgs).Cancel;
          set => ((CancelEventArgs) this.EventArgs).Cancel = value;
        }

        public Args(PXCache cache, PXFieldDefaultingEventArgs args)
        {
          PXCache pxCache = cache;
          PXFieldDefaultingEventArgs defaultingEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = defaultingEventArgs;
        }

        public Args(PXCache cache, TTable row)
          : this(cache, new PXFieldDefaultingEventArgs((object) row))
        {
        }
      }
    }

    public static class ExceptionHandling
    {
      public static void Subscribe<TFieldType>(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.ExceptionHandling.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.ExceptionHandling.Args<TFieldType>>, PXExceptionHandling>.Subscribe(graph, handler, (Action<PXGraph, PXExceptionHandling>) ((g, h) => g.ExceptionHandling.AddHandler(typeof (TTable), fieldName, h)), (Func<Action<ManualEvent.FieldOf<TTable>.ExceptionHandling.Args<TFieldType>>, PXExceptionHandling>) (h => ManualEvent.FieldOf<TTable>.ExceptionHandling.Wrap<TFieldType>(h)));
      }

      public static void Unsubscribe<TFieldType>(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.ExceptionHandling.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.ExceptionHandling.Args<TFieldType>>, PXExceptionHandling>.Unsubscribe(graph, handler, (Action<PXGraph, PXExceptionHandling>) ((g, h) => g.ExceptionHandling.RemoveHandler(typeof (TTable), fieldName, h)));
      }

      private static PXExceptionHandling Wrap<TFieldType>(
        Action<ManualEvent.FieldOf<TTable>.ExceptionHandling.Args<TFieldType>> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXExceptionHandling((object) new ManualEvent.FieldOf<TTable>.ExceptionHandling.\u003C\u003Ec__DisplayClass3_0<TFieldType>()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args<TFieldType>
      {
        public PXCache Cache { get; }

        public PXExceptionHandlingEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public TFieldType NewValue
        {
          get => (TFieldType) this.EventArgs.NewValue;
          set => this.EventArgs.NewValue = (object) value;
        }

        public Exception Exception => this.EventArgs.Exception;

        public bool Cancel
        {
          get => ((CancelEventArgs) this.EventArgs).Cancel;
          set => ((CancelEventArgs) this.EventArgs).Cancel = value;
        }

        public Args(PXCache cache, PXExceptionHandlingEventArgs args)
        {
          PXCache pxCache = cache;
          PXExceptionHandlingEventArgs handlingEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = handlingEventArgs;
        }

        public Args(PXCache cache, TTable row, TFieldType newValue, Exception exception)
          : this(cache, new PXExceptionHandlingEventArgs((object) row, (object) newValue, exception))
        {
        }
      }
    }

    public static class Selecting
    {
      public static void Subscribe(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.Selecting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.Selecting.Args>, PXFieldSelecting>.Subscribe(graph, handler, (Action<PXGraph, PXFieldSelecting>) ((g, h) => g.FieldSelecting.AddHandler(typeof (TTable), fieldName, h)), (Func<Action<ManualEvent.FieldOf<TTable>.Selecting.Args>, PXFieldSelecting>) (h => ManualEvent.FieldOf<TTable>.Selecting.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.Selecting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.Selecting.Args>, PXFieldSelecting>.Unsubscribe(graph, handler, (Action<PXGraph, PXFieldSelecting>) ((g, h) => g.FieldSelecting.RemoveHandler(typeof (TTable), fieldName, h)));
      }

      private static PXFieldSelecting Wrap(
        Action<ManualEvent.FieldOf<TTable>.Selecting.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXFieldSelecting((object) new ManualEvent.FieldOf<TTable>.Selecting.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args
      {
        public PXCache Cache { get; }

        public PXFieldSelectingEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public bool ExternalCall => this.EventArgs.ExternalCall;

        public bool IsAltered
        {
          get => this.EventArgs.IsAltered;
          set => this.EventArgs.IsAltered = value;
        }

        public object ReturnValue
        {
          get => this.EventArgs.ReturnValue;
          set => this.EventArgs.ReturnValue = value;
        }

        public object ReturnState
        {
          get => this.EventArgs.ReturnState;
          set => this.EventArgs.ReturnState = value;
        }

        public bool Cancel
        {
          get => ((CancelEventArgs) this.EventArgs).Cancel;
          set => ((CancelEventArgs) this.EventArgs).Cancel = value;
        }

        public Args(PXCache cache, PXFieldSelectingEventArgs args)
        {
          PXCache pxCache = cache;
          PXFieldSelectingEventArgs selectingEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = selectingEventArgs;
        }

        public Args(
          PXCache cache,
          TTable row,
          object returnValue,
          bool isAltered,
          bool externalCall)
          : this(cache, new PXFieldSelectingEventArgs((object) row, returnValue, isAltered, externalCall))
        {
        }
      }
    }

    public static class Updated
    {
      public static void Subscribe<TFieldType>(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.Updated.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.Updated.Args<TFieldType>>, PXFieldUpdated>.Subscribe(graph, handler, (Action<PXGraph, PXFieldUpdated>) ((g, h) => g.FieldUpdated.AddHandler(typeof (TTable), fieldName, h)), (Func<Action<ManualEvent.FieldOf<TTable>.Updated.Args<TFieldType>>, PXFieldUpdated>) (h => ManualEvent.FieldOf<TTable>.Updated.Wrap<TFieldType>(h)));
      }

      public static void Unsubscribe<TFieldType>(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.Updated.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.Updated.Args<TFieldType>>, PXFieldUpdated>.Unsubscribe(graph, handler, (Action<PXGraph, PXFieldUpdated>) ((g, h) => g.FieldUpdated.RemoveHandler(typeof (TTable), fieldName, h)));
      }

      private static PXFieldUpdated Wrap<TFieldType>(
        Action<ManualEvent.FieldOf<TTable>.Updated.Args<TFieldType>> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXFieldUpdated((object) new ManualEvent.FieldOf<TTable>.Updated.\u003C\u003Ec__DisplayClass3_0<TFieldType>()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args<TFieldType>
      {
        public PXCache Cache { get; }

        public PXFieldUpdatedEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public TFieldType OldValue => (TFieldType) this.EventArgs.OldValue;

        public Args(PXCache cache, PXFieldUpdatedEventArgs args)
        {
          PXCache pxCache = cache;
          PXFieldUpdatedEventArgs updatedEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = updatedEventArgs;
        }

        public Args(PXCache cache, TTable row, TFieldType oldValue, bool externalCall)
          : this(cache, new PXFieldUpdatedEventArgs((object) row, (object) oldValue, externalCall))
        {
        }
      }
    }

    public static class Updating
    {
      public static void Subscribe<TFieldType>(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.Updating.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.Updating.Args<TFieldType>>, PXFieldUpdating>.Subscribe(graph, handler, (Action<PXGraph, PXFieldUpdating>) ((g, h) => g.FieldUpdating.AddHandler(typeof (TTable), fieldName, h)), (Func<Action<ManualEvent.FieldOf<TTable>.Updating.Args<TFieldType>>, PXFieldUpdating>) (h => ManualEvent.FieldOf<TTable>.Updating.Wrap<TFieldType>(h)));
      }

      public static void Unsubscribe<TFieldType>(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.Updating.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.Updating.Args<TFieldType>>, PXFieldUpdating>.Unsubscribe(graph, handler, (Action<PXGraph, PXFieldUpdating>) ((g, h) => g.FieldUpdating.RemoveHandler(typeof (TTable), fieldName, h)));
      }

      private static PXFieldUpdating Wrap<TFieldType>(
        Action<ManualEvent.FieldOf<TTable>.Updating.Args<TFieldType>> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXFieldUpdating((object) new ManualEvent.FieldOf<TTable>.Updating.\u003C\u003Ec__DisplayClass3_0<TFieldType>()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args<TFieldType>
      {
        public PXCache Cache { get; }

        public PXFieldUpdatingEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public TFieldType NewValue
        {
          get => (TFieldType) this.EventArgs.NewValue;
          set => this.EventArgs.NewValue = (object) value;
        }

        public bool Cancel
        {
          get => ((CancelEventArgs) this.EventArgs).Cancel;
          set => ((CancelEventArgs) this.EventArgs).Cancel = value;
        }

        public Args(PXCache cache, PXFieldUpdatingEventArgs args)
        {
          PXCache pxCache = cache;
          PXFieldUpdatingEventArgs updatingEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = updatingEventArgs;
        }

        public Args(PXCache cache, TTable row, TFieldType newValue)
          : this(cache, new PXFieldUpdatingEventArgs((object) row, (object) newValue))
        {
        }
      }
    }

    public static class Verifying
    {
      public static void Subscribe<TFieldType>(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.Verifying.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.Verifying.Args<TFieldType>>, PXFieldVerifying>.Subscribe(graph, handler, (Action<PXGraph, PXFieldVerifying>) ((g, h) => g.FieldVerifying.AddHandler(typeof (TTable), fieldName, h)), (Func<Action<ManualEvent.FieldOf<TTable>.Verifying.Args<TFieldType>>, PXFieldVerifying>) (h => ManualEvent.FieldOf<TTable>.Verifying.Wrap<TFieldType>(h)));
      }

      public static void Unsubscribe<TFieldType>(
        PXGraph graph,
        string fieldName,
        Action<ManualEvent.FieldOf<TTable>.Verifying.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable>.Verifying.Args<TFieldType>>, PXFieldVerifying>.Unsubscribe(graph, handler, (Action<PXGraph, PXFieldVerifying>) ((g, h) => g.FieldVerifying.RemoveHandler(typeof (TTable), fieldName, h)));
      }

      private static PXFieldVerifying Wrap<TFieldType>(
        Action<ManualEvent.FieldOf<TTable>.Verifying.Args<TFieldType>> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXFieldVerifying((object) new ManualEvent.FieldOf<TTable>.Verifying.\u003C\u003Ec__DisplayClass3_0<TFieldType>()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args<TFieldType>
      {
        public PXCache Cache { get; }

        public PXFieldVerifyingEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public TFieldType NewValue
        {
          get => (TFieldType) this.EventArgs.NewValue;
          set => this.EventArgs.NewValue = (object) value;
        }

        public bool ExternalCall => this.EventArgs.ExternalCall;

        public bool Cancel
        {
          get => ((CancelEventArgs) this.EventArgs).Cancel;
          set => ((CancelEventArgs) this.EventArgs).Cancel = value;
        }

        public Args(PXCache cache, PXFieldVerifyingEventArgs args)
        {
          PXCache pxCache = cache;
          PXFieldVerifyingEventArgs verifyingEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = verifyingEventArgs;
        }

        public Args(PXCache cache, TTable row, TFieldType newValue, bool externalCall)
          : this(cache, new PXFieldVerifyingEventArgs((object) row, (object) newValue, externalCall))
        {
        }
      }
    }
  }

  public static class FieldOf<TTable, TField>
    where TTable : class, IBqlTable, new()
    where TField : IBqlField
  {
    public static class Defaulting
    {
      public static void Subscribe<TFieldType>(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.Defaulting.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.Defaulting.Args<TFieldType>>, PXFieldDefaulting>.Subscribe(graph, handler, (Action<PXGraph, PXFieldDefaulting>) ((g, h) => g.FieldDefaulting.AddHandler(typeof (TTable), typeof (TField).Name, h)), (Func<Action<ManualEvent.FieldOf<TTable, TField>.Defaulting.Args<TFieldType>>, PXFieldDefaulting>) (h => ManualEvent.FieldOf<TTable, TField>.Defaulting.Wrap<TFieldType>(h)));
      }

      public static void Unsubscribe<TFieldType>(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.Defaulting.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.Defaulting.Args<TFieldType>>, PXFieldDefaulting>.Unsubscribe(graph, handler, (Action<PXGraph, PXFieldDefaulting>) ((g, h) => g.FieldDefaulting.RemoveHandler(typeof (TTable), typeof (TField).Name, h)));
      }

      private static PXFieldDefaulting Wrap<TFieldType>(
        Action<ManualEvent.FieldOf<TTable, TField>.Defaulting.Args<TFieldType>> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXFieldDefaulting((object) new ManualEvent.FieldOf<TTable, TField>.Defaulting.\u003C\u003Ec__DisplayClass3_0<TFieldType>()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args<TFieldType> : ManualEvent.FieldOf<TTable>.Defaulting.Args<TFieldType>
      {
        public Args(PXCache cache, PXFieldDefaultingEventArgs args)
          : base(cache, args)
        {
        }

        public Args(PXCache cache, TTable row)
          : base(cache, row)
        {
        }
      }
    }

    public static class ExceptionHandling
    {
      public static void Subscribe<TFieldType>(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.ExceptionHandling.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.ExceptionHandling.Args<TFieldType>>, PXExceptionHandling>.Subscribe(graph, handler, (Action<PXGraph, PXExceptionHandling>) ((g, h) => g.ExceptionHandling.AddHandler(typeof (TTable), typeof (TField).Name, h)), (Func<Action<ManualEvent.FieldOf<TTable, TField>.ExceptionHandling.Args<TFieldType>>, PXExceptionHandling>) (h => ManualEvent.FieldOf<TTable, TField>.ExceptionHandling.Wrap<TFieldType>(h)));
      }

      public static void Unsubscribe<TFieldType>(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.ExceptionHandling.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.ExceptionHandling.Args<TFieldType>>, PXExceptionHandling>.Unsubscribe(graph, handler, (Action<PXGraph, PXExceptionHandling>) ((g, h) => g.ExceptionHandling.RemoveHandler(typeof (TTable), typeof (TField).Name, h)));
      }

      private static PXExceptionHandling Wrap<TFieldType>(
        Action<ManualEvent.FieldOf<TTable, TField>.ExceptionHandling.Args<TFieldType>> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXExceptionHandling((object) new ManualEvent.FieldOf<TTable, TField>.ExceptionHandling.\u003C\u003Ec__DisplayClass3_0<TFieldType>()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args<TFieldType> : ManualEvent.FieldOf<TTable>.ExceptionHandling.Args<TFieldType>
      {
        public Args(PXCache cache, PXExceptionHandlingEventArgs args)
          : base(cache, args)
        {
        }

        public Args(PXCache cache, TTable row, TFieldType newValue, Exception exception)
          : base(cache, row, newValue, exception)
        {
        }
      }
    }

    public static class Selecting
    {
      public static void Subscribe(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.Selecting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.Selecting.Args>, PXFieldSelecting>.Subscribe(graph, handler, (Action<PXGraph, PXFieldSelecting>) ((g, h) => g.FieldSelecting.AddHandler(typeof (TTable), typeof (TField).Name, h)), (Func<Action<ManualEvent.FieldOf<TTable, TField>.Selecting.Args>, PXFieldSelecting>) (h => ManualEvent.FieldOf<TTable, TField>.Selecting.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.Selecting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.Selecting.Args>, PXFieldSelecting>.Unsubscribe(graph, handler, (Action<PXGraph, PXFieldSelecting>) ((g, h) => g.FieldSelecting.RemoveHandler(typeof (TTable), typeof (TField).Name, h)));
      }

      private static PXFieldSelecting Wrap(
        Action<ManualEvent.FieldOf<TTable, TField>.Selecting.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXFieldSelecting((object) new ManualEvent.FieldOf<TTable, TField>.Selecting.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args : ManualEvent.FieldOf<TTable>.Selecting.Args
      {
        public Args(PXCache cache, PXFieldSelectingEventArgs args)
          : base(cache, args)
        {
        }

        public Args(
          PXCache cache,
          TTable row,
          object returnValue,
          bool isAltered,
          bool externalCall)
          : base(cache, row, returnValue, isAltered, externalCall)
        {
        }
      }
    }

    public static class Updated
    {
      public static void Subscribe<TFieldType>(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.Updated.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.Updated.Args<TFieldType>>, PXFieldUpdated>.Subscribe(graph, handler, (Action<PXGraph, PXFieldUpdated>) ((g, h) => g.FieldUpdated.AddHandler(typeof (TTable), typeof (TField).Name, h)), (Func<Action<ManualEvent.FieldOf<TTable, TField>.Updated.Args<TFieldType>>, PXFieldUpdated>) (h => ManualEvent.FieldOf<TTable, TField>.Updated.Wrap<TFieldType>(h)));
      }

      public static void Unsubscribe<TFieldType>(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.Updated.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.Updated.Args<TFieldType>>, PXFieldUpdated>.Unsubscribe(graph, handler, (Action<PXGraph, PXFieldUpdated>) ((g, h) => g.FieldUpdated.RemoveHandler(typeof (TTable), typeof (TField).Name, h)));
      }

      private static PXFieldUpdated Wrap<TFieldType>(
        Action<ManualEvent.FieldOf<TTable, TField>.Updated.Args<TFieldType>> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXFieldUpdated((object) new ManualEvent.FieldOf<TTable, TField>.Updated.\u003C\u003Ec__DisplayClass3_0<TFieldType>()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args<TFieldType> : ManualEvent.FieldOf<TTable>.Updated.Args<TFieldType>
      {
        public Args(PXCache cache, PXFieldUpdatedEventArgs args)
          : base(cache, args)
        {
        }

        public Args(PXCache cache, TTable row, TFieldType oldValue, bool externalCall)
          : base(cache, row, oldValue, externalCall)
        {
        }
      }
    }

    public static class Updating
    {
      public static void Subscribe<TFieldType>(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.Updating.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.Updating.Args<TFieldType>>, PXFieldUpdating>.Subscribe(graph, handler, (Action<PXGraph, PXFieldUpdating>) ((g, h) => g.FieldUpdating.AddHandler(typeof (TTable), typeof (TField).Name, h)), (Func<Action<ManualEvent.FieldOf<TTable, TField>.Updating.Args<TFieldType>>, PXFieldUpdating>) (h => ManualEvent.FieldOf<TTable, TField>.Updating.Wrap<TFieldType>(h)));
      }

      public static void Unsubscribe<TFieldType>(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.Updating.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.Updating.Args<TFieldType>>, PXFieldUpdating>.Unsubscribe(graph, handler, (Action<PXGraph, PXFieldUpdating>) ((g, h) => g.FieldUpdating.RemoveHandler(typeof (TTable), typeof (TField).Name, h)));
      }

      private static PXFieldUpdating Wrap<TFieldType>(
        Action<ManualEvent.FieldOf<TTable, TField>.Updating.Args<TFieldType>> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXFieldUpdating((object) new ManualEvent.FieldOf<TTable, TField>.Updating.\u003C\u003Ec__DisplayClass3_0<TFieldType>()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args<TFieldType> : ManualEvent.FieldOf<TTable>.Updating.Args<TFieldType>
      {
        public Args(PXCache cache, PXFieldUpdatingEventArgs args)
          : base(cache, args)
        {
        }

        public Args(PXCache cache, TTable row, TFieldType newValue)
          : base(cache, row, newValue)
        {
        }
      }
    }

    public static class Verifying
    {
      public static void Subscribe<TFieldType>(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.Verifying.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.Verifying.Args<TFieldType>>, PXFieldVerifying>.Subscribe(graph, handler, (Action<PXGraph, PXFieldVerifying>) ((g, h) => g.FieldVerifying.AddHandler(typeof (TTable), typeof (TField).Name, h)), (Func<Action<ManualEvent.FieldOf<TTable, TField>.Verifying.Args<TFieldType>>, PXFieldVerifying>) (h => ManualEvent.FieldOf<TTable, TField>.Verifying.Wrap<TFieldType>(h)));
      }

      public static void Unsubscribe<TFieldType>(
        PXGraph graph,
        Action<ManualEvent.FieldOf<TTable, TField>.Verifying.Args<TFieldType>> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.FieldOf<TTable, TField>.Verifying.Args<TFieldType>>, PXFieldVerifying>.Unsubscribe(graph, handler, (Action<PXGraph, PXFieldVerifying>) ((g, h) => g.FieldVerifying.RemoveHandler(typeof (TTable), typeof (TField).Name, h)));
      }

      private static PXFieldVerifying Wrap<TFieldType>(
        Action<ManualEvent.FieldOf<TTable, TField>.Verifying.Args<TFieldType>> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXFieldVerifying((object) new ManualEvent.FieldOf<TTable, TField>.Verifying.\u003C\u003Ec__DisplayClass3_0<TFieldType>()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args<TFieldType> : ManualEvent.FieldOf<TTable>.Verifying.Args<TFieldType>
      {
        public Args(PXCache cache, PXFieldVerifyingEventArgs args)
          : base(cache, args)
        {
        }

        public Args(PXCache cache, TTable row, TFieldType newValue, bool externalCall)
          : base(cache, row, newValue, externalCall)
        {
        }
      }
    }
  }

  public static class Row<TTable> where TTable : class, IBqlTable, new()
  {
    public static class Deleted
    {
      public static void Subscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Deleted.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Deleted.Args>, PXRowDeleted>.Subscribe(graph, handler, (Action<PXGraph, PXRowDeleted>) ((g, h) => g.RowDeleted.AddHandler<TTable>(h)), (Func<Action<ManualEvent.Row<TTable>.Deleted.Args>, PXRowDeleted>) (h => ManualEvent.Row<TTable>.Deleted.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Deleted.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Deleted.Args>, PXRowDeleted>.Unsubscribe(graph, handler, (Action<PXGraph, PXRowDeleted>) ((g, h) => g.RowDeleted.RemoveHandler<TTable>(h)));
      }

      private static PXRowDeleted Wrap(
        Action<ManualEvent.Row<TTable>.Deleted.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXRowDeleted((object) new ManualEvent.Row<TTable>.Deleted.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args
      {
        public PXCache Cache { get; }

        public PXRowDeletedEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public Args(PXCache cache, PXRowDeletedEventArgs args)
        {
          PXCache pxCache = cache;
          PXRowDeletedEventArgs deletedEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = deletedEventArgs;
        }

        public Args(PXCache cache, TTable row, bool externalCall)
          : this(cache, new PXRowDeletedEventArgs((object) row, externalCall))
        {
        }
      }
    }

    public static class Deleting
    {
      public static void Subscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Deleting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Deleting.Args>, PXRowDeleting>.Subscribe(graph, handler, (Action<PXGraph, PXRowDeleting>) ((g, h) => g.RowDeleting.AddHandler<TTable>(h)), (Func<Action<ManualEvent.Row<TTable>.Deleting.Args>, PXRowDeleting>) (h => ManualEvent.Row<TTable>.Deleting.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Deleting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Deleting.Args>, PXRowDeleting>.Unsubscribe(graph, handler, (Action<PXGraph, PXRowDeleting>) ((g, h) => g.RowDeleting.RemoveHandler<TTable>(h)));
      }

      private static PXRowDeleting Wrap(
        Action<ManualEvent.Row<TTable>.Deleting.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXRowDeleting((object) new ManualEvent.Row<TTable>.Deleting.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args
      {
        public PXCache Cache { get; }

        public PXRowDeletingEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public bool Cancel
        {
          get => ((CancelEventArgs) this.EventArgs).Cancel;
          set => ((CancelEventArgs) this.EventArgs).Cancel = value;
        }

        public bool ExternalCall => this.EventArgs.ExternalCall;

        public Args(PXCache cache, PXRowDeletingEventArgs args)
        {
          PXCache pxCache = cache;
          PXRowDeletingEventArgs deletingEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = deletingEventArgs;
        }

        public Args(PXCache cache, TTable row, bool externalCall)
          : this(cache, new PXRowDeletingEventArgs((object) row, externalCall))
        {
        }
      }
    }

    public static class Inserted
    {
      public static void Subscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Inserted.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Inserted.Args>, PXRowInserted>.Subscribe(graph, handler, (Action<PXGraph, PXRowInserted>) ((g, h) => g.RowInserted.AddHandler<TTable>(h)), (Func<Action<ManualEvent.Row<TTable>.Inserted.Args>, PXRowInserted>) (h => ManualEvent.Row<TTable>.Inserted.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Inserted.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Inserted.Args>, PXRowInserted>.Unsubscribe(graph, handler, (Action<PXGraph, PXRowInserted>) ((g, h) => g.RowInserted.RemoveHandler<TTable>(h)));
      }

      private static PXRowInserted Wrap(
        Action<ManualEvent.Row<TTable>.Inserted.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXRowInserted((object) new ManualEvent.Row<TTable>.Inserted.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args
      {
        public PXCache Cache { get; }

        public PXRowInsertedEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public bool ExternalCall => this.EventArgs.ExternalCall;

        public Args(PXCache cache, PXRowInsertedEventArgs args)
        {
          PXCache pxCache = cache;
          PXRowInsertedEventArgs insertedEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = insertedEventArgs;
        }

        public Args(PXCache cache, TTable row, bool externalCall)
          : this(cache, new PXRowInsertedEventArgs((object) row, externalCall))
        {
        }
      }
    }

    public static class Inserting
    {
      public static void Subscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Inserting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Inserting.Args>, PXRowInserting>.Subscribe(graph, handler, (Action<PXGraph, PXRowInserting>) ((g, h) => g.RowInserting.AddHandler<TTable>(h)), (Func<Action<ManualEvent.Row<TTable>.Inserting.Args>, PXRowInserting>) (h => ManualEvent.Row<TTable>.Inserting.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Inserting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Inserting.Args>, PXRowInserting>.Unsubscribe(graph, handler, (Action<PXGraph, PXRowInserting>) ((g, h) => g.RowInserting.RemoveHandler<TTable>(h)));
      }

      private static PXRowInserting Wrap(
        Action<ManualEvent.Row<TTable>.Inserting.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXRowInserting((object) new ManualEvent.Row<TTable>.Inserting.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args
      {
        public PXCache Cache { get; }

        public PXRowInsertingEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public bool ExternalCall => this.EventArgs.ExternalCall;

        public bool Cancel
        {
          get => ((CancelEventArgs) this.EventArgs).Cancel;
          set => ((CancelEventArgs) this.EventArgs).Cancel = value;
        }

        public Args(PXCache cache, PXRowInsertingEventArgs args)
        {
          PXCache pxCache = cache;
          PXRowInsertingEventArgs insertingEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = insertingEventArgs;
        }

        public Args(PXCache cache, TTable row, bool externalCall)
          : this(cache, new PXRowInsertingEventArgs((object) row, externalCall))
        {
        }
      }
    }

    public static class Persisted
    {
      public static void Subscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Persisted.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Persisted.Args>, PXRowPersisted>.Subscribe(graph, handler, (Action<PXGraph, PXRowPersisted>) ((g, h) => g.RowPersisted.AddHandler<TTable>(h)), (Func<Action<ManualEvent.Row<TTable>.Persisted.Args>, PXRowPersisted>) (h => ManualEvent.Row<TTable>.Persisted.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Persisted.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Persisted.Args>, PXRowPersisted>.Unsubscribe(graph, handler, (Action<PXGraph, PXRowPersisted>) ((g, h) => g.RowPersisted.RemoveHandler<TTable>(h)));
      }

      private static PXRowPersisted Wrap(
        Action<ManualEvent.Row<TTable>.Persisted.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXRowPersisted((object) new ManualEvent.Row<TTable>.Persisted.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args
      {
        public PXCache Cache { get; }

        public PXRowPersistedEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public PXTranStatus TranStatus => this.EventArgs.TranStatus;

        public PXDBOperation Operation => this.EventArgs.Operation;

        public Exception Exception => this.EventArgs.Exception;

        public Args(PXCache cache, PXRowPersistedEventArgs args)
        {
          PXCache pxCache = cache;
          PXRowPersistedEventArgs persistedEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = persistedEventArgs;
        }

        public Args(
          PXCache cache,
          TTable row,
          PXDBOperation operation,
          PXTranStatus tranStatus,
          Exception exception)
          : this(cache, new PXRowPersistedEventArgs((object) row, operation, tranStatus, exception))
        {
        }
      }
    }

    public static class Persisting
    {
      public static void Subscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Persisting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Persisting.Args>, PXRowPersisting>.Subscribe(graph, handler, (Action<PXGraph, PXRowPersisting>) ((g, h) => g.RowPersisting.AddHandler<TTable>(h)), (Func<Action<ManualEvent.Row<TTable>.Persisting.Args>, PXRowPersisting>) (h => ManualEvent.Row<TTable>.Persisting.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Persisting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Persisting.Args>, PXRowPersisting>.Unsubscribe(graph, handler, (Action<PXGraph, PXRowPersisting>) ((g, h) => g.RowPersisting.RemoveHandler<TTable>(h)));
      }

      private static PXRowPersisting Wrap(
        Action<ManualEvent.Row<TTable>.Persisting.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXRowPersisting((object) new ManualEvent.Row<TTable>.Persisting.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args
      {
        public PXCache Cache { get; }

        public PXRowPersistingEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public PXDBOperation Operation => this.EventArgs.Operation;

        public bool Cancel
        {
          get => ((CancelEventArgs) this.EventArgs).Cancel;
          set => ((CancelEventArgs) this.EventArgs).Cancel = value;
        }

        public Args(PXCache cache, PXRowPersistingEventArgs args)
        {
          PXCache pxCache = cache;
          PXRowPersistingEventArgs persistingEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = persistingEventArgs;
        }

        public Args(PXCache cache, PXDBOperation operation, TTable row)
          : this(cache, new PXRowPersistingEventArgs(operation, (object) row))
        {
        }

        public IDictionary<string, (object OldValue, object NewValue)> GetDifference()
        {
          switch (PXDBOperationExt.Command(this.Operation) - 1)
          {
            case 0:
              return PXCacheEx.GetDifference(this.Cache, (IBqlTable) this.Cache.GetOriginal((object) this.Row), (IBqlTable) this.Row, false);
            case 1:
              return (IDictionary<string, (object, object)>) new Dictionary<string, (object, object)>()
              {
                ["__RowExists__"] = ((object) false, (object) true)
              };
            case 2:
              return (IDictionary<string, (object, object)>) new Dictionary<string, (object, object)>()
              {
                ["__RowExists__"] = ((object) true, (object) false)
              };
            default:
              return (IDictionary<string, (object, object)>) null;
          }
        }
      }
    }

    public static class Selected
    {
      public static void Subscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Selected.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Selected.Args>, PXRowSelected>.Subscribe(graph, handler, (Action<PXGraph, PXRowSelected>) ((g, h) => g.RowSelected.AddHandler<TTable>(h)), (Func<Action<ManualEvent.Row<TTable>.Selected.Args>, PXRowSelected>) (h => ManualEvent.Row<TTable>.Selected.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Selected.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Selected.Args>, PXRowSelected>.Unsubscribe(graph, handler, (Action<PXGraph, PXRowSelected>) ((g, h) => g.RowSelected.RemoveHandler<TTable>(h)));
      }

      private static PXRowSelected Wrap(
        Action<ManualEvent.Row<TTable>.Selected.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXRowSelected((object) new ManualEvent.Row<TTable>.Selected.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args
      {
        public PXCache Cache { get; }

        public PXRowSelectedEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public Args(PXCache cache, PXRowSelectedEventArgs args)
        {
          PXCache pxCache = cache;
          PXRowSelectedEventArgs selectedEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = selectedEventArgs;
        }

        public Args(PXCache cache, TTable row)
          : this(cache, new PXRowSelectedEventArgs((object) row))
        {
        }
      }
    }

    public static class Selecting
    {
      public static void Subscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Selecting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Selecting.Args>, PXRowSelecting>.Subscribe(graph, handler, (Action<PXGraph, PXRowSelecting>) ((g, h) => g.RowSelecting.AddHandler<TTable>(h)), (Func<Action<ManualEvent.Row<TTable>.Selecting.Args>, PXRowSelecting>) (h => ManualEvent.Row<TTable>.Selecting.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Selecting.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Selecting.Args>, PXRowSelecting>.Unsubscribe(graph, handler, (Action<PXGraph, PXRowSelecting>) ((g, h) => g.RowSelecting.RemoveHandler<TTable>(h)));
      }

      private static PXRowSelecting Wrap(
        Action<ManualEvent.Row<TTable>.Selecting.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXRowSelecting((object) new ManualEvent.Row<TTable>.Selecting.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args
      {
        public PXCache Cache { get; }

        public PXRowSelectingEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public PXDataRecord Record => this.EventArgs.Record;

        public bool IsReadOnly => this.EventArgs.IsReadOnly;

        public int Position
        {
          get => this.EventArgs.Position;
          set => this.EventArgs.Position = value;
        }

        public bool Cancel
        {
          get => ((CancelEventArgs) this.EventArgs).Cancel;
          set => ((CancelEventArgs) this.EventArgs).Cancel = value;
        }

        public Args(PXCache cache, PXRowSelectingEventArgs args)
        {
          PXCache pxCache = cache;
          PXRowSelectingEventArgs selectingEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = selectingEventArgs;
        }

        public Args(
          PXCache cache,
          TTable row,
          PXDataRecord record,
          int position,
          bool isReadOnly)
          : this(cache, new PXRowSelectingEventArgs((object) row, record, position, isReadOnly))
        {
        }
      }
    }

    public static class Updated
    {
      public static void Subscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Updated.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Updated.Args>, PXRowUpdated>.Subscribe(graph, handler, (Action<PXGraph, PXRowUpdated>) ((g, h) => g.RowUpdated.AddHandler<TTable>(h)), (Func<Action<ManualEvent.Row<TTable>.Updated.Args>, PXRowUpdated>) (h => ManualEvent.Row<TTable>.Updated.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Updated.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Updated.Args>, PXRowUpdated>.Unsubscribe(graph, handler, (Action<PXGraph, PXRowUpdated>) ((g, h) => g.RowUpdated.RemoveHandler<TTable>(h)));
      }

      private static PXRowUpdated Wrap(
        Action<ManualEvent.Row<TTable>.Updated.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXRowUpdated((object) new ManualEvent.Row<TTable>.Updated.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args
      {
        public PXCache Cache { get; }

        public PXRowUpdatedEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public TTable OldRow => (TTable) this.EventArgs.OldRow;

        public bool ExternalCall => this.EventArgs.ExternalCall;

        public Args(PXCache cache, PXRowUpdatedEventArgs args)
        {
          PXCache pxCache = cache;
          PXRowUpdatedEventArgs updatedEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = updatedEventArgs;
        }

        public Args(PXCache cache, TTable row, TTable oldRow, bool externalCall)
          : this(cache, new PXRowUpdatedEventArgs((object) row, (object) oldRow, externalCall))
        {
        }

        public IDictionary<string, (object OldValue, object NewValue)> GetDifference()
        {
          return PXCacheEx.GetDifference(this.Cache, (IBqlTable) this.OldRow, (IBqlTable) this.Row, false);
        }
      }
    }

    public static class Updating
    {
      public static void Subscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Updating.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Updating.Args>, PXRowUpdating>.Subscribe(graph, handler, (Action<PXGraph, PXRowUpdating>) ((g, h) => g.RowUpdating.AddHandler<TTable>(h)), (Func<Action<ManualEvent.Row<TTable>.Updating.Args>, PXRowUpdating>) (h => ManualEvent.Row<TTable>.Updating.Wrap(h)));
      }

      public static void Unsubscribe(
        PXGraph graph,
        Action<ManualEvent.Row<TTable>.Updating.Args> handler)
      {
        ManualEvent.Synchronizer<Action<ManualEvent.Row<TTable>.Updating.Args>, PXRowUpdating>.Unsubscribe(graph, handler, (Action<PXGraph, PXRowUpdating>) ((g, h) => g.RowUpdating.RemoveHandler<TTable>(h)));
      }

      private static PXRowUpdating Wrap(
        Action<ManualEvent.Row<TTable>.Updating.Args> handler)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        return new PXRowUpdating((object) new ManualEvent.Row<TTable>.Updating.\u003C\u003Ec__DisplayClass3_0()
        {
          handler = handler
        }, __methodptr(\u003CWrap\u003Eb__0));
      }

      [DebuggerStepThrough]
      public class Args
      {
        public PXCache Cache { get; }

        public PXRowUpdatingEventArgs EventArgs { get; }

        public TTable Row => (TTable) this.EventArgs.Row;

        public TTable NewRow => (TTable) this.EventArgs.NewRow;

        public bool ExternalCall => this.EventArgs.ExternalCall;

        public bool Cancel
        {
          get => ((CancelEventArgs) this.EventArgs).Cancel;
          set => ((CancelEventArgs) this.EventArgs).Cancel = value;
        }

        public Args(PXCache cache, PXRowUpdatingEventArgs args)
        {
          PXCache pxCache = cache;
          PXRowUpdatingEventArgs updatingEventArgs = args;
          this.Cache = pxCache;
          this.EventArgs = updatingEventArgs;
        }

        public Args(PXCache cache, TTable row, TTable newRow, bool externalCall)
          : this(cache, new PXRowUpdatingEventArgs((object) row, (object) newRow, externalCall))
        {
        }
      }
    }
  }

  private class Synchronizer<TModernDelegate, TOrigDelegate>
    where TModernDelegate : class
    where TOrigDelegate : class
  {
    private static readonly ConditionalWeakTable<PXGraph, ConditionalWeakTable<object, Dictionary<MethodInfo, TOrigDelegate>>> Storage = new ConditionalWeakTable<PXGraph, ConditionalWeakTable<object, Dictionary<MethodInfo, TOrigDelegate>>>();

    public static void Subscribe(
      PXGraph graph,
      TModernDelegate handler,
      Action<PXGraph, TOrigDelegate> subscription,
      Func<TModernDelegate, TOrigDelegate> wrapping)
    {
      TOrigDelegate origDelegate = wrapping(handler);
      Delegate @delegate = (object) handler as Delegate;
      if ((object) @delegate != null)
      {
        ConditionalWeakTable<object, Dictionary<MethodInfo, TOrigDelegate>> conditionalWeakTable1;
        if (ManualEvent.Synchronizer<TModernDelegate, TOrigDelegate>.Storage.TryGetValue(graph, out conditionalWeakTable1))
        {
          Dictionary<MethodInfo, TOrigDelegate> dictionary;
          if (conditionalWeakTable1.TryGetValue(@delegate.Target ?? ManualEvent.StaticTarget, out dictionary))
            dictionary[@delegate.Method] = origDelegate;
          else
            conditionalWeakTable1.Add(@delegate.Target ?? ManualEvent.StaticTarget, new Dictionary<MethodInfo, TOrigDelegate>()
            {
              [@delegate.Method] = origDelegate
            });
        }
        else
        {
          ConditionalWeakTable<object, Dictionary<MethodInfo, TOrigDelegate>> conditionalWeakTable2 = new ConditionalWeakTable<object, Dictionary<MethodInfo, TOrigDelegate>>();
          conditionalWeakTable2.Add(@delegate.Target ?? ManualEvent.StaticTarget, new Dictionary<MethodInfo, TOrigDelegate>()
          {
            [@delegate.Method] = origDelegate
          });
          ManualEvent.Synchronizer<TModernDelegate, TOrigDelegate>.Storage.Add(graph, conditionalWeakTable2);
        }
      }
      subscription(graph, origDelegate);
    }

    public static void Unsubscribe(
      PXGraph graph,
      TModernDelegate handler,
      Action<PXGraph, TOrigDelegate> unsubscription)
    {
      Delegate @delegate = (object) handler as Delegate;
      ConditionalWeakTable<object, Dictionary<MethodInfo, TOrigDelegate>> conditionalWeakTable;
      Dictionary<MethodInfo, TOrigDelegate> dictionary;
      TOrigDelegate origDelegate;
      if ((object) @delegate == null || !ManualEvent.Synchronizer<TModernDelegate, TOrigDelegate>.Storage.TryGetValue(graph, out conditionalWeakTable) || !conditionalWeakTable.TryGetValue(@delegate.Target ?? ManualEvent.StaticTarget, out dictionary) || !dictionary.TryGetValue(@delegate.Method, out origDelegate))
        return;
      unsubscription(graph, origDelegate);
    }
  }
}
