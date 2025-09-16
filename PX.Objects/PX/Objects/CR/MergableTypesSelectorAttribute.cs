// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.MergableTypesSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Web.Compilation;

#nullable enable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
[Obsolete]
[Serializable]
public sealed class MergableTypesSelectorAttribute : PXCustomSelectorAttribute
{
  [Obsolete]
  public MergableTypesSelectorAttribute()
    : base(typeof (MergableTypesSelectorAttribute.EntityInfo.key), new System.Type[1]
    {
      typeof (MergableTypesSelectorAttribute.EntityInfo.name)
    })
  {
    ((PXSelectorAttribute) this).DescriptionField = typeof (MergableTypesSelectorAttribute.EntityInfo.name);
  }

  public virtual 
  #nullable disable
  System.Type DescriptionField
  {
    get => ((PXSelectorAttribute) this).DescriptionField;
    set
    {
    }
  }

  public IEnumerable GetRecords()
  {
    yield return (object) this.GenerateInfo(typeof (Contact));
    yield return (object) this.GenerateInfo(typeof (BAccount));
    yield return (object) this.GenerateInfo(typeof (CRCampaign));
  }

  public virtual void DescriptionFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string alias)
  {
    bool flag = false;
    if (e.Row != null)
    {
      string typeName = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) as string;
      System.Type type = string.IsNullOrEmpty(typeName) ? (System.Type) null : PXBuildManager.GetType(typeName, false);
      if (type == (System.Type) null)
      {
        flag = true;
        e.ReturnValue = (object) typeName;
      }
      else
        e.ReturnValue = (object) this.GenerateInfo(type).Name;
    }
    if (e.Row != null && !e.IsAltered)
      return;
    int? nullable;
    string descriptionName = ((PXSelectorAttribute) this).getDescriptionName(sender, ref nullable);
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(true), new int?(), new int?(), nullable, (object) null, alias, (string) null, descriptionName, flag ? "The record has been deleted." : (string) null, flag ? (PXErrorLevel) 2 : (PXErrorLevel) 0, new bool?(false), new bool?(true), new bool?(), (PXUIVisibility) 1, (string) null, (string[]) null, (string[]) null);
  }

  private MergableTypesSelectorAttribute.EntityInfo GenerateInfo(System.Type type)
  {
    string friendlyEntityName = EntityHelper.GetFriendlyEntityName(type);
    return new MergableTypesSelectorAttribute.EntityInfo()
    {
      Key = MainTools.GetLongName(type),
      Name = friendlyEntityName
    };
  }

  [PXHidden]
  [Serializable]
  public class EntityInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(IsKey = true)]
    [PXUIField(Visible = false)]
    public virtual string Key { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Name")]
    public virtual string Name { get; set; }

    public abstract class key : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      MergableTypesSelectorAttribute.EntityInfo.key>
    {
    }

    public abstract class name : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      MergableTypesSelectorAttribute.EntityInfo.name>
    {
    }
  }
}
