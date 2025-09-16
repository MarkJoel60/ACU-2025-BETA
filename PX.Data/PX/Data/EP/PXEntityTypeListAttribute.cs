// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.PXEntityTypeListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.EP;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class PXEntityTypeListAttribute : PXBaseListAttribute, IPXFieldSelectingSubscriber
{
  public PXEntityTypeListAttribute()
    : base((IPXDBListAttributeHelper) new PXEntityTypeListAttribute.EntityTypeSelectorHelper())
  {
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnState is PXStringState returnState))
      return;
    returnState.Enabled = true;
    if (!sender.Graph.IsContractBasedAPI || !(returnState.Value is string str))
      return;
    e.ReturnState = (object) null;
    e.ReturnValue = (object) str;
  }

  private class EntityTypeSelectorHelper : IPXDBListAttributeHelper, ILocalizableValues
  {
    private static TAttr GetAttributeSafe<TAttr>(MemberInfo mi) where TAttr : Attribute
    {
      TAttr attributeSafe = (object) mi != null ? mi.GetCustomAttribute<TAttr>(false) : default (TAttr);
      if ((object) attributeSafe != null)
        return attributeSafe;
      return (object) mi == null ? default (TAttr) : mi.GetCustomAttribute<TAttr>(true);
    }

    static EntityTypeSelectorHelper()
    {
      PXCodeDirectoryCompiler.NotifyOnChange((System.Action) (() => PXDatabase.ResetSlotForAllCompanies(typeof (PXEntityTypeListAttribute.EntityTypeSelectorHelper.HelperSlot).FullName)));
    }

    public string Key => MainTools.GetLongName(typeof (RelatedEntity));

    public string[] Values
    {
      get => PXEntityTypeListAttribute.EntityTypeSelectorHelper.HelperSlot.CachedSlot.Values;
    }

    public System.Type DefaultValueField
    {
      get => (System.Type) null;
      set
      {
      }
    }

    public string EmptyLabel { get; set; }

    public object DefaultValue => (object) null;

    public void FieldSelecting(
      PXCache sender,
      PXFieldSelectingEventArgs e,
      PXAttributeLevel attributeLevel,
      string fieldName)
    {
      if (attributeLevel != PXAttributeLevel.Item && !e.IsAltered)
        return;
      string[] values = PXEntityTypeListAttribute.EntityTypeSelectorHelper.HelperSlot.CachedSlot.Values;
      string[] labels = PXEntityTypeListAttribute.EntityTypeSelectorHelper.HelperSlot.CachedSlot.Labels;
      string[] strArray = values;
      string[] allowedLabels1 = labels;
      if (e.Row != null && sender.GetValue(e.Row, fieldName) is string typeName && !string.IsNullOrEmpty(typeName) && !((IEnumerable<string>) strArray).Contains<string>(typeName))
      {
        strArray = new string[values.Length + 1];
        allowedLabels1 = new string[labels.Length + 1];
        values.CopyTo((Array) strArray, 0);
        labels.CopyTo((Array) allowedLabels1, 0);
        string name = PXEntityTypeListAttribute.EntityTypeSelectorHelper.GetAttributeSafe<PXCacheNameAttribute>((MemberInfo) PXBuildManager.GetType(typeName, false))?.GetName();
        strArray[strArray.Length - 1] = typeName;
        allowedLabels1[allowedLabels1.Length - 1] = !string.IsNullOrEmpty(name) ? name : typeName;
      }
      if (!CultureInfo.InvariantCulture.Equals((object) Thread.CurrentThread.CurrentCulture))
      {
        string[] allowedLabels2 = new string[allowedLabels1.Length];
        allowedLabels1.CopyTo((Array) allowedLabels2, 0);
        for (int index = 0; index < allowedLabels1.Length; ++index)
        {
          string str = PXLocalizer.Localize(allowedLabels1[index]);
          if (!string.IsNullOrEmpty(str) && str != allowedLabels1[index])
            allowedLabels2[index] = str;
        }
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), fieldName, new bool?(), new int?(-1), (string) null, strArray, allowedLabels2, new bool?(true), (string) null);
      }
      else
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), fieldName, new bool?(), new int?(-1), (string) null, strArray, allowedLabels1, new bool?(true), (string) null);
    }

    public Dictionary<object, string> ValueLabelDic(PXGraph graph)
    {
      return PXEntityTypeListAttribute.EntityTypeSelectorHelper.HelperSlot.CachedSlot.ValueLabelDic;
    }

    private class Box
    {
      private readonly string _type;
      private readonly string _name;

      public Box(string type, string name)
      {
        if (type == null)
          throw new ArgumentNullException(nameof (type));
        if (name == null)
          throw new ArgumentNullException(nameof (name));
        this._type = type;
        this._name = name;
      }

      public string Type => this._type;

      public string Name => this._name;
    }

    private class BoxComparer : IComparer<PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box>
    {
      public int Compare(
        PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box x,
        PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box y)
      {
        return StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name);
      }
    }

    private class HelperSlot : IPrefetchable, IPXCompanyDependent
    {
      public string[] Values { get; private set; }

      public string[] Labels { get; private set; }

      public Dictionary<object, string> ValueLabelDic { get; private set; }

      public void Prefetch()
      {
        List<PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box> source = new List<PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box>();
        foreach (System.Type pxNoteType in PXNoteAttribute.PXNoteTypes)
        {
          if (((IEnumerable<PropertyInfo>) pxNoteType.GetProperties()).Select<PropertyInfo, PXNoteAttribute>((Func<PropertyInfo, PXNoteAttribute>) (prop => PXEntityTypeListAttribute.EntityTypeSelectorHelper.GetAttributeSafe<PXNoteAttribute>((MemberInfo) prop))).Any<PXNoteAttribute>((Func<PXNoteAttribute, bool>) (note => note != null && note.ShowInReferenceSelector)))
          {
            System.Type entityType = pxNoteType;
            if (typeof (PXCacheExtension).IsAssignableFrom(entityType) && entityType.BaseType.IsGenericType)
            {
              entityType = entityType.BaseType.GetGenericArguments()[entityType.BaseType.GetGenericArguments().Length - 1];
              PXCacheNameAttribute customAttribute = pxNoteType.GetCustomAttribute<PXCacheNameAttribute>(false);
              if (customAttribute != null)
              {
                source.RemoveAll((Predicate<PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box>) (b => b.Type == entityType.FullName));
                source.Add(new PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box(entityType.FullName, customAttribute.GetName()));
                continue;
              }
            }
            if (!source.Any<PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box>((Func<PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box, bool>) (b => b.Type == entityType.FullName)))
            {
              PXCacheNameAttribute attributeSafe = PXEntityTypeListAttribute.EntityTypeSelectorHelper.GetAttributeSafe<PXCacheNameAttribute>((MemberInfo) entityType);
              source.Add(new PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box(entityType.FullName, attributeSafe == null ? entityType.Name : attributeSafe.GetName()));
            }
          }
        }
        source.Sort((IComparer<PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box>) new PXEntityTypeListAttribute.EntityTypeSelectorHelper.BoxComparer());
        this.Labels = source.Select<PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box, string>((Func<PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box, string>) (b => b.Name)).ToArray<string>();
        this.Values = source.Select<PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box, string>((Func<PXEntityTypeListAttribute.EntityTypeSelectorHelper.Box, string>) (b => b.Type)).ToArray<string>();
        this.ValueLabelDic = new Dictionary<object, string>();
        for (int index = 0; index < this.Values.Length; ++index)
          this.ValueLabelDic.Add((object) this.Values[index], this.Labels[index]);
      }

      public static PXEntityTypeListAttribute.EntityTypeSelectorHelper.HelperSlot CachedSlot
      {
        get
        {
          return PXDatabase.GetSlot<PXEntityTypeListAttribute.EntityTypeSelectorHelper.HelperSlot>(typeof (PXEntityTypeListAttribute.EntityTypeSelectorHelper.HelperSlot).FullName);
        }
      }
    }
  }
}
