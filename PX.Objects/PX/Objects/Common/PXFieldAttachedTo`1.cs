// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.PXFieldAttachedTo`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.Common;

[PXInternalUseOnly]
public abstract class PXFieldAttachedTo<TTable> where TTable : class, IBqlTable, new()
{
  [PXInternalUseOnly]
  public abstract class By<TGraph> where TGraph : PXGraph
  {
    [PXInternalUseOnly]
    public abstract class ByExt1<TExt1> where TExt1 : PXGraphExtension<TGraph>
    {
      public abstract class ByExt2<TExt2> where TExt2 : PXGraphExtension<TExt1, TGraph>
      {
        [PXInternalUseOnly]
        public abstract class As<TValue> : PXGraphExtension<TExt2, TExt1, TGraph>
        {
          protected PXUIFieldAttribute FieldAttribute
          {
            get => ((object) this).GetType().GetCustomAttribute<PXUIFieldAttribute>();
          }

          public virtual void Initialize()
          {
            this.FieldName = ((IEnumerable<string>) ((object) this).GetType().Name.Split('+')).Last<string>();
            ((PXCache) GraphHelper.Caches<TTable>((PXGraph) ((PXGraphExtension<TGraph>) this).Base)).Fields.Add(this.FieldName);
            // ISSUE: method pointer
            ((PXGraphExtension<TGraph>) this).Base.FieldSelecting.AddHandler(typeof (TTable), this.FieldName, new PXFieldSelecting((object) this, __methodptr(FieldSelecting)));
          }

          private void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
          {
            PXFieldState state = this.DefaultState(sender, e);
            if (this.FieldAttribute != null)
              state = this.AdjustByAttribute(state, this.FieldAttribute);
            PXFieldState pxFieldState = this.AdjustStateByRow(this.AdjustStateBySelf(state), (TTable) e.Row);
            e.ReturnState = (object) pxFieldState;
            if (this.SuppressValueSetting)
              return;
            e.ReturnValue = (object) (e.Row == null ? this.ValueForEmptyRow : this.GetValue((TTable) e.Row));
          }

          protected abstract PXFieldState DefaultState(PXCache sender, PXFieldSelectingEventArgs e);

          protected virtual PXFieldState AdjustByAttribute(
            PXFieldState state,
            PXUIFieldAttribute uiAttribute)
          {
            state.Visible = uiAttribute.Visible;
            state.Visibility = uiAttribute.Visibility;
            if (uiAttribute.DisplayName != null)
              state.DisplayName = PXMessages.LocalizeFormatNoPrefix(uiAttribute.DisplayName, Array.Empty<object>());
            return state;
          }

          protected virtual PXFieldState AdjustStateBySelf(PXFieldState state)
          {
            state.Enabled = false;
            this.Visible.With<bool, bool>((Func<bool, bool>) (it => state.Visible = it));
            this.Visibility.With<PXUIVisibility, PXUIVisibility>((Func<PXUIVisibility, PXUIVisibility>) (it => state.Visibility = it));
            this.DisplayName.With<string, string>((Func<string, string>) (it => state.DisplayName = PXMessages.LocalizeFormatNoPrefix(it, Array.Empty<object>())));
            return state;
          }

          protected virtual PXFieldState AdjustStateByRow(PXFieldState state, TTable row) => state;

          protected virtual bool SuppressValueSetting => false;

          public abstract TValue GetValue(TTable Row);

          protected virtual TValue ValueForEmptyRow => default (TValue);

          protected virtual bool? Visible => new bool?();

          protected virtual PXUIVisibility Visibility
          {
            get => !this.Visible.GetValueOrDefault() ? (PXUIVisibility) 1 : (PXUIVisibility) 3;
          }

          protected virtual string DisplayName => (string) null;

          public Type BqlTable => typeof (TTable);

          public virtual string FieldName { get; private set; }

          public static TValue GetValue<TSelf>(TGraph graph, TTable row) where TSelf : PXFieldAttachedTo<TTable>.By<TGraph>.ByExt1<TExt1>.ByExt2<TExt2>.As<TValue>
          {
            return graph.GetExtension<TSelf>().GetValue(row);
          }

          public static object GetValueExt<TSelf>(TGraph graph, TTable row) where TSelf : PXFieldAttachedTo<TTable>.By<TGraph>.ByExt1<TExt1>.ByExt2<TExt2>.As<TValue>
          {
            return ((PXFieldState) ((PXCache) GraphHelper.Caches<TTable>((PXGraph) graph)).GetStateExt((object) row, ((IEnumerable<string>) typeof (TSelf).Name.Split('+')).Last<string>()))?.Value;
          }
        }

        [PXInternalUseOnly]
        public abstract class AsDecimal : 
          PXFieldAttachedTo<TTable>.By<TGraph>.ByExt1<TExt1>.ByExt2<TExt2>.As<Decimal?>
        {
          protected override PXFieldState DefaultState(PXCache sender, PXFieldSelectingEventArgs e)
          {
            return PXDecimalState.CreateInstance(e.ReturnState, new int?(this.Precision), this.FieldName, new bool?(), new int?(), new Decimal?(), new Decimal?());
          }

          protected virtual int Precision => 2;

          [PXInternalUseOnly]
          public abstract class Named<TSelf> : 
            PXFieldAttachedTo<TTable>.By<TGraph>.ByExt1<TExt1>.ByExt2<TExt2>.AsDecimal
            where TSelf : PXFieldAttachedTo<TTable>.By<TGraph>.ByExt1<TExt1>.ByExt2<TExt2>.AsDecimal
          {
            public static Decimal? GetValue(TGraph graph, TTable row)
            {
              return PXFieldAttachedTo<TTable>.By<TGraph>.ByExt1<TExt1>.ByExt2<TExt2>.As<Decimal?>.GetValue<TSelf>(graph, row);
            }
          }
        }
      }
    }

    [PXInternalUseOnly]
    public abstract class As<TValue> : PXGraphExtension<TGraph>
    {
      protected PXUIFieldAttribute FieldAttribute
      {
        get => ((object) this).GetType().GetCustomAttribute<PXUIFieldAttribute>();
      }

      public virtual void Initialize()
      {
        this.FieldName = StringExtensions.LastSegment(((object) this).GetType().Name, '+');
        ((PXCache) GraphHelper.Caches<TTable>((PXGraph) this.Base)).Fields.Add(this.FieldName);
        // ISSUE: method pointer
        this.Base.FieldSelecting.AddHandler(typeof (TTable), this.FieldName, new PXFieldSelecting((object) this, __methodptr(FieldSelecting)));
      }

      private void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
      {
        PXFieldState state = this.DefaultState(sender, e);
        if (this.FieldAttribute != null)
          state = this.AdjustByAttribute(state, this.FieldAttribute);
        PXFieldState pxFieldState = this.AdjustStateByRow(this.AdjustStateBySelf(state), (TTable) e.Row);
        e.ReturnState = (object) pxFieldState;
        if (this.SuppressValueSetting)
          return;
        e.ReturnValue = (object) (e.Row == null ? this.ValueForEmptyRow : this.GetValue((TTable) e.Row));
      }

      protected abstract PXFieldState DefaultState(PXCache sender, PXFieldSelectingEventArgs e);

      protected virtual PXFieldState AdjustByAttribute(
        PXFieldState state,
        PXUIFieldAttribute uiAttribute)
      {
        state.Visible = uiAttribute.Visible;
        state.Visibility = uiAttribute.Visibility;
        if (uiAttribute.DisplayName != null)
          state.DisplayName = PXMessages.LocalizeFormatNoPrefix(uiAttribute.DisplayName, Array.Empty<object>());
        return state;
      }

      protected virtual PXFieldState AdjustStateBySelf(PXFieldState state)
      {
        state.Enabled = false;
        this.Visible.With<bool, bool>((Func<bool, bool>) (it => state.Visible = it));
        this.DisplayName.With<string, string>((Func<string, string>) (it => state.DisplayName = PXMessages.LocalizeFormatNoPrefix(it, Array.Empty<object>())));
        return state;
      }

      protected virtual PXFieldState AdjustStateByRow(PXFieldState state, TTable row) => state;

      protected virtual bool SuppressValueSetting => false;

      public abstract TValue GetValue(TTable Row);

      protected virtual TValue ValueForEmptyRow => default (TValue);

      protected virtual bool? Visible => new bool?();

      protected virtual string DisplayName => (string) null;

      public Type BqlTable => typeof (TTable);

      public virtual string FieldName { get; private set; }

      public static TValue GetValue<TSelf>(TGraph graph, TTable row) where TSelf : PXFieldAttachedTo<TTable>.By<TGraph>.As<TValue>
      {
        return graph.GetExtension<TSelf>().GetValue(row);
      }

      public static object GetValueExt<TSelf>(TGraph graph, TTable row) where TSelf : PXFieldAttachedTo<TTable>.By<TGraph>.As<TValue>
      {
        return ((PXFieldState) ((PXCache) GraphHelper.Caches<TTable>((PXGraph) graph)).GetStateExt((object) row, StringExtensions.LastSegment(typeof (TSelf).Name, '+')))?.Value;
      }
    }

    [PXInternalUseOnly]
    public abstract class AsBool : PXFieldAttachedTo<TTable>.By<TGraph>.As<bool?>
    {
      protected override PXFieldState DefaultState(PXCache sender, PXFieldSelectingEventArgs e)
      {
        object returnState = e.ReturnState;
        Type type = typeof (bool);
        string fieldName = this.FieldName;
        bool? nullable1 = new bool?();
        bool? nullable2 = new bool?();
        int? nullable3 = new int?();
        int? nullable4 = new int?();
        int? nullable5 = new int?();
        string str = fieldName;
        bool? nullable6 = new bool?();
        bool? nullable7 = new bool?();
        bool? nullable8 = new bool?();
        return PXFieldState.CreateInstance(returnState, type, nullable1, nullable2, nullable3, nullable4, nullable5, (object) null, str, (string) null, (string) null, (string) null, (PXErrorLevel) 0, nullable6, nullable7, nullable8, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
      }

      [PXInternalUseOnly]
      public abstract class Named<TSelf> : PXFieldAttachedTo<TTable>.By<TGraph>.AsBool where TSelf : PXFieldAttachedTo<TTable>.By<TGraph>.AsBool
      {
        public static bool? GetValue(TGraph graph, TTable row)
        {
          return PXFieldAttachedTo<TTable>.By<TGraph>.As<bool?>.GetValue<TSelf>(graph, row);
        }
      }
    }

    [PXInternalUseOnly]
    public abstract class AsInteger : PXFieldAttachedTo<TTable>.By<TGraph>.As<int?>
    {
      protected override PXFieldState DefaultState(PXCache sender, PXFieldSelectingEventArgs e)
      {
        return PXIntState.CreateInstance(e.ReturnState, this.FieldName, new bool?(), new int?(), new int?(), new int?(), (int[]) null, (string[]) null, typeof (int), this.DefaultValue, (string[]) null);
      }

      protected virtual int? DefaultValue => new int?();

      [PXInternalUseOnly]
      public abstract class Named<TSelf> : PXFieldAttachedTo<TTable>.By<TGraph>.AsInteger where TSelf : PXFieldAttachedTo<TTable>.By<TGraph>.AsInteger
      {
        public static int? GetValue(TGraph graph, TTable row)
        {
          return PXFieldAttachedTo<TTable>.By<TGraph>.As<int?>.GetValue<TSelf>(graph, row);
        }
      }
    }

    [PXInternalUseOnly]
    public abstract class AsDecimal : PXFieldAttachedTo<TTable>.By<TGraph>.As<Decimal?>
    {
      protected override PXFieldState DefaultState(PXCache sender, PXFieldSelectingEventArgs e)
      {
        return PXDecimalState.CreateInstance(e.ReturnState, new int?(this.Precision), this.FieldName, new bool?(), new int?(), new Decimal?(), new Decimal?());
      }

      protected virtual int Precision => 2;

      [PXInternalUseOnly]
      public abstract class Named<TSelf> : PXFieldAttachedTo<TTable>.By<TGraph>.AsDecimal where TSelf : PXFieldAttachedTo<TTable>.By<TGraph>.AsDecimal
      {
        public static Decimal? GetValue(TGraph graph, TTable row)
        {
          return PXFieldAttachedTo<TTable>.By<TGraph>.As<Decimal?>.GetValue<TSelf>(graph, row);
        }
      }
    }

    [PXInternalUseOnly]
    public abstract class AsDateTime : PXFieldAttachedTo<TTable>.By<TGraph>.As<DateTime?>
    {
      protected override PXFieldState DefaultState(PXCache sender, PXFieldSelectingEventArgs e)
      {
        return PXDateState.CreateInstance(e.ReturnState, this.FieldName, new bool?(), new int?(), (string) null, (string) null, new DateTime?(), new DateTime?());
      }

      [PXInternalUseOnly]
      public abstract class Named<TSelf> : PXFieldAttachedTo<TTable>.By<TGraph>.AsDateTime where TSelf : PXFieldAttachedTo<TTable>.By<TGraph>.AsDateTime
      {
        public static DateTime? GetValue(TGraph graph, TTable row)
        {
          return PXFieldAttachedTo<TTable>.By<TGraph>.As<DateTime?>.GetValue<TSelf>(graph, row);
        }
      }
    }

    [PXInternalUseOnly]
    public abstract class AsString : PXFieldAttachedTo<TTable>.By<TGraph>.As<string>
    {
      protected override PXFieldState DefaultState(PXCache sender, PXFieldSelectingEventArgs e)
      {
        return PXStringState.CreateInstance(e.ReturnState, this.Length, this.IsUnicode, this.FieldName, new bool?(), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), this.DefaultValue, (string[]) null);
      }

      protected virtual int? Length => new int?();

      protected virtual bool? IsUnicode => new bool?();

      protected virtual string DefaultValue => (string) null;

      [PXInternalUseOnly]
      public abstract class Named<TSelf> : PXFieldAttachedTo<TTable>.By<TGraph>.AsString where TSelf : PXFieldAttachedTo<TTable>.By<TGraph>.AsString
      {
        public static string GetValue(TGraph graph, TTable row)
        {
          return PXFieldAttachedTo<TTable>.By<TGraph>.As<string>.GetValue<TSelf>(graph, row);
        }
      }
    }
  }
}
