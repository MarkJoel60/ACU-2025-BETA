// Decompiled with JetBrains decompiler
// Type: PX.Data.RowNbrAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Property)]
public sealed class RowNbrAttribute : PXEventSubscriberAttribute, IPXFieldDefaultingSubscriber
{
  private string _ViewName;
  private BqlCommand _Select;
  private TypeCode _fieldType;
  private static Dictionary<System.Type, BqlCommand> _selects = new Dictionary<System.Type, BqlCommand>();
  private static object _slock = new object();

  void IPXFieldDefaultingSubscriber.FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!sender.Graph.Views.ContainsKey(this._ViewName))
      sender.Graph.Views[this._ViewName] = new PXView(sender.Graph, false, this._Select);
    long num1 = 0;
    PXView view = sender.Graph.Views[this._ViewName];
    object[] currents = new object[1]{ e.Row };
    object[] objArray = Array.Empty<object>();
    foreach (object data in view.SelectMultiBound(currents, objArray))
    {
      long int64 = Convert.ToInt64(sender.GetValue(data, this._FieldOrdinal));
      if (int64 > num1)
        num1 = int64;
    }
    long num2 = num1 + 1L;
    switch (this._fieldType)
    {
      case TypeCode.SByte:
        e.NewValue = (object) Convert.ToSByte(num2);
        break;
      case TypeCode.Byte:
        e.NewValue = (object) Convert.ToByte(num2);
        break;
      case TypeCode.Int16:
        e.NewValue = (object) Convert.ToInt16(num2);
        break;
      case TypeCode.UInt16:
        e.NewValue = (object) Convert.ToUInt16(num2);
        break;
      case TypeCode.Int32:
        e.NewValue = (object) Convert.ToInt32(num2);
        break;
      case TypeCode.UInt32:
        e.NewValue = (object) Convert.ToUInt32(num2);
        break;
      case TypeCode.Int64:
        e.NewValue = (object) Convert.ToInt64(num2);
        break;
      case TypeCode.UInt64:
        e.NewValue = (object) Convert.ToUInt64(num2);
        break;
      default:
        e.NewValue = (object) num2;
        break;
    }
  }

  public override void CacheAttached(PXCache sender)
  {
    this.DetermineFieldType(sender);
    List<string> list = new List<string>();
    foreach (string key in (IEnumerable<string>) sender.Keys)
    {
      if (key != this._FieldName)
        list.Add(key);
    }
    StringBuilder stringBuilder = new StringBuilder("_");
    stringBuilder.Append(sender.GetItemType().Name);
    foreach (string str in list)
    {
      stringBuilder.Append('_');
      stringBuilder.Append(str);
    }
    stringBuilder.Append('_');
    this._ViewName = stringBuilder.ToString();
    lock (RowNbrAttribute._slock)
    {
      if (RowNbrAttribute._selects.TryGetValue(sender.GetItemType(), out this._Select))
        return;
      System.Type type1 = (System.Type) null;
      foreach (System.Type bqlField in sender.BqlFields)
      {
        if (CompareIgnoreCase.IsInList(list, bqlField.Name))
        {
          if (type1 == (System.Type) null)
            type1 = typeof (Where<,>).MakeGenericType(bqlField, typeof (Equal<>).MakeGenericType(typeof (Current<>).MakeGenericType(bqlField)));
          else
            type1 = typeof (Where<,,>).MakeGenericType(bqlField, typeof (Equal<>).MakeGenericType(typeof (Current<>).MakeGenericType(bqlField)), typeof (And<>).MakeGenericType(type1));
        }
      }
      System.Type type2;
      if (type1 != (System.Type) null)
        type2 = typeof (Select<,>).MakeGenericType(sender.GetItemType(), type1);
      else
        type2 = typeof (Select<>).MakeGenericType(sender.GetItemType());
      this._Select = (BqlCommand) Activator.CreateInstance(type2);
      RowNbrAttribute._selects[sender.GetItemType()] = this._Select;
    }
  }

  private void DetermineFieldType(PXCache sender)
  {
    System.Type type = sender.GetFieldType(this._FieldName);
    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
      type = type.GetGenericArguments()[0];
    this._fieldType = System.Type.GetTypeCode(type);
  }
}
