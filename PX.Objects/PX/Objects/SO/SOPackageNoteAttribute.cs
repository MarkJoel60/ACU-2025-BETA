// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPackageNoteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class SOPackageNoteAttribute : PXNoteAttribute
{
  public const string CommercialInvoiceFilePostfix = "Customs";

  public virtual void noteFilesFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.noteFilesFieldSelecting(sender, e);
    if (!(e.ReturnValue is string[] returnValue) || returnValue.Length <= 1)
      return;
    List<string> stringList = new List<string>();
    List<\u003C\u003Ef__AnonymousType99<string, PackageFileCategory>> list = ((IEnumerable<string>) returnValue).Select(f => new
    {
      FileInfo = f,
      Category = getFileCategory(f)
    }).ToList();
    stringList.AddRange(list.Where(f => !f.Category.HasFlag((Enum) PackageFileCategory.CommercialInvoice)).Select(f => f.FileInfo));
    stringList.AddRange(list.Where(f => f.Category.HasFlag((Enum) PackageFileCategory.CommercialInvoice)).Select(f => f.FileInfo));
    e.ReturnValue = (object) stringList.ToArray();

    PackageFileCategory getFileCategory(string fileInfo)
    {
      PackageFileCategory fileCategory = PackageFileCategory.None;
      string[] strArray = fileInfo.Split('$');
      if (strArray.Length > 1)
        fileCategory = this.GetFileCategory(strArray[1]);
      return fileCategory;
    }
  }

  public virtual PackageFileCategory GetFileCategory(string fileName)
  {
    string filePostfix = getFilePostfix(fileName);
    return !string.IsNullOrEmpty(filePostfix) && filePostfix.Equals("Customs", StringComparison.InvariantCultureIgnoreCase) ? PackageFileCategory.CommercialInvoice : PackageFileCategory.CarrierLabel;

    static string getFilePostfix(string fileName)
    {
      fileName = fileName.TrimEnd();
      string extension = Path.GetExtension(fileName);
      if (!string.IsNullOrEmpty(extension))
        fileName = fileName.Substring(0, fileName.Length - extension.Length);
      int length = fileName.Length;
      do
        ;
      while (!char.IsLetterOrDigit(fileName[--length]) && length > 0);
      string str = string.Empty;
      while (length >= 0)
      {
        char c = fileName[length--];
        if (char.IsLetter(c))
          str = c.ToString() + str;
        else
          return string.IsNullOrEmpty(str) ? string.Empty : str;
      }
      return string.Empty;
    }
  }
}
