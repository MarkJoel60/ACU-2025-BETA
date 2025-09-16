// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAAPARTranType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.EP.Descriptor;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CAAPARTranType : ILabelProvider
{
  protected static readonly ValueLabelPair.KeyComparer Comparer = new ValueLabelPair.KeyComparer();
  protected static readonly ValueLabelList listGL = new ValueLabelList()
  {
    new GLTranType().ValueLabelPairs
  };
  protected static readonly ValueLabelList listAR = new ValueLabelList()
  {
    new ARDocType().ValueLabelPairs,
    (IEnumerable<ValueLabelPair>) CAAPARTranType.listGL
  };
  protected static readonly ValueLabelList restrictedListAR = new ValueLabelList()
  {
    {
      "INV",
      "Invoice"
    },
    {
      "CSL",
      "Cash Sale"
    },
    {
      "CRM",
      "Credit Memo"
    },
    {
      "DRM",
      "Debit Memo"
    },
    {
      "PMT",
      "Payment"
    },
    {
      "PPM",
      "Prepayment"
    },
    {
      "FCH",
      "Overdue Charge"
    },
    {
      "PPI",
      "Prepmt. Invoice"
    }
  };
  protected static readonly ValueLabelList listAP = new ValueLabelList()
  {
    new APDocType().ValueLabelPairs,
    (IEnumerable<ValueLabelPair>) CAAPARTranType.listGL,
    {
      "CBT",
      "AP Batch"
    }
  };
  protected static readonly ValueLabelList restrictedListAP = new ValueLabelList()
  {
    {
      "INV",
      "Bill"
    },
    {
      "QCK",
      "Cash Purchase"
    },
    {
      "CHK",
      "Payment"
    },
    {
      "ACR",
      "Credit Adj."
    },
    {
      "ADR",
      "Debit Adj."
    },
    {
      "PPM",
      "Prepayment"
    },
    {
      "PPI",
      "Prepmt. Invoice"
    }
  };
  protected static readonly ValueLabelList restrictedListARAPIntersect = new ValueLabelList()
  {
    {
      "INV",
      "AR Invoice or AP Bill"
    },
    {
      "PPM",
      "Prepayment"
    },
    {
      "PPI",
      "Prepmt. Invoice"
    }
  };
  protected static readonly ValueLabelList listARAPIntersect = new ValueLabelList()
  {
    (IEnumerable<ValueLabelPair>) CAAPARTranType.restrictedListARAPIntersect,
    {
      "REF",
      "Refund"
    },
    {
      "VRF",
      "Voided Refund"
    }
  };
  protected static readonly ValueLabelList listCA = new ValueLabelList()
  {
    new CATranType().FullValueLabelPairs,
    (IEnumerable<ValueLabelPair>) CAAPARTranType.listGL
  };
  protected static readonly ValueLabelList restrictedListCA = new ValueLabelList()
  {
    {
      "CAE",
      "Cash Entry"
    }
  };
  protected static readonly ValueLabelList listAll = new ValueLabelList()
  {
    new CATranType().FullValueLabelPairs,
    new GLTranType().ValueLabelPairs,
    {
      "CBT",
      "AP Batch"
    },
    new APDocType().ValueLabelPairs.Except<ValueLabelPair>((IEnumerable<ValueLabelPair>) CAAPARTranType.listARAPIntersect, (IEqualityComparer<ValueLabelPair>) CAAPARTranType.Comparer),
    new ARDocType().ValueLabelPairs.Except<ValueLabelPair>((IEnumerable<ValueLabelPair>) CAAPARTranType.listARAPIntersect, (IEqualityComparer<ValueLabelPair>) CAAPARTranType.Comparer),
    (IEnumerable<ValueLabelPair>) CAAPARTranType.listARAPIntersect,
    new EPDocumentType().ValueLabelList
  };
  protected static readonly ValueLabelList listAllUI = new ValueLabelList()
  {
    new CATranType().FullValueLabelPairsUI,
    new GLTranType().ValueLabelPairs,
    {
      "CBT",
      "AP Batch"
    },
    new APDocType().ValueLabelPairsUI.Except<ValueLabelPair>((IEnumerable<ValueLabelPair>) CAAPARTranType.listARAPIntersect, (IEqualityComparer<ValueLabelPair>) CAAPARTranType.Comparer),
    new ARDocType().ValueLabelPairs.Except<ValueLabelPair>((IEnumerable<ValueLabelPair>) CAAPARTranType.listARAPIntersect, (IEqualityComparer<ValueLabelPair>) CAAPARTranType.Comparer),
    (IEnumerable<ValueLabelPair>) CAAPARTranType.listARAPIntersect,
    new EPDocumentType().ValueLabelList
  };
  protected static readonly ValueLabelList restrictedListAll = new ValueLabelList()
  {
    (IEnumerable<ValueLabelPair>) CAAPARTranType.listGL,
    (IEnumerable<ValueLabelPair>) CAAPARTranType.restrictedListCA,
    CAAPARTranType.restrictedListAP.Except<ValueLabelPair>((IEnumerable<ValueLabelPair>) CAAPARTranType.restrictedListARAPIntersect, (IEqualityComparer<ValueLabelPair>) CAAPARTranType.Comparer),
    CAAPARTranType.restrictedListAR.Except<ValueLabelPair>((IEnumerable<ValueLabelPair>) CAAPARTranType.restrictedListARAPIntersect, (IEqualityComparer<ValueLabelPair>) CAAPARTranType.Comparer),
    (IEnumerable<ValueLabelPair>) CAAPARTranType.restrictedListARAPIntersect
  };

  public IEnumerable<ValueLabelPair> ValueLabelPairs
  {
    get => (IEnumerable<ValueLabelPair>) CAAPARTranType.listAll;
  }

  public class ListByModuleRestrictedAttribute(Type moduleField) : 
    CAAPARTranType.ListByModuleAttribute(moduleField, CAAPARTranType.restrictedListAR, CAAPARTranType.restrictedListAP, CAAPARTranType.restrictedListCA, CAAPARTranType.listGL, CAAPARTranType.restrictedListAll)
  {
  }

  public class ListByModuleAttribute : LabelListAttribute
  {
    protected ValueLabelList _listAR;
    protected ValueLabelList _listAP;
    protected ValueLabelList _listCA;
    protected ValueLabelList _listGL;
    protected ValueLabelList _listAll;
    protected Type _ModuleField;
    protected string lastModule;

    protected ListByModuleAttribute(
      Type moduleField,
      ValueLabelList listAR,
      ValueLabelList listAP,
      ValueLabelList listCA,
      ValueLabelList listGL,
      ValueLabelList listAll)
      : base((IEnumerable<ValueLabelPair>) listAll)
    {
      this._ModuleField = moduleField;
      this._listAR = listAR;
      this._listAP = listAP;
      this._listCA = listCA;
      this._listGL = listGL;
      this._listAll = listAll;
    }

    public ListByModuleAttribute(Type moduleField)
      : this(moduleField, CAAPARTranType.listAR, CAAPARTranType.listAP, CAAPARTranType.listCA, CAAPARTranType.listGL, CAAPARTranType.listAll)
    {
    }

    public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      string str = (string) sender.GetValue(e.Row, this._ModuleField.Name);
      if (str != this.lastModule)
      {
        if (sender.Graph.GetType() == typeof (PXGraph))
        {
          this.SetNewList(sender, this._listAll);
        }
        else
        {
          switch (str)
          {
            case "AP":
              this.SetNewList(sender, this._listAP);
              break;
            case "AR":
              this.SetNewList(sender, this._listAR);
              break;
            case "CA":
              this.SetNewList(sender, this._listCA);
              break;
            case "GL":
              this.SetNewList(sender, this._listGL);
              break;
            default:
              this.SetNewList(sender, this._listAll);
              break;
          }
        }
        this.lastModule = str;
      }
      base.FieldSelecting(sender, e);
    }

    protected void SetNewList(PXCache sender, ValueLabelList valueLabelList)
    {
      PXStringListAttribute.SetListInternal((IEnumerable<PXStringListAttribute>) new List<CAAPARTranType.ListByModuleAttribute>()
      {
        this
      }, valueLabelList.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Value)).ToArray<string>(), valueLabelList.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Label)).ToArray<string>(), sender);
    }

    protected virtual void TryLocalize(PXCache sender)
    {
      base.TryLocalize(sender);
      this.RipDynamicLabels(this._listAP.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Label)).ToArray<string>(), sender);
      this.RipDynamicLabels(this._listAR.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Label)).ToArray<string>(), sender);
      this.RipDynamicLabels(this._listCA.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Label)).ToArray<string>(), sender);
      this.RipDynamicLabels(this._listGL.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Label)).ToArray<string>(), sender);
    }
  }

  public class ListByModuleUIAttribute : LabelListAttribute
  {
    protected ValueLabelList _listAll;
    protected Type _ModuleField;
    protected string lastModule;

    protected ListByModuleUIAttribute(Type moduleField, ValueLabelList listAll)
      : base((IEnumerable<ValueLabelPair>) listAll)
    {
      this._ModuleField = moduleField;
      this._listAll = listAll;
    }

    public ListByModuleUIAttribute(Type moduleField)
      : this(moduleField, CAAPARTranType.listAllUI)
    {
    }

    public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      string str = (string) sender.GetValue(e.Row, this._ModuleField.Name);
      if (str != this.lastModule)
      {
        this.SetNewList(sender, this._listAll);
        this.lastModule = str;
      }
      base.FieldSelecting(sender, e);
    }

    protected void SetNewList(PXCache sender, ValueLabelList valueLabelList)
    {
      PXStringListAttribute.SetListInternal((IEnumerable<PXStringListAttribute>) new List<CAAPARTranType.ListByModuleUIAttribute>()
      {
        this
      }, valueLabelList.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Value)).ToArray<string>(), valueLabelList.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Label)).ToArray<string>(), sender);
    }

    protected virtual void TryLocalize(PXCache sender)
    {
      base.TryLocalize(sender);
      this.RipDynamicLabels(this._listAll.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Label)).ToArray<string>(), sender);
    }
  }
}
