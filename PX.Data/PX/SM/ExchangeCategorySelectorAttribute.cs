// Decompiled with JetBrains decompiler
// Type: PX.SM.ExchangeCategorySelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Update.WebServices;
using System;
using System.Collections;

#nullable enable
namespace PX.SM;

public class ExchangeCategorySelectorAttribute : PXCustomSelectorAttribute
{
  public ExchangeCategorySelectorAttribute()
    : base(typeof (ExchangeCategorySelectorAttribute.Category.name))
  {
    this.CacheGlobal = true;
    this.ValidateValue = false;
  }

  protected virtual 
  #nullable disable
  IEnumerable GetRecords()
  {
    ExchangeCategorySelectorAttribute selectorAttribute = this;
    if (selectorAttribute._Graph.Caches[selectorAttribute._CacheType].Current is EMailSyncServer current)
    {
      foreach (PX.Data.Update.WebServices.Category category in PXExchangeServer.GetGate(current).GetCategories())
        yield return (object) new ExchangeCategorySelectorAttribute.Category()
        {
          Name = category.Name
        };
    }
  }

  [Serializable]
  public class Category : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(IsKey = true, IsFixed = false, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Category", Visibility = PXUIVisibility.SelectorVisible)]
    public string Name { get; set; }

    public abstract class name : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExchangeCategorySelectorAttribute.Category.name>
    {
    }
  }
}
