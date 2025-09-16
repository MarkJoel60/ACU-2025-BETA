// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.Relational.CRParentChild`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Reflection;

#nullable disable
namespace PX.Objects.CR.Extensions.Relational;

/// <summary>
/// Extension that is used for mapping creation only. Not so usefull by itself. The whole logic is implemented inside the derived extensions
/// </summary>
public abstract class CRParentChild<TGraph, TThis> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TThis : CRParentChild<TGraph, TThis>
{
  public PXSelectExtension<CRParentChild<TGraph, TThis>.Document> PrimaryDocument;
  public PXSelectExtension<CRParentChild<TGraph, TThis>.Child> ChildDocument;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase) this.ChildDocument).View = new PXView((PXGraph) this.Base, false, BqlCommand.CreateInstance(new System.Type[2]
    {
      typeof (Select<>),
      ((PXSelectBase) this.ChildDocument).View.GetItemType()
    }));
    MethodInfo method = ((PXSelectBase) this.ChildDocument).Cache.GetType().GetMethod("GetBaseBqlField", BindingFlags.Instance | BindingFlags.NonPublic);
    object obj;
    if ((object) method == null)
    {
      obj = (object) null;
    }
    else
    {
      // ISSUE: explicit non-virtual call
      obj = __nonvirtual (method.Invoke((object) ((PXSelectBase) this.ChildDocument).Cache, new object[1]
      {
        (object) "childID"
      }));
    }
    System.Type type = obj as System.Type;
    if (type == (System.Type) null)
      return;
    ((PXSelectBase) this.ChildDocument).View.WhereNew(BqlCommand.Compose(new System.Type[5]
    {
      typeof (Where<,>),
      type,
      typeof (Equal<>),
      typeof (Required<>),
      type
    }));
  }

  public virtual CRParentChild<TGraph, TThis>.Child GetChildByID(int? childID)
  {
    if (!childID.HasValue)
      return (CRParentChild<TGraph, TThis>.Child) null;
    if (((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).Current != null)
    {
      int? nullable1 = ((PXSelectBase) this.ChildDocument).Cache.GetValue((object) ((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).Current, "ChildID") as int?;
      int? nullable2 = childID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        return ((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).Current;
    }
    return ((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).SelectSingle(new object[1]
    {
      (object) childID
    });
  }

  protected virtual CRParentChild<TGraph, TThis>.DocumentMapping GetDocumentMapping()
  {
    return new CRParentChild<TGraph, TThis>.DocumentMapping(typeof (CRParentChild<TGraph, TThis>.Document));
  }

  protected abstract CRParentChild<TGraph, TThis>.ChildMapping GetChildMapping();

  [PXHidden]
  public class Document : PX.Objects.CR.Extensions.Relational.Document<TThis>
  {
  }

  [PXHidden]
  public class Child : PX.Objects.CR.Extensions.Relational.Child<TThis>
  {
  }

  protected class DocumentMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type RelatedID = typeof (PX.Objects.CR.Extensions.Relational.Document<TThis>.relatedID);
    public System.Type ChildID = typeof (PX.Objects.CR.Extensions.Relational.Document<TThis>.childID);
    public System.Type IsOverrideRelated = typeof (PX.Objects.CR.Extensions.Relational.Document<TThis>.isOverrideRelated);

    public System.Type Extension => typeof (CRParentChild<TGraph, TThis>.Document);

    public System.Type Table => this._table;

    public DocumentMapping(System.Type table) => this._table = table;
  }

  protected class ChildMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type ChildID = typeof (PX.Objects.CR.Extensions.Relational.Child<TThis>.childID);
    public System.Type RelatedID = typeof (PX.Objects.CR.Extensions.Relational.Child<TThis>.relatedID);

    public System.Type Extension => typeof (CRParentChild<TGraph, TThis>.Child);

    public System.Type Table => this._table;

    public ChildMapping(System.Type table) => this._table = table;
  }
}
