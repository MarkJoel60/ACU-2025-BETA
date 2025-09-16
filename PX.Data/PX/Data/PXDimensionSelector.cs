// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDimensionSelector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public static class PXDimensionSelector
{
  public class WithCachingByCompositeKeyAttribute : PXDimensionSelectorAttribute
  {
    public bool OnlyKeyConditions
    {
      get
      {
        return ((PXSelectorAttribute.WithCachingByCompositeKeyAttribute) this.SelectorAttribute).OnlyKeyConditions;
      }
      set
      {
        ((PXSelectorAttribute.WithCachingByCompositeKeyAttribute) this.SelectorAttribute).OnlyKeyConditions = value;
      }
    }

    protected WithCachingByCompositeKeyAttribute(string dimension)
      : base(dimension)
    {
    }

    public WithCachingByCompositeKeyAttribute(
      string dimension,
      System.Type search,
      System.Type additionalKeysRelation)
      : base(dimension)
    {
      this.RegisterSelector((PXSelectorAttribute) new PXSelectorAttribute.WithCachingByCompositeKeyAttribute(search, additionalKeysRelation));
    }

    public WithCachingByCompositeKeyAttribute(
      string dimension,
      System.Type search,
      System.Type additionalKeysRelation,
      System.Type substituteKey)
      : base(dimension)
    {
      PXSelectorAttribute.WithCachingByCompositeKeyAttribute selector = new PXSelectorAttribute.WithCachingByCompositeKeyAttribute(search, additionalKeysRelation);
      selector.SubstituteKey = substituteKey;
      this.RegisterSelector((PXSelectorAttribute) selector);
    }

    public WithCachingByCompositeKeyAttribute(
      string dimension,
      System.Type search,
      System.Type additionalKeysRelation,
      System.Type substituteKey,
      System.Type[] fieldList)
      : base(dimension)
    {
      PXSelectorAttribute.WithCachingByCompositeKeyAttribute selector = new PXSelectorAttribute.WithCachingByCompositeKeyAttribute(search, additionalKeysRelation, fieldList);
      selector.SubstituteKey = substituteKey;
      this.RegisterSelector((PXSelectorAttribute) selector);
    }

    public WithCachingByCompositeKeyAttribute(
      string dimension,
      System.Type search,
      System.Type additionalKeysRelation,
      System.Type lookupJoin,
      System.Type substituteKey,
      System.Type[] fieldList)
      : base(dimension)
    {
      PXSelectorAttribute.WithCachingByCompositeKeyAttribute selector = new PXSelectorAttribute.WithCachingByCompositeKeyAttribute(search, additionalKeysRelation, lookupJoin, fieldList);
      selector.SubstituteKey = substituteKey;
      this.RegisterSelector((PXSelectorAttribute) selector);
    }
  }
}
