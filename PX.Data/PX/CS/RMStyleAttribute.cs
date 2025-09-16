// Decompiled with JetBrains decompiler
// Type: PX.CS.RMStyleAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Text;

#nullable disable
namespace PX.CS;

[PXDBInt]
[PXDBChildIdentity(typeof (RMStyle.styleID))]
[PXUIField(DisplayName = "Style")]
public class RMStyleAttribute : 
  PXAggregateAttribute,
  IPXRowSelectedSubscriber,
  IPXRowDeletedSubscriber,
  IPXRowInsertedSubscriber
{
  protected PXCache _RowCache;
  protected PXCache _Cache;

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    object objB = sender.GetValue(e.Row, this._FieldOrdinal);
    if (objB == null)
      return;
    foreach (RMStyle rmStyle in sender.Graph.Caches[typeof (RMStyle)].Inserted)
    {
      if (object.Equals((object) rmStyle.StyleID, objB))
        return;
    }
    RMStyle rmStyle1 = (RMStyle) sender.Graph.Caches[typeof (RMStyle)].Insert();
    if (rmStyle1 == null)
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) rmStyle1.StyleID);
  }

  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    object obj = sender.GetValue(e.Row, this._FieldOrdinal);
    if (obj == null)
      return;
    RMStyle rmStyle = (RMStyle) PXSelectBase<RMStyle, PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Required<RMStyle.styleID>>>>.Config>.Select(sender.Graph, obj);
    if (rmStyle == null)
      return;
    sender.Graph.Caches[typeof (RMStyle)].Delete((object) rmStyle);
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PXCache cach = sender.Graph.Caches[typeof (RMStyle)];
    object obj = sender.GetValue(e.Row, this._FieldOrdinal);
    if (obj != null)
    {
      RMStyle rmStyle1 = new RMStyle()
      {
        StyleID = (int?) obj
      };
      if (cach.Locate((object) rmStyle1) is RMStyle rmStyle2 && EnumerableExtensions.IsNotIn<PXEntryStatus>(cach.GetStatus((object) rmStyle2), PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted))
        return;
      if ((RMStyle) PXSelectBase<RMStyle, PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Required<RMStyle.styleID>>>>.Config>.Select(sender.Graph, obj) == null)
        obj = (object) null;
    }
    if (obj != null)
      return;
    RMStyle rmStyle;
    using (new ReadOnlyScope(new PXCache[1]{ cach }))
      rmStyle = (RMStyle) cach.Insert();
    if (rmStyle == null)
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) rmStyle.StyleID);
    sender.MarkUpdated(e.Row);
  }

  public virtual void TextFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is RMStyle data))
    {
      object obj = sender.GetValue(e.Row, this._FieldOrdinal);
      if (obj != null)
        data = (RMStyle) PXSelectBase<RMStyle, PXSelect<RMStyle, Where<RMStyle.styleID, Equal<Required<RMStyle.styleID>>>>.Config>.Select(sender.Graph, obj);
    }
    if (data != null)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      short? nullable = data.TextAlign;
      if (nullable.GetValueOrDefault() != (short) 0)
      {
        nullable = data.TextAlign;
        switch (nullable.Value)
        {
          case 1:
            stringBuilder1.Append(PX.SM.Messages.GetLocal("Left"));
            break;
          case 2:
            stringBuilder1.Append(PX.SM.Messages.GetLocal("Center"));
            break;
          case 3:
            stringBuilder1.Append(PX.SM.Messages.GetLocal("Right"));
            break;
        }
      }
      if (!string.IsNullOrEmpty(data.FontName))
      {
        object valueExt = sender.Graph.Caches[typeof (RMStyle)].GetValueExt<RMStyle.fontName>((object) data);
        if (valueExt is PXFieldState)
          valueExt = ((PXFieldState) valueExt).Value;
        if (valueExt is string)
        {
          if (stringBuilder1.Length > 0)
            stringBuilder1.Append(", ");
          stringBuilder1.Append((string) valueExt);
        }
      }
      nullable = data.FontStyle;
      if (nullable.GetValueOrDefault() != (short) 0)
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(", ");
        nullable = data.FontStyle;
        switch (nullable.Value)
        {
          case 1:
            stringBuilder1.Append(PX.SM.Messages.GetLocal("Bold"));
            break;
          case 2:
            stringBuilder1.Append(PX.SM.Messages.GetLocal("Italic"));
            break;
          case 4:
            stringBuilder1.Append(PX.SM.Messages.GetLocal("Underline"));
            break;
          case 8:
            stringBuilder1.Append(PX.SM.Messages.GetLocal("Strikethrough"));
            break;
        }
      }
      if (stringBuilder1.Length == 0)
      {
        if (!string.IsNullOrEmpty(data.Color))
        {
          object valueExt = sender.Graph.Caches[typeof (RMStyle)].GetValueExt<RMStyle.color>((object) data);
          if (valueExt is PXFieldState)
            valueExt = ((PXFieldState) valueExt).Value;
          if (valueExt is string)
            stringBuilder1.Append((string) valueExt);
        }
        if (!string.IsNullOrEmpty(data.BackColor))
        {
          object valueExt = sender.Graph.Caches[typeof (RMStyle)].GetValueExt<RMStyle.backColor>((object) data);
          if (valueExt is PXFieldState)
            valueExt = ((PXFieldState) valueExt).Value;
          if (valueExt is string)
          {
            if (stringBuilder1.Length > 0)
              stringBuilder1.Append(", ");
            stringBuilder1.Append((string) valueExt);
          }
        }
      }
      if (stringBuilder1.Length == 0)
      {
        double? fontSize = data.FontSize;
        if (fontSize.GetValueOrDefault() != 0.0)
        {
          StringBuilder stringBuilder2 = stringBuilder1;
          fontSize = data.FontSize;
          double num = fontSize.Value;
          stringBuilder2.Append(num);
          nullable = data.FontSizeType;
          if (nullable.GetValueOrDefault() != (short) 0)
          {
            nullable = data.FontSizeType;
            switch (nullable.Value)
            {
              case 1:
                stringBuilder1.Append(" " + PX.SM.Messages.GetLocal("Pixel"));
                break;
              case 2:
                stringBuilder1.Append(" " + PX.SM.Messages.GetLocal("Point"));
                break;
              case 3:
                stringBuilder1.Append(" " + PX.SM.Messages.GetLocal("Pica"));
                break;
              case 4:
                stringBuilder1.Append(" " + PX.SM.Messages.GetLocal("Inch"));
                break;
              case 5:
                stringBuilder1.Append(" " + PX.SM.Messages.GetLocal("Mm."));
                break;
              case 6:
                stringBuilder1.Append(" " + PX.SM.Messages.GetLocal("Cm."));
                break;
            }
          }
        }
      }
      e.ReturnValue = stringBuilder1.Length <= 0 ? (object) "" : (object) stringBuilder1.ToString();
    }
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(100), new bool?(), this._FieldName + "Text", new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
    ((PXFieldState) e.ReturnState).Visible = false;
  }

  public override void CacheAttached(PXCache sender)
  {
    if (!sender.Graph.Views.Caches.Contains(sender.GetItemType()))
      sender.Graph.Views.Caches.Add(sender.GetItemType());
    base.CacheAttached(sender);
    string field = this._FieldName + "Text";
    if (!sender.Fields.Contains(field))
    {
      sender.Fields.Add(field);
      sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), field, new PXFieldSelecting(this.TextFieldSelecting));
      sender.Graph.Views["Style"] = new PXView(sender.Graph, false, (BqlCommand) new Select<RMStyle, Where<RMStyle.styleID, Equal<Argument<int?>>>>(), (Delegate) new PXPrepareDelegate<int?>(this.GetStyle));
      if (!sender.Graph.Views.Caches.Contains(typeof (RMStyle)))
        sender.Graph.Views.Caches.Add(typeof (RMStyle));
    }
    this._RowCache = sender;
    this._Cache = sender.Graph.Caches[typeof (RMStyle)];
    if (this._Cache.Fields.Contains(field))
      return;
    this._Cache.Fields.Add(field);
    this._Cache.Graph.FieldSelecting.AddHandler(typeof (RMStyle), field, new PXFieldSelecting(this.TextFieldSelecting));
  }

  protected virtual void GetStyle([PXDBInt] ref int? StyleID)
  {
    if (StyleID.HasValue)
    {
      int? nullable1 = StyleID;
      int num = 0;
      if (!(nullable1.GetValueOrDefault() < num & nullable1.HasValue))
        return;
      bool flag = false;
      foreach (RMStyle rmStyle in this._Cache.Inserted)
      {
        nullable1 = rmStyle.StyleID;
        int? nullable2 = StyleID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      this._Cache.Insert((object) new RMStyle()
      {
        StyleID = StyleID
      });
    }
    else
    {
      if (this._RowCache.Current == null)
        return;
      StyleID = (int?) this._RowCache.GetValue(this._RowCache.Current, this._FieldOrdinal);
    }
  }
}
